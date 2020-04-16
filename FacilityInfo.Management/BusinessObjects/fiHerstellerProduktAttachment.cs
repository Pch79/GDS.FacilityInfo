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
    [XafDisplayName("Produktdokument")]
        
    public class fiHerstellerProduktAttachment : boAttachment
    {
        private fiHerstellerProdukt _herstellerProdukt;
        private boHersteller _hersteller;
        public fiHerstellerProduktAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //hier auch wieder die Bibliothek zuordnen
                //prodDoc
                boAttachmentBibliothek chosenLibary = this.Session.FindObject<boAttachmentBibliothek>(new BinaryOperator("Key", "prodDoc", BinaryOperatorType.Equal));
                if(chosenLibary != null)
                {
                    this.Bibliothek = chosenLibary;
                }
                //wenn das Produkt da ist
               if(this.HerstellerProdukt != null)
            {

            }

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "HerstellerProdukt":
                    if (newValue != null)
                    {
                        fiHerstellerProdukt curProduct = (fiHerstellerProdukt)newValue;
                        boHersteller curHersteller = this.Session.GetObjectByKey<boHersteller>(curProduct.Hersteller.Oid);                        
                        this.Betreff = curProduct.Bezeichnung;
                        this.HerstellerProdukt = this.Session.GetObjectByKey<fiHerstellerProdukt>(curProduct.Oid);
                        this.Hersteller = curHersteller;
                      

                    }
                    else
                    {
                        this.Betreff = null;
                        this.Hersteller = null;
                        this.HerstellerProdukt = null;
                    }
                    break;
            }
        }

        [XafDisplayName("Produkt")]
        [Association("fiHerstellerProdukt-fiHerstellerProduktAttachment")]
        public fiHerstellerProdukt HerstellerProdukt
        {
            get
            {
                return _herstellerProdukt;
            }
            set { SetPropertyValue("HerstellerProdukt", ref _herstellerProdukt, value); }
        }

        [XafDisplayName("Hersteller")]
        public boHersteller Hersteller
        {
            get { return _hersteller; }
            set { SetPropertyValue("Hersteller", ref _hersteller, value); }
        }
       
    }
}