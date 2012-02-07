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
            TestTTabController TestTabs = new TestTTabController();
            //TestTabs.CreateFromNotEmptyTabControl2();
            {
                TestWinFormsUI T = new TestWinFormsUI();
                T.CreateTLanEXControl();
            }

        }
    }


}