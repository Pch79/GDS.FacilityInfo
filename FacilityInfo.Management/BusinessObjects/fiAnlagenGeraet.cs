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
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Geräte der Anlage")]
    [XafDefaultProperty("Bezeichnung")]
    public class fiAnlagenGeraet : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _equipmentcode;
        private boGeraet _geraet;
        private boAnlage _anlage;
        private boHersteller _hersteller;
        private System.String _notiz;

        public fiAnlagenGeraet(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "Geraet":
                    if(this.Bezeichnung == null || this.Bezeichnung == string.Empty)
                    {
                        this.Bezeichnung = ((boGeraet)newValue).Bezeichnung;
                    }
                    break;

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
        [XafDisplayName("Equipmentcode")]
        public System.String Equipmentcode
        {
            get
            {
                return _equipmentcode;
            }
            set
            {
                SetPropertyValue("Equipmentcode", ref _equipmentcode, value);

            }
        }
        [XafDisplayName("Gerät")]
        public boGeraet Geraet
        {
            get
            {
                return _geraet;
            }
            set
            {
                SetPropertyValue("Geraet", ref _geraet, value);
            }
        }
        [XafDisplayName("Anlage")]
        [Association("boAnlage-fiAnlagengeraet")]
        public boAnlage Anlage
        {
            get
            {
                return _anlage;
            }
            set
            {
                SetPropertyValue("Anlage", ref _anlage, value);
            }
        }
        [XafDisplayName("Hersteller")]
        public boHersteller Hersteller
        {
            get
            {
                return _hersteller;
            }
            set
            {
                SetPropertyValue("Hersteller", ref _hersteller, value);
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
        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                boMandant retVal;
                retVal = (this.Anlage.Mandant != null) ? this.Anlage.Mandant : null;
                return retVal;
            }
        }
    }
}