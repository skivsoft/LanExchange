using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LanExchange.Utils
{
    /// <summary>
    /// Helper Methods for Connecting SysImageList to Common Controls
    /// </summary>
    [Localizable(false)]
    public static class SysImageListHelper
    {
        #region UnmanagedMethods
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SETIMAGELIST = (LVM_FIRST + 3);

        private const int LVSIL_NORMAL = 0;
        private const int LVSIL_SMALL = 1;
        private const int LVSIL_STATE = 2;

        private const int TV_FIRST = 0x1100;
        private const int TVM_SETIMAGELIST = (TV_FIRST + 9);
		
        private const int TVSIL_NORMAL = 0;
        private const int TVSIL_STATE = 2;

        [DllImport(ExternDll.Shell32, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(
            IntPtr hWnd, 
            int wMsg, 
            IntPtr wParam, 
            IntPtr lParam);
        #endregion

        /// <summary>
        /// Associates a SysImageList with a ListView control
        /// </summary>
        /// <param name="listView">ListView control to associate ImageList with</param>
        /// <param name="sysImageList">System Image List to associate</param>
        /// <param name="forStateImages">Whether to add ImageList as StateImageList</param>
        public static void SetListViewImageList(
            ListView listView,
            SysImageList sysImageList,
            bool forStateImages
            )
        {
            IntPtr wParam;
            if (sysImageList.ImageListSize == SysImageListSize.SmallIcons)
                wParam = (IntPtr)LVSIL_SMALL;
            else
                wParam = (IntPtr)LVSIL_NORMAL;
            if (forStateImages)
            {
                wParam = (IntPtr)LVSIL_STATE;
            }
            SendMessage(
                listView.Handle,
                LVM_SETIMAGELIST,
                wParam,
                sysImageList.Handle);	
        }

        /// <summary>
        /// Associates a SysImageList with a TreeView control
        /// </summary>
        /// <param name="treeView">TreeView control to associated ImageList with</param>
        /// <param name="sysImageList">System Image List to associate</param>
        /// <param name="forStateImages">Whether to add ImageList as StateImageList</param>
        public static void SetTreeViewImageList(
            TreeView treeView,
            SysImageList sysImageList,
            bool forStateImages
            )
        {
            IntPtr wParam;
            if (forStateImages)
                wParam = (IntPtr)TVSIL_STATE;
            else
                wParam = (IntPtr)TVSIL_NORMAL;
            SendMessage(
                treeView.Handle,
                TVM_SETIMAGELIST,
                wParam,
                sysImageList.Handle);
        }		
    }
}