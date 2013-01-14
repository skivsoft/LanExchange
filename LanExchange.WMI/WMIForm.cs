using System;
using System.Windows.Forms;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace LanExchange.WMI
{
    public partial class WMIForm : Form, IWMIView
    {
        private readonly WMIPresenter m_Presenter;
        private string m_CurrentWMIClass;
        private readonly List<string> m_Classes;
        private readonly IWMIComputer m_Comp;
        private ManagementObject m_WMIObject;

        public WMIForm(IWMIComputer comp)
        {
            InitializeComponent();
            // Enable double buffer for ListView
            var mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(lvInstances, new object[] { ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true });

            m_Presenter = new WMIPresenter(comp, this);
            m_Classes = new List<string>();
            if (comp != null)
            {
                Text = String.Format("{0} — {1}", comp.Name, comp.Comment);
                m_Comp = comp;
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public ListView LV
        {
            get { return lvInstances; }
        }

        public ContextMenuStrip MENU
        {
            get { return menuCommands; }
        }

        public string CurrentWMIClass
        {
            get
            {
                return m_CurrentWMIClass;
            }
            set
            {
                m_CurrentWMIClass = value;
                lDescription.Text = m_Presenter.GetClassDescription(value);
                lClassName.Text = value;
                m_Presenter.EnumObjects(value);
                if (lvInstances.Items.Count == 0)
                    PropGrid.SelectedObject = null;
                else
                {
                    lvInstances.FocusedItem = lvInstances.Items[0];
                    lvInstances.FocusedItem.Selected = true;
                    lvInstances_ItemActivate(lvInstances, new EventArgs());
                }
            }
        }

        private void WMIForm_Load(object sender, EventArgs e)
        {
            if (m_Presenter.EnumDynamicClasses())
            {
                lvInstances.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                UpdateClassesMenu();
                CurrentWMIClass = "Win32_OperatingSystem";
            }
            else
                Close();
        }

        public void ShowStat(int classCount, int propCount, int methodCount)
        {
            Status.Items[0].Text = String.Format("Классов: {0}, Свойств: {1}, Методов: {2}", classCount, propCount, methodCount);
        }

        public static void dynObj_AddProperty<T>(DynamicObject dynObj, PropertyData prop, string description, string category, bool isReadOnly)
        {
            if (prop.Value == null)
                dynObj.AddPropertyNull<T>(prop.Name, description, category, isReadOnly);
            else
                if (prop.IsArray)
                    dynObj.AddProperty(prop.Name, (T[])prop.Value, description, category, isReadOnly);
                else
                    dynObj.AddProperty(prop.Name, (T)prop.Value, description, category, isReadOnly);
        }

        private void lvInstances_ItemActivate(object sender, EventArgs e)
        {
            if (m_Presenter.WMIClass == null) return;
            if (lvInstances.FocusedItem == null) return;
            m_WMIObject = (ManagementObject)lvInstances.FocusedItem.Tag;
            if (m_WMIObject == null) return;

            var dynObj = new DynamicObject();
            foreach (PropertyData Prop in m_WMIObject.Properties)
            {
                // skip array of bytes
                if (Prop.Type == CimType.UInt8 && Prop.IsArray)
                    continue;
                
                PropertyData ClassProp = m_Presenter.WMIClass.Properties[Prop.Name];

                bool isCimKey = false;
                bool IsReadOnly = true;
                string Description = "";

                foreach (QualifierData qd in ClassProp.Qualifiers)
                {
                    if (qd.Name.Equals("CIM_Key"))
                        isCimKey = true;
                    if (qd.Name.Equals("write"))
                        IsReadOnly = false;
                    if (qd.Name.Equals("Description"))
                        Description = qd.Value.ToString();
                }
                if (isCimKey) continue;
                string Category = Prop.Type.ToString();
                switch (Prop.Type)
                {

                    //     A signed 16-bit integer. This value maps to the System.Int16 type.
                    case CimType.SInt16:
                        dynObj_AddProperty<Int16>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A signed 32-bit integer. This value maps to the System.Int32 type.
                    case CimType.SInt32:
                        dynObj_AddProperty<Int32>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A floating-point 32-bit number. This value maps to the System.Single type.
                    case CimType.Real32:
                        dynObj_AddProperty<Single>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A floating point 64-bit number. This value maps to the System.Double type.
                    case CimType.Real64:
                        dynObj_AddProperty<Double>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A string. This value maps to the System.String type.
                    case CimType.String:
                        dynObj_AddProperty<String>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A Boolean. This value maps to the System.Boolean type.
                    case CimType.Boolean:
                        dynObj_AddProperty<Boolean>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     An embedded object. Note that embedded objects differ from references in
                    //     that the embedded object does not have a path and its lifetime is identical
                    //     to the lifetime of the containing object. This value maps to the System.Object
                    //     type.
                    case CimType.Object:
                        dynObj_AddProperty<Object>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A signed 8-bit integer. This value maps to the System.SByte type.
                    case CimType.SInt8:
                        dynObj_AddProperty<SByte>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     An unsigned 8-bit integer. This value maps to the System.Byte type.
                    case CimType.UInt8:
                        dynObj_AddProperty<Byte>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     An unsigned 16-bit integer. This value maps to the System.UInt16 type.
                    case CimType.UInt16:
                        dynObj_AddProperty<UInt16>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     An unsigned 32-bit integer. This value maps to the System.UInt32 type.
                    case CimType.UInt32:
                        dynObj_AddProperty<UInt32>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A signed 64-bit integer. This value maps to the System.Int64 type.
                    case CimType.SInt64:
                        dynObj_AddProperty<Int64>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     An unsigned 64-bit integer. This value maps to the System.UInt64 type.
                    case CimType.UInt64:
                        dynObj_AddProperty<UInt64>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A date or time value, represented in a string in DMTF date/time format: yyyymmddHHMMSS.mmmmmmsUUU,
                    //     where yyyymmdd is the date in year/month/day; HHMMSS is the time in hours/minutes/seconds;
                    //     mmmmmm is the number of microseconds in 6 digits; and sUUU is a sign (+ or
                    //     -) and a 3-digit UTC offset. This value maps to the System.DateTime type.
                    case CimType.DateTime:
                        if (Prop.Value == null)
                            dynObj.AddPropertyNull<DateTime>(Prop.Name, Description, Category, IsReadOnly);
                        else
                            dynObj.AddProperty(Prop.Name, WMIUtils.ToDateTime(Prop.Value.ToString()), Description, Category, IsReadOnly);
                        break;
                    //     A reference to another object. This is represented by a string containing
                    //     the path to the referenced object. This value maps to the System.Int16 type.
                    case CimType.Reference:
                        dynObj_AddProperty<Int16>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    //     A 16-bit character. This value maps to the System.Char type.
                    case CimType.Char16:
                        dynObj_AddProperty<Char>(dynObj, Prop, Description, Category, IsReadOnly);
                        break;
                    default:
                        string Value = Prop.Value == null ? null : Prop.Value.ToString();
                        dynObj.AddProperty(String.Format("{0} : {1}", Prop.Name, Prop.Type), Value, Description, "Unknown", IsReadOnly);
                        break;
                }
            }
            PropGrid.SelectedObject = dynObj;
        }


        public void ClearClasses()
        {
            m_Classes.Clear();
        }

        public void AddClass(string className)
        {
            m_Classes.Add(className);
        }

        public void menuClasses_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
                CurrentWMIClass = menuItem.Text;
        }

        public void UpdateClassesMenu()
        {
            //m_Classes.Sort();
            //menuClasses.Items.Clear();
            //m_Classes.ForEach(str =>
            //{
            //    ToolStripMenuItem MI = new ToolStripMenuItem { Text = str };
            //    MI.Click += menuClasses_Click;
            //    menuClasses.Items.Add(MI);
            //});
            m_Classes.Sort();
            menuClasses.Items.Clear();
            foreach(string str in m_Classes)
                using (var MI = new ToolStripMenuItem {Text = str})
                {
                    MI.Click += menuClasses_Click;
                    menuClasses.Items.Add(MI);
                }
        }

        private void menuClasses_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripMenuItem MI in menuClasses.Items)
                MI.Checked = MI.Text.Equals(CurrentWMIClass);
        }

        private void PropGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string PropName = e.ChangedItem.Label;
            if (PropName == null) return;
            object PropValue = e.ChangedItem.Value;
            string Caption = String.Format("Изменение свойства {0}", PropName);
            string Message = String.Format("Компьютер: \\\\{0}\n\nСтарое значение: «{1}»\nНовое значение: «{2}»",
                m_Comp.Name, e.OldValue, PropValue);
            try
            {
                // trying to change wmi property
                m_WMIObject[PropName] = PropValue;
                m_WMIObject.Put();

                // update computer comment if we changes Win32_OperatingSystme.Description
                if (CurrentWMIClass.Equals("Win32_OperatingSystem") && PropName.Equals("Description"))
                    m_Comp.Comment = PropValue.ToString();

                // property has been changed
                Message += String.Format("\n\nСвойство {0} успешно изменено.", PropName);
                MessageBox.Show(Message, Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                // property not changed
                var dynObj = PropGrid.SelectedObject as DynamicObject;
                if (dynObj != null)
                    dynObj[PropName] = e.OldValue;
                Message += "\n\n" + ex.Message;
                if (ex.InnerException != null)
                    Message += "\n\n" + ex.InnerException.Message;
                MessageBox.Show(Message, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
