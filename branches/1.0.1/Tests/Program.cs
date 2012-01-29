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
            //TestTabs.CreateFromEmpyTabControl();
            {
                TestTSettings obj = new TestTSettings();
                obj.SetListValue();
            }

        }
    }


}