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
    [XafDisplayName("Produktdatenfeld")]
    [XafDefaultProperty("DatenfeldProduktgruppe")]
    public class fiDatenfeldHerstellerprodukt : BaseObject
    {
        private fiDatenfeldProduktgruppe _datenfeldProduktgruppe;
        //Antwortkatalog????
        private fiDatenfeldAntwort _datenfeldAntwort;
        private fiHerstellerProdukt _herstellerprodukt;

        public fiDatenfeldHerstellerprodukt(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        //hier kannn ich die Antworten des Datenitems mappen

        
        [XafDisplayName("Herstellerprodukt")]
        [Association("fiHerstellerProdukt-fiDatenfeldHerstellerprodukt")]
        public fiHerstellerProdukt Herstellerprodukt
        {
            get
            {
                return _herstellerprodukt;
            }
            set
            {
                SetPropertyValue("Herstellerprodukt", ref _herstellerprodukt, value);
            }
        }


        [XafDisplayName("Datenfeld")]
        public fiDatenfeldProduktgruppe DatenfeldProduktgruppe
        {
            get
            {
                return _datenfeldProduktgruppe;
            }
            set
            {
                SetPropertyValue("DatenfeldProduktgruppe", ref _datenfeldProduktgruppe, value);
            }
        }

        [XafDisplayName("Eintragswert")]
        
        [DataSourceProperty("DatenfeldProduktgruppe.Datenfeld.lstDatenfeldAntworten")]
        //[DataSourceCriteria("CONTAINS(lstDatenItems[Oid = '@This.Datenfeld.Oid'])")]
        

        public fiDatenfeldAntwort DatenfeldAntwort
        {
            get
            {
                return _datenfeldAntwort;
            }
            set
            {
                SetPropertyValue("DatenfeldAntwort", ref _datenfeldAntwort, value);
            }
        }

        //den Sortindex hier heraus mappen
         [XafDisplayName("Sortindex")]
         public System.Int32 Sortindex
        {
            get
            {
                System.Int32 retVal = 0;
                if(this.DatenfeldProduktgruppe != null)
                {
                    retVal = this.DatenfeldProduktgruppe.Sortindex;
                }
                return retVal;
            }
        }

        
    }
}