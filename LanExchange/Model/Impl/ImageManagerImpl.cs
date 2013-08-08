using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.UI;
using ShellDll;

namespace LanExchange.Model.Impl
{
    public class ImageManagerImpl : IImageManager
    {
        private const int SYSTEM_INDEX_MYCOMPUTER = 15;
        private const int SYSTEM_INDEX_WORKGROUP  = 18;
        private const int SYSTEM_INDEX_FOLDER     = 4;

        private readonly ImageList m_SmallImageList;
        private readonly ImageList m_LargeImageList;
        private readonly Dictionary<string, int> m_NamesMap;
        private int m_LastIndex;

        private static Bitmap SmallEmpty = new Bitmap(16, 16);
        private static Bitmap LargeEmpty = new Bitmap(32, 32);

        public ImageManagerImpl()
        {
            m_NamesMap = new Dictionary<string, int>();
            // init system images
            ShellAPI.FileIconInit(true);
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
            Icon icon1 = Small.Icon(SYSTEM_INDEX_WORKGROUP);
            Icon icon2 = Large.Icon(SYSTEM_INDEX_WORKGROUP);
            RegisterImage(PanelImageNames.Workgroup, icon1, icon2);
            // MyComputer icon
            icon1 = Small.Icon(SYSTEM_INDEX_MYCOMPUTER);
            icon2 = Large.Icon(SYSTEM_INDEX_MYCOMPUTER);
            RegisterImage(PanelImageNames.ComputerNormal, icon1, icon2);
            RegisterDisabledImage(PanelImageNames.ComputerDisabled, icon1, icon2);
            //RegisterImage(PanelImageNames.ComputerDisabled, MadeDisabledBitmap(bitmap1), MadeDisabledBitmap(bitmap2));
            // Folder icon
            icon1 = Small.Icon(SYSTEM_INDEX_FOLDER);
            icon2 = Large.Icon(SYSTEM_INDEX_FOLDER);
            RegisterImage(PanelImageNames.ShareNormal, icon1, icon2);
            //RegisterImage(PanelImageNames.ShareHidden, MadeDisabledBitmap(bitmap1), MadeDisabledBitmap(bitmap2));
            RegisterDisabledImage(PanelImageNames.ShareHidden, icon1, icon2);
            // ".." icon
            RegisterImage(PanelImageNames.DoubleDot, Resources.back_16, Resources.back_32);
            //// User icon
            RegisterImage(PanelImageNames.UserNormal, Resources.user_16, Resources.user_32);
            RegisterDisabledImage(PanelImageNames.UserDisabled, Resources.user_16, Resources.user_32);
            // release sys images list
            Small.Dispose();
            Large.Dispose();
        }

        public void Dispose()
        {
            m_SmallImageList.Dispose();
            m_LargeImageList.Dispose();
        }

        private static Image MadeDisabledBitmap(Image bmp)
        {
            Image Result = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics GR = Graphics.FromImage(Result))
            {
                ControlPaint.DrawImageDisabled(GR, bmp, 0, 0, Color.Transparent);
            }
            return Result;
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
            if (m_NamesMap.TryGetValue(name, out index))
                return index;
            return -1;
        }

        public void RegisterImage(string name, Image imageSmall, Image imageLarge)
        {
            int index;
            if (imageSmall == null) imageSmall = SmallEmpty;
            if (imageLarge == null) imageLarge = LargeEmpty;
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

        public void RegisterDisabledImage(string name, Image imageSmall, Image imageLarge)
        {
            RegisterImage(name, MadeDisabledBitmap(imageSmall), MadeDisabledBitmap(imageLarge));
        }

        public void RegisterImage(string name, Icon iconSmall, Icon iconLarge)
        {
            Image imageSmall = iconSmall == null ? null : iconSmall.ToBitmap();
            Image imageLarge = iconLarge == null ? null : iconLarge.ToBitmap();
            RegisterImage(name, imageSmall, imageLarge);
        }

        private void RegisterDisabledImage(string name, Icon iconSmall, Icon iconLarge)
        {
            Image imageSmall = iconSmall == null ? null : iconSmall.ToBitmap();
            Image imageLarge = iconLarge == null ? null : iconLarge.ToBitmap();
            RegisterImage(name, MadeDisabledBitmap(imageSmall), MadeDisabledBitmap(imageLarge));
        }

        public void UnregisterImage(string name)
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
