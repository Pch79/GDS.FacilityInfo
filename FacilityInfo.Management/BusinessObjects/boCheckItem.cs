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

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Prüfpunkt")]
    [XafDefaultProperty("Bezeichnung")]
    [Serializable]
   // [ImageName("")]
    public class boCheckItem : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private boCheckItemKategorie _kategorie;
        public boCheckItem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
        #region Properties

        [XafDisplayName("Kategorie")]
        [Association("boCheckItem-boCheckItemKategorie")]
        [RuleRequiredField]
        public boCheckItemKategorie Kategorie
        {
            get
            {
                return _kategorie;
            }
            set
            {
                SetPropertyValue("Kategorie", ref _kategorie, value);
            }
        }
        [XafDisplayName("Ergebniswerte")]
        [Association("boCheckItem-boCheckItemValue"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boCheckItemValue> lstItemValues
        {
            get
            {
                return GetCollection<boCheckItemValue>("lstItemValues");
            }
        }


        [XafDisplayName("Checkliste")]
        [Association("boCheckliste-boCheckItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boCheckliste> CheckListe
        {
            get
            {
                return GetCollection<boCheckliste>("CheckListe");
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
        #endregion
        
    }
}