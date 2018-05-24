using System;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using FacilityInfo.Management;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using System.IO;

namespace GDS.FacilityInfo.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
			Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
			Tracing.Initialize();
            //die Applikationseinstellungen initialisieren

            FacilityInfoWindowsFormsApplication winApplication = new FacilityInfoWindowsFormsApplication();
            // Refer to the https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112680.aspx help article for more details on how to provide a custom splash form.
            //winApplication.SplashScreen = new DevExpress.ExpressApp.Win.Utils.DXSplashScreen("YourSplashImage.png");
			SecurityAdapterHelper.Enable();
            if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                
            }

            initAppSettings();

          
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            if(System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
            try {
                
                winApplication.LoggedOn += new EventHandler<LogonEventArgs>(winApplication_LoggedOn);
               
               // winApplication.LoggedOff += new EventHandler<LoggedOffEventArgs>(winApplication_LoggedOff);
                winApplication.Setup();
                winApplication.Start();
            }
            catch(Exception e) {
                winApplication.HandleException(e);
            }
        }

 

        private static void initAppSettings()
        {

            //das Heimverzeichnis prüfen und erstellen
            
            clsStatic.AppName = (ConfigurationManager.AppSettings["AppName"] != null) ? ConfigurationManager.AppSettings["AppName"] : "FacilityInfo";
            var defaultDirectory = @"c:\Temp\" + clsStatic.AppName;

            //das Arbetisverzeichnis = Pfad aus der Knfiguration + AooName+
            //das Heim-Verzreichnis der Anwendung finden

            clsStatic.AppHomeDirectory = (ConfigurationManager.AppSettings["AppHomeDirectory"] != null) ? ConfigurationManager.AppSettings["AppHomeDirectory"] : defaultDirectory;
            //jetzt prüfen ob das Teil vorhanden ist, ansonsten anlegen
            DirectoryInfo di = new DirectoryInfo(clsStatic.AppHomeDirectory);
            if(!di.Exists)
            {
                di.Create();
                di.CreateSubdirectory("Global");

            }
            else
            {
                DirectoryInfo globalDi = new DirectoryInfo(String.Format("{0}\\{1}", clsStatic.AppHomeDirectory, "Global"));
                if(!globalDi.Exists)
                {
                    globalDi.Create();
                }
                globalDi.CreateSubdirectory("Datentransfer");
                
            }
           
            
            //und auch gleich das Globale datenverzeichnis erstellen

            //jetzt hab ich das Arbeitsverzeichnis der Anwendung erstellt. -> rest muss im Mandanten erfolgen

            
        }

       
        private static void winApplication_LoggedOn(object sender, LogonEventArgs e)
        {
            AuthenticationStandardLogonParameters myParams = (AuthenticationStandardLogonParameters)e.LogonParameters;
            Session XpoSession = XpoHelper.GetNewSession();

            //PermissionPolicyUser curUser = XpoSession.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", myParams.UserName, BinaryOperatorType.Equal));
            PermissionPolicyUser curUser = (PermissionPolicyUser)SecuritySystem.CurrentUser;
            //wenn isch der Huasverwalter einoggd muss ich den Mandanten auch finden
            //beim Ersten Aufruf
            //ist der Benutzer in der Adminstratorrolle
            if(!curUser.IsUserInRole("Administrators"))
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
                        if(curHausverwalter != null)
                        {
                            clsStatic.loggedOnMandantOid = curHausverwalter.Mandant.Oid.ToString();
                            clsStatic.loggedOnMandant = XpoSession.GetObjectByKey<boMandant>(curHausverwalter.Mandant.Oid);
                            clsStatic.loggedOnHausverwalter = curHausverwalter;
                        }
                    }

                    //dann schauen ob es ein Hausverwalter ist          
            }
        
           
        }
    }
}
