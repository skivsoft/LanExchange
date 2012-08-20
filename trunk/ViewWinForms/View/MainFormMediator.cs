using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using System.Windows.Forms;

namespace ViewWinForms.View
{
    public class MainFormMediator : Mediator, IMediator
    {
        public new const string NAME = "MainFormMediator";

        public MainFormMediator(Form form) : base(NAME, form)
        {

        }
    }
}
