namespace FacilityInfo.Management.Controllers
{
    partial class Ctrl_ActionAnlage_DetailView
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
            this.doSetActionDone = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // doSetActionDone
            // 
            this.doSetActionDone.Caption = "Erledigt";
            this.doSetActionDone.Category = "RecordEdit";
            this.doSetActionDone.ConfirmationMessage = null;
            this.doSetActionDone.Id = "doSetActionDone";
            this.doSetActionDone.ImageName = "accept";
            this.doSetActionDone.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doSetActionDone.ToolTip = null;
            this.doSetActionDone.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.doSetActionDone_Execute);
            // 
            // Ctrl_ActionAnlage_DetailView
            // 
            this.Actions.Add(this.doSetActionDone);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction doSetActionDone;
    }
}
