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

namespace FacilityInfo.Artikelverwaltung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Serviceartikel")]
    public class artikelServiceArtikel : artikelArtikelBase
    {
        private Decimal _dauerNominell;
        private Decimal _dauerKalkulation;
       


        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public artikelServiceArtikel(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        #region Properties
        [XafDisplayName("Dauer (nominell)")]
        public Decimal DauerNominell
        {
            get {
                return _dauerNominell;
                    }
            set
            {
                SetPropertyValue("DauerNominell", ref _dauerNominell, value);
            }
        }
        [XafDisplayName("Dauer (kalkuliert)")]
        public Decimal DauerKalkulation
        {
            get
            {
                return _dauerKalkulation;
            }
            set
            {
                SetPropertyValue("DauerKalkulation", ref _dauerKalkulation, value);
            }
        }


        #endregion
    }
}