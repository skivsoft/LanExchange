using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LanExchange.Properties;
using LanExchange.Sdk;
using LanExchange.UI;
using LanExchange.Utils;

namespace LanExchange.Model
{
    public class LanExchangeIcons : IPanelImageManager
    {
        private const int SYSTEM_INDEX_MYCOMPUTER = 15;
        private const int SYSTEM_INDEX_WORKGROUP  = 18;
        private const int SYSTEM_INDEX_FOLDER     = 4;

        private static LanExchangeIcons m_Instance;
        private readonly ImageList m_SmallImageList;
        private readonly ImageList m_LargeImageList;
        private readonly Dictionary<string, int> m_NamesMap;
        private int m_LastIndex;

        private static Bitmap SmallEmpty = new Bitmap(16, 16);
        private static Bitmap LargeEmpty = new Bitmap(32, 32);

        private LanExchangeIcons()
        {
            m_NamesMap = new Dictionary<string, int>();
            // init system images
            Shell32.FileIconInit(true);
            var Small = new SysImageList(SysImageListSize.smallIcons);
            var Large = new SysImageList(SysImageListSize.largeIcons);
            // init image lists
            m_SmallImageList = new ImageList();
            m_SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_SmallImageList.ImageSize = Small.Size;
            m_LargeImageList = new ImageList();
            m_LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_LargeImageList.ImageSize = Large.Size;
            // Workgroup icon
            Bitmap icon1 = Small.Icon(SYSTEM_INDEX_WORKGROUP).ToBitmap();
            Bitmap icon2 = Large.Icon(SYSTEM_INDEX_WORKGROUP).ToBitmap();
            RegisterImage(PanelImageNames.Workgroup, icon1, icon2);
            // MyComputer icon
            icon1 = Small.Icon(SYSTEM_INDEX_MYCOMPUTER).ToBitmap();
            icon2 = Large.Icon(SYSTEM_INDEX_MYCOMPUTER).ToBitmap();
            RegisterImage(PanelImageNames.ComputerNormal, icon1, icon2);
            RegisterImage(PanelImageNames.ComputerDisabled, MadeDisabledBitmap(icon1), MadeDisabledBitmap(icon2));
            // Folder icon
            icon1 = Small.Icon(SYSTEM_INDEX_FOLDER).ToBitmap();
            icon2 = Large.Icon(SYSTEM_INDEX_FOLDER).ToBitmap();
            RegisterImage(PanelImageNames.ShareNormal, icon1, icon2);
            RegisterImage(PanelImageNames.ShareHidden, MadeDisabledBitmap(icon1), MadeDisabledBitmap(icon2));
            // ".." icon
            RegisterImage(PanelImageNames.DoubleDot, Resources.back_16, Resources.back_32);
            // release sys images list
            Small.Dispose();
            Large.Dispose();
        }

        public void Dispose()
        {
            m_SmallImageList.Dispose();
            m_LargeImageList.Dispose();
        }

        private static Bitmap MadeDisabledBitmap(Bitmap bmp)
        {
            Bitmap Result = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics GR = Graphics.FromImage(Result))
            {
                ControlPaint.DrawImageDisabled(GR, bmp, 0, 0, Color.Transparent);
            }
            return Result;
        }

        public static LanExchangeIcons Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new LanExchangeIcons();
                return m_Instance;
            }
        }

        public ImageList SmallImageList
        {
            get { return m_SmallImageList; }
        }

        public ImageList LargeImageList
        {
            get { return m_LargeImageList; }
        }

        public int IndexOf(string name)
        {
            int index;
            if (m_Instance.m_NamesMap.TryGetValue(name, out index))
                return index;
            return -1;
        }

        public void RegisterImage(string name, Image imageSmall, Image imageLarge)
        {
            int index;
            if (imageSmall == null)
                imageSmall = SmallEmpty;
            if (imageLarge == null)
                imageLarge = LargeEmpty;
            if (m_NamesMap.TryGetValue(name, out index))
            {
                m_SmallImageList.Images[index] = imageSmall;
                m_LargeImageList.Images[index] = imageLarge;
            }
            else
            {
                m_NamesMap.Add(name, m_LastIndex);
                m_SmallImageList.Images.Add(imageSmall);
                m_LargeImageList.Images.Add(imageLarge);
                m_LastIndex++;
            }
        }

        public void UnRegisterImage(string name)
        {
            int index;
            if (m_NamesMap.TryGetValue(name, out index))
            {
                m_NamesMap.Remove(name);
                SmallImageList.Images[index] = SmallEmpty;
                LargeImageList.Images[index] = LargeEmpty;
            }
        }

        public Image GetSmallImage(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            return SmallImageList.Images[index];
        }

        public Image GetLargeImage(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            return LargeImageList.Images[index];
        }

        public Icon GetSmallIcon(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            Icon result;
            using (Bitmap bitmap = new Bitmap(SmallImageList.Images[index]))
            {
                result = Icon.FromHandle(bitmap.GetHicon());
            }
            return result;
        }

        public Icon GetLargeIcon(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            Icon result;
            using (Bitmap bitmap = new Bitmap(LargeImageList.Images[index]))
            {
                result = Icon.FromHandle(bitmap.GetHicon());
            }
            return result;
        }
    }
}
