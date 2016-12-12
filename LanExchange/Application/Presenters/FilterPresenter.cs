using System;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Presenters
{
    internal sealed class FilterPresenter : PresenterBase<IFilterView>, IFilterPresenter
    {
        private IFilterModel model;
        private string filter;

        public string FilterText
        {
            get { return filter; }
            set
            {
                filter = value;
                if (model != null)
                {
                    model.FilterText = value;
                    model.ApplyFilter();
                    //TODO hide model
                    //View.UpdateFromModel(model);
                    View.DoFilterCountChanged();
                }
                View.IsVisible = IsFiltered;
            }
        }

        public bool IsFiltered
        {
            get
            {
                return !string.IsNullOrEmpty(filter);
            }
        }

        public IFilterModel GetModel()
        {
            return model;
        }

        public void SetModel(IFilterModel value)
        {
            model = null;
            if (value != null)
            {
                View.SetFilterText(value.FilterText);
                model = value;
                model.ApplyFilter();
                //TODO hide model
                //View.UpdateFromModel(model);
                View.DoFilterCountChanged();
            }
        }

        protected override void InitializePresenter()
        {
        }
    }
}