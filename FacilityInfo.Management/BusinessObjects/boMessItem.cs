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
using FacilityInfo.GlobalObjects.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Base.BusinessObjects;

namespace FacilityInfo.Messung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDefaultProperty("Matchkey")]
    [ImageName("measure")]
    [XafDisplayName("Messpunkt")]
    
    public class boMessItem : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _kurzbezeichnung;
        private boEinheit _einheit;
        private AnPruefPunkt _pruefpunkt;

        public boMessItem(Session session)
            : base(session)
        {
        }
        #region Methoden

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "Pruefpunkt":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            //steht schon was in der Bezeichnung?
                            var curBezeichnung = string.Empty;
                            curBezeichnung = this.Bezeichnung;
                            if (this.Bezeichnung != string.Empty)
                            {

                                this.Bezeichnung = String.Format("{0} - {1}", ((AnPruefPunkt)newValue).Bezeichnung, curBezeichnung);

                            }
                            else
                            {
                                this.Bezeichnung = string.Format("{0}", ((AnPruefPunkt)newValue).Bezeichnung);
                            }
                        }
                        else
                        {
                            this.Bezeichnung = string.Empty;
                        }
                    }
                    break;
            }
        }
        #endregion
        #region Properties

        [XafDisplayName("Prüfpunkt")]
        public AnPruefPunkt Pruefpunkt
        {
            get
            {
                return _pruefpunkt;
            }
            set
            {
                SetPropertyValue("Pruefpunkt", ref _pruefpunkt, value);
            }
        }

        [XafDisplayName("Bezeichnung")]
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

        [XafDisplayName("Kurzbezeichnung")]
        public System.String Kurzbezeichnung
        {
            get
            {
                return _kurzbezeichnung;
                    
            }
            set
            {
                SetPropertyValue("Kurzbezeichnung", ref _kurzbezeichnung, value);
            }
        }

        [XafDisplayName("Einheit")]
        public boEinheit Einheit
        {
            get
            {
                return _einheit;
            }
            set
            {
                SetPropertyValue("Einheit", ref _einheit, value);
            }
        }
        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var bezeichnung = string.Empty;
                var einheit = string.Empty;
                var retVal = string.Empty;
                bezeichnung = (this.Bezeichnung != string.Empty) ? this.Bezeichnung : "N/A";
                einheit = (this.Einheit != null) ? this.Einheit.Einheit_Kuerzel : "N/A";
                retVal = string.Format("{0} [{1}]", bezeichnung, einheit);
                return retVal;
            }
        }       
        #endregion
    }
}