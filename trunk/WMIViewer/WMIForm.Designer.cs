using WMIViewer.Properties;

namespace WMIViewer
{
    partial class WMIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WMIForm));
            this.Status = new System.Windows.Forms.StatusStrip();
            this.lStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lClasses = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lProps = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lMethods = new System.Windows.Forms.ToolStripStatusLabel();
            this.pLeft = new System.Windows.Forms.Panel();
            this.lvInstances = new System.Windows.Forms.ListView();
            this.menuCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TheSplitter = new System.Windows.Forms.Splitter();
            this.PropGrid = new System.Windows.Forms.PropertyGrid();
            this.pTop = new System.Windows.Forms.Panel();
            this.menuClasses = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wMIItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lClassName = new System.Windows.Forms.ToolStripDropDownButton();
            this.lDescription = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Status.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.pTop.SuspendLayout();
            this.menuClasses.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lStatus,
            this.toolStripStatusLabel1,
            this.lClasses,
            this.toolStripStatusLabel3,
            this.lProps,
            this.toolStripStatusLabel5,
            this.lMethods});
            this.Status.Location = new System.Drawing.Point(0, 549);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(792, 22);
            this.Status.TabIndex = 3;
            // 
            // lStatus
            // 
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(708, 17);
            this.lStatus.Spring = true;
            this.lStatus.Text = global::WMIViewer.Properties.Resources.EmptyText;
            this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 17);
            // 
            // lClasses
            // 
            this.lClasses.Name = "lClasses";
            this.lClasses.Size = new System.Drawing.Size(19, 17);
            this.lClasses.Text = global::WMIViewer.Properties.Resources.EmptyText;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(4, 17);
            // 
            // lProps
            // 
            this.lProps.Name = "lProps";
            this.lProps.Size = new System.Drawing.Size(19, 17);
            this.lProps.Text = global::WMIViewer.Properties.Resources.EmptyText;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel5.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(4, 17);
            // 
            // lMethods
            // 
            this.lMethods.Name = "lMethods";
            this.lMethods.Size = new System.Drawing.Size(19, 17);
            this.lMethods.Text = global::WMIViewer.Properties.Resources.EmptyText;
            // 
            // pLeft
            // 
            this.pLeft.Controls.Add(this.lvInstances);
            this.pLeft.Controls.Add(this.TheSplitter);
            this.pLeft.Controls.Add(this.PropGrid);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pLeft.Location = new System.Drawing.Point(0, 105);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(792, 444);
            this.pLeft.TabIndex = 4;
            // 
            // lvInstances
            // 
            this.lvInstances.ContextMenuStrip = this.menuCommands;
            this.lvInstances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvInstances.FullRowSelect = true;
            this.lvInstances.GridLines = true;
            this.lvInstances.HideSelection = false;
            this.lvInstances.Location = new System.Drawing.Point(0, 0);
            this.lvInstances.Name = "lvInstances";
            this.lvInstances.Size = new System.Drawing.Size(486, 444);
            this.lvInstances.TabIndex = 8;
            this.lvInstances.UseCompatibleStateImageBehavior = false;
            this.lvInstances.View = System.Windows.Forms.View.Details;
            this.lvInstances.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvComps_ItemSelectionChanged);
            this.lvInstances.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvInstances_KeyDown);
            // 
            // menuCommands
            // 
            this.menuCommands.Name = "menuCommands";
            this.menuCommands.Size = new System.Drawing.Size(61, 4);
            this.menuCommands.Opening += new System.ComponentModel.CancelEventHandler(this.menuCommands_Opening);
            // 
            // TheSplitter
            // 
            this.TheSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.TheSplitter.Location = new System.Drawing.Point(486, 0);
            this.TheSplitter.Name = "TheSplitter";
            this.TheSplitter.Size = new System.Drawing.Size(6, 444);
            this.TheSplitter.TabIndex = 9;
            this.TheSplitter.TabStop = false;
            // 
            // PropGrid
            // 
            this.PropGrid.ContextMenuStrip = this.menuCommands;
            this.PropGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.PropGrid.Location = new System.Drawing.Point(492, 0);
            this.PropGrid.Name = "PropGrid";
            this.PropGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.PropGrid.Size = new System.Drawing.Size(300, 444);
            this.PropGrid.TabIndex = 5;
            this.PropGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropGrid_PropertyValueChanged);
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.ContextMenuStrip = this.menuClasses;
            this.pTop.Controls.Add(this.lDescription);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 25);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(792, 80);
            this.pTop.TabIndex = 5;
            // 
            // menuClasses
            // 
            this.menuClasses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wMIItemToolStripMenuItem});
            this.menuClasses.Name = "menuClasses";
            this.menuClasses.Size = new System.Drawing.Size(79, 26);
            this.menuClasses.Opening += new System.ComponentModel.CancelEventHandler(this.menuClasses_Opening);
            // 
            // wMIItemToolStripMenuItem
            // 
            this.wMIItemToolStripMenuItem.Name = "wMIItemToolStripMenuItem";
            this.wMIItemToolStripMenuItem.Size = new System.Drawing.Size(78, 22);
            // 
            // lClassName
            // 
            this.lClassName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lClassName.DropDown = this.menuClasses;
            this.lClassName.Image = ((System.Drawing.Image)(resources.GetObject("lClassName.Image")));
            this.lClassName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lClassName.Name = "lClassName";
            this.lClassName.Size = new System.Drawing.Size(32, 22);
            this.lClassName.Text = global::WMIViewer.Properties.Resources.EmptyText;
            // 
            // lDescription
            // 
            this.lDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lDescription.Location = new System.Drawing.Point(12, 10);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(768, 56);
            this.lDescription.TabIndex = 1;
            this.lDescription.Text = Resources.EmptyText;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lClassName});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 6;
            // 
            // WMIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 571);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "WMIForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.WMIForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WMIForm_KeyDown);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.pLeft.ResumeLayout(false);
            this.pTop.ResumeLayout(false);
            this.menuClasses.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel lStatus;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.PropertyGrid PropGrid;
        private System.Windows.Forms.ContextMenuStrip menuCommands;
        private System.Windows.Forms.Splitter TheSplitter;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.ContextMenuStrip menuClasses;
        private System.Windows.Forms.ToolStripMenuItem wMIItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lClasses;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lProps;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lMethods;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton lClassName;
        private System.Windows.Forms.ListView lvInstances;

    }
}