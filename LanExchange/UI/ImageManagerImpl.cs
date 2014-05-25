using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.UI.WinForms
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

        private static readonly Bitmap SmallEmpty = new Bitmap(16, 16);
        private static readonly Bitmap LargeEmpty = new Bitmap(32, 32);

        public ImageManagerImpl()
        {
            m_NamesMap = new Dictionary<string, int>();
            // init system images
            App.Resolve<IShell32Service>().FileIconInit(true);

            var small = App.Resolve<ISysImageListService>();
            var large = App.Resolve<ISysImageListService>();
            small.Create(SysImageListSize.SmallIcons);
            large.Create(SysImageListSize.LargeIcons);
            // init image lists
            m_SmallImageList = new ImageList();
            m_SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_SmallImageList.ImageSize = small.Size;
            m_LargeImageList = new ImageList();
            m_LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_LargeImageList.ImageSize = large.Size;
            // Workgroup icon
            Icon icon1 = small.GetIcon(SYSTEM_INDEX_WORKGROUP);
            Icon icon2 = large.GetIcon(SYSTEM_INDEX_WORKGROUP);
            RegisterImage(PanelImageNames.DOMAIN, icon1, icon2);
            // MyComputer icon
            icon1 = small.GetIcon(SYSTEM_INDEX_MYCOMPUTER);
            icon2 = large.GetIcon(SYSTEM_INDEX_MYCOMPUTER);
            RegisterImage(PanelImageNames.COMPUTER, icon1, icon2);
            if (icon1 != null && icon2 != null)
            {
                RegisterDisabledImage(PanelImageNames.COMPUTER + PanelImageNames.HIDDEN_POSTFIX, icon1, icon2);
                RegisterImageWithOtherColor(PanelImageNames.COMPUTER + PanelImageNames.RED_POSTFIX, icon1, icon2, 2);
                RegisterImageWithOtherColor(PanelImageNames.COMPUTER + PanelImageNames.GREEN_POSTFIX, icon1, icon2, 4);
            }
            // Folder icon
            icon1 = small.GetIcon(SYSTEM_INDEX_FOLDER);
            icon2 = large.GetIcon(SYSTEM_INDEX_FOLDER);
            RegisterImage(PanelImageNames.FOLDER, icon1, icon2);
            if (icon1 != null && icon2 != null)
                RegisterDisabledImage(PanelImageNames.FOLDER + PanelImageNames.HIDDEN_POSTFIX, icon1, icon2);
            // ".." icon
            RegisterImage(PanelImageNames.DOUBLEDOT, Resources.back_16, Resources.back_32);
            // User icon
            RegisterImage(PanelImageNames.USER, Resources.user_16, Resources.user_32);
            RegisterDisabledImage(PanelImageNames.USER + PanelImageNames.HIDDEN_POSTFIX, Resources.user_16, Resources.user_32);
            // release sys images list
            small.Dispose();
            large.Dispose();
        }

        private void RegisterImageWithOtherColor(string imageName, Icon icon1, Icon icon2 , int shift)
        {
            var bitmap1 = BitmapUtils.MadeNewBitmap(icon1.ToBitmap(), 72 * shift);
            var bitmap2 = BitmapUtils.MadeNewBitmap(icon2.ToBitmap(), 72 * shift);
            RegisterImage(imageName, bitmap1, bitmap2);
        }

        public void Dispose()
        {
            m_SmallImageList.Dispose();
            m_LargeImageList.Dispose();
        }

        private static Image MadeDisabledBitmap(Image bmp)
        {
            var result = new Bitmap(bmp.Width, bmp.Height);
            using (var gr = Graphics.FromImage(result))
            {
                ControlPaint.DrawImageDisabled(gr, bmp, 0, 0, Color.Transparent);
            }
            return result;
        }

        public int IndexOf(string name)
        {
            int index;
            if (name != null && m_NamesMap.TryGetValue(name, out index))
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
                m_SmallImageList.Images[index] = SmallEmpty;
                m_LargeImageList.Images[index] = LargeEmpty;
            }
        }

        public Image GetSmallImage(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            return m_SmallImageList.Images[index];
        }

        public Image GetLargeImage(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            return m_LargeImageList.Images[index];
        }

        public Icon GetSmallIcon(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            Icon result;
            using (var bitmap = new Bitmap(m_SmallImageList.Images[index]))
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
            using (var bitmap = new Bitmap(m_LargeImageList.Images[index]))
            {
                result = Icon.FromHandle(bitmap.GetHicon());
            }
            return result;
        }

        public Image GetSmallImageOfFileName(string fileName)
        {
            using (var small = App.Resolve<ISysImageListService>())
            {
                small.Create(SysImageListSize.SmallIcons);
                var index = small.GetIconIndex(fileName);
                return index < 0 ? null : small.GetIcon(index).ToBitmap();
            }
        }

        public Image GetLargeImageOfFileName(string fileName)
        {
            using (var large = App.Resolve<ISysImageListService>())
            {
                large.Create(SysImageListSize.LargeIcons);
                var index = large.GetIconIndex(fileName);
                return index < 0 ? null : large.GetIcon(index).ToBitmap();
            }
        }

        public void SetImagesTo(object control)
        {
            if (control is TabControl)
                (control as TabControl).ImageList = m_SmallImageList;
            if (control is StatusStrip)
                (control as StatusStrip).ImageList = m_SmallImageList;
            if (control is ContextMenuStrip)
                (control as ContextMenuStrip).ImageList = m_SmallImageList;
            if (control is ListView)
            {
                var lv = control as ListView;
                lv.SmallImageList = m_SmallImageList;
                lv.LargeImageList = m_LargeImageList;
            }
        }

    }
}
