using System.ComponentModel;
using LanExchange.Utils;

namespace LanExchange.Misc.Addon
{
    /// <summary>
    /// This class for initial generation of default addons only.
    /// Must not be used in production.
    /// </summary>
#if DEBUG
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
            var root = new Misc.Addon.Addon();
            var computer = new AddonItemTypeRef();
            var share = new AddonItemTypeRef();
            AddonMenuItem menuItem;
            // programs
            root.Programs.Add(new AddonProgram("explorer", @"%SystemRoot%\explorer.exe"));
            root.Programs.Add(new AddonProgram("mmc", @"%SystemRoot%\system32\mmc.exe"));
            root.Programs.Add(new AddonProgram("mstsc", @"%SystemRoot%\system32\mstsc.exe"));
            // open in explorer (on computer)
            menuItem = new AddonMenuItem();
            menuItem.Text = "Open in Explorer";
            menuItem.ShortcutKeys = "Shift+Enter";
            menuItem.ProgramRef = new AddonObjectId("explorer");
            menuItem.ProgramArgs = @"\\{0}";
            computer.ContextMenuStrip.Add(menuItem);
            // open in explorer (on share)
            menuItem = new AddonMenuItem();
            menuItem.Text = "Open in Explorer";
            menuItem.ShortcutKeys = "Shift+Enter";
            menuItem.ProgramRef = new AddonObjectId("explorer");
            menuItem.ProgramArgs = @"{0}";
            share.ContextMenuStrip.Add(menuItem);
            // computer management
            menuItem = new AddonMenuItem();
            menuItem.Text = "Computer management";
            menuItem.ShortcutKeys = "Ctrl+F1";
            menuItem.ProgramRef = new AddonObjectId("mmc");
            menuItem.ProgramArgs = @"%SystemRoot%\system32\compmgmt.msc /computer:{0}";
            computer.ContextMenuStrip.Add(menuItem);
            // connect to remote desktop
            menuItem = new AddonMenuItem();
            menuItem.Text = "Connect to remote desktop";
            menuItem.ProgramRef = new AddonObjectId("mstsc");
            menuItem.ProgramArgs = "/v:{0}";
            computer.ContextMenuStrip.Add(menuItem);
            // computer
            computer.Id = "ComputerPanelItem";
            root.PanelItemTypes.Add(computer);
            // share
            share.Id = "SharePanelItem";
            root.PanelItemTypes.Add(share);
            // store xml file
            var fileName = FolderManager.Instance.GetAddonFileName(true, "Default");
            SerializeUtils.SerializeObjectToXMLFile(fileName, root);
        }

        public static void GenerateRadmin()
        {
            var root = new Misc.Addon.Addon();
            var computer = new AddonItemTypeRef();
            AddRadminItems(computer);
            // computer
            computer.Id = "ComputerPanelItem";
            root.PanelItemTypes.Add(computer);
            var program = new AddonProgram();
            program.Id = "Radmin";
            program.FileName = @"%ProgramFiles(x86)%\Radmin Viewer 3\Radmin.exe";
            root.Programs.Add(program);
            // store xml file
            var fileName = FolderManager.Instance.GetAddonFileName(true, "RadminViewer");
            SerializeUtils.SerializeObjectToXMLFile(fileName, root);
        }

        private static void AddRadminItems(AddonItemTypeRef computer)
        {
            AddonMenuItem menuItem;
            // menu item 1
            menuItem = new AddonMenuItem();
            menuItem.Text = "Radmin® Full Control";
            menuItem.ShortcutKeys = "Ctrl+Enter";
            menuItem.ProgramRef = new AddonObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0}";
            computer.ContextMenuStrip.Add(menuItem);
            // menu item 2
            menuItem = new AddonMenuItem();
            menuItem.Text = "Radmin® View";
            menuItem.ProgramRef = new AddonObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0} /noinput";
            computer.ContextMenuStrip.Add(menuItem);
            // menu item 3
            menuItem = new AddonMenuItem();
            menuItem.Text = "Radmin® Telnet";
            menuItem.ProgramRef = new AddonObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0} /telnet";
            computer.ContextMenuStrip.Add(menuItem);
            // menu item 4
            menuItem = new AddonMenuItem();
            menuItem.Text = "Radmin® File transfer";
            menuItem.ProgramRef = new AddonObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0} /file";
            computer.ContextMenuStrip.Add(menuItem);
            // menu item 5
            menuItem = new AddonMenuItem();
            menuItem.Text = "Radmin® Shutdown";
            menuItem.ProgramRef = new AddonObjectId("Radmin");
            menuItem.ProgramArgs = "/connect:{0} /shutdown";
            computer.ContextMenuStrip.Add(menuItem);
        }
    }
#endif
}
