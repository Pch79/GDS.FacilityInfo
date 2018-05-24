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
using FacilityInfo.Fremdsystem.BusinessObjects;
using System.Diagnostics;

namespace FacilityInfo.Management.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
   
    public partial class Ctrl_FremdSysSynchItem : ViewController
    {
        public ViewType curViewType;
        public List<fremdSysSynchItem> lstSelectedItems;
        public Ctrl_FremdSysSynchItem()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            //
            //binich in einem List View oer in einem DetailView
            TargetObjectType = typeof(fremdSysSynchItem);
            
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

        private void doSynchItems_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Die Items erst mal selekteiren 

            //erstmal die Auswall nullen
            if (lstSelectedItems == null)
            {
                lstSelectedItems = new List<fremdSysSynchItem>();
            }
            else
            {
                lstSelectedItems.Clear();
            }
            if(View.GetType() == typeof(ListView))
            {
                if(View.SelectedObjects.Count >0)
                {
                    for(int i = 0; i<View.SelectedObjects.Count;i++)
                    {
                        lstSelectedItems.Add((fremdSysSynchItem)View.SelectedObjects[i]);
                    }
                }
            }

            if(View.GetType() == typeof(DetailView))
            {
                lstSelectedItems.Add((fremdSysSynchItem)View.CurrentObject);
            }
            //jetzt kann ich da weiter machen

            SynchSelection();
        }

        private void SynchSelection()
        {

            fremdSysFremdsystem curFremdsystem;
            IObjectSpace workingOs = Application.CreateObjectSpace();
            if(lstSelectedItems.Count>0)
            {
                for(int i=0;i<lstSelectedItems.Count;i++)
                {
                    curFremdsystem = workingOs.GetObjectByKey<fremdSysFremdsystem>(lstSelectedItems[i].Fremdsystem.Oid);
                        //FindObject<fremdSysFremdsystem>(new BinaryOperator("Name"))
                    //den Aufruf des Programmes starten aber wie krieg ich mit dass das Teil fertig ist
                    Process mySynchProcess = new Process();
                    
                    mySynchProcess.StartInfo.FileName = string.Format("{0}\\{1}", curFremdsystem.SynchAppPath, curFremdsystem.SynchAppName);
                    mySynchProcess.StartInfo.Arguments = lstSelectedItems[i].AufrufParameter;
                  
                    mySynchProcess.Start();
                    mySynchProcess.WaitForExit();
                    showMessage(lstSelectedItems[i].Objekttyp.Name.ToString());
                    
                }
            }
        }

        private void showMessage(string message)
        {
            //hier dann die Meldung ausgeben
            MessageOptions options = new MessageOptions();
            options.Duration = 2000;
            options.Message = message;
            options.Type = InformationType.Success;
            //den Applikationsnamen auslesen           

            options.Win.Caption = Application.Title;
            options.Win.Type = WinMessageType.Alert;
            Application.ShowViewStrategy.ShowMessage(options);
        }
    }
}
