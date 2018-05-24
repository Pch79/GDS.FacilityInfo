using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Building.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using FacilityInfo.Messung.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacilityInfo.Anlagen.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_Anlage_DetailView : ViewController
    {

        DetailView curView;
        boAnlage curAnlage;
        public Ctrl_Anlage_DetailView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(boAnlage);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            curView = (DetailView)View;
            curAnlage = (boAnlage)curView.CurrentObject;
            // XPObjectSpace os = (XPObjectSpace)curView.ObjectSpace;
            if (curAnlage.Typ != null)
            {
                fiHerstellerProdukt curProdukt = curView.ObjectSpace.GetObjectByKey<fiHerstellerProdukt>(curAnlage.Typ.Oid);
                if (curProdukt != null)
                {
                    curAnlage.generateAnlagenfields(curProdukt);
                }
            }
            createStatusAction();
            if(!curView.ObjectSpace.IsNewObject(curAnlage))
            //if (curAnlage.AnlagenArt != null)
            {
                createMenue(doAddKomponente);
                createMenue(doAddMessung);
            }

        }

        private void createMenue(SingleChoiceAction action)
        {
            IObjectSpace os = this.ObjectSpace;
            DetailView curView = (DetailView)View;
            boAnlage curAnlage = os.GetObjectByKey<boAnlage>(((boAnlage)curView.CurrentObject).Oid);
            action.Items.Clear();
            switch (action.Id)
            {
                case ("doAddKomponente"):
                    {
                        boAnlagenArt curArt = os.GetObjectByKey<boAnlagenArt>(curAnlage.AnlagenArt.Oid);
                        action.ImageName = "plugin_16";
                        if (curArt.lstKomponenten != null)
                        {
                            foreach (AnKomponente komponente in curArt.lstKomponenten)
                            {
                                ChoiceActionItem curItem = new ChoiceActionItem(komponente.Oid.ToString(), komponente.Bezeichnung, komponente);
                                curItem.ImageName = "plugin_16";
                                action.Items.Add(curItem);

                            }
                        }
                        break;
                    }

                        case ("doAddMessung"):
                    {
                        boAnlagenArt curArt = os.GetObjectByKey<boAnlagenArt>(curAnlage.AnlagenArt.Oid);
                        action.ImageName = "system_monitor";
                        if(curArt.lstMesstypen != null)
                        { 
                        
                            foreach (boMesstyp messtyp in curArt.lstMesstypen)
                            {
                                ChoiceActionItem curItem = new ChoiceActionItem(messtyp.Oid.ToString(), messtyp.Bezeichnung, messtyp);
                                curItem.ImageName = "system_monitor";
                                action.Items.Add(curItem);

                            }
                        }
                        break;
                    }
        }
        }

        private void createStatusAction()
        {
            doChangeAnlagenStatus.Items.Clear();
            foreach(object current in Enum.GetValues(typeof(enmAnlagenStatus)))
            {
                EnumDescriptor ed = new EnumDescriptor(typeof(enmAnlagenStatus));
                ChoiceActionItem curItem = new ChoiceActionItem(ed.GetCaption(current), current);
                curItem.ImageName = ImageLoader.Instance.GetEnumValueImageName(current);
                doChangeAnlagenStatus.Items.Add(curItem);
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void doPrintLabel_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            curView = (DetailView)View;
            curAnlage = (boAnlage)curView.CurrentObject;
            openReport();
        }

        #region Report öffnen
        private void openReport()
        {

            XtraReport MyRep = new XtraReport();
            MyRep.ReportUnit = ReportUnit.TenthsOfAMillimeter;
            MyRep.BeforePrint += MyRep_BeforePrint;
            // MyRep.AfterPrint += MyRep_AfterPrint;
            //MyRep.PrintDialog();
            MyRep.ShowPreview();

            //this._sMKIV_Bild = generateQrCode().ToImage();
        }
        #endregion

        void MyRep_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRBarCode mybarCode = generateQrCode();

            mybarCode.WidthF = 90;
            mybarCode.HeightF = 90;



            // Create a Detail band and add the bar code to it.
            var myDetailBand = new DetailBand();
            myDetailBand.Controls.Add(mybarCode);

            //barcode im Detailband
            var resultReport = (XtraReport)sender;

            resultReport.Bands.Add(myDetailBand);

            resultReport.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            resultReport.DefaultPrinterSettingsUsing.UsePaperKind = false;

            resultReport.PageHeight = 290;
            resultReport.PageWidth = 620;
            resultReport.Margins.Left = 25;
            resultReport.Margins.Top = 30;
            resultReport.Margins.Right = 25;
            resultReport.Margins.Bottom = 25;
            resultReport.ShowPrintMarginsWarning = false;
            //resultReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            resultReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        

        }

      
        private XRBarCode generateQrCode()
        {
            var barCode = new XRBarCode();
            barCode.Symbology = new QRCodeGenerator();

            barCode.ShowText = false;
            barCode.AutoModule = true;
            // barCode.WidthF = 100;
            //barCode.HeightF = 100;

            //Seitengröße einstellen


            //barCode.BinaryData = curInventar.Oid.ToByteArray();

            //barCode.BinaryData = Encoding.ASCII.GetBytes(curInventar.smCustInv_ModelNumber);
            barCode.BinaryData = Encoding.ASCII.GetBytes(curAnlage.AnlagenNummer);
            barCode.Text = curAnlage.Oid.ToString();
            //barCode.NavigateUrl = "www.gdsinfo.de";
            ((QRCodeGenerator)barCode.Symbology).CompactionMode = QRCodeCompactionMode.Byte;
            ((QRCodeGenerator)barCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            ((QRCodeGenerator)barCode.Symbology).Version = QRCodeVersion.AutoVersion;
            return barCode;
        }

        #region Anlagenstatus ändern
        private void doChangeAnlagenStatus_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace os = this.ObjectSpace;
            DetailView curView = (DetailView)View;
            boAnlage curAnlage = os.GetObjectByKey<boAnlage>(((boAnlage)curView.CurrentObject).Oid);
            curAnlage.Anlagenstatus = (enmAnlagenStatus)e.SelectedChoiceActionItem.Data;
            //den History-Eintrag setzens
            curAnlage.Save();
            os.CommitChanges();
        }
        #endregion


        private void doAddKomponente_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();//this.ObjectSpace;
            DetailView curView = (DetailView)View;
            boAnlage curAnlage = os.GetObjectByKey<boAnlage>(((boAnlage)curView.CurrentObject).Oid);

            //neue Komponente erstellen
            ChoiceActionItem selectedItem = e.SelectedChoiceActionItem;
            AnKomponente selectedKomponente = (AnKomponente)selectedItem.Data;
            if(selectedKomponente != null)
            {
                //neue Anlagenkomponente erstellen
                AnAnlagenKomponente curAnlagenKomponente = os.CreateObject<AnAnlagenKomponente>();
                curAnlagenKomponente.Komponente = os.GetObjectByKey<AnKomponente>(selectedKomponente.Oid);
                curAnlagenKomponente.Anlage = curAnlage;
                //jetzt das View öffnen
                DetailView dv = Application.CreateDetailView(os, curAnlagenKomponente);
                e.ShowViewParameters.CreatedView = dv;
            }
        }

        private void doAddMessung_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();//this.ObjectSpace;
            DetailView curView = (DetailView)View;
            boAnlage curAnlage = os.GetObjectByKey<boAnlage>(((boAnlage)curView.CurrentObject).Oid);
            //die Messung die ausgewählt wurde öffnen
            ChoiceActionItem selectedItem = e.SelectedChoiceActionItem;
            boMesstyp selectedMesstyp = (boMesstyp)selectedItem.Data;

            //die Prüfpunkte der Anlage auslesen
            List<AnPruefPunkt> lstPruefPunkte = new List<AnPruefPunkt>();
            //ich brauch die Komponenten und daraus die Prüfpunkte
            if(curAnlage.lstAnlagenkomponenten != null)
            {
                
                foreach(AnAnlagenKomponente anKompItem in curAnlage.lstAnlagenkomponenten)
                {
                    if (anKompItem.Komponente.lstPruefpunkte != null)
                    {
                        foreach (AnPruefPunkt prItem in anKompItem.Komponente.lstPruefpunkte)
                        {
                            if (!lstPruefPunkte.Contains(prItem))
                            {
                                lstPruefPunkte.Add(prItem);
                            }
                        }
                    }
                }
            }


            if(selectedMesstyp != null)
            {
                boMessung curMessung = os.CreateObject<boMessung>();
                //messtyp gleich setzen
                curMessung.Messtyp = os.GetObjectByKey<boMesstyp>(selectedMesstyp.Oid);
                curMessung.Anlage = curAnlage;
                //die Messpunkte hinzufügen
                //für jeden Messpunkt 
                if(selectedMesstyp.lstMessItemEntries != null)
                {
                    //welche Prüfpunkte finde ich in der Anlage weider?
                    
                    foreach(boMessitemEntry messItemEntry in selectedMesstyp.lstMessItemEntries)
                    {
                        if(lstPruefPunkte.Contains(messItemEntry.Messitem.Pruefpunkt))
                        {
                            AnPruefPunkt curPunkt = os.GetObjectByKey<AnPruefPunkt>(messItemEntry.Messitem.Pruefpunkt.Oid);
                            //Messpunkte erstellen
                            boMessprobe curProbe = os.CreateObject<boMessprobe>();
                            curProbe.Pruefpunkt = curPunkt;
                            curProbe.Save();
                            curMessung.lstProben.Add(curProbe);
                                                    }
                            
                        

                    }
                }
                   
                DetailView dv = Application.CreateDetailView(os, curMessung);
                e.ShowViewParameters.CreatedView = dv;

            }
        }

        private void doOpenBuildingDesigner_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace workingOs = Application.CreateObjectSpace();
            DetailView curView = (DetailView)View;
            boAnlage curAnlage = (boAnlage)curView.CurrentObject;
            boLiegenschaft curLiegenschaft = workingOs.GetObjectByKey<boLiegenschaft>(curAnlage.Liegenschaft.Oid);
            //ein neues Gebäude erstellen
            fiGebaeude curBuilding = workingOs.CreateObject<fiGebaeude>();
            curBuilding.Liegenschaft = curLiegenschaft;
            e.View = Application.CreateDetailView(workingOs, curBuilding);
        }

        private void doOpenBuildingDesigner_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //abspeichern des Begäudes
            //das Gebäude gleich in die Anlagenkomponente schreiben
            IObjectSpace workingOs = Application.CreateObjectSpace();
            DetailView curView = (DetailView)View;
            fiGebaeude curbuilding = (fiGebaeude)e.PopupWindowViewCurrentObject;
            curbuilding.Save();
            e.PopupWindowView.ObjectSpace.CommitChanges();
            boAnlage curAnlage = (boAnlage)curView.CurrentObject;
        }
    }
}
