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
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Artikelverwaltung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Artikelkatalog")]
    [XafDefaultProperty("MatchKey")]
    [ImageName("book_picture_16")]
    public class artikelArtikelKatalog : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
        private Boolean _aktiv;
        private boMandant _mandant;
        

        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public artikelArtikelKatalog(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Matchkey")]
        public String MatchKey
        {
            get
            {
                var retVal = string.Empty;
                var mandant = string.Empty;
                var bezeichnung = string.Empty;
                mandant = (this.Mandant!=null)?this.Mandant.Mandantenkennung:"N/A";
                bezeichnung = (this.Bezeichnung != null)?this.Bezeichnung:"N/A";
                retVal = String.Format("{0}-{1}", mandant, bezeichnung);
                return retVal;
            }
        }
        [XafDisplayName("Mandant")]
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
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
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
        public String Beschreibung
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
        [XafDisplayName("Aktiv")]
        [CaptionsForBoolValues("ja","nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public Boolean Aktiv
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
        [XafDisplayName("Artikel")]
        [Association("artikelArtikelKatalog-artikelArtikelBase")]
        public XPCollection<artikelArtikelBase> lstArtikel
        {
            get
            {
                return GetCollection<artikelArtikelBase>("lstArtikel");
            }
        }
        #endregion
    }
}