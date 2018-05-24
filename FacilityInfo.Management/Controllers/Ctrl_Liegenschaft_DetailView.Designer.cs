namespace FacilityInfo.Management.Controllers
{
    partial class Ctrl_Liegenschaft_DetailView
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
            this.doAddHaustechnikKomponente = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.doAddAnlage = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.doDeleteHtKomponente = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // doAddHaustechnikKomponente
            // 
            this.doAddHaustechnikKomponente.Caption = "Anlagensystem hinzufügen";
            this.doAddHaustechnikKomponente.Category = "RecordEdit";
            this.doAddHaustechnikKomponente.ConfirmationMessage = null;
            this.doAddHaustechnikKomponente.Id = "doAddHaustechnikKomponente";
            this.doAddHaustechnikKomponente.ImageName = "control_panel";
            this.doAddHaustechnikKomponente.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.doAddHaustechnikKomponente.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doAddHaustechnikKomponente.QuickAccess = true;
            this.doAddHaustechnikKomponente.ShowItemsOnClick = true;
            this.doAddHaustechnikKomponente.ToolTip = null;
            this.doAddHaustechnikKomponente.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.doAddHaustechnikKomponente_Execute);
            // 
            // doAddAnlage
            // 
            this.doAddAnlage.Caption = "Anlage hinzufügen";
            this.doAddAnlage.Category = "RecordEdit";
            this.doAddAnlage.ConfirmationMessage = null;
            this.doAddAnlage.Id = "doAddAnlage";
            this.doAddAnlage.ImageName = "centos";
            this.doAddAnlage.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.doAddAnlage.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doAddAnlage.QuickAccess = true;
            this.doAddAnlage.ShowItemsOnClick = true;
            this.doAddAnlage.ToolTip = null;
            this.doAddAnlage.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.doAddAnlage_Execute);
            // 
            // doDeleteHtKomponente
            // 
            this.doDeleteHtKomponente.AcceptButtonCaption = "löschen";
            this.doDeleteHtKomponente.CancelButtonCaption = null;
            this.doDeleteHtKomponente.Caption = "Anlagensystem entfernen";
            this.doDeleteHtKomponente.Category = "RecordEdit";
            this.doDeleteHtKomponente.ConfirmationMessage = null;
            this.doDeleteHtKomponente.Id = "doDeleteHtKomponente";
            this.doDeleteHtKomponente.ImageName = "cross_16";
            this.doDeleteHtKomponente.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doDeleteHtKomponente.ToolTip = null;
            this.doDeleteHtKomponente.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.doDeleteHtKomponente_CustomizePopupWindowParams);
            this.doDeleteHtKomponente.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.doDeleteHtKomponente_Execute);
            // 
            // Ctrl_Liegenschaft_DetailView
            // 
            this.Actions.Add(this.doAddHaustechnikKomponente);
            this.Actions.Add(this.doAddAnlage);
            this.Actions.Add(this.doDeleteHtKomponente);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction doAddHaustechnikKomponente;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction doAddAnlage;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction doDeleteHtKomponente;
    }
}
