using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LanExchange.SDK;

namespace LanExchange.UI.WPF
{
    /// <summary>
    /// Логика взаимодействия для PanelView.xaml
    /// </summary>
    public partial class PanelView : UserControl, IPanelView
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

        public IPanelPresenter Presenter
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

        public void AddColumn(PanelColumnHeader header)
        {
            
        }

        public PanelViewMode ViewMode
        {
            get
            {
                return PanelViewMode.Details;
            }
            set
            {
                
            }
        }

        public PanelItemBase FocusedItem
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

        public IntPtr Handle
        {
            get { return IntPtr.Zero; }
        }
    }
}
