using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LanExchange.View;

namespace LanExchange.UI
{
    public partial class InfoView : UserControl, IInfoView
    {
        public InfoView()
        {
            InitializeComponent();
        }

        public string InfoComp
        {
            get
            {
                return lInfoComp.Text;
            }
            set
            {
                lInfoComp.Text = value;
            }
        }

        public string InfoDesc
        {
            get
            {
                return lInfoDesc.Text;
            }
            set
            {
                lInfoDesc.Text = value;
            }
        }

        public string InfoOS
        {
            get
            {
                return lInfoOS.Text;
            }
            set
            {
                lInfoOS.Text = value;
            }
        }

        public PictureBox Picture
        {
            get
            {
                return imgInfo;
            }
        }
    }
}
