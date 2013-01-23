
using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Windows.Forms;
using NLog;
using System.IO;
using System.Collections;
using LanExchange.Strategy;
using LanExchange.Strategy.Panel;
using System.Reflection;
using LanExchange.Model.Panel;
using LanExchange.Utils;

namespace LanExchange.Model
{
    // TODO need Designer code for this class
    public class PanelSubscription : ISubscription, IDisposable
    {
        #region Static fields and methods

        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        private static ISubscription m_Instance;

        public static ISubscription Instance
        {
            get
            {
                if (m_Instance == null) 
                    m_Instance = new PanelSubscription();
                return m_Instance;
            }
        }
        #endregion

        private readonly IDictionary<ISubject, IList<ISubscriber>> m_Subjects;
        private readonly Dictionary<ISubject, IList<AbstractPanelItem>> m_Results;
        private readonly IList<AbstractPanelStrategy> m_Strategies;

        private int m_RefreshInterval;
        private readonly Timer m_RefreshTimer;
        private bool m_InstantUpdate = true;

        protected PanelSubscription()
        {
            // lists
            m_Subjects = new Dictionary<ISubject, IList<ISubscriber>>();
            m_Results = new Dictionary<ISubject, IList<AbstractPanelItem>>(new ConcreteSubjectComparer());
            m_Strategies = new List<AbstractPanelStrategy>();
            InitStrategies();
            // load cached results
            LoadResultsFromCache();
            // timer
            m_RefreshTimer = new Timer();
            m_RefreshTimer.Tick += RefreshTimer_Tick;
            m_RefreshTimer.Enabled = false;
        }

        public void Dispose()
        {
            m_RefreshTimer.Dispose();
        }

        private void InitStrategies()
        {
            foreach (var T in Assembly.GetExecutingAssembly().GetTypes())
                if (T.IsClass && !T.IsAbstract && T.BaseType == typeof (AbstractPanelStrategy))
                {
                    var strategy = (AbstractPanelStrategy)Activator.CreateInstance(T);
                    m_Strategies.Add(strategy);
                }
        }

        //public bool IsInstantUpdate
        //{
        //    get { return m_InstantUpdate; }
        //}

        //public bool IsBusy
        //{
        //    get { return BackgroundWorkers.Instance.IsBusy; }
        //}

        public int RefreshInterval
        {
            get { return m_RefreshInterval; }
            set
            {
                m_RefreshInterval = value;
                m_RefreshTimer.Interval = value;
            }
        }
        
        public bool HasStrategyForSubject(ISubject subject)
        {
            foreach (var strategy in m_Strategies)
            {
                bool accepted;
                strategy.AcceptSubject(subject, out accepted);
                if (accepted)
                    return true;
            }
            return false;
        }

        private AbstractPanelStrategy CreateConcretePanelStrategy(ISubject subject)
        {
            foreach (var strategy in m_Strategies)
            {
                bool accepted;
                strategy.AcceptSubject(subject, out accepted);
                if (accepted)
                {
                    // create another one instance of the strategy
                    var newStrategy = (AbstractPanelStrategy)Activator.CreateInstance(strategy.GetType());
                    newStrategy.Subject = subject;
                    return newStrategy;
                }
            }
            return null;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            logger.Info("RefreshTimer.Tick() executed. Next tick in {0} sec.", m_RefreshInterval/1000);
            // prepare workers to launch
            foreach (var Pair in m_Subjects)
            {
                // skip subjects without subscribers
                if (Pair.Value.Count == 0) continue;
                
                bool subjectFound = false;
                foreach(BackgroundContext ctx in BackgroundWorkers.Instance.EnumContexts())
                    if (ctx.Strategy is AbstractPanelStrategy)
                    {
                        var strategy = ctx.Strategy as AbstractPanelStrategy;
                        if (strategy.Subject != null && strategy.Subject.Equals(Pair.Key))
                        {
                            subjectFound = true;
                            break;
                        }
                    }
                if (!subjectFound)
                {
                    // select strategy for enum panel items by specified subject Pair.Key
                    var strategy = CreateConcretePanelStrategy(Pair.Key);
                    if (strategy == null) continue;
                    // create background worker for enum via strategy
                    var context = new BackgroundContext(strategy);
                    var worker = new BackgroundWorkerEx();
                    worker.DoWork += OneWorker_DoWork;
                    worker.RunWorkerCompleted += OneWorker_RunWorkerCompleted;
                    BackgroundWorkers.Instance.Add(context, worker);
                    // Run background worker!
                    worker.RunWorkerAsync(context);
                    //GC.KeepAlive(worker);
                }
            }
        }

        private bool SetResult(ISubject subject, IList<AbstractPanelItem> list)
        {
            bool bModified = false;
            lock (m_Results)
            {
                if (!m_Results.ContainsKey(subject))
                {
                    bModified = true;
                    m_Results.Add(subject, list);
                }
                if (!bModified)
                {
                    // store computer and domains only
                    if (!subject.IsCacheable)
                    {
                        bModified = true;
                        m_Results[subject] = list;
                    }
                    else
                    {
                        bModified = SortedListIsModified(m_Results[subject], list);
                        if (bModified)
                        {
                            m_Results[subject] = list;
                            SaveResultsToCache();
                        }
                    }
                }
            }
            return bModified;
        }

        /// <summary>
        /// Browsing network process.
        /// This method virtual for unit-tests.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OneWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var context = e.Argument as BackgroundContext;
            if (context == null)
            {
                e.Cancel = true;
                return;
            }
            try
            {
                context.ExecuteOperation();
            }
            catch (Exception)
            {
                e.Cancel = true;
            }
            e.Result = null;
            if (e.Cancel) return;
            // process panel strategies
            if (context.Strategy is AbstractPanelStrategy)
            {
                var strategy = (context.Strategy as AbstractPanelStrategy);
                if (SetResult(strategy.Subject, strategy.Result))
                    e.Result = strategy.Subject;
                else
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Browsing network complete. If serveinfo list is not modified e.Cancel set to False.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) return;
            var Subject = (ISubject)e.Result;
            // notify all subscribers for current subject
            lock (m_Subjects)
            {
                if (m_Subjects.ContainsKey(Subject))
                {
                    var List = m_Subjects[Subject];
                    if (List.Count > 0)
                    {
                        logger.Info("Notify {0} subscriber(s) with subject {1}", List.Count, Subject);
                        foreach (var Subscriber in List)
                            Subscriber.DataChanged(this, Subject);
                    }
                }
            }
        }

        private static bool SortedListIsModified(IList<AbstractPanelItem> listA, IList<AbstractPanelItem> listB)
        {
            if (listA == null || listB == null)
                return false;
            bool bModified = false;
            if (listA.Count != listB.Count)
                bModified = true;
            else
                for (int i = 0; i < listA.Count - 1; i++)
                    if (listA[i].CompareTo(listB[i]) != 0)
                    {
                        bModified = true;
                        break;
                    }
            return bModified;
        }

        private static string GetCacheFileName()
        {
            return Path.ChangeExtension(Settings.Settings.GetExecutableFileName(), ".cache.tmp");
        }


        private void SaveResultsToCache()
        {
            var fileName = GetCacheFileName();
            logger.Info("SaveResultsToCache(\"{0}\")", fileName);
            lock (m_Results)
            {
                var Temp = new Dictionary<string, ServerInfo[]>();
                foreach (var Pair in m_Results)
                {
                    if ((Pair.Key != ConcreteSubject.Root) && !(Pair.Key is DomainPanelItem))
                        continue;
                    var TempList = new ServerInfo[Pair.Value.Count];
                    for (int i = 0; i < Pair.Value.Count; i++)
                    {
                        var comp = Pair.Value[i] as ComputerPanelItem;
                        if (comp != null)
                            TempList[i] = comp.SI;
                    }
                    Temp.Add(Pair.Key.Subject, TempList);
                }
                try
                {
                    SerializeUtils.SerializeObjectToBinaryFile(fileName, Temp);
                }
                catch (Exception E)
                {
                    logger.Error("SaveResultsToCache: {0}", E.Message);
                }
            }
        }

        private void LoadResultsFromCache()
        {
            var fileName = GetCacheFileName();
            if (!File.Exists(fileName)) return;
            logger.Info("LoadResultsFromCache(\"{0}\")", fileName);
            lock (m_Results)
            {
                Dictionary<string, ServerInfo[]> Temp = null;
                try
                {
                    Temp = (Dictionary<string, ServerInfo[]>)SerializeUtils.DeserializeObjectFromBinaryFile(fileName);
                }
                catch (Exception E)
                {
                    logger.Error("LoadResultsFromCache: {0}", E.Message);
                }
                if (Temp != null)
                {
                    m_Results.Clear();
                    foreach (var Pair in Temp)
                    {
                        var domain = new DomainPanelItem(Pair.Key);
                        var TempList = new List<AbstractPanelItem>();
                        foreach (var si in Pair.Value)
                            if (si != null)
                                TempList.Add(new ComputerPanelItem(domain, si));
                        m_Results.Add(domain, TempList);
                        //TempList.Sort();
                    }
                }
            }
        }

        public bool HasSubscribers()
        {
            foreach (var Pair in m_Subjects)
                if (Pair.Value.Count > 0)
                    return true;
            return false;
        }

        private void SubscribersChanged()
        {
            bool Found = HasSubscribers();
            if (!Found)
            {
                logger.Info("RefreshTimer stopped.");
                m_RefreshTimer.Enabled = false;
                m_InstantUpdate = true;
            }
            else
            {
                if (m_InstantUpdate)
                {
                    m_RefreshTimer.Enabled = false;
                    RefreshTimer_Tick(m_RefreshTimer, EventArgs.Empty);
                    m_RefreshTimer.Enabled = true;
                    m_InstantUpdate = false;
                    logger.Info("RefreshTimer started. Next tick in {0} sec.", m_RefreshInterval/1000);
                }
            }
        }

        #region ISubscription interface
        public void SubscribeToSubject(ISubscriber sender, ISubject subject)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");
            if (subject == null)
                throw new ArgumentNullException("subject");
            bool Modified = false;
            if (subject.IsCacheable && m_Subjects.ContainsKey(subject))
            {
                var List = m_Subjects[subject];
                if (!List.Contains(sender))
                {
                    List.Add(sender);
                    Modified = true;
                }
            }
            else
            {
                IList<ISubscriber> List;
                if (m_Subjects.ContainsKey(subject))
                    List = m_Subjects[subject];
                else
                {
                    List = new List<ISubscriber>();
                    m_Subjects.Add(subject, List);
                }
                List.Add(sender);
                m_InstantUpdate = true;
                Modified = true;
            }
            if (Modified)
            {
                if (m_Results.ContainsKey(subject))
                {
                    logger.Info("Subject \"{0}\" already exists in cache.", subject);
                    sender.DataChanged(this, subject);
                }
                SubscribersChanged();
            }
        }

        public void UnSubscribe(ISubscriber sender, bool updateTimer)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");
            bool Modified = false;
            foreach (var Pair in m_Subjects)
            {
                if (Pair.Value.Contains(sender))
                {
                    Pair.Value.Remove(sender);
                    Modified = true;
                }
            }
            if (Modified)
            {
                sender.DataChanged(this, ConcreteSubject.Empty);
                if (updateTimer)
                    SubscribersChanged();
            }
        }
        #endregion

        public IEnumerable<KeyValuePair<ISubject, IList<ISubscriber>>> GetSubjects()
        {
            return m_Subjects;
        }


        public IEnumerable GetListBySubject(ISubject subject)
        {
            if (subject == null)
                yield break;
            lock (m_Results)
            {
                if (!m_Results.ContainsKey(subject))
                    yield break;
                foreach (var si in m_Results[subject])
                    yield return si;
            }
        }
    }
}
