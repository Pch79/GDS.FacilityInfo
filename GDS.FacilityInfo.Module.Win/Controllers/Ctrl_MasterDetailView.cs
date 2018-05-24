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
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;


namespace GDS.FacilityInfo.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_MasterDetailView : ViewController<ListView>
    {
        public Ctrl_MasterDetailView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            /*
            GridListEditor gridListEditor = View.Editor as GridListEditor;
            if (gridListEditor != null)
            {
                GridControl grid = gridListEditor.Grid;
                GridView view = gridListEditor.GridView;
                view.OptionsPrint.PrintDetails = true;
                view.OptionsDetail.ShowDetailTabs = true;
                view.OptionsView.ShowDetailButtons = true;
                view.OptionsDetail.EnableMasterViewMode = true;

                view.MasterRowExpanded += view_MasterRowExpanded;
            }
            */
        }
        private void view_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            /*
            GridView masterView = sender as GridView;
            GridView detailView = masterView.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detailView.BeginUpdate();
            detailView.OptionsDetail.EnableMasterViewMode = false;
            detailView.OptionsBehavior.Editable = false;
            foreach (GridColumn col in detailView.Columns)
            {
                if (col.FieldName.Contains("!"))
                {
                    col.Visible = false;
                    col.OptionsColumn.ShowInCustomizationForm = false;
                }

            }
            detailView.EndUpdate();
            */
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
