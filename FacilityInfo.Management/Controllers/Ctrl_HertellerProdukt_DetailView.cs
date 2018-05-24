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
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.Management.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_HertellerProdukt_DetailView : ViewController
    {

        DetailView curView;
        fiHerstellerProdukt curHerstellerProdukt;
        public Ctrl_HertellerProdukt_DetailView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(fiHerstellerProdukt);
            TargetViewType = ViewType.DetailView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //hier die Felder generieren
            curView = (DetailView)View;
            if (curView.CurrentObject != null)
            {
                curHerstellerProdukt = (fiHerstellerProdukt)curView.CurrentObject;

                // XPObjectSpace os = (XPObjectSpace)curView.ObjectSpace;

                if (curHerstellerProdukt.Produktgruppe != null)
                {
                    //dann die Felder nachziehen

                    fiHerstellerProduktgruppe curProduktGruppe = curView.ObjectSpace.GetObjectByKey<fiHerstellerProduktgruppe>(curHerstellerProdukt.Produktgruppe.Oid);
                    if (curProduktGruppe != null)
                    {
                        curHerstellerProdukt.generateFields(curProduktGruppe);  //generateAnlagenfields(curProdukt);
                    }
                }
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
    }
}
