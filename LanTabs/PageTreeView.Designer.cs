namespace LanTabs
{
    partial class PageTreeView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("DOMAIN1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("COMP1");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("COMP2");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("DOMAIN2", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("DOMAIN3");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("DOMAIN4");
            this.TV = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // TV
            // 
            this.TV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV.FullRowSelect = true;
            this.TV.Location = new System.Drawing.Point(0, 0);
            this.TV.Name = "TV";
            treeNode1.Name = "Node0";
            treeNode1.Text = "DOMAIN1";
            treeNode2.Name = "Node2";
            treeNode2.Text = "COMP1";
            treeNode3.Name = "Node3";
            treeNode3.Text = "COMP2";
            treeNode4.Name = "Node1";
            treeNode4.Text = "DOMAIN2";
            treeNode5.Name = "Node0";
            treeNode5.Text = "DOMAIN3";
            treeNode6.Name = "Node1";
            treeNode6.Text = "DOMAIN4";
            this.TV.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4,
            treeNode5,
            treeNode6});
            this.TV.Size = new System.Drawing.Size(420, 437);
            this.TV.TabIndex = 0;
            // 
            // PageTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TV);
            this.Name = "PageTreeView";
            this.Size = new System.Drawing.Size(420, 437);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TV;
    }
}
