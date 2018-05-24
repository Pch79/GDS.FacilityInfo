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

namespace FacilityInfo.Datenfeld.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Datenfeld (Produktgruppe)")]
    [XafDefaultProperty("Datenfeld")]
    public class fiDatenfeldProduktgruppe : BaseObject
    {
        private fiDatenfeld _datenfeld;
        //hier schon den Antwortkatalog vorgeben
        private System.Int32 _sortindex;
        public fiDatenfeldProduktgruppe(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Sortindex")]
        public System.Int32 Sortindex
        {
            get
            {
                return _sortindex;
            }
            set
            {
                SetPropertyValue("Sortindex", ref _sortindex, value);
            }
        }
        [XafDisplayName("Datenfeld")]
        public fiDatenfeld Datenfeld
        {
            get
            {
                return _datenfeld;
            }
            set
            {
                SetPropertyValue("Datenfeld", ref _datenfeld, value);
            }
        }
        [Association("fiHerstellerProduktgruppe-fiDatenfeldProduktgruppe")]
        [XafDisplayName("Datenfelder")]
        public XPCollection<fiHerstellerProduktgruppe> lstHerstellerproduktgruppen
        {
            get
            {
                return GetCollection<fiHerstellerProduktgruppe>("lstHerstellerproduktgruppen");
            }
        }
        #endregion
    }
}