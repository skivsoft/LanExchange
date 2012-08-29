using System;
using System.Collections.Generic;
using System.Text;
using BrightIdeasSoftware;
using System.Windows.Forms;
using System.Collections;
using LanExchange.Model.VO;

namespace LanExchange.View.Components
{
    /// <summary>
    /// Provide a data source for a FastObjectListView
    /// </summary>
    /// <remarks>
    /// This class isn't intended to be used directly, but it is left as a public
    /// class just in case someone wants to subclass it.
    /// </remarks>
    public class FastPanelItemDataSource : AbstractVirtualListDataSource
    {
        /// <summary>
        /// Create a FastObjectListDataSource
        /// </summary>
        /// <param name="listView"></param>
        public FastPanelItemDataSource(FastObjectListView listView)
            : base(listView)
        {
        }

        internal ArrayList ObjectList
        {
            get { return fullObjectList; }
        }

        internal ArrayList FilteredObjectList
        {
            get { return filteredObjectList; }
        }

        #region IVirtualListDataSource Members

        /// <summary>
        /// Get n'th object
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public override object GetNthObject(int n)
        {
            if (n >= 0 && n < this.filteredObjectList.Count)
                return this.filteredObjectList[n];
            else
                return null;
        }

        /// <summary>
        /// How many items are in the data source
        /// </summary>
        /// <returns></returns>
        public override int GetObjectCount()
        {
            return this.filteredObjectList.Count;
        }

        /// <summary>
        /// Get the index of the given model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int GetObjectIndex(object model)
        {
            int index;

            if (model != null && this.objectsToIndexMap.TryGetValue(model, out index))
                return index;
            else
                return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public override int SearchText(string value, int first, int last, OLVColumn column)
        {
            return DefaultSearchText(value, first, last, column, this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="sortOrder"></param>
        public override void Sort(OLVColumn column, SortOrder sortOrder)
        {
            if (sortOrder != SortOrder.None)
            {
                PanelItemComparer comparer = new PanelItemComparer(column, sortOrder, this.listView.SecondarySortColumn, this.listView.SecondarySortOrder);
                this.fullObjectList.Sort(comparer);
                this.filteredObjectList.Sort(comparer);
            }
            this.RebuildIndexMap();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelObjects"></param>
        public override void AddObjects(ICollection modelObjects)
        {
            foreach (object modelObject in modelObjects)
            {
                if (modelObject != null)
                    this.fullObjectList.Add(modelObject);
            }
            this.FilterObjects();
            this.RebuildIndexMap();
        }

        /// <summary>
        /// Remove the given collection of models from this source.
        /// </summary>
        /// <param name="modelObjects"></param>
        public override void RemoveObjects(ICollection modelObjects)
        {
            List<int> indicesToRemove = new List<int>();
            foreach (object modelObject in modelObjects)
            {
                int i = this.GetObjectIndex(modelObject);
                if (i >= 0)
                    indicesToRemove.Add(i);

                // Remove the objects from the unfiltered list
                this.fullObjectList.Remove(modelObject);
            }

            // Sort the indices from highest to lowest so that we
            // remove latter ones before earlier ones. In this way, the
            // indices of the rows doesn't change after the deletes.
            indicesToRemove.Sort();
            indicesToRemove.Reverse();

            foreach (int i in indicesToRemove)
                this.listView.SelectedIndices.Remove(i);

            this.FilterObjects();
            this.RebuildIndexMap();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public override void SetObjects(IEnumerable collection)
        {
            ArrayList newObjects = ObjectListView.EnumerableToArray(collection, true);

            this.fullObjectList = newObjects;
            this.FilterObjects();
            this.RebuildIndexMap();
        }

        private ArrayList fullObjectList = new ArrayList();
        private ArrayList filteredObjectList = new ArrayList();
        private IModelFilter modelFilter;
        private IListFilter listFilter;

        #endregion

        #region IFilterableDataSource Members

        /// <summary>
        /// Apply the given filters to this data source. One or both may be null.
        /// </summary>
        /// <param name="iModelFilter"></param>
        /// <param name="iListFilter"></param>
        public override void ApplyFilters(IModelFilter iModelFilter, IListFilter iListFilter)
        {
            this.modelFilter = iModelFilter;
            this.listFilter = iListFilter;
            this.SetObjects(this.fullObjectList);
        }

        #endregion


        #region Implementation

        /// <summary>
        /// Rebuild the map that remembers which model object is displayed at which line
        /// </summary>
        protected void RebuildIndexMap()
        {
            this.objectsToIndexMap.Clear();
            for (int i = 0; i < this.filteredObjectList.Count; i++)
                this.objectsToIndexMap[this.filteredObjectList[i]] = i;
        }
        readonly Dictionary<Object, int> objectsToIndexMap = new Dictionary<Object, int>();

        /// <summary>
        /// Build our filtered list from our full list.
        /// </summary>
        protected void FilterObjects()
        {
            if (!this.listView.UseFiltering || (this.modelFilter == null && this.listFilter == null))
            {
                this.filteredObjectList = new ArrayList(this.fullObjectList);
                return;
            }

            IEnumerable objects = (this.listFilter == null) ?
                this.fullObjectList : this.listFilter.Filter(this.fullObjectList);

            // Apply the object filter if there is one
            if (this.modelFilter == null)
            {
                this.filteredObjectList = ObjectListView.EnumerableToArray(objects, false);
            }
            else
            {
                this.filteredObjectList = new ArrayList();
                foreach (object model in objects)
                {
                    if (this.modelFilter.Filter(model))
                        this.filteredObjectList.Add(model);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// This comparer can be used to sort a collection of model objects by a given column
    /// </summary>
    public class PanelItemComparer : IComparer, IComparer<object>
    {
        /// <summary>
        /// Create a model object comparer
        /// </summary>
        /// <param name="col"></param>
        /// <param name="order"></param>
        public PanelItemComparer(OLVColumn col, SortOrder order)
        {
            this.column = col;
            this.sortOrder = order;
        }

        /// <summary>
        /// Create a model object comparer with a secondary sorting column
        /// </summary>
        /// <param name="col"></param>
        /// <param name="order"></param>
        /// <param name="col2"></param>
        /// <param name="order2"></param>
        public PanelItemComparer(OLVColumn col, SortOrder order, OLVColumn col2, SortOrder order2)
            : this(col, order)
        {
            // There is no point in secondary sorting on the same column
            if (col != col2 && col2 != null && order2 != SortOrder.None)
                this.secondComparer = new PanelItemComparer(col2, order2);
        }

        /// <summary>
        /// Compare the two model objects
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            if (this.sortOrder == SortOrder.None)
                return 0;

            int result = 0;
            object x1 = this.column.GetValue(x);
            object y1 = this.column.GetValue(y);
            PanelItemVO Item1 = x as PanelItemVO;
            PanelItemVO Item2 = y as PanelItemVO;
            bool bAnyIsBack = Item1.IsBackButton || Item2.IsBackButton;

            // Handle nulls. Null values come last
            bool xIsNull = (x1 == null || x1 == System.DBNull.Value);
            bool yIsNull = (y1 == null || y1 == System.DBNull.Value);
            if (xIsNull || yIsNull)
            {
                if (xIsNull && yIsNull)
                    result = 0;
                else
                    result = (xIsNull ? -1 : 1);
            }
            else
            {
                if (Item1.IsBackButton)
                    if (Item2.IsBackButton)
                        result = 0;
                    else
                        result = -1;
                else
                    if (Item2.IsBackButton)
                        result = +1;
                    else
                        result = this.CompareValues(x1, y1);
            }

            if (this.sortOrder == SortOrder.Descending && !bAnyIsBack)
                result = 0 - result;

            // If the result was equality, use the secondary comparer to resolve it
            if (result == 0 && this.secondComparer != null)
                result = this.secondComparer.Compare(x, y);

            return result;
        }

        /// <summary>
        /// Compare the actual values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int CompareValues(object x, object y)
        {
            // Force case insensitive compares on strings
            String xStr = x as String;
            if (xStr != null)
                return String.Compare(xStr, (String)y, StringComparison.CurrentCultureIgnoreCase);
            else
            {
                IComparable comparable = x as IComparable;
                if (comparable != null)
                    return comparable.CompareTo(y);
                else
                    return 0;
            }
        }

        private OLVColumn column;
        private SortOrder sortOrder;
        private PanelItemComparer secondComparer;
    }
}
