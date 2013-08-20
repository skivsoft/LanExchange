using System;
using System.ComponentModel;
using System.IO;
using LanExchange.Utils;

namespace LanExchange.Model.Addon
{
    [Localizable(false)]
    class AddonGen
    {

        public static void Generate()
        {
            GenerateDefault();
            GenerateRadmin();
        }

        public static void GenerateDefault()
        {
            var root = new LanExchangeAddon();
            var computer = new PanelItemBaseRef();
            var share = new PanelItemBaseRef();
            ToolStripMenuItem menuItem;
            // programs
            root.Programs.Add(new Program("explorer", @"%systemroot%\explorer.exe"));
            root.Programs.Add(new Program("mmc", @"%systemroot%\system32\mmc.exe"));
            root.Programs.Add(new Program("mstsc", @"%systemroot%\system32\mstsc.exe"));
            // open in explorer (on computer)
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Open in Explorer";
            menuItem.ShortcutKeys = "Shift+Enter";
            menuItem.ProgramRef = new ObjectId("explorer");
            menuItem.ProgramArgs = @"\\{0}";
            computer.ContextMenuStrip.Add(menuItem);
            // open in explorer (on share)
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Open in Explorer";
            menuItem.ShortcutKeys = "Shift+Enter";
            menuItem.ProgramRef = new ObjectId("explorer");
            menuItem.ProgramArgs = @"{0}";
            share.ContextMenuStrip.Add(menuItem);
            // computer management
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Computer management";
            menuItem.ShortcutKeys = "Ctrl+F1";
            menuItem.ProgramRef = new ObjectId("mmc");
            menuItem.ProgramArgs = @"%systemroot%\system32\compmgmt.msc /computer:{0}";
            computer.ContextMenuStrip.Add(menuItem);
            // connect to remote desktop
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Connect to remote desktop";
            menuItem.ProgramRef = new ObjectId("mstsc");
            menuItem.ProgramArgs = "/v:{0}";
            computer.ContextMenuStrip.Add(menuItem);
            // computer
            computer.Id = "ComputerPanelItem";
            root.PanelItems.Add(computer);
            // share
            share.Id = "SharePanelItem";
            root.PanelItems.Add(share);
            // store xml file
            var fileName = FolderManager.Instance.GetAddonFileName(true, "Default");
            SerializeUtils.SerializeObjectToXMLFile(fileName, root);
        }

        public static void GenerateRadmin()
        {
            var root = new LanExchangeAddon();
            var computer = new PanelItemBaseRef();
            AddRadminItems(computer);
            // computer
            computer.Id = "ComputerPanelItem";
            root.PanelItems.Add(computer);
            var program = new Program();
            program.Id = "Radmin";
            program.FileName = @"%ProgramFiles(x86)%\Radmin Viewer 3\Radmin.exe";
            root.Programs.Add(program);
            // store xml file
            var fileName = FolderManager.Instance.GetAddonFileName(true, "RadminViewer");
            SerializeUtils.SerializeObjectToXMLFile(fileName, root);
        }

        private static void AddRadminItems(PanelItemBaseRef computer)
        {
            ToolStripMenuItem menuItem;
            // menu item 1
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Radmin® Full Control";
            menuItem.ShortcutKeys = "Ctrl+Enter";
            menuItem.ProgramRef = new ObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0}";
            computer.ContextMenuStrip.Add(menuItem);
            // menu item 2
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Radmin® View";
            menuItem.ProgramRef = new ObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0} /noinput";
            computer.ContextMenuStrip.Add(menuItem);
            // menu item 3
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Radmin® Telnet";
            menuItem.ProgramRef = new ObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0} /telnet";
            computer.ContextMenuStrip.Add(menuItem);
            // menu item 4
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Radmin® File transfer";
            menuItem.ProgramRef = new ObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0} /file";
            computer.ContextMenuStrip.Add(menuItem);
            // menu item 5
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Radmin® Shutdown";
            menuItem.ProgramRef = new ObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0} /shutdown";
            computer.ContextMenuStrip.Add(menuItem);
        }
    }
}
