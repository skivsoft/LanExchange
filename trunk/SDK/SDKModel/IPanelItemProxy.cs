using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK.SDKModel.VO;

namespace LanExchange.SDK.SDKModel
{
    public interface IPanelItemProxy
    {

        IList<PanelItemVO> Objects { get; }
        int NumObjects { get; }

        void EnumObjects(string path);
        void Sort();
        void ChangeSort(int ColIndex);

        ColumnVO[] GetColumns();
    }
}
