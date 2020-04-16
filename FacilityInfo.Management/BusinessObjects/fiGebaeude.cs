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
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Management.Helpers;
using System.Drawing;
using FacilityInfo.Anlagen.BusinessObjects;

namespace FacilityInfo.Building.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Gebäude")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("real_estate_16")]

    public class fiGebaeude : BaseObject
    {
        private System.String _bauteilnummer;
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private boLiegenschaft _liegenschaft;
        private System.String _notiz;
        private boAdresse _adresse; 
        //TODO Gebäudetypen einführen
        public fiGebaeude(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            //Erdgeschoss anlegen
            createEbene("EG",0);
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            
        }

        public void createEbene(string kuerzel, Int32 sortPosition)
        {
            fiEbene eg = new fiEbene(this.Session);
            fiEbenenart curArt = this.Session.FindObject<fiEbenenart>(new BinaryOperator("Kuerzel", kuerzel, BinaryOperatorType.Equal));
            eg.Art = curArt;
            eg.Sortposition = curArt.DefaultSortPosition;
            eg.Gebaeude = this;
            eg.Save();
            this.lstEbenen.Add(eg);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //gibt es ein Erdgeschoss und ein Kellergeschoss??
            // wen
            if (this.lstEbenen != null)
            {
                Int32 egCount = this.lstEbenen.Where(t => t.Art.Kuerzel == "EG").Count();
                if(egCount == 0)
                {
                    createEbene("EG",0);
                } 

            }

            //wenn keine Gebäudeadresse da ist die Adresse der Liegenaschaft eintragen
            if(this.Adresse == null)
            {
                if(this.Liegenschaft != null)
                {
                    if(this.Liegenschaft.Liegenschaftsadresse != null)
                    {
                        this.Adresse = this.Session.GetObjectByKey<boAdresse>(this.Liegenschaft.Liegenschaftsadresse.Oid);
                        this.Save();
                    }
                }
            }
        }
        //wenn eine Adresse eingegeben wird dann die Bezeichnung aus der Adresse nehmen
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.Session.IsObjectToDelete(this))
            {
                switch (propertyName)
                {

                    case "MainImage":
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
                    case "Liegenschaft":
                        if (!this.IsLoading)
                        {
                            if (newValue != null)
                            {
                                boLiegenschaft chosenLg = (boLiegenschaft)newValue;
                                var retVal = string.Empty;
                                var workingString = chosenLg.Bezeichnung;
                                Int32 startIndex = 0;
                                if (newValue != null)
                                {
                                    if (workingString != null)
                                    {
                                        if (workingString.StartsWith("LG"))
                                        {
                                            startIndex = 3;
                                        }
                                        if (workingString.StartsWith("ETG"))
                                        {
                                            startIndex = 4;
                                        }
                                        retVal = workingString.Substring(startIndex);
                                        this.Bezeichnung = retVal;
                                    }
                                }
                                else
                                {
                                    this.Bezeichnung = null;
                                }


                                //hier auch gleich prüfen ob eine Adresse vorhanden ist 
                                if (chosenLg.Liegenschaftsadresse != null)
                                {
                                    this.Adresse = this.Session.GetObjectByKey<boAdresse>(chosenLg.Liegenschaftsadresse.Oid);

                                }
                            }
                        }
                        break;
                }
            }
        }
        public void setMainImageWeb()
        {
            if (this.MainImage != null)
            {
                Image workingImage = PictureHelper.ImageFromByteArray(this.MainImage);
                this.MainImageWeb = PictureHelper.ResizePicByWidth(workingImage, 400);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        public void setMainThumbnail()
        {
            if (this.MainImage != null)
            {
                this.MainImageThumb = PictureHelper.getThumbnailByteArray(this.MainImage);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        [XafDisplayName("Titelbild")]
        [Delayed(true)]
        public byte[] MainImage
        {
            get
            {
                return GetDelayedPropertyValue<byte[]>("MainImage");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("MainImage", value);
            }
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

        [XafDisplayName("Vorschaubild")]
        public byte[] MainImageThumb
        {
            get
            {
                return GetPropertyValue<byte[]>("MainImageThumb");
            }
            set { SetPropertyValue<byte[]>("MainImageThumb", value); }


        }
        //Bilder -> vgl Liegenschaften

        [XafDisplayName("Gebäudeadresse")]
        public boAdresse Adresse
        {
        get
        {
                return _adresse;
        }
        set
        {
                SetPropertyValue("Adresse", ref _adresse, value);
        }
        }

        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                boMandant retVal;
                retVal = (this.Liegenschaft != null) ? this.Liegenschaft.Mandant : null;
                return retVal;
            }
        }

        [XafDisplayName("Notiz")]
        [Size(-1)]
        public System.String Notiz
        {
            get
            {
                return _notiz;
            }
            set
            {
                SetPropertyValue("Notiz", ref _notiz, value);
            }
        }
        [XafDisplayName("Bauteilnummer")]
        public System.String Bauteilnummer
        {
            get
            {
                return _bauteilnummer;

            }
            set
            {
                SetPropertyValue("Bauteilnummer", ref _bauteilnummer, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        public System.String Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
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
        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-fiGebaeude")]
        public boLiegenschaft Liegenschaft
        {
            get
            {
                return _liegenschaft;
            }
            set
            {
                SetPropertyValue("Liegenschaft", ref _liegenschaft, value);
            }
        }

        

        [XafDisplayName("Ebenen")]      
        [Association("fiGebaeude-fiEbene")]
        public XPCollection<fiEbene> lstEbenen
        {
            get
            {
                return GetCollection<fiEbene>("lstEbenen");
            }
        }

        [XafDisplayName("Räume")]
        
        [Association("fiGebaeude-fiRaum"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<fiRaum> lstRaeume
        {
            get
            {
                return GetCollection<fiRaum>("lstRaeume");
            }
        }

        [XafDisplayName("Anlagen")]
        public XPCollection<boAnlage> lstAnlagen
        {
            get
            {
                XPCollection<boAnlage> lstRetVal = new XPCollection<boAnlage>(this.Session, new BinaryOperator("Building.Oid",this.Oid, BinaryOperatorType.Equal));
                return lstRetVal;
            }
        }
    }
}