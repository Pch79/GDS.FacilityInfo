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

namespace GDS.FacilityInfo.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_WinListView : ViewController
    {
        public Ctrl_WinListView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewType = ViewType.ListView;
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
            ListView curView = (ListView)View;
            curView.Model.IsFooterVisible = true;
            GridListEditor gridListEditor = curView.Editor as GridListEditor;
            if (gridListEditor != null)
            {
                gridListEditor.GridView.OptionsCustomization.AllowSort = true;
                gridListEditor.GridView.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
                gridListEditor.GridView.OptionsCustomization.AllowGroup = true;
                gridListEditor.GridView.OptionsFilter.AllowFilterEditor = true;
                gridListEditor.GridView.OptionsView.AllowHtmlDrawHeaders = true;
                gridListEditor.GridView.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateFocusedItem;
                gridListEditor.GridView.OptionsView.AutoCalcPreviewLineCount = true;
               
                gridListEditor.GridView.OptionsView.ColumnAutoWidth = true;
                gridListEditor.GridView.OptionsView.ShowGroupPanelColumnsAsSingleRow = false;
                gridListEditor.GridView.OptionsView.EnableAppearanceOddRow = true;
                gridListEditor.GridView.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(244,244,244);
                gridListEditor.GridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
                gridListEditor.ColumnView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
                gridListEditor.GridView.RowHeight = 60;
                
                gridListEditor.Grid.UseEmbeddedNavigator = true;
                
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
