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

namespace FacilityInfo.Datenfeld.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Antwortkatalog (Herstellerprodukt)")]
    [XafDefaultProperty("Antwort")]
    public class fiDatenFeldHerstellerProduktAntwort : BaseObject
    {
        private fiDatenfeldAntwort _antwort;
        private fiDatenfeldHerstellerprodukt _datenFeldHerstellerProdukt;

        //hier muss dan die Aktion rein

        public fiDatenFeldHerstellerProduktAntwort(Session session)
            : base(session)
        {
        }
        #region Properties
        //[Association("fiDatenfeldHerstellerprodukt-fiDatenFeldHerstellerProduktAntwort")]
        public fiDatenfeldHerstellerprodukt DatenfeldHerstellerprodukt
        {
            get
            {
                return _datenFeldHerstellerProdukt;
            }
            set
            {
                SetPropertyValue("DatenfeldHerstellerprodukt", ref _datenFeldHerstellerProdukt, value);
            }
        }
        
        public fiDatenfeldAntwort Antwort
        {
            get
            {
                return _antwort;
            }
            set
            {
                SetPropertyValue("Antwort", ref _antwort, value);
            }
        }
        #endregion
        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #endregion
      
    }
}