namespace FacilityInfo.Anlagen.Controllers
{
    partial class Ctrl_Anlage_DetailView
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
            this.doChangeAnlagenStatus = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.doOpenBuildingDesigner = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.doAddKomponente = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // doChangeAnlagenStatus
            // 
            this.doChangeAnlagenStatus.Caption = "Status ändern";
            this.doChangeAnlagenStatus.Category = "RecordEdit";
            this.doChangeAnlagenStatus.ConfirmationMessage = null;
            this.doChangeAnlagenStatus.Id = "doChangeAnlagenStatus";
            this.doChangeAnlagenStatus.ImageName = "caution_board_16";
            this.doChangeAnlagenStatus.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.doChangeAnlagenStatus.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doChangeAnlagenStatus.ShowItemsOnClick = true;
            this.doChangeAnlagenStatus.ToolTip = null;
            this.doChangeAnlagenStatus.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.doChangeAnlagenStatus_Execute);
            // 
            // doOpenBuildingDesigner
            // 
            this.doOpenBuildingDesigner.AcceptButtonCaption = null;
            this.doOpenBuildingDesigner.CancelButtonCaption = null;
            this.doOpenBuildingDesigner.Caption = "Gebäudedesigner öffnen";
            this.doOpenBuildingDesigner.Category = "RecordEdit";
            this.doOpenBuildingDesigner.ConfirmationMessage = null;
            this.doOpenBuildingDesigner.Id = "doOpenBuildingDesigner";
            this.doOpenBuildingDesigner.ToolTip = null;
            this.doOpenBuildingDesigner.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.doOpenBuildingDesigner_CustomizePopupWindowParams);
            this.doOpenBuildingDesigner.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.doOpenBuildingDesigner_Execute);
            // 
            // doAddKomponente
            // 
            this.doAddKomponente.Caption = "Komponente anfügen";
            this.doAddKomponente.Category = "RecordEdit";
            this.doAddKomponente.ConfirmationMessage = null;
            this.doAddKomponente.Id = "doAddKomponente";
            this.doAddKomponente.ImageName = "plugin_16";
            this.doAddKomponente.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.doAddKomponente.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doAddKomponente.ShowItemsOnClick = true;
            this.doAddKomponente.ToolTip = null;
            
            // 
            // Ctrl_Anlage_DetailView
            // 
            this.Actions.Add(this.doChangeAnlagenStatus);
            this.Actions.Add(this.doOpenBuildingDesigner);
            this.Actions.Add(this.doAddKomponente);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SingleChoiceAction doChangeAnlagenStatus;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction doOpenBuildingDesigner;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction doAddKomponente;
    }
}
