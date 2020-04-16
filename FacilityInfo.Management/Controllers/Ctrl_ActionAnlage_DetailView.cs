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
using FacilityInfo.Action.BusinessObjects;

namespace FacilityInfo.Management.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_ActionAnlage_DetailView : ViewController
    {
        public Ctrl_ActionAnlage_DetailView()
        {
            InitializeComponent();
            TargetObjectType = typeof(actionActionAnlage);
            TargetViewType = ViewType.DetailView;
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

        private void doSetActionDone_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DetailView curView = (DetailView)View;
            actionActionAnlage curAction = (actionActionAnlage)curView.CurrentObject;
            curAction.Status = EnumStore.enmBearbeitungsStatus.erledigt;
            if(curAction.lstActionPosition != null)
            {
                for(int i=0;i<curAction.lstActionPosition.Count;i++)
                {
                    ((actionActionPosition)curAction.lstActionPosition[i]).Status = EnumStore.enmBearbeitungsStatus.erledigt;
                    ((actionActionPosition)curAction.lstActionPosition[i]).Save();
                }
            }
            curAction.Save();
            curView.ObjectSpace.CommitChanges();
            curAction.Anlage.Reload();
            curView.ObjectSpace.Refresh();

            
        }
    }
}
