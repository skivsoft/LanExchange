#define __REMOVE_RANDOM_COMPS

using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Windows.Forms;
using NLog;
using LanExchange.Utils;
using System.IO;

namespace LanExchange.Model
{
    public class ServerListSubscription : ISubscription
    {
        #region Static fields and methods

        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        private static ServerListSubscription m_Instance;

        public static ServerListSubscription Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ServerListSubscription();
                }
                return m_Instance;
            }
        }
        #endregion

        private readonly Dictionary<string, IList<ISubscriber>> m_Subjects;
        private readonly Dictionary<string, List<ServerInfo>> m_Results;

        private int m_RefreshInterval;
        private readonly Timer m_RefreshTimer;
        private readonly BackgroundWorkerList m_Workers;
        private bool m_InstantUpdate = true;

        protected ServerListSubscription()
        {
            // lists
            m_Subjects = new Dictionary<string, IList<ISubscriber>>();
            m_Results = new Dictionary<string, List<ServerInfo>>();
            // load cached results
            LoadResultFromCache();
            // timer
            m_RefreshTimer = new Timer();
            m_RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            m_RefreshTimer.Enabled = false;
            // worker list for scanning network
            m_Workers = new BackgroundWorkerList();
        }

        public bool IsInstantUpdate
        {
            get { return m_InstantUpdate; }
        }

        public bool IsBusy
        {
            get { return m_Workers.IsBusy; }
        }

        public int RefreshInterval
        {
            get { return m_RefreshInterval; }
            set
            {
                m_RefreshInterval = value;
                m_RefreshTimer.Interval = value;
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            //m_RefreshTimer.Enabled = false;
            //if (!m_DomainsWorker.IsBusy)
            //    m_DomainsWorker.RunWorkerAsync();
            //if (!m_Workers.IsBusy)
            //{
            //    m_Workers.ClearNotBusy();
            //    // prepare workers to launch
            //    foreach (var Pair in m_Subjects)
            //    {
            //        if (!m_Workers.Exists(Pair.Key))
            //            m_Workers.Add(Pair.Key, CreateOneWorker());
            //    }
            //    if (m_AllSubjects.Count > 0)
            //    {
            //        foreach (var domain in m_Domains)
            //            if (!m_Subjects.ContainsKey(domain.Name))
            //            {
            //                if (!m_Workers.Exists(domain))
            //                    m_Workers.Add(domain, CreateOneWorker());
            //            }
            //    }
            //    // launch!
            //    m_Workers.RunWorkerAsync();
            //}
            //else
            //{
            //    logger.Info("Tick: {0} of {1} worker(s) busy, no action", m_Workers.BusyCount, m_Workers.Count);
            //}
            //m_RefreshTimer.Enabled = true;
        }

        private BackgroundWorker CreateOneWorker()
        {
            var Result = new BackgroundWorker();
            Result.DoWork += new DoWorkEventHandler(OneWorker_DoWork);
            Result.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OneWorker_RunWorkerCompleted);
            return Result;
        }

        /// <summary>
        /// This method must return server list instantly. Use cache is preferred.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        private List<ServerInfo> GetResultNow(string domain, bool cachePref)
        {
            NetApi32.SERVER_INFO_101[] List;
            List<ServerInfo> Result;
            if (cachePref && m_Results.ContainsKey(domain))
            {
                logger.Info("GetFromCache(\"{0}\")", domain);
                Result = m_Results[domain];
            }
            else
            {
                // get server list via OS api
                if (String.IsNullOrEmpty(domain))
                {
                    logger.Info("GetDomainList()");
                    List = NetApi32Utils.GetDomainList();
                }
                else
                {
                    logger.Info("GetComputerList(\"{0}\")", domain);
                    List = NetApi32Utils.GetComputerList(domain);
                }
                // convert array to IList<ServerInfo>
                Result = new List<ServerInfo>();
                Array.ForEach(List, item =>
                {
                    Result.Add(new ServerInfo(item));
                });
                SetResult(domain, Result);
            }
            return Result;
        }

        private bool SetResult(string Domain, List<ServerInfo> List)
        {
            bool bModified = false;
            lock (m_Results)
            {
                if (!m_Results.ContainsKey(Domain))
                {
                    bModified = true;
                    m_Results.Add(Domain, List);
                }
                if (!bModified)
                {
                    var ResultList = m_Results[Domain];
                    bModified = SortedListIsModified(m_Results[Domain], List);
                    if (bModified)
                        m_Results[Domain] = List;
                }
            }
            if (bModified)
            {
                SaveResultToCache();
            }
            return bModified;
        }

        protected virtual void OneWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string domain = (string)e.Argument;
            logger.Info("DoWork({0})", domain);

            var List = GetResultNow(domain, false);
            logger.Info(String.Format("NetServerEnum: {0}", List.Count));
            #if REMOVE_RANDOM_COMPS
            Random R = new Random();
            int Count = R.Next(List.Count * 2 / 3);
            for (int i = 0; i < Count; i++)
            {
                int Index = R.Next(List.Count);
                List.RemoveAt(Index);
            }
            logger.Info(String.Format("Random comps removed: {0}", List.Count));
            #endif
            var args = new DataChangedEventArgs { Subject = domain, Data = List };
            e.Result = args;
        }

        protected virtual void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var Result = (DataChangedEventArgs)e.Result;
            if (e.Cancelled)
            {
                logger.Info("Cancelled({0})", Result.Subject);
                return;
            }
            logger.Info("Completed({0})", Result.Subject);

            bool bModified = SetResult(Result.Subject, (List<ServerInfo>)Result.Data);
            if (bModified)
            {
                //lock (m_Subjects)
                {
                    if (m_Subjects.ContainsKey(Result.Subject))
                    {
                        var List = m_Subjects[Result.Subject];
                        logger.Info("Notify {0} subscriber(s) [one subject]", List.Count);
                        foreach (var Subscriber in List)
                            Subscriber.DataChanged(this, Result);
                    }
                }
            }
        }

        private static bool SortedListIsModified(IList<ServerInfo> ListA, IList<ServerInfo> ListB)
        {
            if (ListA == null || ListB == null)
                return false;
            bool bModified = false;
            if (ListA.Count != ListB.Count)
                bModified = true;
            else
                for (int i = 0; i < ListA.Count - 1; i++)
                    if (ListA[i].CompareTo(ListB[i]) != 0)
                    {
                        bModified = true;
                        break;
                    }
            return bModified;
        }

        private string GetCacheFileName()
        {
            return Path.ChangeExtension(Settings.GetExecutableFileName(), ".cache");
        }


        private void SaveResultToCache()
        {
            logger.Info("SaveResultToCache()");
            var Temp = new Dictionary<string, NetApi32.SERVER_INFO_101[]>();
            foreach (var Pair in m_Results)
            {
                NetApi32.SERVER_INFO_101[] TempList = new NetApi32.SERVER_INFO_101[Pair.Value.Count];
                for (int i = 0; i < Pair.Value.Count; i++)
                    TempList[i] = Pair.Value[i].GetInfo();
                Temp.Add(Pair.Key, TempList);
            }
            try
            {
                SerializeUtils.SerializeObjectToBinaryFile(GetCacheFileName(), Temp);
            }
            catch { }
        }

        private void LoadResultFromCache()
        {
            logger.Info("LoadResultFromCache()");
            try
            {
                var Temp = (Dictionary<string, NetApi32.SERVER_INFO_101[]>)SerializeUtils.DeserializeObjectFromBinaryFile(GetCacheFileName());
                lock (m_Results)
                {
                    m_Results.Clear();
                    foreach (var Pair in Temp)
                    {
                        var TempList = new List<ServerInfo>();
                        Array.ForEach(Pair.Value, Info => TempList.Add(new ServerInfo(Info)));
                        m_Results.Add(Pair.Key, TempList);
                        TempList.Sort();
                    }
                }
            }
            catch { }
        }

        public bool HasSubscribers()
        {
            bool Found = false;
            foreach (var Pair in m_Subjects)
            {
                if (Pair.Value.Count > 0)
                {
                    Found = true;
                    break;
                }
            }
            return Found;
        }

        private void SubscribersChanged()
        {
            bool Found = HasSubscribers();
            if (!Found)
            {
                m_RefreshTimer.Enabled = false;
                m_InstantUpdate = true;
            }
            else
            {
                if (m_InstantUpdate)
                {
                    m_RefreshTimer.Enabled = false;
                    RefreshTimer_Tick(m_RefreshTimer, new EventArgs());
                    m_RefreshTimer.Enabled = true;
                    m_InstantUpdate = false;
                }
            }
        }

        #region ISubscription interface
        public void SubscribeToSubject(ISubscriber sender, string subject)
        {
            if (sender == null)
                return;
            bool Modified = false;
            if (m_Subjects.ContainsKey(subject))
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
                var List = new List<ISubscriber>();
                List.Add(sender);
                m_Subjects.Add(subject, List);
                Modified = true;
            }
            if (Modified)
            {
                var Result = GetResultNow(subject, true);
                sender.DataChanged(this, new DataChangedEventArgs() { Subject = subject, Data = Result });
            }
        }

        public void UnSubscribe(ISubscriber sender)
        {
            if (sender == null)
                return;
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
                var args = new DataChangedEventArgs() { Subject = null, Data = null };
                sender.DataChanged(this, args);
            }
        }
        #endregion
    }
}
