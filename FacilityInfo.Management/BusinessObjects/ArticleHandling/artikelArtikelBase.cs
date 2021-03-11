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
using FacilityInfo.Management.Helpers;
using System.Drawing;

namespace FacilityInfo.Artikelverwaltung.BusinessObjects
{
    [DefaultClassOptions]
     [XafDisplayName("Artikel")]
     [ImageName("box_16")]
     [XafDefaultProperty("KurzText")]
    public class artikelArtikelBase : BaseObject
    {
        private String _artikelNummer;
        private String _eanCode;
        private String _herstellerNummer;
        private String _kurzText;
        private String _langText;
        private artikelArtikelKatalog _artikelKatalog;
        private artikelWarenGruppe _warenGruppe;
        public artikelArtikelBase(Session session)
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
                case "Bild":
                    if (!this.IsLoading)
                    {
                        if (newValue != null)
                        {
                            setMainThumbnail();
                            //hier kan ich auch gleich die Web-Implementierung erstellen
                            setMainImageWeb();
                        }
                        if (newValue == null)
                        {
                            this.MainImageThumb = null;
                            this.MainImageWeb = null;
                        }
                    }
                    break;
            }
        }

        public void makeMainThumbnail()
        {
            setMainThumbnail();
            setMainImageWeb();
        }
        public void setMainThumbnail()
        {
            if (this.Bild != null)
            {
                this.MainImageThumb = PictureHelper.getThumbnailByteArray(this.Bild);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        public void setMainImageWeb()
        {
            if (this.Bild != null)
            {
                Image workingImage = PictureHelper.ImageFromByteArray(this.Bild);
                this.MainImageWeb = PictureHelper.ResizePicByWidth(workingImage, 300);
                this.Save();
                this.Session.CommitTransaction();
            }
        }
        #region Properties
        [XafDisplayName("Warengruppe")]
        [Association("artikelArtikelBase-artikelWarenGruppe")]
        public artikelWarenGruppe WarenGruppe
        {
            get { return _warenGruppe; }
            set { SetPropertyValue("WarenGruppe", ref _warenGruppe, value); }
        }
        [XafDisplayName("Herstellernummer")]
        public String HerstellerNummer
        {
            get
            {
                return _herstellerNummer;
            }
            set
            {
                SetPropertyValue("HerstellerNummer", ref _herstellerNummer, value);
            }
        }

        
        [XafDisplayName("Langtext")]
        [Size(-1)]
        public String Langtext
        {
            get
            {
                return _langText;
            }
            set
            {
                SetPropertyValue("LangText", ref _langText, value);
            }
        }
        [XafDisplayName("Kurztext")]
        
        public String KurzText
        {
            get
            {
                return _kurzText;
            }
            set
            {
                SetPropertyValue("KurzText", ref _kurzText, value);
            }
        }
        [XafDisplayName("Artikelnummer")]
        public String ArtikelNummer
        {
            get
            {
                return _artikelNummer;
            }
            set
            {
                SetPropertyValue("ArtikelNummer", ref _artikelNummer, value);
            }
        }
        [XafDisplayName("EAN-Code")]
        public String EanCode
        {
            get
            {
                return _eanCode;
            }
            set
            {
                SetPropertyValue("EanCode", ref _eanCode, value);
            }
        }

        [XafDisplayName("Artikelkatalog")]
        [Association("artikelArtikelKatalog-artikelArtikelBase")]
        public artikelArtikelKatalog ArtikelKatalog
        {
            get
            {
                return _artikelKatalog;   
            }
            set
            {
                SetPropertyValue("ArtikelKatalog", ref _artikelKatalog, value);
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

        [XafDisplayName("Vorschaubild")]
        public byte[] MainImageThumb
        {
            get
            {
                return GetPropertyValue<byte[]>("MainImageThumb");
            }
            set { SetPropertyValue<byte[]>("MainImageThumb", value); }


        }

        [XafDisplayName("Titelbild (Web)")]
        public byte[] MainImageWeb
        {
            get
            {

                return GetPropertyValue<byte[]>("MainImageWeb");
            }
            set
            {
                SetPropertyValue<byte[]>("MainImageWeb", value);
            }
        }
        #endregion

    }
}