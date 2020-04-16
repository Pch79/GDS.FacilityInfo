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

namespace FacilityInfo.Vertrag.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Produkt")]
   [Serializable]
   [XafDefaultProperty("Betreff")]
   [ImageName("resources_16")]
    public class fiProdukt : BaseObject
    {
        private System.String _name;
        private System.String _beschreibung;
        private System.Decimal _ekpreis;
        private System.Decimal _vkpreis;
        private fiLeistungskatalog _katalog;


        public fiProdukt(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        #region

        [XafDisplayName("Produktbild")]
       // [ImageEditor]
        public byte[] Produktbild
        {
            get
            {
                return GetPropertyValue<byte[]>("Produktbild");
            }
            set
            {
                SetPropertyValue<byte[]>("Produltbild", value);
            }
        }

        [XafDisplayName("Katalog")]
        [Association("boLeistungskatalog-boProdukt")]
        public fiLeistungskatalog Katalog
        {
            get
            {
                return _katalog;
            }
            set
            {
                SetPropertyValue("Katalog", ref _katalog, value);
            }
        }


        [XafDisplayName("Vk-Preis")]
        public System.Decimal VkPreis
        {
            get
            {
                return _vkpreis;
            }
            set
            {
                SetPropertyValue("VkPreis", ref _vkpreis, value);
            }
        }


        [XafDisplayName("Name")]
        [Size(200)]
        public System.String Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue("Name", ref _name, value);
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


        [XafDisplayName("EK-Preis")]
        public System.Decimal EkPreis
        {
            get
            {
                return _ekpreis;
            }
            set
            {
                SetPropertyValue("EkPreis", ref _ekpreis, value);
            }
        }

        #endregion
    }
}