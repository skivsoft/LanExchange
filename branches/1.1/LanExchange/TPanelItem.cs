using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using OSTools;
using System.Net;

namespace LanExchange
{
    public abstract class TPanelItem : IComparable<TPanelItem>
    {
        protected abstract string GetComment();
        protected abstract void SetComment(string value);
        protected abstract string GetName();
        protected abstract void SetName(string value);
        
        protected virtual int GetImageIndex()
        {
            return 0;
        }

        protected virtual string GetToolTipText()
        {
            return Comment;
        }

        public virtual string[] getStrings()
        {
            return new string[2] { Name.ToUpper(), Comment.ToUpper() };
        }

        public virtual string[] GetSubItems()
        {
            return new string[1] { Comment };
        }

        public virtual void CopyExtraFrom(TPanelItem Comp)
        {
            // empty for base class
        }
        
        public string Name 
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        public string Comment
        {
            get { return GetComment(); }
            set { SetComment(value); }
        }

        public int ImageIndex
        {
            get { return GetImageIndex(); }
        }

        public string ToolTipText
        {
            get { return GetToolTipText(); }
        }

        public virtual int CompareTo(TPanelItem p2)
        {
            int Result;
            if (String.IsNullOrEmpty(this.Name))
                if (String.IsNullOrEmpty(p2.Name))
                    Result = 0;
                else
                    Result = -1;
            else
                if (String.IsNullOrEmpty(p2.Name))
                    Result = +1;
                else
                    Result = this.Name.CompareTo(p2.Name);
            return Result;
        }
    }

    public class TPanelItemComparer : IComparer<TPanelItem>
    {
        public int Compare(TPanelItem Item1, TPanelItem Item2)
        {
            return Item1.CompareTo(Item2);
        }
    }
}
