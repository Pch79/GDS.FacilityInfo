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
using System.Drawing;
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Fremdsystem.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.BusinessManagement.BusinessObjects;
using System.IO;
using FacilityInfo.Management;
using System.Configuration;
using FacilityInfo.Artikelverwaltung.BusinessObjects;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace FacilityInfo.Core.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Mandant")]
    [XafDefaultProperty("Mandantenname")]
    [ImageName("hostname_16")]
    public class boMandant : BaseObject
    {
        private System.String _mandantenname;
        private System.String _slogan;
     

        
        private System.Boolean _aktiv;

        //adresse als Objekt hinterlegen
        private boAdresse _adresse;
       
        private System.String _steuernummer;
        private System.String _ustId;
        private System.String _registernummer;
        private System.String _registergericht;
       
        private boWatermark _wasserzeichen;
        private System.String _mandantenkennung;
        private fremdSysFremdsystem _fremdsystem;
        private DirectoryInfo _homeDirectory;
        private String _homePath;
       
     
         private Boolean _isDefault;
     


        //Kommunikation

        public boMandant(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
         
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            /*
            var homeDir = ConfigurationManager.AppSettings["AppHomeDirectory"];
            var curPath = string.Format("{0}\\{1}", homeDir, this.Mandantenkennung);
            this.HomePath = curPath;
            this.Save();
            */

            //Artikelkataloge defineiren
            //1. Standard
            if (checkArtikelkatalog("Standard") == null)
            {
                createArtikelkatalog("Standard");
            }
            //2. Anlagenartikel

            if (checkArtikelkatalog("Anlagenartikel") == null)
            {
                createArtikelkatalog("Anlagenartikel");
            }
        }

       private artikelArtikelKatalog checkArtikelkatalog(string bezeichnung)
        {
            artikelArtikelKatalog retVal = this.Session.FindObject<artikelArtikelKatalog>(new GroupOperator(new BinaryOperator("Mandant.Oid", this.Oid, BinaryOperatorType.Equal), new BinaryOperator("Bezeichnung", bezeichnung, BinaryOperatorType.Equal)));
            return retVal;
        }
        private void createArtikelkatalog(string bezeichnung)
        {
            artikelArtikelKatalog AnlagenArtikelKatalog = new artikelArtikelKatalog(this.Session);
            AnlagenArtikelKatalog.Bezeichnung = bezeichnung;
            AnlagenArtikelKatalog.Mandant = this;
            AnlagenArtikelKatalog.Save();
            this.Session.CommitTransaction();
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            //Das Arbeitsverzeichnis erstellen bzw prüfen

            //Standardbiliothek setzen
            boAttachmentBibliothek standardBib = this.Session.FindObject<boAttachmentBibliothek>(new GroupOperator(new BinaryOperator("Mandant.Oid", this.Oid, BinaryOperatorType.Equal), new BinaryOperator("Bezeichnung", "Standardbibliothek", BinaryOperatorType.Equal)));
            if(standardBib == null)
            {
                standardBib = new boAttachmentBibliothek(this.Session);
                standardBib.Mandant = this.Session.GetObjectByKey<boMandant>(this.Oid);
                standardBib.Bezeichnung = "Standardbibliothek";
                standardBib.Save();
                this.Session.CommitTransaction();
            }

            //gibt es schon ein Verzeichnis????
            DirectoryInfo curDi = null;

            DirectoryInfo austauschDir = null;
            DirectoryInfo bilderDir = null;
            DirectoryInfo dokumenteDir = null;
            //TODO: Das Home-verzeichnis der Anwendung in der App-Config anlgen
            var homeDir = "C:\\Temp";
            var curPath = string.Format("{0}\\{1}", homeDir, this.Mandantenkennung);

            curDi = new DirectoryInfo(curPath);
            if(!curDi.Exists)
            {
                curDi.Create();            
            }
            //hier das Austauschverzeichnis gleich erstellen

            var austauschPfad = string.Format("{0}\\{1}", curDi.FullName, "Datentransfer");
            austauschDir = new DirectoryInfo(austauschPfad);
            
                if(!austauschDir.Exists)
            {
                austauschDir.Create();
                austauschDir.CreateSubdirectory("In");
                austauschDir.CreateSubdirectory("Out");
            }

            //Bilderordner
            var bilderPfad = string.Format("{0}\\{1}", curDi.FullName, "Bilder");
            bilderDir = new DirectoryInfo(bilderPfad);

            if (!bilderDir.Exists)
            {
                bilderDir.Create();
                bilderDir.CreateSubdirectory("Liegenschaften");
                bilderDir.CreateSubdirectory("Anlagen");
            }

            //Dokumente
            var dokumentePfad = string.Format("{0}\\{1}", curDi.FullName, "Dokumente");
            dokumenteDir = new DirectoryInfo(dokumentePfad);

            if (!dokumenteDir.Exists)
            {
                dokumenteDir.Create();
               
            }

            this.HomePath = curDi.FullName;
            this.Save();
           



        }


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich die Mandantenkennung ändert
            //gibt es mit dem Alten
            DirectoryInfo workingDi = null;
            DirectoryInfo curDi = null;
           // var homeDir = clsStatic.AppHomeDirectory;
            //var curPath = string.Format("{0}\\{1}", homeDir, this.Mandantenkennung);

            if (!this.Session.IsObjectToDelete(this))
            {
                if (this.Session.IsObjectToSave(this))
                {
                    switch (propertyName)
                    {
                        case "Mandantenkennung":
                            //hier auch gleich das Homeverzeichnis ändern
                            if(newValue != null)
                            {
                                var homeDir = ConfigurationManager.AppSettings["AppHomeDirectory"];
                                var curPath = string.Format("{0}\\{1}", homeDir, this.Mandantenkennung);
                                this.HomePath = curPath;                            
                            }
                            break;

                        case "HomePath":
                            //prüfen ob es das verzeichnis gibt, wenn nciht erstellen
                            DirectoryInfo di = (this.HomePath != null) ? new DirectoryInfo(this.HomePath) : null;
                            if(di!= null)
                            {
                                if(!di.Exists)
                                {
                                    di.Create();
                                }
                            }
                            break;
                    }
                }
            }

        }

      [XafDisplayName("Homepath")]
      public string HomePath
        {
            get
            {
                return _homePath;
            }
            set
            {
                SetPropertyValue("HomePath", ref _homePath, value);
            }
        }
        public DirectoryInfo HomeDirectory
        {
            get
            {
                DirectoryInfo retVal;
                retVal = (this.HomePath != null) ? new DirectoryInfo(this.HomePath) : null;
                return retVal;
            }
        }

        [XafDisplayName("Mandantenkennung")]
        [RuleRequiredField]
        [RuleUniqueValue("Mandantenkennung muss eindeutig sein",DefaultContexts.Save)]
        
        public System.String Mandantenkennung
        {
            get
            {
                return _mandantenkennung;
            }
            set
            {
                String result = value;
                if (result != null)
                {
                    result = result.ToUpper().Trim();
                }
                SetPropertyValue("Mandantenkennung", ref _mandantenkennung, result);
            }
        }
        
        [XafDisplayName("Wasserzeichen")]
        public boWatermark Wasserzeichen
        {
            get
            {
                return _wasserzeichen;
            }
            set
            {
                SetPropertyValue("Wasserzeichen", ref _wasserzeichen, value);
            }
        }
        [XafDisplayName("Bildplatzhalter")]

        public byte[] Bildplatzhalter
        {
            get
            {
                return GetPropertyValue<byte[]>("Bildplatzhalter");
            }
            set
            {
                SetPropertyValue<byte[]>("Bildplatzhalter", value);
            }
        }
        
        [XafDisplayName("Geschäftsführer")]
        public XPCollection<boKontakt> lstGeschaeftsFuehrer
        {
            get

            {
                XPCollection<boKontakt> lstKontakt;
                if (this.Adresse != null)
                {
                     lstKontakt = new XPCollection<boKontakt>(this.Session, new GroupOperator(new BinaryOperator("Adresse.Oid", this.Adresse.Oid, BinaryOperatorType.Equal), new BinaryOperator("Position.Bezeichnung", "Geschäftsführer", BinaryOperatorType.Equal)));
                }
                else
                {
                    lstKontakt = null;
                }

                return lstKontakt;
            }
        }

        [XafDisplayName("Adresse")]
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
           
       

       

        [XafDisplayName("Steuernummer")]
        public System.String Steuernummer
        {
            get
            {
                return _steuernummer;
            }

            set
            {
                SetPropertyValue("Steuernummer", ref _steuernummer, value);
            }
        }
        [XafDisplayName("UstId")]
        public System.String UstId
        {
            get
            {
                return _ustId;
            }
            set
            {
                SetPropertyValue("UstId", ref _ustId, value);
            }
        }
        [XafDisplayName("Registernummer")]
        public System.String Registernummer
        {
            get
            {
                return _registernummer;
            }
            set
            {
                SetPropertyValue("Registernummer", ref _registernummer, value);
            }
        }
        [XafDisplayName("Registergericht")]
        public System.String Registergericht
        {
            get
            {
                return _registergericht;
            }
            set
            {
                SetPropertyValue("Registergericht", ref _registergericht, value);
            }
        }

        [XafDisplayName("Aktiv")]
        [CaptionsForBoolValues("Ja","Nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public System.Boolean Aktiv
        {
            get
            {
                return _aktiv;
            }
            set
            {
                SetPropertyValue("Aktiv", ref _aktiv, value);
            }
        }

        [XafDisplayName("Standardmandant")]
        [CaptionsForBoolValues("Ja", "Nein")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        public System.Boolean IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                SetPropertyValue("IsDefault", ref _isDefault, value);
            }
        }
        /*
        [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 30, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        [XafDisplayName("Firmenlogo")]
        */
        // [Delayed("Logo")]
        [XafDisplayName("Firmenlogo")]
        public byte[] Logo
        {
            get
            {
                return GetPropertyValue<byte[]>("Logo");
            }
            set
            {
                SetPropertyValue<byte[]>("Logo", value);
            }
        }


        [XafDisplayName("Slogan")]
        public System.String Slogan
        {
            get
            {
                return _slogan;
            }
            set
            {
                SetPropertyValue("Slogan", ref _slogan, value);
            }
        }


        [XafDisplayName("Mandant")]
        public System.String Mandantenname
        {
            get
            {
                return _mandantenname;
            }
            set
            {
                SetPropertyValue("Mandantenname", ref _mandantenname, value);
            }
        }


        [XafDisplayName("Hausbetreuer")]
        [Association("boMandant-boHausverwalter"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boHausverwalter> Hausverwalter
        {
            get
            {
                return GetCollection<boHausverwalter>("Hausverwalter");
            }
        }


        [XafDisplayName("Hausverwalter")]
        [Association("boMandant-Hausbetreuer"), DevExpress.ExpressApp.DC.Aggregated]
      
        public XPCollection<fiHausbetreuer> lstHausbetreuer
        {
            get
            {
                return GetCollection<fiHausbetreuer>("lstHausbetreuer");
            }
        }

        [XafDisplayName("Liegenschaften")]
        [Association("boLiegenschaft-boMandant"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boLiegenschaft> Liegenschaften
        {
            get
            {
                return GetCollection<boLiegenschaft>("Liegenschaften");
            }
        }

        [XafDisplayName("FI-Objekte")]
        [Association("boMandant-boFiObjekt"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boFIObjekt> lstFiObjekte
        {
            get
            {
                return GetCollection<boFIObjekt>("lstFiObjekte");
            }
        }
        [XafDisplayName("Dokumentbibliotheken")]
        [Association("boMandant-boAttachmentBibliothek")]
        public XPCollection<boAttachmentBibliothek>  lstBibliotheken
        {
            get
            {
                return GetCollection<boAttachmentBibliothek>("lstBibliotheken");
            }
        }

        [XafDisplayName("Dokumente")]
        [DevExpress.Xpo.Aggregated]
        [Association("boMandant-boAttachment")]
        public XPCollection<boAttachment> lstDokumente
        {
            get
            {
                return GetCollection<boAttachment>("lstDokumente");
            }
        }


        [XafDisplayName("Nummernkreise")]
        [Association("boMandant-boNummernkreis"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boNummernkreis> lstNummernkreise
        {
            get
            {
                return GetCollection<boNummernkreis>("lstNummernkreise");
            }
        }

        //Die Mitarbeiter
        
        [XafDisplayName("Mitarbeiter")]
        [Association("boMandant-boMitarbeiter"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boMitarbeiter> lstMitarbeiter
        {
            get
            {
                return GetCollection<boMitarbeiter>("lstMitarbeiter");
            }
        }
        
        [XafDisplayName("Geschäftsbereiche")]
        [Association("boMandant-coreBusinessUnit"),DevExpress.Xpo.Aggregated]
        public XPCollection<coreBusinessUnit> lstBusinessUnits
        {
        get {
                return GetCollection<coreBusinessUnit>("lstBusinessUnits");
        }
        }
        [XafDisplayName("Kontaktinformationen")]
        [Association("boMandant-boMandantKommunikation")]
        public XPCollection<boMandantKommunikation> lstKommunikation
        {
            get
            {
                return GetCollection<boMandantKommunikation>("lstKommunikation");
            }
        }
        [XafDisplayName("Fremdsystem")]
       
        public fremdSysFremdsystem Fremdsystem
        {
            get
            {
                return _fremdsystem;
            }
            set
            {
                SetPropertyValue("Fremdsystem", ref _fremdsystem, value);
            }
        }

        //Debitorenkonto
        [XafDisplayName("Debitorenkonten")]
        [Association("boMandant-Debitorenkonto")]
        public XPCollection<Debitorenkonto> lstDebitorenkonten
        {
        get
        {
                return GetCollection<Debitorenkonto>("lstDebitorenkonten");
        }
        }

        [XafDisplayName("Kreditorenkonten")]
        [Association("boMandant-Kreditorenkonto")]
        public XPCollection<Kreditorenkonto> lstKreditorenkonten
        {
            get
            {
                return GetCollection<Kreditorenkonto>("lstKreditorenkonten");
            }
        }

      

    }
}