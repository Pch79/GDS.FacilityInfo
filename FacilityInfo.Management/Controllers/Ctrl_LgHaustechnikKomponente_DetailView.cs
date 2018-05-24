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
using FacilityInfo.Building.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;

namespace FacilityInfo.Management.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_LgHaustechnikKomponente_DetailView : ViewController
    {
        public Ctrl_LgHaustechnikKomponente_DetailView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(LgHaustechnikKomponente);
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

        private void doOpenBuildingDesigner_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //ein Popup mit dem gebäude öffnen
            IObjectSpace workingOs = Application.CreateObjectSpace();
            DetailView curView = (DetailView)View;
            LgHaustechnikKomponente curHtKomponente = (LgHaustechnikKomponente)curView.CurrentObject;
            boLiegenschaft curLiegenschaft = workingOs.GetObjectByKey<boLiegenschaft>(curHtKomponente.Liegenschaft.Oid);
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
            fiGebaeude curbuilding =(fiGebaeude)e.PopupWindowViewCurrentObject;
            curbuilding.Save();
            e.PopupWindowView.ObjectSpace.CommitChanges();
            LgHaustechnikKomponente curKomponente =(LgHaustechnikKomponente)curView.CurrentObject;
            curKomponente.Gebaeude = this.ObjectSpace.GetObjectByKey<fiGebaeude>(curbuilding.Oid);
        }

        public void setBuilding()
        {
          
        }

        private void doOpenRoomDesigner_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //

            
        }

        private void doOpenRoomDesigner_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace workingOs = Application.CreateObjectSpace();
            DetailView curView = (DetailView)View;
            LgHaustechnikKomponente curHtKomponente = (LgHaustechnikKomponente)curView.CurrentObject;
            boLiegenschaft curLiegenschaft = workingOs.GetObjectByKey<boLiegenschaft>(curHtKomponente.Liegenschaft.Oid);
            //ein neues Gebäude erstellen
            //fiGebaeude curBuilding = workingOs.CreateObject<fiGebaeude>();
            //curBuilding.Liegenschaft = curLiegenschaft;
            fiRaum curRaum = workingOs.CreateObject<fiRaum>();
            e.View = Application.CreateDetailView(workingOs, curRaum);
        }
    }
}
