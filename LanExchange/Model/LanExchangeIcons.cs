using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vbAccelerator.Components.ImageList;
using System.Drawing;
using GongSolutions.Shell.Interop;

namespace LanExchange.Model
{
    /// <summary>
    /// Icons collection for LanExchange program.
    /// Lazy load pattern used.
    /// </summary>
    public class LanExchangeIcons
    {
        public const int imgCompDefault   = 0; // MyComputer icon
        public const int imgCompDisabled  = 1; // Disabled MyComputer icon
        public const int imgWorkgroup     = 2; // Workgroup icon
        public const int imgFolderNormal  = 3; // Folder icon
        public const int imgFolderHidden  = 4; // Disabled Folder icon
        public const int imgFolderPrinter = 5;
        public const int imgFolderBack    = 6;
        public const int imgCompGreen = 0;

        private static LanExchangeIcons m_Instance;
        private ImageList m_SmallImageList;
        private ImageList m_LargeImageList;
        private ImageList m_ExtraLargeImageList;

        private LanExchangeIcons()
        {
            // init system images
            Shell32.FileIconInit(true);
            SysImageList Small = new SysImageList(SysImageListSize.smallIcons);
            SysImageList Large = new SysImageList(SysImageListSize.largeIcons);
            SysImageList ExtraLarge = new SysImageList(SysImageListSize.extraLargeIcons);
            // init image lists
            m_SmallImageList = new ImageList();
            m_SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_SmallImageList.ImageSize = Small.Size;
            m_LargeImageList = new ImageList();
            m_LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_LargeImageList.ImageSize = Large.Size;
            m_ExtraLargeImageList = new ImageList();
            m_ExtraLargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_ExtraLargeImageList.ImageSize = ExtraLarge.Size;
            // MyComputer icon
            Icon icon1 = Small.Icon(15);
            Icon icon2 = Large.Icon(15);
            Icon icon3 = ExtraLarge.Icon(15);
            m_SmallImageList.Images.Add(icon1);
            m_LargeImageList.Images.Add(icon2);
            m_ExtraLargeImageList.Images.Add(icon3);
            // Disabled MyComputer icon
            m_SmallImageList.Images.Add(MadeDisabledBitmap(icon1.ToBitmap()));
            m_LargeImageList.Images.Add(MadeDisabledBitmap(icon2.ToBitmap()));
            m_ExtraLargeImageList.Images.Add(MadeDisabledBitmap(icon3.ToBitmap()));
            // Workgroup icon
            m_SmallImageList.Images.Add(Small.Icon(18));
            m_LargeImageList.Images.Add(Large.Icon(18));
            m_ExtraLargeImageList.Images.Add(ExtraLarge.Icon(18));
            // Folder icon
            icon1 = Small.Icon(4);
            icon2 = Large.Icon(4);
            icon3 = ExtraLarge.Icon(4);
            m_SmallImageList.Images.Add(icon1);
            m_LargeImageList.Images.Add(icon2);
            m_ExtraLargeImageList.Images.Add(icon3);
            // Disabled Folder icon
            m_SmallImageList.Images.Add(MadeDisabledBitmap(icon1.ToBitmap()));
            m_LargeImageList.Images.Add(MadeDisabledBitmap(icon2.ToBitmap()));
            m_ExtraLargeImageList.Images.Add(MadeDisabledBitmap(icon3.ToBitmap()));
            // release sys images list
            Small.Dispose();
            Large.Dispose();
            ExtraLarge.Dispose();
        }

        private Bitmap MadeDisabledBitmap(Bitmap bmp)
        {
            Bitmap Result = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics GR = Graphics.FromImage(Result))
            {
                ControlPaint.DrawImageDisabled(GR, bmp, 0, 0, Color.Transparent);
            }
            return Result;
        }

        public static ImageList SmallImageList
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new LanExchangeIcons();
                return m_Instance.m_SmallImageList;
            }
        }

        public static ImageList LargeImageList
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new LanExchangeIcons();
                return m_Instance.m_LargeImageList;
            }
        }

        public static ImageList ExtraLargeImageList
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new LanExchangeIcons();
                return m_Instance.m_ExtraLargeImageList;
            }
        }

    }
}
