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
            // Perform various tasks depending on the target View.
            //hat der angezeigrte Typ ein Feld mandant??

            //bin ich admin? Wenn ja is wurscht ansonten hier weitermachen

            //
            ListView curView = (ListView)View;
            curView.Model.IsGroupPanelVisible = true;
            curView.Model.AutoExpandAllGroups = true;
            curView.Model.FilterEnabled = true;

            curView.Model.DataAccessMode = CollectionSourceDataAccessMode.Client;

            Type curType = curView.ObjectTypeInfo.Type;
            PermissionPolicyUser curUser = (PermissionPolicyUser)SecuritySystem.CurrentUser;

            curView.CollectionSource.Criteria.Clear();

            if (!curUser.IsUserInRole("Administrators"))              
            {
                //bei welchem Mandantenbin ich?

                boMandant loggedOnMandant = clsStatic.loggedOnMandant;//getMandant();
                boHausverwalter loggedOnHausverwalter = clsStatic.loggedOnHausverwalter;
             
                if (loggedOnHausverwalter != null)
                {
                    //
                    if (curView.ObjectTypeInfo.FindMember("Hausverwalter") != null)
                    {
                        curView.CollectionSource.Criteria.Clear();

                        //wenn der Member nicht null ist
                        

                        try
                        {
                            curView.CollectionSource.Criteria.Add("ownObjectsFilter_Hausverwalter", new BinaryOperator("Hausverwalter.Oid", loggedOnHausverwalter.Oid, BinaryOperatorType.Equal));
                           
                        }
                        catch
                        {

                            curView.CollectionSource.ResetCollection();
                        }

                    }

                }
                else {
                    if (loggedOnMandant != null)
                    {
                        //ListView curView = (ListView)View;


                        //Type curType = curView.ObjectTypeInfo.Type;

                        if (curView.ObjectTypeInfo.FindMember("Mandant") != null)
                        {
                            curView.CollectionSource.Criteria.Clear();
                            curView.CollectionSource.Criteria.Add("ownObjectsFilter_Mandant", new BinaryOperator("Mandant.Oid", loggedOnMandant.Oid, BinaryOperatorType.Equal));
                        }

                        //wenn es ssich um einen Hausverwalter handelt


                    
                    }
                    else
                    {
                        throw new Exception("Keine Berechtigung oder keine Mandantenzuordnung");
                    }
                }

                
            }

            if (curView.ObjectTypeInfo.FindMember("Sortindex") != null)
            {
                acitvateSorter("Sortindex");
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

      private boMandant getMandant()
        {
            boMandant retVal;
            if (clsStatic.loggedOnMandant != null)
            {
                retVal = clsStatic.loggedOnMandant;
            }
            else
            {
                retVal = null;
            }
            return retVal;
        }


        /*
        private Boolean getAdminState()
        {
            var retVal = false;
            //nur wenn der Benutzername admin angelemdet ist darf alles angezeigt werden
            IObjectSpace os = Application.CreateObjectSpace();
            //PermissionPolicyUser curUser = os.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId);
           // PermissionPolicyUser curUser = os.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", SecuritySystem.CurrentUserName, BinaryOperatorType.Equal));
            PermissionPolicyUser curUser = (PermissionPolicyUser)SecuritySystem.CurrentUser;
           
            if(curUser.UserName.ToLower() == "admin" || curUser.UserName.ToLower()=="gdsadmin")
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }
        */

      
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            ListView curView = (ListView)View;
            curView.Model.IsGroupPanelVisible = true;
            curView.Model.AutoExpandAllGroups = true;
            curView.Model.FilterEnabled = true;
          
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();

        }
    }
}
