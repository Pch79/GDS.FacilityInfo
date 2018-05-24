using DevExpress.Map;
using DevExpress.XtraMap;

namespace GDS.FacilityInfo.Module.Win.UserControls
{
    partial class ucMapView
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
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            this.imageLayer1 = new DevExpress.XtraMap.ImageLayer();
            this.bingMapDataProvider1 = new DevExpress.XtraMap.BingMapDataProvider();
            this.informationLayer1 = new DevExpress.XtraMap.InformationLayer();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // mapControl1
            // 
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.Layers.Add(this.imageLayer1);
            this.mapControl1.Layers.Add(this.informationLayer1);
            this.mapControl1.CenterPoint = new GeoPoint(48.5523690, 12.3727440);
            this.mapControl1.ZoomLevel = 10;
            this.mapControl1.Location = new System.Drawing.Point(0, 0);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(958, 471);
            this.mapControl1.TabIndex = 0;
            // 
            // imageLayer1
            // 
            this.imageLayer1.DataProvider = this.bingMapDataProvider1;
            // 
            // bingMapDataProvider1
            // 
            this.bingMapDataProvider1.BingKey = "AorVZsdqqGVj-amy3PSeJAeuQMre2AZ28CMAmEwJzUMie2cEZHVjs7mODXWs1k85";
            // 
            // ucMapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mapControl1);
            this.Name = "ucMapView";
            this.Size = new System.Drawing.Size(958, 471);
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraMap.MapControl mapControl1;
        private DevExpress.XtraMap.ImageLayer imageLayer1;
        private DevExpress.XtraMap.BingMapDataProvider bingMapDataProvider1;
        private DevExpress.XtraMap.InformationLayer informationLayer1;
    }
}
