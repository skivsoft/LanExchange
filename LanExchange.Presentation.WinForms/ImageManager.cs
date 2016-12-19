using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.WinForms.Properties;

namespace LanExchange.Presentation.WinForms
{
    internal sealed class ImageManager : IImageManager
    {
        private const int SYSTEM_INDEX_MYCOMPUTER = 15;
        private const int SYSTEM_INDEX_WORKGROUP  = 18;
        private const int SYSTEM_INDEX_FOLDER     = 4;

        private static readonly Bitmap SmallEmpty = new Bitmap(16, 16);
        private static readonly Bitmap LargeEmpty = new Bitmap(32, 32);

        private readonly IShell32Service shellService;
        private readonly IServiceFactory serviceFactory;
        private readonly ILogService logService;

        private ImageList smallImageList;
        private ImageList largeImageList;
        private Dictionary<string, int> namesMap;
        private int lastIndex;

        public ImageManager(
            IShell32Service shellService,
            IServiceFactory serviceFactory,
            ILogService logService)
        {
            if (shellService == null) throw new ArgumentNullException(nameof(shellService));
            if (serviceFactory == null) throw new ArgumentNullException(nameof(serviceFactory));
            if (logService == null) throw new ArgumentNullException(nameof(logService));

            this.shellService = shellService;
            this.serviceFactory = serviceFactory;
            this.logService = logService;

            namesMap = new Dictionary<string, int>();
            Initialize();
        }

        public void Dispose()
        {
            smallImageList.Dispose();
            largeImageList.Dispose();
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
            logService.Log("register image {0}", name);
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
            if (control == null) throw new ArgumentNullException(nameof(control));

            var needImageList = control as ISupportImageList;
            needImageList?.SetImageList(smallImageList);

            // TODO use ISupportImageList
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

        private static Image MadeDisabledBitmap(Image bmp)
        {
            var result = new Bitmap(bmp.Width, bmp.Height);
            using (var gr = Graphics.FromImage(result))
            {
                ControlPaint.DrawImageDisabled(gr, bmp, 0, 0, Color.Transparent);
            }

            return result;
        }

        private void Initialize()
        {
            shellService.FileIconInit(true);
            using (var small = serviceFactory.CreateSysImageListService())
            using (var large = serviceFactory.CreateSysImageListService())
            {
                small.Create(SysImageListSize.SmallIcons);
                large.Create(SysImageListSize.LargeIcons);

                InitializeImageLists(small, large);
                RegisterWorkgroupIcon(small, large);
                RegisterMyComputerIcon(small, large);
                RegisterFolderIcon(small, large);

                // register double dot icon
                RegisterImage(PanelImageNames.DOUBLEDOT, Resources.back_16, Resources.back_32);

                // register user icon
                RegisterImage(PanelImageNames.USER, Resources.user_16, Resources.user_32);
                RegisterDisabledImage(PanelImageNames.USER + PanelImageNames.HiddenPostfix, Resources.user_16, Resources.user_32);
            }
        }

        private void InitializeImageLists(ISysImageListService small, ISysImageListService large)
        {
            smallImageList = new ImageList();
            smallImageList.ColorDepth = ColorDepth.Depth32Bit;
            smallImageList.ImageSize = small.Size;
            largeImageList = new ImageList();
            largeImageList.ColorDepth = ColorDepth.Depth32Bit;
            largeImageList.ImageSize = large.Size;
        }

        private void RegisterWorkgroupIcon(ISysImageListService small, ISysImageListService large)
        {
            var icon1 = small.GetIcon(SYSTEM_INDEX_WORKGROUP);
            var icon2 = large.GetIcon(SYSTEM_INDEX_WORKGROUP);
            RegisterImage(PanelImageNames.DOMAIN, icon1, icon2);
        }

        private void RegisterMyComputerIcon(ISysImageListService small, ISysImageListService large)
        {
            var icon1 = small.GetIcon(SYSTEM_INDEX_MYCOMPUTER);
            var icon2 = large.GetIcon(SYSTEM_INDEX_MYCOMPUTER);
            RegisterImage(PanelImageNames.COMPUTER, icon1, icon2);
            if (icon1 != null && icon2 != null)
            {
                RegisterDisabledImage(PanelImageNames.COMPUTER + PanelImageNames.HiddenPostfix, icon1, icon2);
                RegisterImageWithOtherColor(PanelImageNames.COMPUTER + PanelImageNames.RedPostfix, icon1, icon2, 2);
                RegisterImageWithOtherColor(PanelImageNames.COMPUTER + PanelImageNames.GreenPostfix, icon1, icon2, 4);
            }
        }

        private void RegisterFolderIcon(ISysImageListService small, ISysImageListService large)
        {
            var icon1 = small.GetIcon(SYSTEM_INDEX_FOLDER);
            var icon2 = large.GetIcon(SYSTEM_INDEX_FOLDER);
            RegisterImage(PanelImageNames.FOLDER, icon1, icon2);
            if (icon1 != null && icon2 != null)
                RegisterDisabledImage(PanelImageNames.FOLDER + PanelImageNames.HiddenPostfix, icon1, icon2);
        }

        private void RegisterImageWithOtherColor(string imageName, Icon icon1, Icon icon2, int shift)
        {
            var bitmap1 = BitmapUtils.MadeNewBitmap(icon1.ToBitmap(), 72 * shift);
            var bitmap2 = BitmapUtils.MadeNewBitmap(icon2.ToBitmap(), 72 * shift);
            RegisterImage(imageName, bitmap1, bitmap2);
        }

        private void RegisterDisabledImage(string name, Icon iconSmall, Icon iconLarge)
        {
            Image imageSmall = iconSmall == null ? null : iconSmall.ToBitmap();
            Image imageLarge = iconLarge == null ? null : iconLarge.ToBitmap();
            RegisterImage(name, MadeDisabledBitmap(imageSmall), MadeDisabledBitmap(imageLarge));
        }
    }
}