namespace FacilityInfo.Management.Controllers
{
    partial class Ctrl_Liegenschaften_ListView
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
            this.doRefreshData = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.doPrintListe = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // doRefreshData
            // 
            this.doRefreshData.Caption = "do Refresh Data";
            this.doRefreshData.ConfirmationMessage = null;
            this.doRefreshData.Id = "doRefreshData";
            this.doRefreshData.ToolTip = null;
            // 
            // doPrintListe
            // 
            this.doPrintListe.Caption = "Liegenschaftsliste";
            this.doPrintListe.Category = "RecordsNavigation";
            this.doPrintListe.ConfirmationMessage = null;
            this.doPrintListe.Id = "doPrintListe";
            this.doPrintListe.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doPrintListe.Tag = "Liegenschaftsliste";
            this.doPrintListe.ToolTip = null;
            this.doPrintListe.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.doPrintListe_Execute);
            // 
            // Ctrl_Liegenschaften_ListView
            // 
            this.Actions.Add(this.doRefreshData);
            this.Actions.Add(this.doPrintListe);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction doRefreshData;
        private DevExpress.ExpressApp.Actions.SimpleAction doPrintListe;
    }
}
