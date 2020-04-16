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
using FacilityInfo.Anlagen.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDefaultProperty("Matchkey")]
    [XafDisplayName("Kostengruppe (DIN 276")]
    [Serializable]
    [ImageName("table_money_16")]
    
    public class boKostenGruppe : BaseObject
    {
        private Int32 _key;
        private System.String _bezeichnung;
        private System.String _kurzBezeichnung;
        private boKostenGruppe _parentItem;

        public boKostenGruppe(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        public System.String Matchkey
        {
            get
            {
                var retval = string.Empty;
                var key = string.Empty;
                var bezeichnung = string.Empty;
                key = (this.Key != 0) ? this.Key.ToString() : "N/A";
                bezeichnung = (this.Bezeichnung != null) ? this.Bezeichnung : "N/A";
                retval = string.Format("{0} {1}", key, bezeichnung);
                return retval;
            }
        }
        [XafDisplayName("Key")]
        [RuleRequiredField]
        public Int32 Key
        {
            get
            {
                return _key;
            }
            set
            {
                SetPropertyValue("Key", ref _key, value);
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
        public System.String KurzBezeichnung
        {
            get
            {
                return _kurzBezeichnung;
            }
            set
            {
                SetPropertyValue("KurzBezeichnung", ref _kurzBezeichnung, value);
            }
        }
        [XafDisplayName("Parent")]
        public boKostenGruppe ParentItem
        {
            get
            {
                return _parentItem;
            }
            set
            {
                SetPropertyValue("ParentItem", ref _parentItem, value);
            }
        }
        [XafDisplayName("Anlagenkategorien")]
        [Association("boKostenGruppe-boAnlagenKategorie")]
        public XPCollection<boAnlagenKategorie> lstAnlagenKategorien
        {
            get
            {
                return GetCollection<boAnlagenKategorie>("lstAnlagenKategorien");
            }
        }

    }
}