using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using FacilityInfo.Management;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using FacilityInfo.Management.BusinessObjects;
using DevExpress.Data.Filtering;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;

namespace GDS.FacilityInfo.Web {
    public class Global : System.Web.HttpApplication {
        public Global() {
            InitializeComponent();
        }
        protected void Application_Start(Object sender, EventArgs e) {
			SecurityAdapterHelper.Enable();
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
            
#if EASYTEST
            DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = true;
#endif
        }
        protected void Session_Start(Object sender, EventArgs e) {
		    Tracing.Initialize();
            WebApplication.SetInstance(Session, new FacilityInfoAspNetApplication());
			DevExpress.ExpressApp.Web.Templates.DefaultVerticalTemplateContentNew.ClearSizeLimit();
            WebApplication.Instance.SwitchToNewStyle();
            
            //WebApplication.LoggedOn += Global_LoggedOn;
            if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            if(System.Diagnostics.Debugger.IsAttached && WebApplication.Instance.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                WebApplication.Instance.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
            WebApplication.Instance.LoggedOn += Instance_LoggedOn;
            WebApplication.Instance.LoggedOff += Instance_LoggedOff;
            
        }

        private void Instance_LoggedOff(object sender, EventArgs e)
        {
            clsStatic.loggedOnHausverwalter = null;
            clsStatic.loggedOnMandant = null;
            clsStatic.loggedOnMandantOid = string.Empty;
        }

        private void Instance_LoggedOn(object sender, LogonEventArgs e)
        {
            Session XpoSession = XpoHelper.GetNewSession();

            //PermissionPolicyUser curUser = XpoSession.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", myParams.UserName, BinaryOperatorType.Equal));
            PermissionPolicyUser curUser = (PermissionPolicyUser)SecuritySystem.CurrentUser;
            //wenn isch der Huasverwalter einoggd muss ich den Mandanten auch finden
            //beim Ersten Aufruf
            //ist der Benutzer in der Adminstratorrolle
            if (!curUser.IsUserInRole("Administrators"))
            {

                //jetzt aufgrund des Systembenutzers den Mandanten finden
                boMitarbeiter curMitarbeiter = XpoSession.FindObject<boMitarbeiter>(new BinaryOperator("Systembenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));
                if (curMitarbeiter != null)
                {
                    if (curMitarbeiter.Mandant != null)
                    {
                        clsStatic.loggedOnMandantOid = curMitarbeiter.Mandant.Oid.ToString();
                        clsStatic.loggedOnMandant = XpoSession.GetObjectByKey<boMandant>(curMitarbeiter.Mandant.Oid);
                        clsStatic.loggedOnHausverwalter = null;
                    }
                }
                else
                {

                    boHausverwalter curHausverwalter = XpoSession.FindObject<boHausverwalter>(new BinaryOperator("Systembenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));
                    if (curHausverwalter != null)
                    {
                        clsStatic.loggedOnMandantOid = curHausverwalter.Mandant.Oid.ToString();
                        clsStatic.loggedOnMandant = XpoSession.GetObjectByKey<boMandant>(curHausverwalter.Mandant.Oid);
                        clsStatic.loggedOnHausverwalter = curHausverwalter;
                    }



                }

            }
        }



        protected void Application_BeginRequest(Object sender, EventArgs e) {
        }
        protected void Application_EndRequest(Object sender, EventArgs e) {
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
        }
        protected void Application_Error(Object sender, EventArgs e) {
            ErrorHandling.Instance.ProcessApplicationError();
        }
        protected void Session_End(Object sender, EventArgs e) {
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(Object sender, EventArgs e) {
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }


        #endregion

        #region LogOn


        #endregion
    }
}
