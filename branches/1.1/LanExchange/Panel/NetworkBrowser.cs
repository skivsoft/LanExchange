using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LanExchange.Forms;

namespace LanExchange
{
    public class NetworkBrowser
    {
        public ListView LV = null;
        private LVType CurrentType = LVType.COMPUTERS;
        private string path = null;

        // внутренний список элементов панели, сравнивается с новым списком при обновлении
        public IList<PanelItem> InternalItems = null;
        // стек списков элементов для навигации
        public Stack<IList<PanelItem>> InternalStack = new Stack<IList<PanelItem>>();
        // внутренний список элементов с возможностью фильтрации для вывода через RetrieveVirtualItem
        //public PanelItemList InternalItemList = new PanelItemList();

        public enum LVType
        {
            COMPUTERS,
            SHARES,
            FILES
        }

        public NetworkBrowser(ListView lv)
        {
            LV = lv;
            RebuildColumns();
        }

        public LVType ViewType
        {
            get { return CurrentType; }
            set
            {
                if (CurrentType != value)
                {
                    CurrentType = value;
                    RebuildColumns();
                }
            }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private void RebuildColumns()
        {
            if (LV == null) return;
            LV.Columns.Clear();
            switch (CurrentType)
            {
                case LVType.COMPUTERS:
                    LV.Columns.Add("Сетевое имя", 130);
                    LV.Columns.Add("Описание", 250);
                    
                    break;
                case LVType.SHARES:
                    LV.Columns.Add("Общий ресурс", 130);
                    LV.Columns.Add("*:", 20);
                    LV.Columns.Add("Описание", 250);
                    break;
                case LVType.FILES:
                    LV.Columns.Add("Имя", 100);
                    LV.Columns.Add("Дата изменения", 100);
                    LV.Columns.Add("Тип", 100);
                    LV.Columns.Add("Размер", 100);
                    break;
            }

        }

        /// <summary>
        /// Возвращает список элементов с верхнего уровня из стека переходов.
        /// В частности это будет список копьютеров, даже если мы находимся на уровне списка ресуров.
        /// </summary>
        /// <returns></returns>
        public IList<PanelItem> GetTopItemList()
        {
            if (InternalStack.Count == 0)
                return InternalItems;
            else
            {
                IList<PanelItem>[] Arr = InternalStack.ToArray();
                return Arr[0];
            }
        }

        public void LevelDown()
        {
            if (LV == null || LV.FocusedItem == null)
                return;
            string FocusedText = LV.FocusedItem.Text;
            if (String.IsNullOrEmpty(FocusedText))
            {
                LevelUp();
                return;
            }

            switch (ViewType)
            {
                case LVType.COMPUTERS:
                    if (LV.FocusedItem == null)
                        break;
                    // останавливаем поток пингов
                    MainForm.GetInstance().CancelCompRelatedThreads();
                    // сбрасываем фильтр
                    MainForm.GetInstance().UpdateFilter(MainForm.GetInstance().GetActiveListView(), "", false);
                    // текущий список добавляем в стек
                    //if (InternalItems == null)
                    //    InternalItems = InternalItemList.ToList();
                    InternalStack.Push(InternalItems);
                    // получаем новый список объектов, в данном случае список ресурсов компа
                    InternalItems = PanelItemList.EnumNetShares(FocusedText);
                    // устанавливаем новый список для визуального компонента
                    CurrentDataTable = InternalItems;
                    if (LV.VirtualListSize > 0)
                    {
                        LV.FocusedItem = LV.Items[0];
                        LV.SelectedIndices.Add(0);
                    }
                    // меняем колонки в ListView
                    Path = @"\\" + FocusedText;
                    ViewType = LVType.SHARES;
                    break;
                case LVType.SHARES:
                    MainForm.GetInstance().mFolderOpen_Click(MainForm.GetInstance().mFolderOpen, new EventArgs());
                    break;
                case LVType.FILES:
                    break;
            }
        }

        public void LevelUp()
        {
            /*
            if (InternalStack.Count == 0)
                return;

            //TPanelItem PItem = null;
            string CompName = null;
            if (InternalItemList.Count > 0)
            {
                CompName = Path;
                if (CompName.Length > 2 && CompName[0] == '\\' && CompName[1] == '\\')
                    CompName = CompName.Remove(0, 2);
            }

            InternalItems = InternalStack.Pop();

            
            switch (CurrentType)
            {
                case LVType.COMPUTERS:
                    break;
                case LVType.SHARES:
                    ViewType = LVType.COMPUTERS;
                    break;
                case LVType.FILES:
                    ViewType = LVType.SHARES;
                    break;
            }
            CurrentDataTable = InternalItems;
            InternalItemList.ListView_SelectComputer(MainForm.GetInstance().lvComps, CompName);

            MainForm.GetInstance().UpdateFilter(MainForm.GetInstance().GetActiveListView(), MainForm.GetInstance().eFilter.Text, true);
             */
        }

        // Отображаемая таблица
        public IList<PanelItem> CurrentDataTable
        {
            get
            {
                return InternalItems;
            }
            set
            {
                /*
                List<string> SaveSelected = null;
                if (!MainForm.GetInstance().bFirstStart)
                {
                    // запоминаем выделенные элементы
                    SaveSelected = InternalItemList.ListView_GetSelected(LV, false);
                }
                // установка нового списка компов
                InternalItems = value;
                // обновление внутреннего списка для отображения в ListView
                LV.BeginUpdate();
                try
                {
                    InternalItemList.Clear();
                    foreach (PanelItem Comp in InternalItems)
                        InternalItemList.Add(Comp);
                    if (!MainForm.GetInstance().bFirstStart)
                    {
                        InternalItemList.ApplyFilter();
                        MainForm.GetInstance().TotalItems = InternalItemList.Count;
                        // восстанавливаем выделение компов
                        InternalItemList.ListView_SetSelected(LV, SaveSelected);
                    }
                }
                finally
                {
                    LV.EndUpdate();
                }
                 */
            }
        }
    }
}
