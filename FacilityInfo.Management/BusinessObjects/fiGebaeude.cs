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
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;

namespace FacilityInfo.Building.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Gebäude")]
    [XafDefaultProperty("Bezeichnung")]

    public class fiGebaeude : BaseObject
    {
        private System.String _bauteilnummer;
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private boLiegenschaft _liegenschaft;
        private System.String _notiz;
        private boAdresse _adresse; 
        //TODO Gebäudetypen einführen
        public fiGebaeude(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            
        }
        //wenn eine Adresse eingegeben wird dann die Bezeichnung aus der Adresse nehmen
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "Adresse":
                if(newValue != null)
                {
                        boAdresse selectedAdresse = (boAdresse)newValue;
                        this.Bezeichnung = selectedAdresse.Matchkey;
                }
                    break;
            }
        }
        [XafDisplayName("Gebäudeadresse")]
        public boAdresse Adresse
        {
        get
        {
                return _adresse;
        }
        set
        {
                SetPropertyValue("Adresse", ref _adresse, value);
        }
        }

        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                boMandant retVal;
                retVal = (this.Liegenschaft != null) ? this.Liegenschaft.Mandant : null;
                return retVal;
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
        [XafDisplayName("Bauteilnummer")]
        public System.String Bauteilnummer
        {
            get
            {
                return _bauteilnummer;

            }
            set
            {
                SetPropertyValue("Bauteilnummer", ref _bauteilnummer, value);
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
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
        {
            get
            {
                return _beschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
            }
        }
        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-fiGebaeude")]
        public boLiegenschaft Liegenschaft
        {
            get
            {
                return _liegenschaft;
            }
            set
            {
                SetPropertyValue("Liegenschaft", ref _liegenschaft, value);
            }
        }
        [XafDisplayName("Ebenen")]      
        [Association("fiGebaeude-fiEbene")]
        public XPCollection<fiEbene> lstEbenen
        {
            get
            {
                return GetCollection<fiEbene>("lstEbenen");
            }
        }

        [XafDisplayName("Räume")]
        
        [Association("fiGebaeude-fiRaum")]
        public XPCollection<fiRaum> lstRaeume
        {
            get
            {
                return GetCollection<fiRaum>("lstRaeume");
            }
        }
    }
}