using System;

namespace LanExchange.Model
{
    public abstract class AbstractPanelItem : IComparable<AbstractPanelItem>
    {
        public abstract string Name { get; set; }
        public abstract string Comment { get; set; }

        public virtual string[] getStrings()
        {
            return new[] { Name.ToUpper(), Comment.ToUpper() };
        }

        public virtual string[] GetSubItems()
        {
            return new[] { Comment };
        }

        public virtual int ImageIndex
        {
            get { return -1; }
        }

        public virtual string ToolTipText
        {
            get { return Comment; }
        }

        public virtual int CompareTo(AbstractPanelItem p2)
        {
            int Result;
            if (String.IsNullOrEmpty(Name))
                Result = String.IsNullOrEmpty(p2.Name) ? 0 : -1;
            else
                Result = String.IsNullOrEmpty(p2.Name) ? +1 : String.Compare(Name, p2.Name, StringComparison.Ordinal);
            return Result;
        }
    }
}