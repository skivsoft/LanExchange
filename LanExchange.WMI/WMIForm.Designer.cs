namespace LanExchange.WMI
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
            this.Status = new System.Windows.Forms.StatusStrip();
            this.lStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pLeft = new System.Windows.Forms.Panel();
            this.lvInstances = new System.Windows.Forms.ListView();
            this.menuCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.PropGrid = new System.Windows.Forms.PropertyGrid();
            this.pTop = new System.Windows.Forms.Panel();
            this.menuClasses = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lDescription = new System.Windows.Forms.Label();
            this.lClassName = new System.Windows.Forms.Label();
            this.wMIItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Status.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.pTop.SuspendLayout();
            this.menuClasses.SuspendLayout();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lStatus});
            this.Status.Location = new System.Drawing.Point(0, 551);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(792, 22);
            this.Status.TabIndex = 3;
            this.Status.Text = "statusStrip1";
            // 
            // lStatus
            // 
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(777, 17);
            this.lStatus.Spring = true;
            this.lStatus.Text = "    ";
            this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pLeft
            // 
            this.pLeft.Controls.Add(this.lvInstances);
            this.pLeft.Controls.Add(this.splitter1);
            this.pLeft.Controls.Add(this.PropGrid);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pLeft.Location = new System.Drawing.Point(0, 100);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(792, 451);
            this.pLeft.TabIndex = 4;
            // 
            // lvInstances
            // 
            this.lvInstances.ContextMenuStrip = this.menuCommands;
            this.lvInstances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvInstances.FullRowSelect = true;
            this.lvInstances.GridLines = true;
            this.lvInstances.HideSelection = false;
            this.lvInstances.Location = new System.Drawing.Point(306, 0);
            this.lvInstances.Name = "lvInstances";
            this.lvInstances.Size = new System.Drawing.Size(486, 451);
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
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(300, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(6, 451);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // PropGrid
            // 
            this.PropGrid.ContextMenuStrip = this.menuCommands;
            this.PropGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.PropGrid.Location = new System.Drawing.Point(0, 0);
            this.PropGrid.Name = "PropGrid";
            this.PropGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.PropGrid.Size = new System.Drawing.Size(300, 451);
            this.PropGrid.TabIndex = 5;
            this.PropGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropGrid_PropertyValueChanged);
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.White;
            this.pTop.ContextMenuStrip = this.menuClasses;
            this.pTop.Controls.Add(this.lDescription);
            this.pTop.Controls.Add(this.lClassName);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(792, 100);
            this.pTop.TabIndex = 5;
            // 
            // menuClasses
            // 
            this.menuClasses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wMIItemToolStripMenuItem});
            this.menuClasses.Name = "menuClasses";
            this.menuClasses.Size = new System.Drawing.Size(124, 26);
            this.menuClasses.Opening += new System.ComponentModel.CancelEventHandler(this.menuClasses_Opening);
            // 
            // lDescription
            // 
            this.lDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lDescription.Location = new System.Drawing.Point(12, 37);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(768, 49);
            this.lDescription.TabIndex = 1;
            this.lDescription.Text = "    ";
            // 
            // lClassName
            // 
            this.lClassName.AutoSize = true;
            this.lClassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClassName.Location = new System.Drawing.Point(12, 12);
            this.lClassName.Name = "lClassName";
            this.lClassName.Size = new System.Drawing.Size(23, 13);
            this.lClassName.TabIndex = 0;
            this.lClassName.Text = "    ";
            // 
            // wMIItemToolStripMenuItem
            // 
            this.wMIItemToolStripMenuItem.Name = "wMIItemToolStripMenuItem";
            this.wMIItemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wMIItemToolStripMenuItem.Text = "WMIItem";
            // 
            // WMIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.pTop);
            this.MinimizeBox = false;
            this.Name = "WMIForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.WMIForm_Load);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.pLeft.ResumeLayout(false);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.menuClasses.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel lStatus;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.PropertyGrid PropGrid;
        private System.Windows.Forms.ContextMenuStrip menuCommands;
        private System.Windows.Forms.ListView lvInstances;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.Label lClassName;
        private System.Windows.Forms.ContextMenuStrip menuClasses;
        private System.Windows.Forms.ToolStripMenuItem wMIItemToolStripMenuItem;

    }
}