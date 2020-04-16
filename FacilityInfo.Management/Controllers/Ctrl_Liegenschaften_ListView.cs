using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl;

using DevExpress.XtraReports.UI;
using FacilityInfo.Liegenschaft.BusinessObjects;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using FacilityInfo.Management.Helpers;

namespace FacilityInfo.Management.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_Liegenschaften_ListView : ViewController
    {
        public Ctrl_Liegenschaften_ListView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(boLiegenschaft);
            TargetViewType = ViewType.ListView;

               

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        
        private void doPrintListe_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            /*
            //die collectionSource der List view an den Report binden
            ListView curView = (ListView)View;
            //den Report finden
            //Brauch ich hier den Mandanten??? 
            //ja der muss mitgegeben werden und zwar in der Parameterliste
            //jeder Mandant hat eine Liste mit Dokumenten
            var DisplayName = String.Empty;
            IObjectSpace workingOs = this.ObjectSpace;

            XPObjectSpace workingXpOs = (XPObjectSpace)workingOs;
            if (clsStatic.loggedOnMandant != null)
            {
                //DisplayName = String.Format("{0}_{1}",((SimpleAction)sender).Tag, clsStatic.loggedOnMandant.Mandantenkennung); 
                DisplayName = ((SimpleAction)sender).Tag.ToString();
                
                ReportDataV2 curReport = ObjectSpace.FindObject<ReportDataV2>(new BinaryOperator("DisplayName", DisplayName, BinaryOperatorType.Equal));

           

                if (curView.SelectedObjects != null)
                {
                    if (curReport != null)
                    {
                        
                        XtraReport report = ReportDataProvider.ReportsStorage.LoadReport(curReport);
                        ReportsModuleV2 reportsModule = ReportsModuleV2.FindReportsModule(Application.Modules);


                        //new XPCollection<boLiegenschaft>(workingXpOs.Session,CriteriaOperator.Parse(filterString));
                        if (reportsModule != null && reportsModule.ReportsDataSourceHelper != null)
                        {
                            reportsModule.ReportsDataSourceHelper.SetupBeforePrint(report);

                            report.DataSource = new ProxyCollection(this.ObjectSpace, XafTypesInfo.Instance.FindTypeInfo(typeof(boLiegenschaft)), curView.SelectedObjects);

                            //hier den header gleich zusammenstellen
                            ReportHeaderBand myHeader = new ReportHeaderBand();
                            XRPictureBox myLogoBox = new XRPictureBox();
                          //  myLogoBox.Image = PictureHelper.byteArrayToImage(clsStatic.loggedOnMandant.Logo);
                            //lsStatic.loggedOnMandant.Logo;
                            XRLabel myLabel = new XRLabel();
                            //myLabel.Text = clsStatic.loggedOnMandant.Mandantenname;
                            myHeader.Controls.Add(myLabel);
                            myHeader.Controls.Add(myLogoBox);
                            //report. Add(myHeader);  
                            report.ShowPreview();
                        }
                    }
                }
              
                

            }

           
            */
        }
        
        
    }
}
