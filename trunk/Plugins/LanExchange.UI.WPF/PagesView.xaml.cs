using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LanExchange.SDK;

namespace LanExchange.UI.WPF
{
    /// <summary>
    /// Логика взаимодействия для PagesView.xaml
    /// </summary>
    public partial class PagesView : UserControl,IPagesView
    {
        private readonly IPagesPresenter m_Presenter;
        

        public PagesView(IPagesPresenter presenter)
        {
            InitializeComponent();
            m_Presenter = presenter;
            m_Presenter.View = this;
            //App.Images.SetImagesTo(popPages);
        }

        public int TabPagesCount
        {
            get { return 0; }
        }

        public int PopupSelectedIndex
        {
            get { return -1; }
        }

        public int SelectedIndex
        {
            get
            {
                return -1;
            }
            set
            {
                
            }
        }

        public IPanelView ActivePanelView
        {
            get { return null; }
        }

        public void RemoveTabAt(int index)
        {
            
        }

        public void SetTabToolTip(int index, string value)
        {
            
        }

        public void FocusPanelView()
        {
            
        }

        public IPanelView CreatePanelView(IPanelModel info)
        {
            var panelView = (PanelView)App.Resolve<IPanelView>();
            panelView.GridLines = App.Config.ShowGridLines;
            var Tab = new TabItem();
            Tab.Header = info.TabName;
            Pages.Items.Add(Tab);
            //var listView = panelView.Controls[0] as ListView;
            //if (listView != null)
            //{
            //    App.Images.SetImagesTo(listView);
            //    listView.View = (View)info.CurrentView;
            //}
            //m_Presenter.SetupPanelViewEvents(panelView);
            // add new tab and insert panel into it
            //var tabPage = CreateTabPageFromModel(info);
            //panelView.Dock = DockStyle.Fill;
            //tabPage.Controls.Add(panelView);
            //Pages.Controls.Add(tabPage);
            return panelView;
        }

        public IPagesPresenter Presenter
        {
            get { return m_Presenter; }
        }

        public void SetupContextMenu()
        {
            
        }

        public void SetTabText(int index, string title)
        {
            
        }
    }
}
