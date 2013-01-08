using System;

namespace LanExchange.Model
{
    public abstract class PanelItem : IComparable<PanelItem>
    {
        //protected abstract string GetComment();
        //protected abstract void SetComment(string value);
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

        public virtual void CopyExtraFrom(PanelItem Comp)
        {
            // empty for base class
        }
        
        public string Name 
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        public abstract string Comment { get; set; }

        public int ImageIndex
        {
            get { return GetImageIndex(); }
        }

        public string ToolTipText
        {
            get { return GetToolTipText(); }
        }

        public virtual int CompareTo(PanelItem p2)
        {
            int Result;
            if (String.IsNullOrEmpty(Name))
                Result = String.IsNullOrEmpty(p2.Name) ? 0 : -1;
            else
                Result = String.IsNullOrEmpty(p2.Name) ? +1 : Name.CompareTo(p2.Name);
            return Result;
        }
    }
}