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
            // 
            // doRefreshData
            // 
            this.doRefreshData.Caption = "do Refresh Data";
            this.doRefreshData.ConfirmationMessage = null;
            this.doRefreshData.Id = "doRefreshData";
            this.doRefreshData.ToolTip = null;
            // 
            // Ctrl_Liegenschaften_ListView
            // 
            this.Actions.Add(this.doRefreshData);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction doRefreshData;
    }
}
