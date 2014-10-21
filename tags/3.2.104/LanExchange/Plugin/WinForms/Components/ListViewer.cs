﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using LanExchange.Properties;

namespace LanExchange.Plugin.WinForms.Components
{
    public class ListViewer : ListView
    {
        private ToolTip m_ToolTip;
        public event EventHandler<ColumnClickEventArgs> ColumnRightClick;

        public ListViewer()
        {
            if (!SystemInformation.TerminalServerSession)
                // switch off flikering only if not terminal session
                SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);    
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_CONTEXTMENU = 0x007B;

            switch (m.Msg)
            {
                case WM_CONTEXTMENU: 
                    if (!HandleContextMenu(ref m))
                        base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// The user wants to see the context menu.
        /// </summary>
        /// <param name="m">The windows m</param>
        /// <returns>A bool indicating if this m has been handled</returns>
        /// <remarks>
        /// We want to ignore context menu requests that are triggered by right clicks on the header
        /// </remarks>
        protected virtual bool HandleContextMenu(ref Message m)
        {
            // Don't try to handle context menu commands at design time.
            if (DesignMode)
                return false;

            // If the context menu command was generated by the keyboard, LParam will be -1.
            // We don't want to process these.
            if (m.LParam == minusOne)
                return false;

            // If the context menu came from somewhere other than the header control,
            // we also don't want to ignore it
            if (m.WParam != HeaderControl.Handle)
                return false;

            int columnIndex = HeaderControl.ColumnIndexUnderCursor;
            return HandleHeaderRightClick(columnIndex);
        }
        readonly IntPtr minusOne = new IntPtr(-1);

        /// <summary>
        /// Gets the header control for the ListView
        /// </summary>
        [Browsable(false)]
        public HeaderControl HeaderControl
        {
            get { return headerControl ?? (headerControl = new HeaderControl(this)); }
        }
        private HeaderControl headerControl;


        /// <summary>
        /// The user has right clicked on the column headers. Do whatever is required
        /// </summary>
        /// <returns>Return true if this event has been handle</returns>
        protected virtual bool HandleHeaderRightClick(int columnIndex)
        {
            if (ColumnRightClick != null)
            {
                ColumnRightClick(this, new ColumnClickEventArgs(columnIndex));
                return true;
            }
            return false;
        }

        [Localizable(false)]
        protected override void OnCreateControl()
        {
            m_ToolTip = new ToolTip(); 
            m_ToolTip.IsBalloon = true;
            m_ToolTip.ToolTipIcon = ToolTipIcon.Info;
            m_ToolTip.SetToolTip(this, Resources.EmptyText);
            m_ToolTip.Popup += ToolTipOnPopup;
            m_ToolTip.Active = View != View.Details;
        }

        private void ToolTipOnPopup(object sender, PopupEventArgs e)
        {
            if (sender == m_ToolTip && e.AssociatedControl == this)
            {
                var point = PointToClient(MousePosition);
                var info = HitTest(point);
                if (info.Item != null)
                    m_ToolTip.ToolTipTitle = info.Item.Text;
            }
        }

        public bool ToolTipActive
        {
            get { return m_ToolTip != null && m_ToolTip.Active; }
            set
            {
                if (m_ToolTip != null)
                    m_ToolTip.Active = value;
            }
        }
    }
}