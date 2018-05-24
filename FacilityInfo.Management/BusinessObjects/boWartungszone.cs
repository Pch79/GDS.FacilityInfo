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
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Wartungszone")]
    [Serializable]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("map_magnify")]
    public class boWartungszone : BaseObject
    {
        private System.String _bezeichnung;
        private boMandant _mandant;
        private System.Decimal _calcFaktor;
        private String _beschreibung;
        private Decimal _anfahrtsPauschale;

        public boWartungszone(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        #region Properties
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
        {
         get {
                return _beschreibung;
         }
         set {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
         }
        }

        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var mandant = string.Empty;
                var bezeichnung = string.Empty;
                mandant = (this.Mandant != null) ? this.Mandant.Mandantenname : "N/A";
                bezeichnung = (this.Bezeichnung != null) ? this.Bezeichnung : "N/A";
                retVal = string.Format("{0} - {1}", mandant, bezeichnung);
                return retVal;

            }
        }

        //hat eine Liste an Ortsteilen
        [XafDisplayName("Orte")]
        [Association("boWartungszone-lstOrte")]
        public XPCollection<boOrt> lstOrte
        {
            get
            {
                return GetCollection<boOrt>("lstOrte");
            }
        }


        

        [XafDisplayName("Mandant")]
        [RuleRequiredField]
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

        [XafDisplayName("Zonenname")]
        [RuleRequiredField]
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

        [XafDisplayName("Anfahrstpauschale")]
        public System.Decimal AnfahrtsPauschale
        {
            get
            {
                return _anfahrtsPauschale;
            }
            set
            {
                SetPropertyValue("AnfahrtsPauschale", ref _anfahrtsPauschale, value);
            }
        }

        [XafDisplayName("Kalkulationsfaktor")]
        public System.Decimal CalcFaktor
        {
            get
            {
                return _calcFaktor;
            }
            set
            {
                SetPropertyValue("CalcFaktor", ref _calcFaktor, value);
            }
        }
        #endregion
    }
}