using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;

namespace Tests
{
    internal class FilterModelMock : IFilterModel
    {

        #region IFilterModel Members

        public int FilterCount { get; set; }
        public string FilterText { get; set; }

        public void ApplyFilter()
        {
        }

        #endregion
    }
}
