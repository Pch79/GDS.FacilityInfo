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
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Produktattachment")]
        
    public class fiHerstellerProduktAttachment : boAttachment
    {
        //private fiHerstellerProdukt _herstellerprodukt;
        public fiHerstellerProduktAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "Produkt":
                    if (newValue != null)
                    {
                        /*
                        fiHerstellerProdukt selectedProdukt = this.Session.GetObjectByKey<fiHerstellerProdukt>(this.Produkt.Oid);
                        this.Parentkey = selectedProdukt.Oid.ToString();
                        this.Objektkey = selectedProdukt.Hersteller.Oid.ToString();
                        */

                    }
                    break;
            }
        }
        [XafDisplayName("Produkte")]
        [Association("fiHerstellerProdukt-fiHerstellerProduktAttachment")]
        public XPCollection<fiHerstellerProdukt> lstHerstellerprodukte
        {
            get
            {
                return GetCollection<fiHerstellerProdukt>("lstHerstellerprodukte");
            }
        }
        /*
        [XafDisplayName("Produkt")]
        [Association("fiHerstellerProdukt-fiHerstellerProduktAttachment")]
        public fiHerstellerProdukt  Produkt
        {
            get
            {
                return _herstellerprodukt;
            }
            set
            {
                SetPropertyValue("Produkt", ref _herstellerprodukt, value);
            }
        }
        */
    }
}