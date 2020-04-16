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
using FacilityInfo.Building.BusinessObjects;

namespace FacilityInfo.Online.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_Gebaeude_Web_DetailView : ViewController
    {
        public Ctrl_Gebaeude_Web_DetailView()
        {
            InitializeComponent();
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(fiGebaeude);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            DetailView curView = (DetailView)View;
            IList<ASPxLookupPropertyEditor> lstPropEditors = curView.GetItems<ASPxLookupPropertyEditor>();
            if (lstPropEditors != null)
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
