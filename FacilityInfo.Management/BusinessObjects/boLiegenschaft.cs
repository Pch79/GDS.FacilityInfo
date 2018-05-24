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
using FacilityInfo.GlobalObjects.EnumStore;
using FacilityInfo.GlobalObjects.BusinessObjects;
using FacilityInfo.GlobalObjects.Helpers;
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
        //private boAdresse _hausbetreuer;
        private System.String _objektNummer;
        private System.Boolean _liegenschaftOnline;
        private System.String _bezeichnung;

        //private System.Drawing
        //private byte[] _mainimage;
        private enmZustand _zustand;
        private System.String _beschreibung;
        private System.Int32 _wohneinheiten;

        //private System.DateTime _zugang;
        //private System.DateTime _abgang;
        private enmWartungsStatus _Wartungsstatus;
        private System.String _notiz;
        private enmWasserbereitung _warmwasserbereitung;
        private enmWasserbereitung _heizungsbereitung;
        private System.String _liegenschaftsnummer;

        private System.String curMandantID;
        private System.String _fremdsystemId;
        private enmBetreuungsStatus _betreuungsstaus;

        private Debitorenkonto _debitorenKonto;

        private fiRisikoGruppe _risikoGruppe;

        //Anlagedatum
        // private System.DateTime _erstellungsdatum;
        //private System.DateTime _ladstChange;


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
            }
        }

        #region Methoden


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            curMandantID = clsStatic.loggedOnMandantOid;
            //hier gleich den Mandanten setzen
            this.Mandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));


            this._Wartungsstatus = enmWartungsStatus.BesichtigungOffen;
            this._warmwasserbereitung = enmWasserbereitung.unbekannt;
            this._heizungsbereitung = enmWasserbereitung.unbekannt;
            //beim Erstellen könnte ich nachschauen ob schon eine Aufnahmecheckliste existiert und diese dann gleich einbauen

            //die datenitems setzen

            //erst nachschauen ob eine Objektdefinition da ist
            Type curType = this.GetType();
            boFIObjekt curFiObjekt = Session.FindObject<boFIObjekt>(new BinaryOperator("Objekttyp", curType, BinaryOperatorType.Equal));
            if (curFiObjekt != null)
            {
                if (curFiObjekt.lstDatenFelder.Count > 0)
                {
                    //dann die Collection durchparsen und die Felder erstellen
                    foreach (boDatenItem datenItem in curFiObjekt.lstDatenFelder)
                    {
                        boLGDatenEntry curLgDatenEntry = Session.FindObject<boLGDatenEntry>(new GroupOperator(new BinaryOperator("Liegenschaft.Oid", this.Oid, BinaryOperatorType.Equal), new BinaryOperator("Datenfeld.Oid", datenItem.Oid, BinaryOperatorType.Equal)));
                        if (curLgDatenEntry == null)
                        {
                            curLgDatenEntry = new boLGDatenEntry(Session);
                            curLgDatenEntry.Datenfeld = Session.GetObjectByKey<boDatenItem>(datenItem.Oid);
                            curLgDatenEntry.Liegenschaft = this;
                            this.lstObjektDaten.Add(curLgDatenEntry);
                            curLgDatenEntry.Save();
                        }
                    }
                }
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (!this.IsDeleted)
            {
                if (!this.Session.IsObjectToDelete(this))
                {
                    if (this.Mandant != null)
                    {
                        if (this.Liegenschaftsnummer == null || this.Liegenschaftsnummer == string.Empty)
                        {
                            this.Liegenschaftsnummer = createNumber(this.Mandant);
                        }
                    }
                    if (this.Mainimage != null)
                    {
                        saveImageCopy();
                    }
                    //das Bild mit Wasserzeichen versehen
                    if (this.MainImageOriginal != null)
                    {
                        try
                        {
                            setWatermark();
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //hier könnte man gleich ein Gebäude anlegen
                   

                        createDefaultBuilding();


                    
                }
            }
        }

        private fiGebaeude createDefaultBuilding()
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
        private System.String createNumber(boMandant selectedMandant)
        {
            Type curType = this.GetType();

            var nummer = this.Session.FindObject<boNummernkreis>(new GroupOperator(new BinaryOperator("Objekt", curType, BinaryOperatorType.Equal), new BinaryOperator("Mandant.Oid", selectedMandant.Oid, BinaryOperatorType.Equal),
                    new BinaryOperator("GueltigAb", DateTime.Now, BinaryOperatorType.LessOrEqual),
                    new BinaryOperator("GueltigBis", DateTime.Now, BinaryOperatorType.GreaterOrEqual)));

            var retVal = string.Empty;

            if (nummer != null)
            {
                retVal = nummer.NextNumber;
                nummer.FortlaufendeNummer = nummer.FortlaufendeNummer + 1;
                nummer.Save();
            }
            return retVal;
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
            }
        }
        #endregion

        #region Properties

        [XafDisplayName("Trinkwasserprüfung")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
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
                    cnt = this.lstKwpVertrag.Where(t => t.VertragZurueck == true && t.WartungsAnlage.BrennstoffArt.StartsWith("TW-")).Count();
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

        [XafDisplayName("Wartungsstatus")]
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

     

        [XafDisplayName("Hauptzugang")]
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


        [XafDisplayName("Hausverwlaterselekt")]
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
        [Association("boLiegenschaft-LGHaustechnikKomponente")]

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

        [XafDisplayName("Massnahmen")]
        [Association("boLiegenschaft-boLGMassnahme"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boLGMassnahme> lstLgMassnahmen
        {
            get
            {
                return GetCollection<boLGMassnahme>("lstLgMassnahmen");
            }
        }



        [XafDisplayName("Wartungszone")]
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


        [XafDisplayName("Objektdaten")]
        [Association("boLiegenschaft-boLGDatenEntry"), DevExpress.Xpo.Aggregated]
        public XPCollection<boLGDatenEntry> lstObjektDaten
        {
            get
            {
                return GetCollection<boLGDatenEntry>("lstObjektDaten");
            }
        }


       
       
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
        //die Assoziation mit den Liegenschaftsbildern 
        
        [XafDisplayName("Bilder")]
        [Association("boLiegenschaft-boLiegenschaftsBild"), DevExpress.ExpressApp.DC.Aggregated]
        [Delayed("LiegenschaftsBilder")]
        public XPCollection<boLiegenschaftsBild> LiegenschaftsBilder
        {
            get
            {
                return GetCollection<boLiegenschaftsBild>("LiegenschaftsBilder");
            }
        }
        



        [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 60, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        [XafDisplayName("Titelbild")]
        [ImmediatePostData(true)]
      
        public byte[] Mainimage
        {
            get
            {
                return GetPropertyValue<byte[]>("Mainimage");
            }
            set
            {
                SetPropertyValue<byte[]>("Mainimage", value);
            }
        }

        [XafDisplayName("Originalbild")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ReadOnly(true)]
        [ImageEditor]
  
        public byte[] MainImageOriginal
        {
            get
            {
                
                return GetPropertyValue<byte[]>("MainImageOriginal");
            }
            set
            {
                SetPropertyValue<byte[]>("MainImageOriginal", value);
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
        [XafDisplayName("Objektnummmer")]
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
        //Termine
        /*
        [XafDisplayName("KWP-Termine")]
        public XPCollection<KwpWartTermin> lstKwpTermine
        {
            get
            {
                XPCollection<KwpWartTermin> lstRetVal = new XPCollection<KwpWartTermin>(this.Session, new BinaryOperator("Liegenschaft.Oid", this.Oid, BinaryOperatorType.Equal));

                return lstRetVal;
            }
        }
        */

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
        #endregion
    }
}