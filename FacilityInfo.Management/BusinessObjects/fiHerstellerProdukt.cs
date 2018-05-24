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
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Datenfeld.BusinessObjects;

namespace FacilityInfo.Hersteller.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Produktblatt")]
    [XafDefaultProperty("Bezeichnung")]
    public class fiHerstellerProdukt : BaseObject
    {
        private boHersteller _hersteller;
        private System.String _typbezeichnung;
        private System.String _modellbezeichnung;
        private System.String _produktbeschreibung;
        private fiHerstellerProduktgruppe _produktgruppe;
        private System.String _seriennummer;

        public fiHerstellerProdukt(Session session)
            : base(session)
        {
        }

        #region Methoden
        protected override void OnLoaded()
        {
            base.OnLoaded();
            /*
            if (!this.Session.IsObjectToSave(this))
            {
                if (!this.Session.IsNewObject(this))
                {
                    if (this.Produktgruppe != null)
                    {
                        fiHerstellerProduktgruppe curGruppe = this.Session.GetObjectByKey<fiHerstellerProduktgruppe>
                    (this.Produktgruppe.Oid);

                        generateFields(curGruppe);

                    }
                }

            }
            */
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich die Gruppe ändert müssen die Felder neu geschrieben werden
            switch(propertyName)
            {
                case "Produktgruppe":
                    this.Session.Delete(lstDatenFeldHerstellerprodukt);
                    //die Felder des tps auslesn und hzuordnen
                    if (newValue != null)
                    {
                        fiHerstellerProduktgruppe curGruppe = (fiHerstellerProduktgruppe)newValue;
                        generateFields(curGruppe);
                   
                    }
                    else
                    {
                        this.Session.Delete(lstDatenFeldHerstellerprodukt);

                    }

                    break;
            }
        }

        public void generateFields(fiHerstellerProduktgruppe curGruppe)
        {
            if (curGruppe.lstDatenfeldProduktgruppe != null)
            {
                foreach (fiDatenfeldProduktgruppe item in curGruppe.lstDatenfeldProduktgruppe)
                {
                    //dieFelder neu schreiben
                    //gibt es das Tei schon im aktuellen Produkt???
                    fiDatenfeldHerstellerprodukt curFeld = this.Session.FindObject<fiDatenfeldHerstellerprodukt>(new GroupOperator(new BinaryOperator("Herstellerprodukt.Oid", this.Oid, BinaryOperatorType.Equal), new BinaryOperator("DatenfeldProduktgruppe.Oid", item.Oid, BinaryOperatorType.Equal)));
                    if (curFeld == null)
                    {
                        //das Feild neu erstellen
                        curFeld = new fiDatenfeldHerstellerprodukt(this.Session);
                        curFeld.DatenfeldProduktgruppe = this.Session.GetObjectByKey<fiDatenfeldProduktgruppe>(item.Oid);
                        curFeld.Save();

                        this.lstDatenFeldHerstellerprodukt.Add(curFeld);
                    }
                   
                        
                    

                }
                this.Save();
            }
        }
        #endregion

        #region Properties

        [XafDisplayName("Parameter")]
        [Association("fiHerstellerprodukt-fiProduktparameter")]
        public XPCollection<fiProduktparameter> lstParameter
        {
            get
            {
                return GetCollection<fiProduktparameter>("lstParameter");
            }
        }

        [XafDisplayName("Seriennummer")]
        public System.String Seriennummer
        {

            get
            {
                return _seriennummer;
            }
            set
            {
                SetPropertyValue("Seriennummer", ref _seriennummer, value);
            }
        }
        [XafDisplayName("Modellbezeichnung")]
        public System.String Modellbezeichnung
        {
            get
            {
                return _modellbezeichnung;
            }
            set
            {
                SetPropertyValue("Modellbezeichnung", ref _modellbezeichnung, value);
            }
        }
            
        [XafDisplayName("Produktgruppe")]
        [ImmediatePostData]
        public fiHerstellerProduktgruppe Produktgruppe
        {
            get
            {
                return _produktgruppe;
            }
            set
            {
                SetPropertyValue("Produktgruppe", ref _produktgruppe, value);
            }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
        {
            get
            {
                return _produktbeschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _produktbeschreibung, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        public System.String Bezeichnung
        {
            get
            {
                return _typbezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnung", ref _typbezeichnung, value);
            }
        }


        [XafDisplayName("Hersteller")]
        [Association("fiHersteller-fiHerstellerProdukt")]
        public boHersteller Hersteller
        {
            get
            {
                return _hersteller;
            }
            set
            {
                SetPropertyValue("Hersteller", ref _hersteller, value);
            }
        }

        [XafDisplayName("Produktbild")]
        [ImageEditor]
        [Delayed]
        public byte[] Produktbild
        {
            get
            {
                return GetDelayedPropertyValue<byte[]>("Produktbild");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("Produktbild", value);
            }
        }

        [XafDisplayName("Dokumente")]
        [Association("fiHerstellerProdukt-fiHerstellerProduktAttachment")]
        public XPCollection<fiHerstellerProduktAttachment> lstDokumente
        {
            get
            {
                return GetCollection<fiHerstellerProduktAttachment>("lstDokumente");
            }
        }

        [XafDisplayName("Bauteile")]
        [Association("fiHerstellerProdukt-fiBauteil")]
        public XPCollection<fiBauteil> lstBauteile
        {
            get
            {
                return GetCollection<fiBauteil>("lstBauteile");
            }
        }

        [XafDisplayName("Datenfelder")]
        [Association("fiHerstellerProdukt-fiDatenfeldHerstellerprodukt")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<fiDatenfeldHerstellerprodukt> lstDatenFeldHerstellerprodukt
        {
            get
            {
                return GetCollection<fiDatenfeldHerstellerprodukt>("lstDatenFeldHerstellerprodukt");
            }
        }




        #endregion

        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

      



        #endregion
    }
}