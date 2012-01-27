using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using OSTools;
using System.Net;

namespace LanExchange
{
    #region Модель "Элемент панели LanExchange"
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

        public int CompareTo(TPanelItem p2)
        {
            return Name.CompareTo(p2.Name);
        }
    }

    public class TPanelItemComparer : IComparer<TPanelItem>
    {
        public int Compare(TPanelItem Item1, TPanelItem Item2)
        {
            if (Item1.Name == "..")
                if (Item2.Name == "..")
                    return 0;
                else
                    return -1;
            else
                return Item1.CompareTo(Item2);
        }
    }
    #endregion

    #region Модель "Компьютер"
    public class TComputerItem : TPanelItem
    {
        // индексы иконок компов
        public const int imgCompDefault = 0;
        public const int imgCompBlue = 1;
        public const int imgCompMagenta = 2;
        public const int imgCompGray = 3;
        public const int imgCompGreen = 4;
        public const int imgCompRed = 5;

        private string computer_name;
        private string comment;
        private string version;
        private uint platform_id;
        private uint ver_major;
        private uint ver_minor;
        private uint type_id;
        private bool pingable;
        private bool logged;
        private IPEndPoint ipendpoint;

        public TComputerItem()
        {
            this.pingable = true;
        }

        public TComputerItem(string computer_name, string comment, uint platform_id, uint ver_major, uint ver_minor, uint type_id)
        {
            this.computer_name = computer_name;
            this.comment = comment;
            this.SetPlatform(platform_id, ver_major, ver_minor, type_id);
            this.pingable = true;
        }

        public void SetPlatform(uint platform_id, uint ver_major, uint ver_minor, uint type_id)
        {
            this.platform_id = platform_id;
            this.ver_major = ver_major;
            this.ver_minor = ver_minor;
            this.type_id = type_id;
            this.version = LocalNetwork.PlatformToString(platform_id, ver_major, ver_minor, type_id);
        }

        protected override string GetName()
        {
            return computer_name;
        }

        protected override void SetName(string value)
        {
            computer_name = value;
        }

        protected override string GetComment()
        {
            return this.comment;
        }

        protected override void SetComment(string value)
        {
            this.comment = value;
        }

        public override string[] getStrings()
        {
            return new string[3] { Comment.ToUpper(), Name.ToUpper(), OSVersion.ToUpper() };
        }

        protected override int GetImageIndex()
        {
            if (IsLogged)
                return imgCompGreen;
            else
                if (IsPingable)
                    return  imgCompDefault;
                else
                    return imgCompRed;
        }

        protected override string GetToolTipText()
        {
            return String.Format("{0}\n{1}", Comment, OSVersion);
        }

        public string OSVersion
        {
            get { return version; }
            set { version = value; }
        }

        public uint Type
        {
            get { return type_id; }
            set { type_id = value; }
        }

        public bool IsPingable
        {
            get { return pingable; }
            set { pingable = value; }
        }

        public bool IsLogged
        {
            get { return logged; }
            set { logged = value; }
        }

        public IPEndPoint EndPoint
        {
            get { return ipendpoint; }
            set { ipendpoint = value; }
        }

        public override void CopyExtraFrom(TPanelItem Comp)
        {
            if (Comp == null) return;
            pingable = (Comp as TComputerItem).pingable;
            logged = (Comp as TComputerItem).logged;
            ipendpoint = (Comp as TComputerItem).ipendpoint;
        }
    }
    #endregion

    #region Модель "Общий ресурс"
    public class TShareItem : TPanelItem
    {
        public const int imgHiddenFolder  = 6;
        public const int imgNormalFolder  = 7;
        public const int imgPrinterFolder = 8;

        private string share_name;
        private string share_comment;
        private uint share_type;
        private string computer_name;

        public TShareItem(string share_name, string share_comment, uint share_type, string computer_name)
        {
            this.share_name = share_name;
            this.share_comment = share_comment;
            this.share_type = share_type;
            this.computer_name = computer_name;
        }

        protected override string GetName()
        {
            return share_name;
        }

        protected override void SetName(string value)
        {
            share_name = value;
        }

        protected override string GetComment()
        {
            return this.share_comment;
        }

        protected override void SetComment(string value)
        {
            this.share_comment = value;
        }

        public uint Type
        {
            get { return share_type; }
            set { share_type = value; }
        }

        public string ComputerName
        {
            get { return computer_name; }
            set { computer_name = value; }
        }

        protected override int GetImageIndex()
        {
            if (IsPrinter)
                return imgPrinterFolder;
            else
                return IsHidden ? imgHiddenFolder : imgNormalFolder;
        }

        public bool IsPrinter
        {
            get
            {
                return (share_type == (uint)LocalNetwork.SHARE_TYPE.STYPE_PRINTQ);
            }
        }

        public bool IsHidden
        {
            get
            {
                if (!String.IsNullOrEmpty(share_name))
                    return share_name[share_name.Length - 1] == '$';
                else
                    return false;
            }
        }

        public override string[] GetSubItems()
        {
            return new string[2] { "", Comment };
        }
    }
    #endregion

    #region Модель "Элемент диска"
    public class TFileItem : TPanelItem
    {
        protected override void SetName(string value)
        {
            throw new NotImplementedException();
        }

        protected override string GetName()
        {
            throw new NotImplementedException();
        }

        protected override void SetComment(string value)
        {
            throw new NotImplementedException();
        }

        protected override string GetComment()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
