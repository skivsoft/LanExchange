using System;
using System.Windows;
using LanExchange.SDK;
using System.Windows.Input;

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
            Visibility = Visibility.Collapsed;
            Width = width;
            Height = height;
            Left = left;
            Top = top;
            Visibility = Visibility.Visible;
        }

        public void SetupPages()
        {
        /*     // init Pages presenter
            Pages = (PagesView)App.Resolve<IPagesView>();
            Pages.Dock = DockStyle.Fill;
            Controls.Add(Pages);
            Pages.BringToFront();
            // setup images
            App.Images.SetImagesTo(Pages.Pages);
            App.Images.SetImagesTo(Status);
            // load saved pages from config
            Pages.SetupContextMenu();*/
            var Pages = (PagesView)App.Resolve<IPagesView>();
            PageContainer.Child=Pages;
        }


        public string Text
        {
            get { return Title; }
            set
            {
                Title = value;
            }
        }


        private void KeysCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            App.Presenter.ExecuteAction("ActionShortcutKeys");
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

    }
}
