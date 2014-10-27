using System;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using LanExchange.Forms;

namespace LanExchange
{
    static class Program
    {
        private static string s_LibPath;

        [STAThread]
        static void Main()
        {
            SubscribeAssemblyResolver();
            SubMain();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void SubMain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void SubscribeAssemblyResolver()
        {
            s_LibPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (s_LibPath != null)
                s_LibPath = Path.Combine(s_LibPath, "Lib");
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        [Localizable(false)]
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = args.Name.Substring(0, args.Name.IndexOf(','));
            if (name.EndsWith(".resources")) return null;
            if (name.EndsWith(".XmlSerializers")) return null;
            var filePath = Path.Combine(s_LibPath, name + ".dll");
            return Assembly.LoadFrom(filePath);
        }
    }
}
