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

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Produktparameter")]
    public class fiProduktparameter : fiParameter
    {
        private System.String _beschreibung;
        private System.String _menuepunkt;
        private fiHerstellerProdukt _herstellerprodukt;
        public fiProduktparameter(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
       [XafDisplayName("Beschreibung")]
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
        [XafDisplayName("Menüeintrag")]
        public System.String Menuepunkt
        {
            get
            {
                return _menuepunkt;
            }
            set
            {
                SetPropertyValue("Menuepunkt", ref _menuepunkt, value);
            }
        }
        [XafDisplayName("Herstellerprodukt")]
        [Association("fiHerstellerprodukt-fiProduktparameter")]
        public fiHerstellerProdukt Herstellerprodukt
        {
            get
            {
                return _herstellerprodukt;
            }
            set
            {
                SetPropertyValue<fiHerstellerProdukt>("Herstellerprodukt", ref _herstellerprodukt, value);
            }
        }
    }
}