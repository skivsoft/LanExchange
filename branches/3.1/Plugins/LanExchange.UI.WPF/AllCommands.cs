using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LanExchange.UI.WPF
{
    public static class AllCommands
    {
        public static readonly RoutedUICommand cmdKeys = new RoutedUICommand("Shortcut Keys", "cmdKeys", typeof(MainWindow));
        public static readonly RoutedUICommand cmdExit = new RoutedUICommand("Exit", "cmdExit", typeof(MainWindow));

    }
}
