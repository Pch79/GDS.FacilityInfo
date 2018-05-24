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
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Building.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Raum")]
    [XafDefaultProperty("Bezeichnung")]
    public class fiRaum : BaseObject
    {
        private System.String _raumnummer;
        private System.String _tuerschild;
        private System.String _bezeichnung;
        private fiRaumart _raumart;
        private fiEbene _ebene;
        private fiGebaeude _gebaeude;
        private System.String _notiz;
        
        public fiRaum(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            //this.Mandant = this.Session.GetObjectByKey<boMandant>(clsStatic.loggedOnMandantOid);
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "Raumart":
                    if(this.Raumart != null)
                    {
                        this.Bezeichnung = this.Raumart.Bezeichnung;
                    }
                    else
                    {
                        this.Bezeichnung = string.Empty;
                    }
                    break;
            }
        }
        
        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
       
            get
            {
                boMandant retVal = null;
              
                    if (this.Gebaeude != null)
                    {
                        retVal = (this.Gebaeude.Mandant != null) ? this.Gebaeude.Mandant : null;
                    }
                return retVal;
              }
                        
        }
        
        [XafDisplayName("Bezeichnung")]
  
        public System.String Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
            }
        }


        [XafDisplayName("Notiz")]
        [Size(-1)]
        public System.String Notiz
        {
            get
            {
                return _notiz;
            }
            set
            {
                SetPropertyValue("Notiz", ref _notiz, value);
            }
        }
        [XafDisplayName("Ebene")]
        [Association("fiEbene-fiRaum")]
        public fiEbene Ebene
        {
            get
            {
                return _ebene;
            }
            set
            {
                SetPropertyValue("Ebene", ref _ebene, value);
            }
        }

        [XafDisplayName("Gebäude")]
        [Association("fiGebaeude-fiRaum")]
        public fiGebaeude Gebaeude
        {
            get
            {
                return _gebaeude;
            }
            set
            {
                SetPropertyValue("Gebaeude", ref _gebaeude, value);
            }
        }

        [XafDisplayName("Raumnummer")]
       public System.String Raumnummer
        {
            get
            {
                return _raumnummer;
            }
            set
            {
                SetPropertyValue("Raumnummer", ref _raumnummer, value);
            }
        }
        [XafDisplayName("Türschild")]
        public System.String Tuerschild
        {
            get
            {
                return _tuerschild;
            }
            set
            {
                SetPropertyValue("Tuerschild", ref _tuerschild, value);
            }
        }
        [XafDisplayName("Raumart")]
        public fiRaumart Raumart
        {
            get
            {
                return _raumart;
            }
            set
            {
                SetPropertyValue("Raumart", ref _raumart, value);
            }
        }
    }
}