namespace GDS.FacilityInfo.Module.Win.UserControls
{
    partial class ucAnlage_TreeView
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeMapControl1 = new DevExpress.XtraTreeMap.TreeMapControl();
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.treeMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ucAnlage_TreeViewlayoutControl1ConvertedLayout)).BeginInit();
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeMapControl1
            // 
            this.treeMapControl1.BorderOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(160)))), ((int)(((byte)(170)))));
            this.treeMapControl1.Location = new System.Drawing.Point(12, 12);
            this.treeMapControl1.Name = "treeMapControl1";
            this.treeMapControl1.Size = new System.Drawing.Size(846, 520);
            this.treeMapControl1.TabIndex = 0;
            // 
            // ucAnlage_TreeViewlayoutControl1ConvertedLayout
            // 
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.Controls.Add(this.treeMapControl1);
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.Name = "ucAnlage_TreeViewlayoutControl1ConvertedLayout";
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-3490, -110, 250, 350);
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(870, 544);
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(870, 544);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeMapControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "treeMapControl1item";
            this.layoutControlItem1.Size = new System.Drawing.Size(850, 524);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ucAnlage_TreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucAnlage_TreeViewlayoutControl1ConvertedLayout);
            this.Name = "ucAnlage_TreeView";
            this.Size = new System.Drawing.Size(870, 544);
            this.Load += new System.EventHandler(this.ucAnlage_TreeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ucAnlage_TreeViewlayoutControl1ConvertedLayout)).EndInit();
            this.ucAnlage_TreeViewlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeMap.TreeMapControl treeMapControl1;
        private DevExpress.XtraLayout.LayoutControl ucAnlage_TreeViewlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
