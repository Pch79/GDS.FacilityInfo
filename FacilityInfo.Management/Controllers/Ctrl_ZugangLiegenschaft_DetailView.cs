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
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;

namespace FacilityInfo.Management.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_ZugangLiegenschaft_DetailView : ViewController
    {
        public Ctrl_ZugangLiegenschaft_DetailView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(fiZugangLiegenschaft);
            TargetViewType = ViewType.DetailView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //wenn sich die Adrese ändert
            // DetailView curView = (DetailView)View;
            // ListPropertyEditor lstKontakte = curView.FindItem("boKontakt_LookupListView") as ListPropertyEditor;
            //on changed -> muss das Objet reloaden
            fiZugangLiegenschaft curObject = (fiZugangLiegenschaft)((DetailView)View).CurrentObject;
            //curObject.ZugangKategorie.Changed += ZugangKategorie_Changed;          
        }

        private void ZugangKategorie_Changed(object sender, DevExpress.Xpo.ObjectChangeEventArgs e)
        {

            fiZugangLiegenschaft curZugang = (fiZugangLiegenschaft)View.CurrentObject;
            var controller = Frame.GetController<ModificationsController>();
            if(controller != null)
            {
                if (controller.SaveAction.Active)                 
                {
                    try
                    {
                        controller.SaveAction.DoExecute();
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            

        }


        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
