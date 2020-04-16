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
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using FacilityInfo.Liegenschaft.BusinessObjects;

namespace FacilityInfo.Online.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_Liegenschaft_Web_DetailView : ViewController
    {
        public Ctrl_Liegenschaft_Web_DetailView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(boLiegenschaft);
            TargetViewType = ViewType.DetailView;
            

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            DetailView curView = (DetailView)View;
            //das View durchgehen und die Lookup Property editoren heraus suchen
            IList<ASPxLookupPropertyEditor> lstPropEditors = curView.GetItems<ASPxLookupPropertyEditor>();
            if(lstPropEditors != null)
            {
                foreach (ASPxLookupPropertyEditor item in lstPropEditors)
                {
                    item.ViewModeBehavior = ObjectPropertyEditorViewModeBehavior.ShowLabel;
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
