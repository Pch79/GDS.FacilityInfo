using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using FacilityInfo.Action.BusinessObjects;
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
        PermissionPolicyUser curUser;

        
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
            if (curAnlage != null)
            {
                if (curAnlage.Typ != null)
                {
                    fiHerstellerProdukt curProdukt = curView.ObjectSpace.GetObjectByKey<fiHerstellerProdukt>(curAnlage.Typ.Oid);
                    if (curProdukt != null)
                    {
                        curAnlage.generateAnlagenparameter(curProdukt);
                        curAnlage.Save();
                        curView.ObjectSpace.CommitChanges();
                    }
                }
                if (curAnlage.AnlagenArt != null)
                {
                    boAnlagenArt curAnlagenart = curView.ObjectSpace.GetObjectByKey<boAnlagenArt>(curAnlage.AnlagenArt.Oid);
                    if (curAnlagenart != null)
                    {
                        curAnlage.generateAnlagenparameter(curAnlagenart);
                        curAnlage.Save();
                        curView.ObjectSpace.CommitChanges();
                    }
                }

                createStatusAction();
                if (!curView.ObjectSpace.IsNewObject(curAnlage))
                //if (curAnlage.AnlagenArt != null)
                {
                    createMenue(doAddKomponente);
                   
                }

              //  curAnlage.checkAnlagenActions();
                curAnlage.Save();
                curView.ObjectSpace.CommitChanges();

              //  curAnlage.checkProduktActions();
                curAnlage.Save();
                curView.ObjectSpace.CommitChanges();


                this.View.Refresh();

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
            this.doChangeAnlagenStatus.Active.SetItemValue("NotAvailable", true);
             this.doOpenBuildingDesigner.Active.SetItemValue("NotAvailable", false);

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        
        #region Anlagenstatus ändern
        private void doChangeAnlagenStatus_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace os = this.ObjectSpace;
            DetailView curView = (DetailView)View;
            boAnlage curAnlage = os.GetObjectByKey<boAnlage>(((boAnlage)curView.CurrentObject).Oid);
            boAnlage workingAnlage;
            curAnlage.Anlagenstatus = (enmAnlagenStatus)e.SelectedChoiceActionItem.Data;
            //den History-Eintrag setzens
            curAnlage.Save();
            // wenn es unteranlagen gibt müssen diese auch den neuen Status erhalten
            if(curAnlage.lstUnteranlagen != null)
            {
                for(int i=0;i<curAnlage.lstUnteranlagen.Count;i++)
                {
                    workingAnlage = os.GetObjectByKey<boAnlage>(curAnlage.lstUnteranlagen[i].Oid);
                    workingAnlage.Anlagenstatus = (enmAnlagenStatus)e.SelectedChoiceActionItem.Data;
                }
            }
            os.CommitChanges();
        }
        #endregion


       
       

        private void doOpenBuildingDesigner_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace workingOs = Application.CreateObjectSpace();
            DetailView curView = (DetailView)View;
            boAnlage curAnlage = (boAnlage)curView.CurrentObject;
            fiGebaeude workingBuilding;
            boLiegenschaft curLiegenschaft = workingOs.GetObjectByKey<boLiegenschaft>(curAnlage.Liegenschaft.Oid);
            //ein neues Gebäude erstellen
            //ist bereits ein Gebäude gewählt dann dieses übernehmen
            if (curAnlage.Gebaeude != null)
            {
                workingBuilding = workingOs.GetObjectByKey<fiGebaeude>(curAnlage.Gebaeude.Oid);
            }
            else
            {
                workingBuilding = workingOs.CreateObject<fiGebaeude>();
                workingBuilding.Liegenschaft = curLiegenschaft;
            }
            e.View = Application.CreateDetailView(workingOs, workingBuilding);
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
