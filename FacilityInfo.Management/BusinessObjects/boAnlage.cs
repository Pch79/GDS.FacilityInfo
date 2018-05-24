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
using FacilityInfo.GlobalObjects.EnumStore;
using FacilityInfo.GlobalObjects.BusinessObjects;
using System.Drawing;
using FacilityInfo.GlobalObjects.Helpers;
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Building.BusinessObjects;
using FacilityInfo.Messung.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Management;
using FacilityInfo.Datenfeld.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Service.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using FacilityInfo.Bildverarbeitung.BusinessObjects;

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlage")]
    [ImageName("centos_16")]
    [XafDefaultProperty("AnlagenNummer")]
   
    public class boAnlage : BaseObject //,ITreeNode,ITreeNodeImageProvider
    {

       
        private System.String _bezeichnung;
        private System.String _anlagennummer;
        private System.String _anlagencode;
        //private System.String _ansprechcode;
        private boAdresse _anlagenAdresse;
        private boAdresse _rechnungsadresse;
        private System.String _beschreibung;

        private boMandant _mandant;
        private boAnlage _parentAnlage;
        private System.String _fremdsystemId;
        private boAnlagenGruppe _anlagenGruppe;
        private boAnlagenArt _anlagenArt;
        private boLiegenschaft _liegenschaft;
        //Zustand und Status
        private enmAnlagenStatus _anlagenstatus;
        private enmZustand _zustand;
        private System.String _seriennummer;
        //technische Daten global
        private Int32 _baujahr;
        private System.DateTime _einbaudatum;
        private System.String _typbezeichnung;
        private fiHerstellerProdukt _typ;
        private boHersteller _hersteller;
        private boAdresse _anlagenbetreuer;
        private System.String _notiz;

        private LgHaustechnikKomponente _lghaustechnikkomponente;

        //Standort
        private fiGebaeude _gebaeude;
        private fiEbene _ebene;
        private fiRaum _raum;

        



        private System.String curMandantID = string.Empty;
        public boAnlage(Session session)
            : base(session)
        {
            curMandantID = clsStatic.loggedOnMandantOid;
        }
        
      

        public override void AfterConstruction()
        {
            base.AfterConstruction();
           curMandantID =  clsStatic.loggedOnMandantOid;
            this.Anlagenstatus = enmAnlagenStatus.Aktiv;

        }
       
     
            

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            
                    //wenn es sich 
                    switch (propertyName)
            {
                case "Liegenschaft":

                    if(this.Rechnungsadresse == null)
                    {
                        if(newValue != null)
                        {
                            
                            setRechnungsAdresse(((boLiegenschaft)newValue).Liegenschaftsadresse);
                        }
                    }

                    if(this.AnlagenAdresse == null)
                    {
                        if(newValue != null)
                        {
                            setAnlagenAdresse(((boLiegenschaft)newValue).Liegenschaftsadresse);
                        }
                    }

                    //TODO: hier auch den Mandanten noch prüfen

                    break;

                case "AnlagenArt":
                    if (newValue != null)
                    {
                        this.AnlagenGruppe = ((boAnlagenArt)newValue).AnlagenGruppe;
                        boMandant curMandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));

                        this.AnlagenNummer = createNumber(curMandant);
                    }
                    break;
               
                case "Hersteller":
                    this.Typ = null;
                    break;

                //wenn sich der Typ ändert dann die Felder anschreiben
                case "Typ":
                    //die Felder anschreiben
                    this.Session.Delete(this.lstAnlagenfelder);
                    if (newValue != null)
                    {
                        fiHerstellerProdukt curProdukt = (fiHerstellerProdukt)newValue;
                        generateAnlagenfields(curProdukt);
                        /*
                        if(curProdukt.lstDatenFeldHerstellerprodukt != null)
                        {
                            //Anlagenfelder
                            foreach(fiDatenfeldHerstellerprodukt item in curProdukt.lstDatenFeldHerstellerprodukt)
                            {
                                fiAnlagenfeld curAnlagenfeld = this.Session.FindObject<fiAnlagenfeld>(new GroupOperator(new BinaryOperator("Anlage.Oid", this.Oid, BinaryOperatorType.Equal), new BinaryOperator("DatenfeldHerstellerprodukt.Oid", item.Oid, BinaryOperatorType.Equal)));
                                if (curAnlagenfeld == null)
                                {
                                    curAnlagenfeld = new fiAnlagenfeld(this.Session);
                                     fiDatenfeldHerstellerprodukt curField = this.Session.GetObjectByKey<fiDatenfeldHerstellerprodukt>(item.Oid);
                                    curAnlagenfeld.DatenfeldHerstellerprodukt = curField;
                                    if(curField.DatenfeldAntwort != null)
                                    {
                                        fiDatenfeldAntwort curAntwort = this.Session.GetObjectByKey<fiDatenfeldAntwort>(curField.DatenfeldAntwort.Oid);
                                        curAnlagenfeld.DatenfeldAntwort = curAntwort;
                                    }

                                    //die Antwort holen
                                    //gibt es in dem gefunden Feld eine Antwortvorbelegung?

                                    
                                    
                                    
                                   
                                    curAnlagenfeld.Save();
                                    this.lstAnlagenfelder.Add(curAnlagenfeld); 
                                }
                            }
                        } 
                        */                      
                    }
                    else
                    {
                        this.Session.Delete(this.lstAnlagenfelder);
                    }
                    break;
            }
        }

        public void renewAntwort()
        {
            //Prüfen ob die Antworten sich geändert haben

        }


        public void generateAnlagenfields(fiHerstellerProdukt curProdukt)
        {
            if (curProdukt.lstDatenFeldHerstellerprodukt != null)
            {
                //Anlagenfelder
                foreach (fiDatenfeldHerstellerprodukt item in curProdukt.lstDatenFeldHerstellerprodukt)
                {
                    fiAnlagenfeld curAnlagenfeld = this.Session.FindObject<fiAnlagenfeld>(new GroupOperator(new BinaryOperator("Anlage.Oid", this.Oid, BinaryOperatorType.Equal), new BinaryOperator("DatenfeldHerstellerprodukt.Oid", item.Oid, BinaryOperatorType.Equal)));
                    fiDatenfeldHerstellerprodukt curField = this.Session.GetObjectByKey<fiDatenfeldHerstellerprodukt>(item.Oid);
                    if (curAnlagenfeld == null)
                    {
                        curAnlagenfeld = new fiAnlagenfeld(this.Session);
                    }
                        //fiDatenfeldHerstellerprodukt curField = this.Session.GetObjectByKey<fiDatenfeldHerstellerprodukt>(item.Oid);
                        curAnlagenfeld.DatenfeldHerstellerprodukt = curField;

                        if (curAnlagenfeld.DatenfeldAntwort == null)
                        {
                            if (curField.DatenfeldAntwort != null)
                            {
                                fiDatenfeldAntwort curAntwort = this.Session.GetObjectByKey<fiDatenfeldAntwort>(curField.DatenfeldAntwort.Oid);
                                //was mach ich mit dem Antworteintrag??
                                curAnlagenfeld.DatenfeldAntwort = curAntwort;
                            }
                        }


                        curAnlagenfeld.Save();
                        this.lstAnlagenfelder.Add(curAnlagenfeld);
                    }
                }
            }
        protected override void OnLoading()
        {
            base.OnLoading();
           

        }


        protected override void OnLoaded()
        {
            base.OnLoaded();
            /*
                if (this.Liegenschaft != null)
                {
                    this.Mandant = (this.Liegenschaft.Mandant != null) ? this.Session.GetObjectByKey<boMandant>(this.Liegenschaft.Mandant.Oid) : null;
                    if(this.Mandant != null)
                    {
                        this.Save();
                    }
                }

            */
            /*
            #region Anlagennummer
            if (this.AnlagenNummer != null)
            {
                if (this.AnlagenNummer.Contains("N/A"))
                {
                    //dann prüfen obs einen Acode gibt
                    if (this.AnlagenArt.Ansprechcode != null)
                    {
                        this.AnlagenNummer = this.AnlagenNummer.Replace("N/A", this.AnlagenArt.Ansprechcode);
                    }
                }
            }

            else
            {
                //
                boMandant curMandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));

                this.AnlagenNummer = createNumber(curMandant);
            }
            #endregion
            */
            /*
            #region Anlagencode

            if(this.Anlagencode != null)
            {
                if (this.Anlagencode.Contains("N/A"))
                {
                    //dann prüfen obs einen Acode gibt
                    if (this.AnlagenArt.Ansprechcode != null)
                    {
                        this.Anlagencode = this.Anlagencode.Replace("N/A", this.AnlagenArt.Ansprechcode);
                    }
                }

            }
            #endregion
            */
            #region Anlagenfelder
            /*
            if(this.Typ != null)
            {
                fiHerstellerProdukt curProdukt = this.Session.GetObjectByKey<fiHerstellerProdukt>(this.Typ.Oid);
                generateAnlagenfields(curProdukt);
            }
            */

            #endregion

            //this.Session.CommitTransaction();
           
        }

        //wenn die Nummer "N/A" enthält dann 
        private System.String doRecode()
        {
            var retVal = string.Empty;
            if(this.AnlagenNummer != null && this.AnlagenNummer.Contains("N/A"))
            {
                //dann das NA durch den Acode ersetzen
                if(this.AnlagenArt.Ansprechcode != null)
                {
                    retVal = this.AnlagenNummer.Replace("N/A", this.AnlagenArt.Ansprechcode);
                }
            }
            return retVal;
        }


        protected override void OnSaved()
        {
            base.OnSaved();
        }

        private void generateAnlagencode()
        {
            var acode = string.Empty;
            acode = this.AnlagenArt.Ansprechcode;
        }

        private void saveImageCopy()
        {
            Bitmap addedImage = PictureHelper.byteArrayToBitmap(this.Mainimage);


            Bitmap copy = new Bitmap(addedImage.Width, addedImage.Height);

            using (Graphics gr = Graphics.FromImage(copy))
            {
                Rectangle myRec = new Rectangle(0, 0, copy.Width, copy.Height);
                gr.DrawImage(addedImage, myRec, myRec, GraphicsUnit.Pixel);
            }
            this.MainImageOriginal = PictureHelper.imageToByteArray(copy);
        }

        private void setWatermark()
        {
            // und dann die Watermark setzen
            if (this.Mandant != null)
            {
                //das Wasserzeichen für den Mandanten suchen
                boWatermark curWatermark = this.Session.GetObjectByKey<boWatermark>(this.Mandant.Wasserzeichen.Oid);
                if (curWatermark != null)
                {
                    //die Einstellungen auslesen
                    //1. Wo wird Positioniert
                    //das Bild an sich
                    System.Drawing.Image curWaterMarkImage = PictureHelper.byteArrayToImage(curWatermark.Wasserzeichen);
                    System.Drawing.Image curMainImage = PictureHelper.byteArrayToImage(this.MainImageOriginal);
                    this.Mainimage = PictureHelper.imageToByteArray(PictureHelper.SetWaterMark(curMainImage, curWaterMarkImage, curWatermark.Vertical, curWatermark.Horizontal, curWatermark.Breite, curWatermark.Hoehe));
                }
                //this.Liegenschaft.Mandant.Watermark
                //this.Bild = PictureHelper.SetWaterMark(this.Bild, this.Liegenschaft.Mandant.Watermark);
            }
        }



        private System.String  createNumber(boMandant selectedMandant)
        {
            Type curType = this.GetType();
           
            var nummer = this.Session.FindObject<boNummernkreis>(new GroupOperator(new BinaryOperator("Objekt", curType, BinaryOperatorType.Equal), new BinaryOperator("Mandant.Oid", selectedMandant.Oid, BinaryOperatorType.Equal),
                     new BinaryOperator("GueltigAb", DateTime.Now, BinaryOperatorType.LessOrEqual),
                     new BinaryOperator("GueltigBis", DateTime.Now, BinaryOperatorType.GreaterOrEqual)));
            var retVal = string.Empty;



            //dann noch den Acode dazuholen
            var acode = string.Empty;
            if(this.AnlagenArt != null)
            {
                if (this.AnlagenArt.Ansprechcode != null)
                {
                    acode = this.AnlagenArt.Ansprechcode;
                }
               else
                {
                    acode = "N/A";
                }
            }
            if (nummer != null)
            {
                //jetzt das Teil zusammenbauen
                 retVal = string.Format("{0}{1}-{2}{3}", nummer.Praefix, acode, nummer.FortlaufendeNummer, nummer.Suffix);
                createCode(acode, nummer.FortlaufendeNummer);
                //retVal = nummer.NextNumber;
                nummer.FortlaufendeNummer = nummer.FortlaufendeNummer + 1;
                nummer.Save();
            }
            return retVal;
        }

        private void createCode(string acode,System.Int32 nummer)
        {
            var retVal = string.Empty;
            //Acode(4-Stellig)+Nummer
            var Ansprechcode = string.Empty;
            var Nummer = string.Empty;
            Ansprechcode = acode.PadRight(4, ' ');
            Nummer = nummer.ToString();
            retVal = string.Format("{0}-{1}", Ansprechcode, Nummer);
            this.Anlagencode = retVal;
            
        }





      
       
 /*

            #region ITReeNode
            IBindingList ITreeNode.Children
        {
            get
            {
                return lstUnteranlagen;
            }
        }
        String ITreeNode.Name
        {
            get
            {
                return AnlagenNummer;
            }
        }

        ITreeNode ITreeNode.Parent
        {
            get
            {
                return ParentAnlage;
            }
        }
        #endregion
        #region iTreeImageProvider
        public System.Drawing.Image GetImage(out string imageName)
        {
            if(lstUnteranlagen != null && lstUnteranlagen.Count>0)
            {
                imageName = "BO_Category";
            
            }
            else
            {
                imageName = "centos_16";

            }
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
        #endregion
        */
        private void setRechnungsAdresse(boAdresse curAdresse)
        {
            this.Rechnungsadresse = curAdresse;
        }

        private void setAnlagenAdresse(boAdresse curAdresse)
        {
            this.AnlagenAdresse = curAdresse;
        }


        [XafDisplayName("Anlagenstatus")]
        public enmAnlagenStatus Anlagenstatus
        {
            get
            {
                return _anlagenstatus;

            }
            set
            {
                SetPropertyValue("Anlagenstatus", ref _anlagenstatus, value);
            }
        }

        [XafDisplayName("Notiz")]
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
        [XafDisplayName("Gebäude")]
        [DataSourceCriteria("Liegenschaft.Oid='@this.Liegenschaft.Oid'")]
        public fiGebaeude Gebaeude
        {
            get
            {
                return _gebaeude;
            }
            set
            {
                SetPropertyValue("Gebaeude", ref _gebaeude, value);
            }
        }
        [XafDisplayName("Ebene")]
        [DataSourceCriteria("Gebaeude.Oid='@this.Gebaeude.Oid'")]
        public fiEbene Ebene
        {
            get
            {
                return _ebene;
            }
            set
            {
                SetPropertyValue("Ebene", ref _ebene, value);
            }
        }
        [XafDisplayName("Raum")]
        [DataSourceCriteria("Ebene.Oid='@this.Ebene.Oid'")]
        public fiRaum Raum
        {
            get
            {
                return _raum;
            }
            set
            {
                SetPropertyValue("Raum", ref _raum, value);
            }


        }


        [Association("boAnlage-boZugangAnlage")]
        [XafDisplayName("Zugangsinformationen")]
        public XPCollection<fiZugangAnlage> lstZugangAnlage
        {
            get
            {
                return GetCollection<fiZugangAnlage>("lstZugangAnlage");
            }
        }
        [XafDisplayName("Datenfelder")]
        [Association("boAnlage-boANDatenEntry"), DevExpress.Xpo.Aggregated]
        public XPCollection<boANDatenEntry> lstANDatenfelder
        {
            get
            {
                return GetCollection<boANDatenEntry>("lstANDatenfelder");
            }
        }
        [XafDisplayName("Maßnahmen")]
        [Association("boAnlage-boANMassnahme")]
        public XPCollection<boANMassnahme> lstAnMassnahmen
        {
            get
            {
               return GetCollection<boANMassnahme>("lstAnMassnahmen");
            }
        }

        [XafDisplayName("Originalbild")]
        [VisibleInDetailView(true)]
        [VisibleInListView(false)]
        [ReadOnly(true)]
        [ImageEditor]
        [Delayed(true)]

        public byte[] MainImageOriginal
        {
            get
            {
                return GetDelayedPropertyValue<byte[]>("MainImageOriginal");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("MainImageOriginal", value);
            }
        }




        
        [XafDisplayName("Wartungszone")]
        public boWartungszone Wartungszone
        {
            get
            {
                boWartungszone retVal;
                if (this.AnlagenAdresse != null && this.Liegenschaft != null)
                {
                    if (this.AnlagenAdresse.getWartungszone(this.Liegenschaft.Mandant) != null)
                    {
                        retVal = this.AnlagenAdresse.getWartungszone(this.Liegenschaft.Mandant);
                    }
                    else
                    {
                        retVal = null;
                    }
                }
                else
                {
                    retVal = null;
                }
                return retVal;
            }

        }

        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                return _mandant;
            }

            set
            {
                SetPropertyValue("Mandant", ref _mandant, value);
            }
        }

    

    [XafDisplayName("Husverwalter")]
        public boHausverwalter Hausverwalter
        {
            get
            {
                boHausverwalter retVal;
                if (this.Liegenschaft != null)
                {

                    retVal = (this.Liegenschaft.Hausverwalter != null) ? retVal = this.Liegenschaft.Hausverwalter : null;
                }
                else
                {
                    retVal = null;
                }
                return retVal;
            }
        }


        [XafDisplayName("Anlagenbetreuer")]
        public boAdresse Anlagenbetreuer
        {
            get
            {
                return _anlagenbetreuer;
            }
            set
            {
                SetPropertyValue("Anlagenbetreuer", ref _anlagenbetreuer, value);
            }
        }


        [XafDisplayName("Rechnungsadresse")]
        public boAdresse Rechnungsadresse
        {
            get
            {
                return _rechnungsadresse;
            }
            set
            {
                SetPropertyValue("Rechnungsadresse", ref _rechnungsadresse, value);
            }
        }

        [XafDisplayName("Baujahr")]

        [ModelDefault("DisplayFormat", "0:D0")]
        //[ModelDefault("EditMask", "d")]
        public System.Int32 Baujahr
        {
            get
            {
                return _baujahr;
            }
            set
            {
                SetPropertyValue("Baujahr", ref _baujahr, value);
            }
        }

        [XafDisplayName("Einbaudatum")]
        public System.DateTime Einbaudatum
        {
            get
            {
                return _einbaudatum;
            }
            set
            {
                SetPropertyValue("Einbaudatum", ref _einbaudatum, value);
            }
        }


        [XafDisplayName("Typbezeichnung")]
        public System.String Typbezeichnung
        {
            get
            {
                return _typbezeichnung;
            }
            set
            {
                SetPropertyValue("Typbezeichnung", ref _typbezeichnung, value);
            }
        }

        [XafDisplayName("Typ")]
        [DataSourceCriteria("Hersteller.Oid='@this.Hersteller.Oid'")]
        [ImmediatePostData]
        public fiHerstellerProdukt Typ
        {
            get
            {
                return _typ;
            }
            set
            {
                SetPropertyValue("Typ", ref _typ, value);
            }
        }
        [XafDisplayName("Hersteller")]
        [ImmediatePostData(true)]
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
        
        [XafDisplayName("Herstellerprodukt")]
        public fiHerstellerProdukt Herstellerprodukt
        {
            get
            {
                fiHerstellerProdukt curProdukt;
                if(this.Typ != null)
                {
                    curProdukt = this.Session.GetObjectByKey<fiHerstellerProdukt>(this.Typ.Oid);
                }
                else
                {
                    curProdukt = null;
                }
                return curProdukt;
            }
        }
        

        [XafDisplayName("Zustand")]
        public enmZustand Zustand
        {
            get
            {
                return _zustand;
            }
            set
            {
                SetPropertyValue("Zustand", ref _zustand, value);
            }
        }
        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var nummer = string.Empty;
                var bezeichnung = string.Empty;
                nummer = (this.AnlagenNummer!= null)?this.AnlagenNummer:"N/A";
                bezeichnung = (this.Bezeichnung != null)?this.Bezeichnung:"N/A";
                retVal = String.Format("{0} - {1}", nummer, bezeichnung);
                return retVal;
                
            }
        }


        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-boAnlage")]
        [DataSourceCriteria("Mandant.Oid = '@this.curMandantID'")]
        [RuleRequiredField]
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

        [XafDisplayName("Anlagenbilder")]
        [Association("boAnlage-boAnlagenBild"), DevExpress.Xpo.Aggregated]
        [Delayed(true)]
        public XPCollection<boAnlagenBild> lstAnlagenBilds
        {
            get
            {
                return GetCollection<boAnlagenBild>("lstAnlagenBilds");
            }
        }

        [XafDisplayName("Anlagenart")]
        [RuleRequiredField]
        [ImmediatePostData(true)]
        [Association("boAnlage-boAnlagenArt")]
        public boAnlagenArt AnlagenArt
        {
            get
            {
                return _anlagenArt;
            }
            set
            {
                SetPropertyValue("AnlagenArt", ref _anlagenArt, value);
            }
        }

        [XafDisplayName("Anlagengruppe")]
        [ReadOnly(true)]
        [ImmediatePostData(true)]
        [Association("boAnlage-boAnlagenGruppe")]
        public boAnlagenGruppe AnlagenGruppe
        {
            get
            {
                return _anlagenGruppe;
            }
            set
            {
                SetPropertyValue("AnlagenGruppe", ref _anlagenGruppe, value);
            }
        }

        [XafDisplayName("Fremdsystem ID")]
        public System.String FremdsystemId
        {
            get
            {
                return _fremdsystemId;
            }
            set
            {
                SetPropertyValue("FremdsystemId", ref _fremdsystemId, value);
            }
        }


      

        [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 30, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        [XafDisplayName("Titelbild")]
       
        public byte[] Mainimage
        {
            get
            {
                //return GetPropertyValue<byte[]>("Mainimage");
                return GetPropertyValue<byte[]>("Mainimage");
            }
            set
            {
                SetPropertyValue<byte[]>("Mainimage", value);
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

        [XafDisplayName("Anlagenadresse")]
       
        public boAdresse AnlagenAdresse
        {
            get
            {
                return _anlagenAdresse;
            }
            set
            {
                SetPropertyValue("AnlagenAdresse", ref _anlagenAdresse, value);
            }
        }

        [XafDisplayName("Anlagencode")]
        [ReadOnly(true)]
        public System.String Anlagencode
        {
            get
            {
                return _anlagencode;
            }
            set
            {
                SetPropertyValue("Anlagencode", ref _anlagencode, value);
            }
        }

      
       

        [XafDisplayName("Anlagennummer")]
        
        public System.String AnlagenNummer
        {
            get
            {
                return _anlagennummer;
            }
            set
            {
                SetPropertyValue("AnlagenNummer", ref _anlagennummer, value);
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

        [XafDisplayName("Dateien und Dokumente")]
        [Association("boAnlage-boANAttachment"),DevExpress.Xpo.Aggregated]
        public XPCollection<boANAttachment> lstANAttachments
        {
            get
            {
                return GetCollection<boANAttachment>("lstANAttachments");
            }
        }

        [XafDisplayName("Messungen")]
        [Association("boAnlage-boMessung"),DevExpress.Xpo.Aggregated]
        public XPCollection<boMessung> lstMessungen
        {
            get
            {
                return GetCollection<boMessung>("lstMessungen");
            }
        }

        [XafDisplayName("Service")]

        [Association("boAnlage-serviceAnlagenService")]
        [DevExpress.Xpo.Aggregated]
      
        public XPCollection<serviceAnlagenService> lstAnlagenService
        {
            get
            {
                return GetCollection<serviceAnlagenService>("lstAnlagenService");
            }
        }

        [XafDisplayName("Geräte")]
        [Association("boAnlage-fiAnlagengeraet")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<fiAnlagenGeraet> lstAnlagengeraete
        {
            get
            {
                return GetCollection<fiAnlagenGeraet>("lstAnlagengeraete");
            }
        }

        [XafDisplayName("Komponenten")]
        [Association("boAnlage-AnAnlagenKomponente")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<AnAnlagenKomponente> lstAnlagenkomponenten
        {
            get
            {
                return GetCollection<AnAnlagenKomponente>("lstAnlagenkomponenten");
            }
        }

        //TODO: hier kann ich sicher noch was optimieren

        [XafDisplayName("Hauptanlage")]
        [DataSourceCriteria("Liegenschaft.Oid ='@this.Liegenschaft.Oid' AND Oid != '@this.Oid'")]
        [Association("parentAnlage-subAnlagen")]
        public boAnlage ParentAnlage
        {
            get
            {
                return _parentAnlage;
            }
            set
            {
                SetPropertyValue("ParentAnlage", ref _parentAnlage, value);
            }
        }

        [XafDisplayName("abhängige Anlagen")]
        [Association("parentAnlage-subAnlagen")]
        public XPCollection<boAnlage> lstUnteranlagen
        {
            get
            {
                return GetCollection<boAnlage>("lstUnteranlagen");              
            }
        }

        [Association("boAnlage-fiAnlagenfeld")]
        [XafDisplayName("Anlagenfelder")]
        public XPCollection<fiAnlagenfeld> lstAnlagenfelder
        {
            get
            {
                return GetCollection<fiAnlagenfeld>("lstAnlagenfelder");
            }
        }
        [XafDisplayName("Haustechnikkomponente")]
        [Association("LgHaustechnikKomponente-boAnlage")]
        [DataSourceCriteria("Liegenschaft.Oid ='@this.Liegenschaft.Oid' AND Oid != '@this.Oid'")]
        public LgHaustechnikKomponente HaustechnikKomponente
        {
            get
            {
                return _lghaustechnikkomponente;
            }
            set
            {
                SetPropertyValue("HaustechnikKomponente", ref _lghaustechnikkomponente, value);
            }
        }

        [XafDisplayName("Ansprechpartner")]
        [Association("boAnlage-fiKontaktAnlage")]
        public XPCollection<fiKontaktAnlage> lstKontakte
        {
            get
            {
                return GetCollection<fiKontaktAnlage>("lstKontakte");
            }
        }
    }
}