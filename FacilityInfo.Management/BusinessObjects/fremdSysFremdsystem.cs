using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Utils;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Fremdsystem.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Fremdsystem")]
    [ImageName("direction_16")]
    public class fremdSysFremdsystem : BaseObject
    {
        private System.String _name;
        private System.String _connectionString;      
        private System.Boolean _aktiv;
        private boMandant _mandant;
        private String _synchAppPath;
        private String _synchAppName;


        private System.String curMandantID;
        public fremdSysFremdsystem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //wenn der Mandant feststeht kann ich diesen gleich setzen
            curMandantID = clsStatic.loggedOnMandantOid;
            //hier gleich den Mandanten setzen
            this.Mandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Synch-Items")]
        [Association("fremdSysFremdsystem-fremdSysFremdsystem")]
        public XPCollection<fremdSysSynchItem> lstSynchItems
        {
          get 
          {
                return GetCollection<fremdSysSynchItem>("lstSynchItems");
          }
        }

        [XafDisplayName("Name (Synch-APP)")]
        
        public String SynchAppName
        {
        get {
                return _synchAppName;
        }
        set {
                SetPropertyValue("SynchAppName", ref _synchAppName, value);
        }
        }

        [XafDisplayName("Pfad (Synch-APP)")]
        public String SynchAppPath
        {
        get {
                return _synchAppPath;
        }
        set {
                SetPropertyValue("SynchAppPath", ref _synchAppPath, value);
        }
        }
        [XafDisplayName("Mandant")]
       // [Association("boMandant-fremdSysFremdsystem")]
        public boMandant Mandant
        {
            get
            {
                return _mandant;
            }
            set
            {
                SetPropertyValue("Mandant", ref _mandant, value);
            }
        }

        [XafDisplayName("Connection-String")]
        public System.String ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                SetPropertyValue("ConnectionString", ref _connectionString, value);
            }
        }

        [XafDisplayName("Aktiv")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public System.Boolean Aktiv
        {
            get
            {
                return _aktiv;
            }
            set
            {
                SetPropertyValue("Aktiv", ref _aktiv, value);
            }
        }

        

        [XafDisplayName("Name")]
        public System.String Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue("Name", ref _name, value);
            }
        }

        #endregion
    }
}