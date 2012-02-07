using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{

    class Program
    {

        static void Main()
        {
            {
                TestTTabController T = new TestTTabController();
            }
            {
                TestWinFormsUI T = new TestWinFormsUI();
                T.TLanEXItemList();
                T.TLanEXListView();
                T.TLanEXListViewItem();
                T.TLanEXMenuItem();
                T.TLanEXStatusStrip();
            }

        }
    }


}