using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Application.Interfaces;
using LanExchange.Application.Interfaces.Factories;
using LanExchange.Plugin.WinForms.Utils;
using LanExchange.Presentation.Interfaces;
using LanExchange.Properties;

namespace LanExchange.Application.Implementation
{
    internal sealed class ImageManager : IImageManager
    {
        private const int SYSTEM_INDEX_MYCOMPUTER = 15;
        private const int SYSTEM_INDEX_WORKGROUP  = 18;
        private const int SYSTEM_INDEX_FOLDER     = 4;

        private readonly IServiceFactory serviceFactory;

        private readonly ImageList smallImageList;
        private readonly ImageList largeImageList;
        private readonly Dictionary<string, int> namesMap;
        private int lastIndex;

        private static readonly Bitmap SmallEmpty = new Bitmap(16, 16);
        private static readonly Bitmap LargeEmpty = new Bitmap(32, 32);

        public ImageManager(
            IShell32Service shellService,
            IServiceFactory serviceFactory)
        {
            Contract.Requires<ArgumentNullException>(shellService != null);
            Contract.Requires<ArgumentNullException>(serviceFactory != null);

            this.serviceFactory = serviceFactory;

            namesMap = new Dictionary<string, int>();
            // init system images
            shellService.FileIconInit(true);

            var small = serviceFactory.CreateSysImageListService();
            var large = serviceFactory.CreateSysImageListService();
            small.Create(SysImageListSize.SmallIcons);
            large.Create(SysImageListSize.LargeIcons);
            // init image lists
            smallImageList = new ImageList();
            smallImageList.ColorDepth = ColorDepth.Depth32Bit;
            smallImageList.ImageSize = small.Size;
            largeImageList = new ImageList();
            largeImageList.ColorDepth = ColorDepth.Depth32Bit;
            largeImageList.ImageSize = large.Size;
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
            smallImageList.Dispose();
            largeImageList.Dispose();
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
            if (name != null && namesMap.TryGetValue(name, out index))
                return index;
            return -1;
        }

        public void RegisterImage(string name, Image imageSmall, Image imageLarge)
        {
            int index;
            if (imageSmall == null) imageSmall = SmallEmpty;
            if (imageLarge == null) imageLarge = LargeEmpty;
            if (namesMap.TryGetValue(name, out index))
            {
                smallImageList.Images[index] = imageSmall;
                largeImageList.Images[index] = imageLarge;
            }
            else
            {
                namesMap.Add(name, lastIndex);
                smallImageList.Images.Add(imageSmall);
                largeImageList.Images.Add(imageLarge);
                lastIndex++;
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
            if (namesMap.TryGetValue(name, out index))
            {
                namesMap.Remove(name);
                smallImageList.Images[index] = SmallEmpty;
                largeImageList.Images[index] = LargeEmpty;
            }
        }

        public Image GetSmallImage(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            return smallImageList.Images[index];
        }

        public Image GetLargeImage(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            return largeImageList.Images[index];
        }

        public Icon GetSmallIcon(string key)
        {
            var index = IndexOf(key);
            if (index == -1) return null;
            Icon result;
            using (var bitmap = new Bitmap(smallImageList.Images[index]))
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
            using (var bitmap = new Bitmap(largeImageList.Images[index]))
            {
                result = Icon.FromHandle(bitmap.GetHicon());
            }
            return result;
        }

        public Image GetSmallImageOfFileName(string fileName)
        {
            using (var small = serviceFactory.CreateSysImageListService())
            {
                small.Create(SysImageListSize.SmallIcons);
                var index = small.GetIconIndex(fileName);
                return index < 0 ? null : small.GetIcon(index).ToBitmap();
            }
        }

        public Image GetLargeImageOfFileName(string fileName)
        {
            using (var large = serviceFactory.CreateSysImageListService())
            {
                large.Create(SysImageListSize.LargeIcons);
                var index = large.GetIconIndex(fileName);
                return index < 0 ? null : large.GetIcon(index).ToBitmap();
            }
        }

        public void SetImagesTo(object control)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            var needImageList = control as ISupportImageList;
            needImageList?.SetImageList(smallImageList);

            // TODO use IWithImageList
            var tabControl = control as TabControl;
            if (tabControl != null)
                tabControl.ImageList = smallImageList;

            var contextMenu = control as ContextMenuStrip;
            if (contextMenu != null)
                contextMenu.ImageList = smallImageList;

            var listView = control as ListView;
            if (listView != null)
            {
                listView.SmallImageList = smallImageList;
                listView.LargeImageList = largeImageList;
            }
        }

    }
}
