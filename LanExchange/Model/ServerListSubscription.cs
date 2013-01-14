﻿#define __REMOVE_RANDOM_COMPS

using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Windows.Forms;
using NLog;
using LanExchange.Utils;
using System.IO;
using System.Collections;

namespace LanExchange.Model
{
    public class ServerListSubscription : ISubscription
    {
        #region Static fields and methods

        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        private static ServerListSubscription m_Instance;

        public static ISubscription Instance
        {
            get
            {
                if (m_Instance != null) return m_Instance;
                m_Instance = new ServerListSubscription();
                return m_Instance;
            }
        }
        #endregion

        private readonly IDictionary<string, IList<ISubscriber>> m_Subjects;
        private readonly IDictionary<string, IList<ServerInfo>> m_Results;

        private int m_RefreshInterval;
        private readonly Timer m_RefreshTimer;
        private readonly BackgroundWorkerList m_Workers;
        private bool m_InstantUpdate = true;

        protected ServerListSubscription()
        {
            // lists
            m_Subjects = new Dictionary<string, IList<ISubscriber>>();
            m_Results = new Dictionary<string, IList<ServerInfo>>();
            // load cached results
            LoadResultFromCache();
            // timer
            m_RefreshTimer = new Timer();
            m_RefreshTimer.Tick += RefreshTimer_Tick;
            m_RefreshTimer.Enabled = false;
            // worker list for scanning network
            m_Workers = new BackgroundWorkerList();
        }

        //public bool IsInstantUpdate
        //{
        //    get { return m_InstantUpdate; }
        //}

        //public bool IsBusy
        //{
        //    get { return m_Workers.IsBusy; }
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

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            logger.Info("RefreshTimer.Tick() executed. Next tick in {0} sec.", m_RefreshInterval/1000);
            if (!m_Workers.IsBusy)
            {
                // prepare workers to launch
                foreach (var Pair in m_Subjects)
                {
                    if (!m_Workers.Exists(Pair.Key))
                        m_Workers.Add(Pair.Key, CreateOneWorker());
                }
                // launch!
                m_Workers.RunWorkerAsync();
            }
            else
                logger.Info("Tick: {0} of {1} worker(s) busy, no action", m_Workers.BusyCount, m_Workers.Count);
        }

        private BackgroundWorker CreateOneWorker()
        {
            var Result = new BackgroundWorker();
            Result.DoWork += OneWorker_DoWork;
            Result.RunWorkerCompleted += OneWorker_RunWorkerCompleted;
            return Result;
        }

        /// <summary>
        /// This method must return server list instantly. Use cache is preferred.
        /// </summary>
        private bool PrepareResult(string domain, bool cachePref)
        {
            if (cachePref && m_Results.ContainsKey(domain))
            {
                logger.Info("GetFromCache(domain:{0})", domain);
                return true;
            }
            // get server list via OS api
            NetApi32.SERVER_INFO_101[] List;
            if (String.IsNullOrEmpty(domain))
                List = NetApi32Utils.GetDomainList();
            else
                List = NetApi32Utils.GetComputerList(domain);
            // convert array to IList<ServerInfo>
            var result = new List<ServerInfo>();
            Array.ForEach(List, item => result.Add(new ServerInfo(item)));
            return SetResult(domain, result);
        }

        private bool SetResult(string domain, IList<ServerInfo> list)
        {
            bool bModified = false;
            lock (m_Results)
            {
                if (!m_Results.ContainsKey(domain))
                {
                    bModified = true;
                    m_Results.Add(domain, list);
                }
                if (!bModified)
                {
                    bModified = SortedListIsModified(m_Results[domain], list);
                    if (bModified)
                        m_Results[domain] = list;
                }
            }
            if (bModified)
            {
                SaveResultToCache();
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
            var domain = (string)e.Argument;
            #if REMOVE_RANDOM_COMPS
            var List = m_Results[domain];
            var R = new Random();
            int Count = R.Next(List.Count * 2 / 3);
            for (int i = 0; i < Count; i++)
            {
                int Index = R.Next(List.Count);
                List.RemoveAt(Index);
            }
            m_Results[domain] = List;
            logger.Info(String.Format("Random comps removed: {0}", List.Count));
            #endif
            if (PrepareResult(domain, false))
                e.Result = domain;
            else
                e.Result = null;
        }

        /// <summary>
        /// Browsing network complete. If serveinfo list is not modified e.Cancel set to False.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var Subject = (string)e.Result;
            if (e.Cancelled || Subject == null)
                return;
            logger.Info("Completed(domain:{0})", Subject);
            // notify all subscribers for current subject
            lock (m_Subjects)
            {
                if (m_Subjects.ContainsKey(Subject))
                {
                    var List = m_Subjects[Subject];
                    if (List.Count > 0)
                    {
                        logger.Info("Notify {0} subscriber(s) with subject \"{1}\"", List.Count, Subject);
                        foreach (var Subscriber in List)
                            Subscriber.DataChanged(this, Subject);
                    }
                }
            }
        }

        private static bool SortedListIsModified(IList<ServerInfo> listA, IList<ServerInfo> listB)
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
            return Path.ChangeExtension(Settings.GetExecutableFileName(), ".cache");
        }


        private void SaveResultToCache()
        {
            logger.Info("SaveResultToCache()");
            var Temp = new Dictionary<string, NetApi32.SERVER_INFO_101[]>();
            foreach (var Pair in m_Results)
            {
                var TempList = new NetApi32.SERVER_INFO_101[Pair.Value.Count];
                for (int i = 0; i < Pair.Value.Count; i++)
                    TempList[i] = Pair.Value[i].GetInfo();
                Temp.Add(Pair.Key, TempList);
            }
            try
            {
                SerializeUtils.SerializeObjectToBinaryFile(GetCacheFileName(), Temp);
            }
            catch (Exception E)
            {
                logger.Error("SaveResultToCache: {0}", E.Message);
            }
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
                        Array.ForEach(Pair.Value, info => TempList.Add(new ServerInfo(info)));
                        m_Results.Add(Pair.Key, TempList);
                        TempList.Sort();
                    }
                }
            }
            catch (Exception E)
            {
                logger.Error("LoadResultFromCache: {0}", E.Message);
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
                    RefreshTimer_Tick(m_RefreshTimer, new EventArgs());
                    m_RefreshTimer.Enabled = true;
                    m_InstantUpdate = false;
                    logger.Info("RefreshTimer started. Next tick in {0} sec.", m_RefreshInterval/1000);
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
                PrepareResult(subject, true);
                sender.DataChanged(this, subject);
                SubscribersChanged();
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
                sender.DataChanged(this, null);
                SubscribersChanged();
            }
        }
        #endregion
        public IEnumerable<KeyValuePair<string, IList<ISubscriber>>> GetSubjects()
        {
            return m_Subjects;
        }


        public IEnumerable GetListBySubject(string subject)
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