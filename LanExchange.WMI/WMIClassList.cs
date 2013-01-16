using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.WMI
{
    /// <summary>
    /// List of used wmi classes.
    /// </summary>
    public class WMIClassList
    {
        private static WMIClassList m_Instance;
        private bool m_Loaded;

        protected WMIClassList()
        {
            
        }

        public static WMIClassList Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new WMIClassList();
                return m_Instance;
            }
        }

        public bool Loaded
        {
            get { return m_Loaded; }
        }

        public void EnumClasses()
        {
            m_Loaded = true;

        }
    }
}
