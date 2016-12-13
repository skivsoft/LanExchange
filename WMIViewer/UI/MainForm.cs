using System;
using System.ComponentModel;
using System.Globalization;
using System.Management;
using System.Reflection;
using System.Windows.Forms;
using WMIViewer.Model;
using WMIViewer.Presenter;
using WMIViewer.Properties;

namespace WMIViewer.UI
{
    internal sealed partial class MainForm : Form
    {
        public const string TITLE_FMT = @"\\{0} — {1}";

        private readonly WmiPresenter presenter;
        private readonly CmdLineArgs args;
        private string currentWmiClass;
        private bool menuUpdated;

        [Localizable(false)]
        public MainForm(WmiPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException(nameof(presenter));
            this.presenter = presenter;
            this.presenter.View = this;
            args = this.presenter.Args;
            InitializeComponent();

            // Enable double buffer for ListView
            var mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(lvInstances, new object[] { ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true });

            FocusedItemChanged += ListViewInstances_FocusedItemChanged;
            UpdateTitle();
            ShowStat(WmiClassList.Instance.ClassCount, WmiClassList.Instance.PropCount, WmiClassList.Instance.MethodCount);
            Icon = Resources.WMIViewer16;
        }

        public event EventHandler FocusedItemChanged;

        public WmiPresenter Presenter
        {
            get { return presenter; }
        }

        public ListView LV
        {
            get { return lvInstances; }
        }

        public string CurrentWmiClass
        {
            get
            {
                return currentWmiClass;
            }

            set
            {
                currentWmiClass = value;
                lDescription.Text = WmiClassList.Instance.GetClassDescription(presenter.Namespace, value);
                lClassName.Text = value;
                presenter.EnumObjects(value);
                presenter.BuildContextMenu(menuCommands.Items);

                // m_Presenter.BuildContextMenu(mMethod.DropDownItems);
                if (lvInstances.Items.Count == 0)
                    PropGrid.SelectedObject = null;
                else
                {
                    lvInstances.FocusedItem = lvInstances.Items[0];
                    lvInstances.FocusedItem.Selected = true;
                    ListViewInstances_FocusedItemChanged(lvInstances, EventArgs.Empty);
                    lvInstances.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }

                lStatus.Text = string.Format(CultureInfo.InvariantCulture, Resources.MainForm_Items, lvInstances.Items.Count);
            }
        }

        public void UpdateTitle()
        {
            var description = WmiClassList.GetPropertyValue(
                presenter.Namespace,
                "Win32_OperatingSystem",
                "Description");
            if (string.IsNullOrEmpty(description))
                Text = @"\\" + args.ComputerName;
            else
                Text = string.Format(CultureInfo.InvariantCulture, TITLE_FMT, args.ComputerName, description);
        }

        public void ShowStat(int classCount, int propCount, int methodCount)
        {
            lClasses.Text = string.Format(CultureInfo.InvariantCulture, Resources.MainForm_Classes, classCount);
            lProps.Text = string.Format(CultureInfo.InvariantCulture, Resources.MainForm_Properties, propCount);
            lMethods.Text = string.Format(CultureInfo.InvariantCulture, Resources.MainForm_Methods, methodCount);
        }

        public void MenuClassesOnClick(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
                CurrentWmiClass = menuItem.Text;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public void UpdateClassesMenu()
        {
            menuClasses.Items.Clear();
            int count1 = WmiClassList.Instance.Classes.Count;
            int count2 = WmiClassList.Instance.ReadOnlyClasses.Count;
            foreach (var str in WmiClassList.Instance.Classes)
            {
                var menuItem = new ToolStripMenuItem { Text = str };

               menuItem.Click += MenuClassesOnClick;
                menuClasses.Items.Add(menuItem);
            }

            if (count1 > 0 && count2 > 0)
                menuClasses.Items.Add(new ToolStripSeparator());
            foreach (var str in WmiClassList.Instance.ReadOnlyClasses)
            {
                var menuItem = new ToolStripMenuItem { Text = str };

               menuItem.Click += MenuClassesOnClick;
                menuClasses.Items.Add(menuItem);
            }

            // TODO uncomment setup wmi-classes
            // if (Count1 + Count2 > 0)
            // menuClasses.Items.Add(new ToolStripSeparator());

            // ToolStripMenuItem mSetup = new ToolStripMenuItem { Text = "Настроить..."};

           // mSetup.Click += mSetup_Click;
            // menuClasses.Items.Add(mSetup);
        }

        [Localizable(false)]
        private void ListViewInstances_FocusedItemChanged(object sender, EventArgs e)
        {
            if (presenter.WmiClass == null) return;
            if (lvInstances.FocusedItem == null) return;
            presenter.WmiObject = (ManagementObject)lvInstances.FocusedItem.Tag;
            if (presenter.WmiObject == null) return;
            PropGrid.SelectedObject = presenter.CreateDynamicObject();
        }

        [Localizable(false)]
        private void WMIForm_Load(object sender, EventArgs e)
        {
            CurrentWmiClass = "Win32_OperatingSystem";
            ActiveControl = lvInstances;
        }

        private void MenuClasses_Opening(object sender, CancelEventArgs e)
        {
            if (!WmiClassList.Instance.Loaded)
            {
                e.Cancel = true;
                return;
            }

            if (!menuUpdated)
            {
                UpdateClassesMenu();
                menuUpdated = true;
            }

            foreach (var menuItem in menuClasses.Items)
            {
                var mi = menuItem as ToolStripMenuItem;
                if (mi != null)
                    mi.Checked = mi.Text.Equals(CurrentWmiClass);
            }
        }

        [Localizable(false)]
        private void PropGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string propName = e.ChangedItem.Label;
            if (propName == null) return;
            object propValue = e.ChangedItem.Value;
            string caption = string.Format(CultureInfo.InvariantCulture, Resources.MainForm_EditingProperty, propName);
            string message = string.Format(CultureInfo.InvariantCulture, Resources.MainForm_PropertyChanged_Message, args.ComputerName, e.OldValue, propValue);
            try
            {
                // trying to change wmi property
                presenter.WmiObject[propName] = propValue;
                presenter.WmiObject.Put();

                // update computer comment if we changes Win32_OperatingSystme.Description
                if (CurrentWmiClass.Equals("Win32_OperatingSystem") && propName.Equals("Description"))
                    UpdateTitle();

                // property has been changed
                message += string.Format(CultureInfo.InvariantCulture, Resources.MainForm_PropertyChanged_Success, propName);
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (ManagementException ex)
            {
                // property not changed
                var dynObj = PropGrid.SelectedObject as DynamicObject;
                if (dynObj != null)
                    dynObj[propName] = e.OldValue;
                message += "\n\n" + ex.Message;
                if (ex.InnerException != null)
                    message += "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void DoFocusedItemChanged()
        {
            if (FocusedItemChanged != null)
                FocusedItemChanged(this, EventArgs.Empty);
        }

        private void LVCompsOnItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                DoFocusedItemChanged();
        }

        private void MenuCommandsOnOpening(object sender, CancelEventArgs e)
        {
            if (LV.FocusedItem != null)
                if (LV.FocusedItem.Selected)
                    DoFocusedItemChanged();
        }

        private void LVInstances_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }

        private void WMIForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Esc, F10 - quit program
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.F10)
            {
                Close();
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F2)
            {
                lClassName.ShowDropDown();
                e.Handled = true;
            }

            if (e.Control && e.KeyCode == Keys.R)
            {
                CurrentWmiClass = CurrentWmiClass;
                e.Handled = true;
            }

            // Ctrl+Left
            if (e.Control && e.KeyCode == Keys.Left)
            {
                PropGrid.Dock = DockStyle.Left;
                TheSplitter.Dock = DockStyle.Left;
                e.Handled = true;
            }

            // Ctrl+Right
            if (e.Control && e.KeyCode == Keys.Right)
            {
                PropGrid.Dock = DockStyle.Right;
                TheSplitter.Dock = DockStyle.Right;
                e.Handled = true;
            }
        }
    }

}
