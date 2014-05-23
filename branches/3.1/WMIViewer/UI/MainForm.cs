using System;
using System.Globalization;
using System.Windows.Forms;
using System.Management;
using System.ComponentModel;
using System.Reflection;
using WMIViewer.Model;
using WMIViewer.Presenter;
using WMIViewer.Properties;

namespace WMIViewer.UI
{
    internal sealed partial class MainForm : Form
    {
        public const string TITLE_FMT = @"\\{0} — {1}";

        private readonly WmiPresenter m_Presenter;
        private readonly CmdLineArgs m_Args;
        private string m_CurrentWmiClass;

        public event EventHandler FocusedItemChanged;

        [Localizable(false)]
        public MainForm(WmiPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException("presenter");
            m_Presenter = presenter;
            m_Presenter.View = this;
            m_Args = m_Presenter.Args;
            InitializeComponent();
            // Enable double buffer for ListView
            var mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(lvInstances, new object[] { ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true });

            FocusedItemChanged += lvInstances_FocusedItemChanged;
            UpdateTitle();
            ShowStat(WmiClassList.Instance.ClassCount, WmiClassList.Instance.PropCount, WmiClassList.Instance.MethodCount);
            Icon = Resources.WMIViewer16;
        }

        public void UpdateTitle()
        {
            var description = WmiClassList.GetPropertyValue(m_Presenter.Namespace, "Win32_OperatingSystem",
                "Description");
            if (string.IsNullOrEmpty(description))
                Text = @"\\" + m_Args.ComputerName;
            else
                Text = string.Format(CultureInfo.InvariantCulture, TITLE_FMT, m_Args.ComputerName, description);
        }

        public WmiPresenter Presenter
        {
            get { return m_Presenter; }
        }

        [Localizable(false)]
        private void lvInstances_FocusedItemChanged(object sender, EventArgs e)
        {
            if (m_Presenter.WmiClass == null) return;
            if (lvInstances.FocusedItem == null) return;
            m_Presenter.WmiObject = (ManagementObject)lvInstances.FocusedItem.Tag;
            if (m_Presenter.WmiObject == null) return;
            PropGrid.SelectedObject = m_Presenter.CreateDynamicObject();
        }

        public ListView LV
        {
            get { return lvInstances; }
        }

        public string CurrentWmiClass
        {
            get
            {
                return m_CurrentWmiClass;
            }
            set
            {
                m_CurrentWmiClass = value;
                lDescription.Text = WmiClassList.Instance.GetClassDescription(m_Presenter.Namespace, value);
                lClassName.Text = value;
                m_Presenter.EnumObjects(value);
                m_Presenter.BuildContextMenu(menuCommands.Items);
                //m_Presenter.BuildContextMenu(mMethod.DropDownItems);
                if (lvInstances.Items.Count == 0)
                    PropGrid.SelectedObject = null;
                else
                {
                    lvInstances.FocusedItem = lvInstances.Items[0];
                    lvInstances.FocusedItem.Selected = true;
                    lvInstances_FocusedItemChanged(lvInstances, EventArgs.Empty);
                    lvInstances.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                lStatus.Text = String.Format(CultureInfo.InvariantCulture, Resources.MainForm_Items, lvInstances.Items.Count);
            }
        }

        [Localizable(false)]
        private void WMIForm_Load(object sender, EventArgs e)
        {
            CurrentWmiClass = "Win32_OperatingSystem";
            ActiveControl = lvInstances;
        }

        public void ShowStat(int classCount, int propCount, int methodCount)
        {
            lClasses.Text = String.Format(CultureInfo.InvariantCulture, Resources.MainForm_Classes, classCount);
            lProps.Text = String.Format(CultureInfo.InvariantCulture, Resources.MainForm_Properties, propCount);
            lMethods.Text = String.Format(CultureInfo.InvariantCulture, Resources.MainForm_Methods, methodCount);
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
            foreach(var str in WmiClassList.Instance.Classes)
            {
                var menuItem = new ToolStripMenuItem {Text = str};
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
            //if (Count1 + Count2 > 0)
            //    menuClasses.Items.Add(new ToolStripSeparator());
            //ToolStripMenuItem mSetup = new ToolStripMenuItem { Text = "Настроить..."};
            //mSetup.Click += mSetup_Click;
            //menuClasses.Items.Add(mSetup);
        }

        private bool m_MenuUpdated;

        private void menuClasses_Opening(object sender, CancelEventArgs e)
        {
            if (!WmiClassList.Instance.Loaded)
            {
                e.Cancel = true;
                return;
            }
            if (!m_MenuUpdated)
            {
                UpdateClassesMenu();
                m_MenuUpdated = true;
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
            string caption = String.Format(CultureInfo.InvariantCulture, Resources.MainForm_EditingProperty, propName);
            string message = String.Format(CultureInfo.InvariantCulture, Resources.MainForm_PropertyChanged_Message, m_Args.ComputerName, e.OldValue, propValue);
            try
            {
                // trying to change wmi property
                m_Presenter.WmiObject[propName] = propValue;
                m_Presenter.WmiObject.Put();

                // update computer comment if we changes Win32_OperatingSystme.Description
                if (CurrentWmiClass.Equals("Win32_OperatingSystem") && propName.Equals("Description"))
                    UpdateTitle();

                // property has been changed
                message += String.Format(CultureInfo.InvariantCulture, Resources.MainForm_PropertyChanged_Success, propName);
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
            catch(ManagementException ex)
            {
                // property not changed
                var dynObj = PropGrid.SelectedObject as DynamicObject;
                if (dynObj != null)
                    dynObj[propName] = e.OldValue;
                message += "\n\n" + ex.Message;
                if (ex.InnerException != null)
                    message += "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
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

        private void lvInstances_KeyDown(object sender, KeyEventArgs e)
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
