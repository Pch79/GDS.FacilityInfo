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
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;


namespace FacilityInfo.Online.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_Web_ListView : ViewController
    {

        private const string Key = "DeactivatedInWeb";
        ExportController exportCtrl;
        WebExportController webExportCtrl;
        bool isAdmin = false;
        PermissionPolicyUser curUser;
        
        public Ctrl_Web_ListView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewType = ViewType.ListView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
             exportCtrl = Frame.GetController<ExportController>();
            webExportCtrl = Frame.GetController<WebExportController>();
            //ist der aktuelle Benutze admin??
            curUser = (PermissionPolicyUser)SecuritySystem.CurrentUser;
            int adminQuery = curUser.Roles.Where(t => t.IsAdministrative).Count();

            if (adminQuery <= 0)
            {

                if (exportCtrl != null)
                {
                    exportCtrl.Active[Key] = false;
                }

                if (webExportCtrl != null)
                {
                    webExportCtrl.Active[Key] = false;
                }
            }
           
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            ListView curView = (ListView)View;
            if (!curView.Id.Contains("LookupListView"))
            {
                curView.Model.IsFooterVisible = true;
                ASPxGridListEditor gridListEditor = curView.Editor as ASPxGridListEditor;
                if (gridListEditor != null)
                {
                    
                    gridListEditor.Grid.EnablePagingGestures = DevExpress.Web.AutoBoolean.True;     
                  
                    
                }
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
           
            if(exportCtrl != null)
            {
                exportCtrl.Active.RemoveItem(Key);
                exportCtrl = null;
            }
            if(webExportCtrl != null)
            {
                webExportCtrl.Active.RemoveItem(Key);
                webExportCtrl = null;
            }
            base.OnDeactivated();
        }
    }
}
