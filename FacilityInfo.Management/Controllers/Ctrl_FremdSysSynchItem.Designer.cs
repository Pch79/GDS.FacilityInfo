namespace FacilityInfo.Management.Controllers
{
    partial class Ctrl_FremdSysSynchItem
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
            this.components = new System.ComponentModel.Container();
            this.doSynchItems = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // doSynchItems
            // 
            this.doSynchItems.Caption = "Synchronisierung";
            this.doSynchItems.Category = "RecordEdit";
            this.doSynchItems.ConfirmationMessage = null;
            this.doSynchItems.Id = "doSynchitems";
            this.doSynchItems.ImageName = "arrow_refresh";
            this.doSynchItems.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doSynchItems.TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll;
            this.doSynchItems.ToolTip = null;
            this.doSynchItems.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.doSynchItems_Execute);
            // 
            // Ctrl_FremdSysSynchItem
            // 
            this.Actions.Add(this.doSynchItems);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction doSynchItems;
    }
}
