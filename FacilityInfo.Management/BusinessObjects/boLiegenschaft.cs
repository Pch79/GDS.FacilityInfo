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
using FacilityInfo.Management.DomainComponents;
using FacilityInfo.Anlagen.BusinessObjects;



using System.Drawing;
using System.IO;
using FacilityInfo.Management.EnumStore;
using FacilityInfo.DMS.BusinessObjects;

using FacilityInfo.Building.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Management;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Fremdsystem.BusinessObjects;
using FacilityInfo.BusinessManagement.BusinessObjects;
using FacilityInfo.Bildverarbeitung.BusinessObjects;
using FacilityInfo.Management.Helpers;
using DevExpress.ExpressApp.Actions;

namespace FacilityInfo.Liegenschaft.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Liegenschaft")]
    [ImageName("house_one_16")]
    [Serializable]
    [XafDefaultProperty("Bezeichnung")]
    public class boLiegenschaft : BaseObject
    {


        //Liegenschaft hat ein Debitorenkonto

        private boMandant _mandant;
        private boHausverwalter _hausverwalter;
        private fiHausbetreuer _hausbetreuer;
        private System.String _hausverwlaterSelekt;
        private boAdresse _liegenschaftsadresse;
   
        private System.String _objektNummer;
        private System.Boolean _liegenschaftOnline;
        private System.String _bezeichnung;

        private enmZustand _zustand;
        private System.String _beschreibung;
        private System.Int32 _wohneinheiten;

        
        private enmWartungsStatus _wartungsStatus;
        private System.String _notiz;
        private enmWasserbereitung _warmwasserbereitung;
        private enmWasserbereitung _heizungsbereitung;
        private System.String _liegenschaftsnummer;

        private System.String curMandantID;
        private System.String _fremdsystemId;
        private enmBetreuungsStatus _betreuungsstaus;

        private Debitorenkonto _debitorenKonto;

        private fiRisikoGruppe _risikoGruppe;
        private System.String _kuerzel;

       

        public boLiegenschaft(Session session)
            : base(session)
        {
            this.lstHaustechnikKomponenten.CollectionChanged += LstHaustechnikKomponenten_CollectionChanged;
        }
        private void LstHaustechnikKomponenten_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.CollectionChangedType == XPCollectionChangedType.AfterAdd || e.CollectionChangedType == XPCollectionChangedType.AfterRemove)
            {
                this.lstHaustechnikKomponenten.Reload();
                this.Reload();
            }
        }

        #region Methoden
       
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                //wenn sich das MainImage ändert Thumbnial und Web-Titelbild erstellen
                case "MainImage":
                    if (!this.IsLoading)
                    {
                        if (newValue != null)
                        {
                            setMainThumbnail();
                            //hier kan ich auch gleich die Web-Implementierung erstellen
                            setMainImageWeb();
                        }
                        if(newValue == null)
                        {
                            this.MainImageThumb = null;
                            this.MainImageWeb = null;
                        }
                    }
                    break;
            }

          

        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //ier umbauen auf den Stasndard-Mandanten
            //TODO: Mandantenzuordnung umbauen
            /*
            curMandantID = clsStatic.loggedOnMandantOid;
            //hier gleich den Mandanten setzen
            this.Mandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));

            */
            this.WartungsStatus = enmWartungsStatus.BesichtigungOffen;
            this.Warmwasserbereitung = enmWasserbereitung.unbekannt;
            this.Heizungsbereitung = enmWasserbereitung.unbekannt;
            //beim Erstellen könnte ich nachschauen ob schon eine Aufnahmecheckliste existiert und diese dann gleich einbauen

            //die datenitems setzen

            //erst nachschauen ob eine Objektdefinition da ist
            Type curType = this.GetType();
            boFIObjekt curFiObjekt = Session.FindObject<boFIObjekt>(new BinaryOperator("Objekttyp", curType, BinaryOperatorType.Equal));
            if (curFiObjekt != null)
            {
                
                
            }

            //createNumber();
        }

        /// <summary>
        /// Creates the number.
        /// </summary>
        public void createNumber()
        {
            Type curType = this.GetType();
            boMandant curMandant;
            //die Anlage sollte einemMandanten zugeweisen werden
            if (this.Mandant != null)
            {
                curMandant = this.Session.GetObjectByKey<boMandant>(this.Mandant.Oid);


                var nummer = this.Session.FindObject<boNummernkreis>(new GroupOperator(new BinaryOperator("Objekt", curType, BinaryOperatorType.Equal), new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal),
                         new BinaryOperator("GueltigAb", DateTime.Now, BinaryOperatorType.LessOrEqual),
                         new BinaryOperator("GueltigBis", DateTime.Now, BinaryOperatorType.GreaterOrEqual)));
                var retVal = string.Empty;

                //dann noch den Acode dazuholen
                if (nummer != null)
                {
                    //jetzt das Teil zusammenbauen
                    if (nummer.Suffix != null && nummer.Suffix != string.Empty)
                    {
                        retVal = string.Format("{0}{1}{2}", nummer.Praefix, nummer.FortlaufendeNummer, nummer.Suffix);
                    }
                    else
                    {
                        retVal = string.Format("{0}{1}", nummer.Praefix, nummer.FortlaufendeNummer, nummer.Suffix);
                    }

                    //retVal = nummer.NextNumber;
                    nummer.FortlaufendeNummer = nummer.FortlaufendeNummer + 1;
                    nummer.Save();
                }

                this.Liegenschaftsnummer = retVal;
                this.Save();
                //this.DebitKreditNr = retVal;
            }
            this.Session.CommitTransaction();
        }

        protected override void OnSaved()
        {
            base.OnSaved();

            if (!this.IsDeleted)
            {
                
                if (!this.Session.IsObjectToDelete(this))
                {
                    /*
                    if (this.Mandant != null)
                    {
                        if (this.Liegenschaftsnummer == null || this.Liegenschaftsnummer == string.Empty)
                        {
                             createNumber();
                        }
                    }
                    
                   */
                    CreateDefaultBuilding();
                }
            }
        }
       
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //TODO Wartungsstatus muss anders gesetzt werden
            getWartungsStatus();
        }

        /// <summary>
        /// Creates the default building.
        /// </summary>
        /// <returns></returns>
        private fiGebaeude CreateDefaultBuilding()
        {
            fiGebaeude curBuilding = this.Session.FindObject<fiGebaeude>(new BinaryOperator("Liegenschaft.Oid", this.Oid, BinaryOperatorType.Equal));
            if(curBuilding == null)
            {
                curBuilding = new fiGebaeude(this.Session);
                curBuilding.Liegenschaft = this;
                curBuilding.Bezeichnung = "Haus 1";
                boAdresse workingAddress = (this.Liegenschaftsadresse != null) ? this.Session.GetObjectByKey<boAdresse>(this.Liegenschaftsadresse.Oid) : null;
                curBuilding.Adresse = workingAddress;//(this.Liegenschaftsadresse != null) ? this.Liegenschaftsadresse : null;
                curBuilding.Save();
                fiEbene defaultFloor = new fiEbene(this.Session);
                defaultFloor.Art = (this.Session.FindObject<fiEbenenart>(new BinaryOperator("Kuerzel", "EG", BinaryOperatorType.Equal))!=null)? this.Session.FindObject<fiEbenenart>(new BinaryOperator("Kuerzel", "EG", BinaryOperatorType.Equal)):null;
                if(defaultFloor.Art != null)
                {
                    defaultFloor.Bezeichnung = defaultFloor.Art.Bezeichnung;
                }
                defaultFloor.Gebaeude = curBuilding;
                defaultFloor.Save();
                curBuilding.Save();
                //den Raum auch gleich erstellen
                fiRaum defaultRaum = new fiRaum(this.Session);
                defaultRaum.Raumart = (this.Session.FindObject<fiRaumart>(new BinaryOperator("Kuerzel", "TR", BinaryOperatorType.Equal)) != null) ? this.Session.FindObject<fiRaumart>(new BinaryOperator("Kuerzel", "TR", BinaryOperatorType.Equal)) : null;
                if(defaultRaum.Raumart != null)
                {
                    defaultRaum.Bezeichnung = defaultRaum.Raumart.Bezeichnung;
                }
                defaultRaum.Ebene = defaultFloor;
                defaultRaum.Gebaeude = curBuilding;
                defaultRaum.Save();
                this.Session.CommitTransaction();
                return curBuilding;
            }
            else
            {
                return null;
            }
        }
      

        /*
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
        */
        /*
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
            }
        }
        */

        public enmWartungsStatus getWartungsStatus()
        {
               enmWartungsStatus retVal = enmWartungsStatus.none;
                try
                {
                    if (this.lstKwpVertrag != null)
                    {
                    var resultList = this.lstKwpVertrag.Where(t => t.WartungsAnlage != null).ToList();
                    
                    int contractgesamtCount = 0;
                        int contractAktivCount = 0;
                        try
                        {
                            contractgesamtCount = resultList.Where(t => !t.WartungsAnlage.BrennstoffArt.StartsWith("LEGIO")).Count();
                        }
                        catch
                        {
                            contractgesamtCount = 0;
                        }
                        try
                        {
                            contractAktivCount = resultList.Where(t => t.VertragZurueck == true && !t.WartungsAnlage.BrennstoffArt.StartsWith("LEGIO")).Count();
                        }
                        catch
                        {
                            contractAktivCount = 0;
                        }
                        if (contractgesamtCount > 0)
                        {
                            if (contractAktivCount > 0)
                            {

                                if (contractAktivCount < contractgesamtCount)
                                {
                                    retVal = enmWartungsStatus.WartungTeilweise;
                                }

                                else
                                {
                                    retVal = enmWartungsStatus.WartungKomplett;
                                }
                            }
                            else
                            {
                                retVal = enmWartungsStatus.VerträgeOffen;
                            }

                        }
                        else
                        {
                            retVal = enmWartungsStatus.BesichtigungOffen;
                        }
                    }
                    else
                    {
                        retVal = enmWartungsStatus.BesichtigungOffen;
                    }

                this.WartungsStatus = retVal;
                    return retVal;
                }
                catch (Exception excWartungStatus)
                {
                   
                this.WartungsStatus = enmWartungsStatus.none;
                return enmWartungsStatus.none;

            }
                

        }
        #endregion

        #region Properties
        [XafDisplayName("Kommunikation")]
        public List<boKommunikationItem> lstKommunikation
        {
            get
            {
                List<boKommunikationItem> retVal = new List<boKommunikationItem>();
                //1. Kommunikation aus der Liegenschaftsadresse
                //2. Kommnikation aus dem Hausverwalter
                //3. 

                return retVal;
            }
        }
        
        [XafDisplayName("Wartungsstatus")]
        public enmWartungsStatus WartungsStatus
        {
            get { return _wartungsStatus; }
            set { SetPropertyValue("WartungsStatus", ref _wartungsStatus, value); }
        }
        [XafDisplayName("Trinkwasserprüfung")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        [Delayed(true)]
        public Boolean TrinkwasserPruefung
        {
            get
            {
                bool retVal = false;
                int cnt = 0;
                //Gibt es Verträge zu der Liegenschaft
                //
                if (this.lstKwpVertrag != null)
                {
                    cnt = this.lstKwpVertrag.Where(t => t.VertragZurueck == true && t.WartungsAnlage.BrennstoffArt.StartsWith("LEGIO")).Count();
                }
                if(cnt>0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
                return retVal;
            }
        }
        [XafDisplayName("Status Hauptzugang")]
        [CaptionsForBoolValues("vorhanden","nicht vorhanden")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [Delayed(true)]
        public Boolean StatusHauptzugang
        {
            get
            {
                var retVal = false;
                
                if(this.lstZugangLiegenschaft != null)
                {
                    var cnt = this.lstZugangLiegenschaft.Where(t => t.HauptZugang == true).Count();
                    if(cnt>0)
                    {
                        retVal = true;
                    }
                    else
                    {
                        retVal = false;
                    }
                }
                else
                {
                    retVal = false;
                }
                 
                return retVal;
            }
        }



        //den Wartungsstatus automatisiert setzen.
        /*
        [XafDisplayName("Wartungsstatus")]
        [Delayed(true)]
        public enmWartungsStatus WartungsStatus
        {
            get
            {

                //Alle verträge in betracht ziehen ausser soclhe deren Anlage die brennstoffart TW-% haben
                //1. ist kein Vertrag aktiv 
                //alle vertreäge ohne TW- zählen
                enmWartungsStatus retVal = enmWartungsStatus.none;
                try
                {
                    if (this.lstKwpVertrag != null)
                    {
                        int contractgesamtCount = 0;
                        int contractAktivCount = 0;
                        try
                        {
                            contractgesamtCount = this.lstKwpVertrag.Where(t => !t.WartungsAnlage.BrennstoffArt.StartsWith("TW-")).Count();
                        }
                        catch
                        {
                            contractgesamtCount = 0;
                        }
                        try
                        {
                            contractAktivCount = this.lstKwpVertrag.Where(t => t.VertragZurueck == true && !t.WartungsAnlage.BrennstoffArt.StartsWith("TW-")).Count();
                        }
                        catch
                        {
                            contractAktivCount = 0;
                        }
                        if (contractgesamtCount > 0)
                        {
                            if (contractAktivCount > 0)
                            {

                                if (contractAktivCount < contractgesamtCount)
                                {
                                    retVal = enmWartungsStatus.WartungTeilweise;
                                }

                                else
                                {
                                    retVal = enmWartungsStatus.WartungKomplett;
                                }
                            }
                            else
                            {
                                retVal = enmWartungsStatus.VerträgeOffen;
                            }

                        }
                        else
                        {
                            retVal = enmWartungsStatus.BesichtigungOffen;
                        }
                    }
                    else
                    {
                        retVal = enmWartungsStatus.BesichtigungOffen;
                    }

                    return retVal;
                }
                catch (Exception excWartungStatus)
                {
                    return enmWartungsStatus.none;
                }
                }
        }
        */
        [XafDisplayName("Kürzel")]
     
        public System.String Kuerzel
        {
            get
            {
                return _kuerzel;
            }
            set
            {
                SetPropertyValue("Kuerzel", ref _kuerzel, value);

            }
        }

        [XafDisplayName("Hauptzugang")]
        [Delayed(true)]
        public fiZugangLiegenschaft HauptZuganng
        {
            get
            {
                fiZugangLiegenschaft retVal = null;
                if(this.StatusHauptzugang)
                {
                    retVal = this.lstZugangLiegenschaft.Where(t => t.HauptZugang == true).FirstOrDefault();
                }
                return retVal;
            }
        }

        [XafDisplayName("Debitorenkonto")]
        public Debitorenkonto DebitorenKonto
        {
            get
            {
                return _debitorenKonto;
            }
            set
            {
                SetPropertyValue("DebitorenKonto", ref _debitorenKonto, value);
            }
        }


        [XafDisplayName("Hausverawlterselekt")]
        public System.String HausverwlaterSelekt
        {
            get {
                return _hausverwlaterSelekt;
            }
            set {
                SetPropertyValue("HausverwlaterSelekt", ref _hausverwlaterSelekt, value);
            }
        }

        [XafDisplayName("Betreuungsstatus")]
        public enmBetreuungsStatus Betreuungsstatus
        {
            get
            {
                return _betreuungsstaus;
            }
            set
            {
                SetPropertyValue("Betreuungsstatus", ref _betreuungsstaus, value);
            }
        }


       

        [XafDisplayName("Fremdsystem-ID")]
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

        [XafDisplayName("Haustechnikkomponenten")]
        [Association("boLiegenschaft-LGHaustechnikKomponente"), DevExpress.ExpressApp.DC.Aggregated]

        public XPCollection<LgHaustechnikKomponente> lstHaustechnikKomponenten
        {
            get
            {
                return GetCollection<LgHaustechnikKomponente>("lstHaustechnikKomponenten");
            }
        }

        [XafDisplayName("Liegenschaftsnummer")]
        public System.String Liegenschaftsnummer
        {
            get
            {
                return _liegenschaftsnummer;
            }
            set
            {
                SetPropertyValue("Liegenschaftsnummer", ref _liegenschaftsnummer, value);
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

        [XafDisplayName("Warmwasserbereitung")]
        public enmWasserbereitung Warmwasserbereitung
        {
            get
            {
                return _warmwasserbereitung;
            }
            set
            {
                SetPropertyValue("Warmwasserbereitung", ref _warmwasserbereitung, value);
            }
        }

        [XafDisplayName("Heizungsbereitung")]
        public enmWasserbereitung Heizungsbereitung
        {
            get
            {
                return _heizungsbereitung;
            }
            set
            {
                SetPropertyValue("Heizungsbereitung", ref _heizungsbereitung, value);
            }
        }


        [XafDisplayName("Zugangsinformationen")]
        [Association("boLiegenschaft-boZugangLiegenschaft")]
        public XPCollection<fiZugangLiegenschaft> lstZugangLiegenschaft
        {
            get
            {
                return GetCollection<fiZugangLiegenschaft>("lstZugangLiegenschaft");
            }
        }

        



        [XafDisplayName("Wartungszone")]
       // [Delayed(true)]
        public boWartungszone Wartungszone
        {
            get
            {
                boWartungszone retVal;
                if (this.Liegenschaftsadresse != null)
                {
                    retVal = this.Liegenschaftsadresse.getWartungszone(this.Mandant);
                }
                else
                {
                    retVal = null;
                }
                return retVal;
            }
        }



        [XafDisplayName("Hausbetreuer")]
        [Association("boLiegenschaft-fiHausbetreuer")]
        [ImmediatePostData(true)]
        public fiHausbetreuer Hausbetreuer
        {
            get
            {
                return _hausbetreuer;
            }
            set
            {
                SetPropertyValue("Hausbetreuer", ref _hausbetreuer, value);
            }
        }

        /*
        [XafDisplayName("Objektdaten")]
        [Association("boLiegenschaft-boLGDatenEntry"), DevExpress.Xpo.Aggregated]
        public XPCollection<boLGDatenEntry> lstObjektDaten
        {
            get
            {
                return GetCollection<boLGDatenEntry>("lstObjektDaten");
            }
        }

        */
       
       
        [XafDisplayName("Wohneinheiten")]
        public System.Int32 Wohneinheiten
        {
            get
            {
                return _wohneinheiten;
            }
            set
            {
                SetPropertyValue("Wohneinheiten", ref _wohneinheiten, value);
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
        [XafDisplayName("Checklisten")]
        [Association("boLiegenschaft-boLGCheckListe"), DevExpress.ExpressApp.DC.Aggregated]

        public XPCollection<boLGCheckListe> lstLGCheckListes
        {
            get
            {
                return GetCollection<boLGCheckListe>("lstLGCheckListes");
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
        [XafDisplayName("Anlagen")]
        [Association("boLiegenschaft-boAnlage")]
        public XPCollection<boAnlage> Anlagen
        {
            get
            {
                return GetCollection<boAnlage>("Anlagen");
            }
        }

        //Hauptanlagen und deren Unteranlagen 
        [XafDisplayName("Hauptanlagen")]
        public List<boAnlage> lstHauptanlagen
        {
            get
            {
                List<boAnlage> lstRetVal = new List<boAnlage>();
                lstRetVal = this.Anlagen.Where(t => t.ParentAnlage == null).ToList();
                return lstRetVal;
            }
        }
        //die Assoziation mit den Liegenschaftsbildern 
        
        [XafDisplayName("Bilder")]
        [Association("boLiegenschaft-boLiegenschaftsBild"), DevExpress.ExpressApp.DC.Aggregated]
        [Delayed(true)]
        public XPCollection<boLiegenschaftsBild> LiegenschaftsBilder
        {
            get
            {
          
                return GetCollection<boLiegenschaftsBild>("LiegenschaftsBilder");
            }
        }



        /*
        [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 60, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        [XafDisplayName("Titelbild")]
        [ImmediatePostData(true)]
         */
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
        //TODO': auskommenteirt 13.02.02019 -> Test der Webversion
        /*
        [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 60, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        */
        [XafDisplayName("Vorschaubild")]
        public byte[] MainImageThumb
        {
            get
            {
                return GetPropertyValue<byte[]>("MainImageThumb");
            }
            set { SetPropertyValue<byte[]>("MainImageThumb", value); }


        }

        
       // [Action(Caption ="Thumbnail erstellen")]
       //TODO: Den Funktionsaufruf in einen Controller packen (Web und Desktop)
        public void makeMainThumbnail()
        {
            setMainThumbnail();
            //hier kan ich auch gleich die Web-Implementierung erstellen
            setMainImageWeb();
           
        }

        //TODO: 
        
        public void setMainImageWeb()
        {
            if(this.MainImage != null)
            {
                Image workingImage = PictureHelper.ImageFromByteArray(this.MainImage);
                this.MainImageWeb = PictureHelper.ResizePicByWidth(workingImage, 400);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        public void setMainThumbnail()
        {
            if(this.MainImage != null)
            {
                this.MainImageThumb = PictureHelper.getThumbnailByteArray(this.MainImage);
                this.Save();
                this.Session.CommitTransaction();
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


        [XafDisplayName("Liegenschaft Online")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        [CaptionsForBoolValues("online", "offline")]


        public System.Boolean LiegenschaftOnline
        {
            get
            {
                return _liegenschaftOnline;
            }
            set
            {
                SetPropertyValue("LiegenschaftOnline", ref _liegenschaftOnline, value);
            }
        }
        [XafDisplayName("Objekt Nr.")]
        public System.String ObjektNummer
        {
            get
            {
                return _objektNummer;
            }
            set
            {
                SetPropertyValue("ObjektNummer", ref _objektNummer, value);
            }
        }
        [XafDisplayName("Liegenschaftsadresse")]
        [Association("boAdresse-boLiegenschaft")]
        [ImmediatePostData(true)]

        public boAdresse Liegenschaftsadresse
        {
            get
            {
                return _liegenschaftsadresse;
            }
            set
            {
                SetPropertyValue("Liegenschaftsadresse", ref _liegenschaftsadresse, value);
            }
        }


        [XafDisplayName("Hausverwalter")]
        [Association("boLiegenschaft-boHausverwalter")]
        [ImmediatePostData(true)]
        public boHausverwalter Hausverwalter
        {
            get
            {
                return _hausverwalter;
            }
            set
            {
                SetPropertyValue("Hausverwalter", ref _hausverwalter, value);
            }
        }


        

        [XafDisplayName("Mandant")]
        [ImmediatePostData(true)]
        [Association("boLiegenschaft-boMandant")]
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

       

        [XafDisplayName("Liegenschaftsdokumente")]
        [Association("boLiegenschaft-boLGAttachment"), DevExpress.Xpo.Aggregated]
        public XPCollection<boLGAttachment> lstLGAttachments
        {
            get
            {
                return GetCollection<boLGAttachment>("lstLGAttachments");
            }
        }

      
        [XafDisplayName("Gebäude")]

        [Association("boLiegenschaft-fiGebaeude")]
        public XPCollection<fiGebaeude> lstGebaeude
        {
            get
            {
                return GetCollection<fiGebaeude>("lstGebaeude");
            }
        }

        /* TODO: Dokumentverwaltung anpassebn
        [XafDisplayName("Dokumente (Alle)")]
        public XPCollection<boAttachment> lstAttachment
        {
            get
            {
                //alle Dokumente zurückgeben
                XPCollection<boAttachment> lstRetVal = new XPCollection<boAttachment>(this.Session, new BinaryOperator("Objektkey", this.Oid.ToString(), BinaryOperatorType.Equal));
                return lstRetVal;
            }
        }
        */

        [XafDisplayName("KWP-Verträge")]
        [Association("boLiegenschaft-KwpWartungsVertrag")]
        public XPCollection<KwpWartungsVertrag> lstKwpVertrag
        {
            get {
                return GetCollection<KwpWartungsVertrag>("lstKwpVertrag");
            }
        }

        //KWP-Anlagen
        [XafDisplayName("KWP-Anlagen")]
        [Association("boLiegenschaft-kwpWartungsAnlage")]
        public XPCollection<KwpWartungsAnlage> lstKwpAnlagen
        {
            get {
                return GetCollection<KwpWartungsAnlage>("lstKwpAnlagen");
                
            }
        }
        

        [XafDisplayName("Risikogruppe")]
        public fiRisikoGruppe RisikoGruppe
        {
            get
            {
                return _risikoGruppe;
            }
            set
            {
                SetPropertyValue("RisikoGruppe", ref _risikoGruppe, value);
            }
        }
        [XafDisplayName("Ansprechpartner")]
        [Association("boLiegenschaft-fiKontaktLiegenschaft")]
        public XPCollection<fiKontaktLiegenschaft> lstAnsprechpartner
        {
            get
            {
                return GetCollection<fiKontaktLiegenschaft>("lstAnsprechpartner");
            }
        }
        [XafDisplayName("Wartungstermine")]
        public XPCollection<KwpWartTermin> lstKwpWartungsTermine
        {
            get
            {
                //List<KwpWartTermin> lstRetVal = new List<KwpWartTermin>();
                XPCollection<KwpWartTermin> retVal = new XPCollection<KwpWartTermin>(this.Session ,new BinaryOperator("Liegenschaft.Oid", this.Oid, BinaryOperatorType.Equal));
                return retVal;
            }
        }

        #endregion
    }
}