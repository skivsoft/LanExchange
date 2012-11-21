using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LanExchange
{
    public class TNetworkBrowser
    {
        public ListView LV = null;
        private LVType CurrentType = LVType.COMPUTERS;
        private string path = null;

        // внутренний список элементов панели, сравнивается с новым списком при обновлении
        public IList<TPanelItem> InternalItems = null;
        // стек списков элементов для навигации
        public Stack<IList<TPanelItem>> InternalStack = new Stack<IList<TPanelItem>>();
        // внутренний список элементов с возможностью фильтрации для вывода через RetrieveVirtualItem
        public TPanelItemList InternalItemList = new TPanelItemList();

        public enum LVType
        {
            COMPUTERS,
            SHARES,
            FILES
        }

        public TNetworkBrowser(ListView lv)
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
        public IList<TPanelItem> GetTopItemList()
        {
            if (InternalStack.Count == 0)
                return InternalItems;
            else
            {
                IList<TPanelItem>[] Arr = InternalStack.ToArray();
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
                    MainForm.MainFormInstance.CancelCompRelatedThreads();
                    // сбрасываем фильтр
                    MainForm.MainFormInstance.UpdateFilter(MainForm.MainFormInstance.GetActiveListView(), "", false);
                    // текущий список добавляем в стек
                    //if (InternalItems == null)
                    //    InternalItems = InternalItemList.ToList();
                    InternalStack.Push(InternalItems);
                    // получаем новый список объектов, в данном случае список ресурсов компа
                    InternalItems = TPanelItemList.EnumNetShares(FocusedText);
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
                    MainForm.MainFormInstance.mCompOpen_Click(MainForm.MainFormInstance.mFolderOpen, new EventArgs());
                    //Path += @"\" + FocusedText;
                    //LV.VirtualListSize = 1;
                    //ViewType = LVType.FILES;
                    break;
                case LVType.FILES:

                    break;
            }
            /*
            ShellContextMenu scm = new ShellContextMenu();
            FileInfo[] files = new FileInfo[1];
            files[0] = new FileInfo(@"\\MIKHAILYUK-KA\.\Exchange");
            //files[0].DirectoryName = @"\\MIKHAILYUK-KA\Exchange";
            scm.ShowContextMenu(this.Handle, files, Cursor.Position);
             */
        }

        public void LevelUp()
        {
            if (InternalStack.Count == 0)
                return;

            TPanelItem PItem = null;
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
            InternalItemList.ListView_SelectComputer(MainForm.MainFormInstance.lvComps, CompName);

            MainForm.MainFormInstance.UpdateFilter(MainForm.MainFormInstance.GetActiveListView(), MainForm.MainFormInstance.eFilter.Text, true);
            //MainForm.MainFormInstance.UpdateFilterPanel();
        }

        // Отображаемая таблица
        public IList<TPanelItem> CurrentDataTable
        {
            get
            {
                return InternalItems;
            }
            set
            {
                List<string> SaveSelected = null;
                if (!MainForm.MainFormInstance.bFirstStart)
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
                    foreach (TPanelItem Comp in InternalItems)
                        InternalItemList.Add(Comp);
                    if (!MainForm.MainFormInstance.bFirstStart)
                    {
                        InternalItemList.ApplyFilter();
                        MainForm.MainFormInstance.TotalItems = InternalItemList.Count;
                        // восстанавливаем выделение компов
                        InternalItemList.ListView_SetSelected(LV, SaveSelected);
                    }
                }
                finally
                {
                    LV.EndUpdate();
                }
            }
        }
    }
}
