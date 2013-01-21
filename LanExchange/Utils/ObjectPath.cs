using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Utils
{
    public class ObjectPath
    {
        private readonly Stack<object> m_Path;

        public event EventHandler Changed;

        public ObjectPath()
        {
            m_Path = new Stack<object>();
        }

        private void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public void Clear()
        {
            m_Path.Clear();
            OnChanged();
        }

        public void Push(object item)
        {
            m_Path.Push(item);
            OnChanged();
        }

        public object Pop()
        {
            return m_Path.Pop();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in m_Path)
            {
                sb.Insert(0, item);
                if (sb.Length > 0)
                    sb.Insert(0, @"\");
            }
            return sb.ToString();
        }
    }
}
