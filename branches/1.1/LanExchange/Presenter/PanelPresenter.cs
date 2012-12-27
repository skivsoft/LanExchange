using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.View;
using System.Windows.Forms;
using LanExchange.Model;

namespace LanExchange.Presenter
{
    public class PanelPresenter
    {
        private readonly IPanelView m_View;

        public PanelPresenter(IPanelView view)
        {
            m_View = view;

        }
        public void CopyCompNameCommand()
        {
            StringBuilder S = new StringBuilder();
            PanelItem PItem;
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = m_View.GetItem(index);
                if (PItem != null)
                    S.Append(@"\\" + PItem.Name);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void CopyCommentCommand()
        {
            StringBuilder S = new StringBuilder();
            PanelItem PItem;
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = m_View.GetItem(index);
                if (PItem != null)
                    S.Append(PItem.Comment);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void CopySelectedCommand()
        {
            StringBuilder S = new StringBuilder();
            PanelItem PItem;
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = m_View.GetItem(index);
                if (PItem != null)
                {
                    S.Append(@"\\" + PItem.Name);
                    S.Append("\t");
                    S.Append(PItem.Comment);
                }
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void CopyPathCommand()
        {
            StringBuilder S = new StringBuilder();
            SharePanelItem PItem;
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = m_View.GetItem(index) as SharePanelItem;
                if (PItem != null)
                    S.Append(String.Format(@"\\{0}\{1}", PItem.ComputerName, PItem.Name));
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }
    }
}
