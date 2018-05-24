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
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Management;
using System.IO;
using System.Collections;

namespace FacilityInfo.Import.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Dokumenttyp")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("document_import_16")]
    public class importDokumentTyp : BaseObject
    {
        private String _bezeichnung;
        private bool _aktiv;
        private String _kuerzel;
        private boMandant _mandant;
        private String _ordnername;
        private String _passwort;
        private importFileType _fileTyp;

        public importDokumentTyp(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            this.Mandant = this.Session.GetObjectByKey<boMandant>(clsStatic.loggedOnMandant.Oid);
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich der Importordner ändert
            DirectoryInfo oldDi = null;
            DirectoryInfo newDi = null;

            if(!this.Session.IsObjectToDelete(this))
            {
                if(!this.Session.IsObjectToSave(this))
                {
                    //
                    switch (propertyName)
                    {
                        case "Ordnername":
                            if(oldValue != null)
                            {
                                oldDi = new DirectoryInfo(String.Format("{0}\\Datentransfer\\In\\{1}", this.Mandant.HomeDirectory.FullName, oldValue.ToString()));
                                if(oldDi.Exists)
                                {
                                   if(oldValue.ToString() != newValue.ToString())
                                    {
                                        oldDi.MoveTo(String.Format("{0}\\Datentransfer\\In\\{1}", this.Mandant.HomeDirectory.FullName, newValue.ToString()));
                                    }
                                }
                                else
                                {
                                    newDi = new DirectoryInfo(String.Format("{0}\\Datentransfer\\In\\{1}", this.Mandant.HomeDirectory.FullName, newValue.ToString()));
                                    if(!newDi.Exists)
                                    {
                                        newDi.Create();
                                    }
                                }
                            }
                            else
                            {
                                newDi = new DirectoryInfo(String.Format("{0}\\Datentransfer\\In\\{1}", this.Mandant.HomeDirectory.FullName, newValue.ToString()));
                                if (!newDi.Exists)
                                {
                                    newDi.Create();
                                }
                            }

                            break;
                    }
                }
            }

        }

        protected override void OnSaved()
        {
            base.OnSaved();

            //gib tes den Ordner schon??
            DirectoryInfo curDi = null;
            curDi = new DirectoryInfo(String.Format("{0}\\Datentransfer\\In\\{1}", this.Mandant.HomeDirectory.FullName, this.Ordnername));
            if (!curDi.Exists)
            {
                curDi.Create();
            }
        }

        //was mach ich mit bestehenden 
        #region Properties

        [XafDisplayName("Importdirectory")]
        public DirectoryInfo ImportDirectory
        {
            get
            {
                DirectoryInfo retVal;

                if (this.Ordnername != null && this.Ordnername != string.Empty)
                {
                    retVal = new DirectoryInfo(String.Format("{0}\\Datentransfer\\In\\{1}", this.Mandant.HomeDirectory.FullName, this.Ordnername));
                }
                else
                {
                    retVal = null;
                }
                return retVal;
            }
        }
    
      
        [XafDisplayName("Importordner")]
        [RuleRequiredField]
        public String Ordnername
        {
            get
            {
                return _ordnername;
            }
            set
            {
                SetPropertyValue("Ordnername", ref _ordnername, value);
            }
        }

        [XafDisplayName("Mandant")]
        [RuleRequiredField]
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
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
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
        [XafDisplayName("Kürzel")]
        public String Kuerzel
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
        [XafDisplayName("Aktiv")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public bool Aktiv
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
        //Der hat Dokumentobjekte
        //
        [XafDisplayName("Dokumentobjekte")]
        [Association("importDokumentTyp-importDokumentObjekt")]
        public XPCollection<importDokumentObjekt> lstDokumentObjekte
        {
            get
            {
                return GetCollection<importDokumentObjekt>("lstDokumentObjekte");
            }
        }

        [XafDisplayName("Passwort")]
        
        public String Passwort
        {
            get
            {
                return _passwort;
            }
            set
            {
                SetPropertyValue("Passwort", ref _passwort, value);
            }
        }

        [XafDisplayName("Filetype")]
        [RuleRequiredField]
        public importFileType FileType
        {
            get
            {
                return _fileTyp;
            }
            set
            {
                SetPropertyValue("FileType", ref _fileTyp, value);

            }              
        }
        #endregion

    }
}