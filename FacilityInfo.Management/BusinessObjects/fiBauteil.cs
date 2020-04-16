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

namespace FacilityInfo.Hersteller.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Bauteil")]
    [ImageName("cooler_16")]
    [DefaultProperty("Bauteilkatalog")]

    public class fiBauteil : BaseObject
    {
        private System.String _herstellernummer;
        private boHersteller _hersteller;
        private System.String _bauteilkennung;
        private System.String _beschreibung;
        private fiBauteilkatalog _bauteilkatalog;


        public fiBauteil(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region properties
        [XafDisplayName("Bauteilkatalog")]
        public fiBauteilkatalog Bauteilkatalog
        {
            get
            {
                return _bauteilkatalog;
            }
            set
            {
                SetPropertyValue("Bauteilkatalog", ref _bauteilkatalog, value);
            }
        }
        [XafDisplayName("Image")]
     //   [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 30, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
     //   [Delayed]
        public byte[] Bild
        {
            get
            {
                return GetPropertyValue<byte[]>("Bild");
            }
            set
            {
                SetPropertyValue<byte[]>("Bild", value);
            }
        }
        [XafDisplayName("Herstellernummer")]
        public System.String herstellernummer
        {
            get
            {
                return _herstellernummer;
            }
            set
            {
                SetPropertyValue("Herstellernummer", ref _herstellernummer, value);
            }
        }
        [XafDisplayName("Hersteller")]
        public boHersteller Hersteller
        {
            get
            {
                return _hersteller;
            }
            set
            {
                SetPropertyValue("Hersteller", ref _hersteller, value)
;            }
        }
        [XafDisplayName("Bauteilkennung")]
        public System.String Bauteilkennung
        {
            get
            {
                return _bauteilkennung;
            }
            set
            {
                SetPropertyValue("Bauteilkennung", ref _bauteilkennung, value);
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

        

        //beziehung zu den Herstellerprodukten
        [XafDisplayName("Produkte")]
        [Association("fiHerstellerProdukt-fiBauteil")]
        public XPCollection<fiHerstellerProdukt> lstHerstellerprodukte
        {
            get
            {
                return GetCollection<fiHerstellerProdukt>("lstHerstellerprodukte");
            }
        }
        #endregion
    }
}