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
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Nummernkreis")]
    [XafDefaultProperty("Matchkey")]
    [ImageName("counter_16")]
    
    [RuleObjectExists(DefaultContexts.Save, criteria: "Objekt = '@this.Objekt' AND Mandant.Oid = '@this.Mandant.Oid' AND GueltigAb = '@this.GueltigAb' AND GueltigBis = '@this.GueltigBis' ", InvertResult =true)]
    public class boNummernkreis : BaseObject
    {
        private System.Int32 _fortlaufendeNummer;
        private boMandant _mandant;
        private System.DateTime _gueltigab;
        private System.DateTime _gueltgigbis;
        private System.String _praefix;
        private System.String _suffix;
        private System.String _name;
        private Type _objekt;

        private System.String curMandantID;
        public boNummernkreis(Session session)
            : base(session)
        {
            curMandantID = clsStatic.loggedOnMandantOid;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            curMandantID = clsStatic.loggedOnMandantOid;
            //hier gleich den Mandanten setzen
            this.Mandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));

        }

         
        
        
        #region Properties

        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                //Mandant + Objekt
                var mandant = string.Empty;
                var objekt = string.Empty;
                mandant = (this.Mandant != null) ? this.Mandant.Mandantenname : "N/A";
                objekt = (this.Objekt != null) ? this.Objekt.Name : "N/A";
                retVal = string.Format("{0} - {1}", objekt, mandant);
                return retVal;
            }
        }
        [XafDisplayName("Fortlaufende Nummer")]
        public System.Int32 FortlaufendeNummer
        {
            get
            {
                return _fortlaufendeNummer;
            }
            set
            {
                SetPropertyValue("FortlaufendeNummer", ref _fortlaufendeNummer, value);
            }
        }
        [XafDisplayName("Mandant")]
        [Association("boMandant-boNummernkreis")]
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
        [XafDisplayName("Gültig ab")]
        public System.DateTime GueltigAb
        {
            get
            {
                return _gueltigab;
            }
            set
            {
                SetPropertyValue("GueltigAb", ref _gueltigab, value);
            }
        }
        [XafDisplayName("Gültig bis")]
        public System.DateTime GueltigBis
        {
            get
            {
                return _gueltgigbis;
            }
            set
            {
                SetPropertyValue("GueltigBis", ref _gueltgigbis, value);
            }
        }
        [XafDisplayName("Präfix")]
        public System.String Praefix
        {
            get
            {
                return _praefix;
            }
            set
            {
                SetPropertyValue("Praefix", ref _praefix, value);
            }
        }
        [XafDisplayName("Suffix")]
        public System.String Suffix
        {
            get
            {
                return _suffix;
            }
            set
            {
                SetPropertyValue("Suffix", ref _suffix, value);
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
        [XafDisplayName("Objekt")]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
       
        public Type Objekt
        {
            get
            {
                return _objekt;
            }
            set
            {
                SetPropertyValue("Objekt", ref _objekt, value);
            }
        }
        public System.String NextNumber
        {
            get
            {
                var retVal = string.Empty;
                //was passiert mit null-einträgen
                var praefix = string.Empty;
                var suffix = string.Empty;
                var number = String.Empty;
                praefix = (this.Praefix != null) ? this.Praefix : string.Empty;
                suffix = (this.Suffix != null) ? this.Suffix : string.Empty;
                number = this.FortlaufendeNummer.ToString();
                retVal = String.Format("{0}{1}{2}", praefix, number, suffix);
                return retVal;
            }
        }
            

        #endregion

    }
}