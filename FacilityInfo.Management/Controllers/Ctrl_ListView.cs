using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using DevExpress.ExpressApp.Security;

namespace FacilityInfo.Management.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_ListView : ViewController
    {

        public Type curViewType;
        public Ctrl_ListView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewType = ViewType.ListView;
            TargetViewNesting = Nesting.Any;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
     
            ListView curView = (ListView)View;
           
            curView.Model.FilterEnabled = true;

            if (curView.ObjectTypeInfo.FindMember("SortIndex") != null)
            {
                acitvateSorter("SortIndex");
            }
            
        }

        public void acitvateSorter(string propertyname)
        {
            ListView curView = (ListView)View;
            
            IModelColumn columnInfo = ((IModelList<IModelColumn>)curView.Model.Columns)[propertyname];

            if (columnInfo != null)
            {
                columnInfo.SortIndex = 1;
                columnInfo.SortOrder = ColumnSortOrder.Ascending;
            }
        }


       
      
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        //    ListView curView = (ListView)View;
            //curView.Model.IsGroupPanelVisible = true;
            //curView.Model.AutoExpandAllGroups = true;
          //  curView.Model.FilterEnabled = true;
          
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();

        }
    }
}
