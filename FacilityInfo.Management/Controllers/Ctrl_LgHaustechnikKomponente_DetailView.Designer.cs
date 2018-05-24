namespace FacilityInfo.Management.Controllers
{
    partial class Ctrl_LgHaustechnikKomponente_DetailView
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
            this.doOpenBuildingDesignerAnlage = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.doOpenRoomDesigner = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // doOpenBuildingDesignerAnlage
            // 
            this.doOpenBuildingDesignerAnlage.AcceptButtonCaption = null;
            this.doOpenBuildingDesignerAnlage.CancelButtonCaption = null;
            this.doOpenBuildingDesignerAnlage.Caption = "Gebäude hinzufügen";
            this.doOpenBuildingDesignerAnlage.Category = "RecordEdit";
            this.doOpenBuildingDesignerAnlage.ConfirmationMessage = null;
            this.doOpenBuildingDesignerAnlage.Id = "doOpenBuildingDesignerAnlage";
            this.doOpenBuildingDesignerAnlage.ImageName = "real_estate_16";
            this.doOpenBuildingDesignerAnlage.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.doOpenBuildingDesignerAnlage.ToolTip = null;
            this.doOpenBuildingDesignerAnlage.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.doOpenBuildingDesigner_CustomizePopupWindowParams);
            this.doOpenBuildingDesignerAnlage.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.doOpenBuildingDesigner_Execute);
            // 
            // doOpenRoomDesigner
            // 
            this.doOpenRoomDesigner.AcceptButtonCaption = null;
            this.doOpenRoomDesigner.CancelButtonCaption = null;
            this.doOpenRoomDesigner.Caption = "Raum hinzufügen";
            this.doOpenRoomDesigner.Category = "RecordEdit";
            this.doOpenRoomDesigner.ConfirmationMessage = null;
            this.doOpenRoomDesigner.Id = "doOpenRoomDesigner";
            this.doOpenRoomDesigner.ToolTip = null;
            this.doOpenRoomDesigner.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.doOpenRoomDesigner_CustomizePopupWindowParams);
            this.doOpenRoomDesigner.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.doOpenRoomDesigner_Execute);
            // 
            // Ctrl_LgHaustechnikKomponente_DetailView
            // 
            this.Actions.Add(this.doOpenBuildingDesignerAnlage);
            this.Actions.Add(this.doOpenRoomDesigner);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction doOpenBuildingDesignerAnlage;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction doOpenRoomDesigner;
    }
}
