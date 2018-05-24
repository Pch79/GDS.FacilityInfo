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
using FacilityInfo.Base.BusinessObjects;

namespace FacilityInfo.GlobalObjects.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("DatenItem")]
    [XafDefaultProperty("Matchkey")]
    [ImageName("brick_edit")]
    
    public class boDatenItem : BaseObject
    {
        private boDatenKategorie _datenItem_kategorie;
        private System.String _datenItem_Bezeichnung;
        private boEinheit _datenItem_Einheit;
        public boDatenItem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        #region Properties
        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var einheit = string.Empty;
                var kategorie = string.Empty;
                var notAssignedString = "N/A";
                bezeichnung = (this.DatenItem_Bezeichnung != null) ? this.DatenItem_Bezeichnung : notAssignedString;
                
                einheit = (this.DatenItem_Einheit != null) ? this.DatenItem_Einheit.Einheit_Kuerzel : notAssignedString;
                //kategorie = (this.DatenItem_Kategorie != null) ? this.DatenItem_Kategorie.DatenKategorie_Bezeichnung : notAssignedString;
                if(einheit == notAssignedString)
                {
                    retVal = string.Format("{0}", bezeichnung);
                }
                else
                {
                    retVal = string.Format("{0}[{1}]", bezeichnung,  einheit);
                }               
                return retVal;
            }
        }


        [XafDisplayName("Kategorie")]
        [Association("boDatenItem-boDatenKategorie")]
        [RuleRequiredField]
        public boDatenKategorie DatenItem_Kategorie
        {
            get
            {
                return _datenItem_kategorie;
            }
            set
            {
                SetPropertyValue("DatenItem_Kategorie", ref _datenItem_kategorie, value);
            }
        }

        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField]
        public System.String DatenItem_Bezeichnung
        {
            get
            {
                return _datenItem_Bezeichnung;
            }
            set
            {
                SetPropertyValue("DatenItem_Bezeichnung", ref _datenItem_Bezeichnung, value);
            }
        }

        [XafDisplayName("Einheit")]
        public boEinheit DatenItem_Einheit
        {
            get
            {
                return _datenItem_Einheit;
            }
            set
            {
                SetPropertyValue("DatenItem_Einheit", ref _datenItem_Einheit, value);
            }
        }


        [XafDisplayName("FI-Objekte")]
        [Association("boFIObjekt-boDatenItem"), DevExpress.ExpressApp.DC.Aggregated]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public XPCollection<boFIObjekt> lstFIObjekte
        {
            get
            {
                return GetCollection<boFIObjekt>("lstFIObjekte");
            }
        }

        //die möglichen Antworten efinieren
        [XafDisplayName("Eintragswerte")]
        [Association("boDatenItem-boDatenValueItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boDatenValueItem> lstDatenValueItems
        {
            get
            {
                return GetCollection<boDatenValueItem>("lstDatenValueItems");
            }
        }
        #endregion
    }
}