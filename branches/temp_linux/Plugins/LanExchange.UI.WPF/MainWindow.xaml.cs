using System;
using System.Windows;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WPF
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView

    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ApplicationExit()
        {
            throw new NotImplementedException();
        }

        public void ShowStatusText(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void SetToolTip(object control, string tipText)
        {
            throw new NotImplementedException();
        }

        public bool ShowInfoPanel
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int NumInfoLines
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void ClearInfoPanel()
        {
            throw new NotImplementedException();
        }

        public void Invoke(Delegate method, object sender)
        {
            throw new NotImplementedException();
        }
    }
}
