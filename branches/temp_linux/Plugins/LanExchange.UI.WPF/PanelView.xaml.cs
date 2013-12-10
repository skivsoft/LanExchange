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
using LanExchange.SDK.UI;
using LanExchange.SDK;
using LanExchange.SDK.Presenter;

namespace LanExchange.UI.WPF
{
    /// <summary>
    /// Логика взаимодействия для PanelView.xaml
    /// </summary>
    public partial class PanelView : UserControl,IPanelView
    {

        private readonly IPanelPresenter m_Presenter;

        public PanelView(IPanelPresenter presenter)
        {
            InitializeComponent();
            // init presenters
            m_Presenter = presenter;
            m_Presenter.View = this;
        }

        public event EventHandler FocusedItemChanged;

        public event EventHandler FilterTextChanged;

        public IFilterView Filter
        {
            get { return null; }
        }

        public IEnumerable<int> SelectedIndexes
        {
            get { return null; }
        }

        public string FocusedItemText
        {
            get { return ""; }
        }

        public int FocusedItemIndex
        {
            get
            {
                return -1;
            }
            set
            {
                
            }
        }

        public SDK.Presenter.IPanelPresenter Presenter
        {
            get { return m_Presenter; }
        }

        public void SelectItem(int index)
        {
            
        }

        public void SetVirtualListSize(int count)
        {
            
        }

        public void RedrawFocusedItem()
        {
            
        }

        public void FocusListView()
        {
            
        }

        public void ClearSelected()
        {
            
        }

        public void ShowRunCmdError(string cmdLine)
        {
            
        }

        public void ColumnsClear()
        {
            
        }

        public void AddColumn(SDK.PanelColumnHeader header)
        {
            
        }

        public SDK.PanelViewMode ViewMode
        {
            get
            {
                return PanelViewMode.Details;
            }
            set
            {
                
            }
        }

        public SDK.PanelItemBase FocusedItem
        {
            get { return null; }
        }

        public bool GridLines
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public void RedrawItem(int index)
        {
            
        }

        public void SetColumnMarker(int columnIndex, SDK.PanelSortOrder sortOrder)
        {
            
        }

        public void ShowHeaderMenu(IList<SDK.PanelColumnHeader> columns)
        {
            
        }
    }
}
