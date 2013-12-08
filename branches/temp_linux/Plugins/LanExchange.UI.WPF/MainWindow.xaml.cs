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
        }

        public void ShowStatusText(string format, params object[] args)
        {
        }

        public void SetToolTip(object control, string tipText)
        {
        }

        public bool ShowInfoPanel
        {
            get { return false; }
            set
            {
            }
        }

        public int NumInfoLines
        {
            get { return 0; }
            set
            {
            }
        }

        public void ClearInfoPanel()
        {
        }

        public void Invoke(Delegate method, object sender)
        {
        }

        public string TrayText
        {
            get { return string.Empty; }
            set
            {
            }
        }

        public bool TrayVisible
        {
            get { return false; }
            set
            {
            }
        }

        public void SetRunMinimized(bool minimized)
        {
        }

        public void SetupMenuLanguages()
        {
        }

        public void SetBounds(int left, int top, int width, int height)
        {
        }

        public void SetupPages()
        {
        }


        public string Text
        {
            get { return string.Empty; }
            set
            {
            }
        }
    }
}
