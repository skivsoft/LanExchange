using System;
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

        private const int SYSTEM_INDEX_MYCOMPUTER = 15;
        private const int SYSTEM_INDEX_WORKGROUP  = 18;
        private const int SYSTEM_INDEX_FOLDER     = 4;

        private static LanExchangeIcons m_Instance;
        private readonly ImageList m_SmallImageList;
        private readonly ImageList m_LargeImageList;

        private LanExchangeIcons()
        {
            // init system images
            Shell32.FileIconInit(true);
            SysImageList Small = new SysImageList(SysImageListSize.smallIcons);
            SysImageList Large = new SysImageList(SysImageListSize.largeIcons);
            // init image lists
            m_SmallImageList = new ImageList();
            m_SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_SmallImageList.ImageSize = Small.Size;
            m_LargeImageList = new ImageList();
            m_LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            m_LargeImageList.ImageSize = Large.Size;
            // MyComputer icon
            Icon icon1 = Small.Icon(SYSTEM_INDEX_MYCOMPUTER);
            Icon icon2 = Large.Icon(SYSTEM_INDEX_MYCOMPUTER);
            m_SmallImageList.Images.Add(icon1);
            m_LargeImageList.Images.Add(icon2);
            // Disabled MyComputer icon
            m_SmallImageList.Images.Add(MadeDisabledBitmap(icon1.ToBitmap()));
            m_LargeImageList.Images.Add(MadeDisabledBitmap(icon2.ToBitmap()));
            // Workgroup icon
            m_SmallImageList.Images.Add(Small.Icon(SYSTEM_INDEX_WORKGROUP));
            m_LargeImageList.Images.Add(Large.Icon(SYSTEM_INDEX_WORKGROUP));
            // Folder icon
            icon1 = Small.Icon(SYSTEM_INDEX_FOLDER);
            icon2 = Large.Icon(SYSTEM_INDEX_FOLDER);
            m_SmallImageList.Images.Add(icon1);
            m_LargeImageList.Images.Add(icon2);
            // Disabled Folder icon
            m_SmallImageList.Images.Add(MadeDisabledBitmap(icon1.ToBitmap()));
            m_LargeImageList.Images.Add(MadeDisabledBitmap(icon2.ToBitmap()));
            // release sys images list
            Small.Dispose();
            Large.Dispose();
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
    }
}
