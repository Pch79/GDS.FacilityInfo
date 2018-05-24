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
using FacilityInfo.GlobalObjects.DomainComponents;
using FacilityInfo.GlobalObjects.EnumStore;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.Core.BusinessObjects;


namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Dokument")]
    
    public class boAttachment : FileAttachmentBase
    {
        //das Dokument 
        private System.DateTime _uploaddate;
        private System.DateTime _lastchangedate;
        private System.String _extension;
        //private FileData _file;
        private Int32 _filesize;
        private System.String _filename;
        private boAttachmentBibliothek _bibliothek;
        private boAttachmentkategorie _attachmentkategorie;
        private System.String _beschreibung;
        private System.String _betreff;
        private PermissionPolicyUser _uploaduser;
        private boMandant _mandant;
        private boAttachmentkategorie _kategorie;
        //Key für das parent
        private System.String _parentkey;
        //key für das Basisobjekt
        private System.String _objektkey;
        //private System.String _linkedobject;



        public boAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Uploaddate = DateTime.Now;
            PermissionPolicyUser curUser = this.Session.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId);
            this.Uploaduser = curUser;
            //hier gleich den Mandanten Standardmässig setzen
            //gibt es zu dem User ein Mitarbeiterkonto??
            boMitarbeiter curMitarbeiter = this.Session.FindObject<boMitarbeiter>(new BinaryOperator("Systembenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));
            if(curMitarbeiter != null)
            {
                this.Mandant = this.Session.GetObjectByKey<boMandant>(curMitarbeiter.Mandant.Oid);
            }         
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "Mandant":
                    //immer auf die Standardbibliothek setzen
                    if(newValue != null)
                    {
                        boMandant selectedMandant = (boMandant)newValue;
                        boAttachmentBibliothek curBib = this.Session.FindObject<boAttachmentBibliothek>(new GroupOperator(new BinaryOperator("Mandant.Oid", selectedMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("Bezeichnung", "Standardbibliothek", BinaryOperatorType.Equal)));
                        if(curBib != null)
                        {
                            this.Bibliothek = this.Session.GetObjectByKey<boAttachmentBibliothek>(curBib.Oid);
                        }
                    }
                    break;
                case "File":
                    this.Filesize = this.File.Size;
                    //this.Extension = this.File.FileName.Substring(this.File.FileName.LastIndexOf('.'), this.File.FileName.Length-1);
                    break;
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            FileData curFile = this.File;

            if(curFile != null)
            {
                this.Filesize = this.File.Size;
               // this.Extension = this.File.FileName.Substring(this.File.FileName.LastIndexOf('.'), this.File.FileName.Length-1);
                //this.Filename = curFile.FileName;
                //this.Filesize = curFile.Size;
            }
            else
            {
                this.Filesize = 0;
                this.Filename = string.Empty;
            }
        }

        /*
        [XafDisplayName("Linked Object")]
        public System.String LinkedObject
        {
            get
            {
                return _linkedobject;
            }
            set
            {
                SetPropertyValue("LinkedObject", ref _linkedobject, value); 
            }
        }
        */
        [XafDisplayName("Parentkey")]
        public System.String Parentkey
        {
            get
            {
                return _parentkey;
            }
            set
            {
                SetPropertyValue("Parentkey", ref _parentkey, value);
            }
        }
        [XafDisplayName("Objektkey")]
        public System.String Objektkey
        {
            get
            {
                return _objektkey;
            }
            set
            {
                SetPropertyValue("Objektkey", ref _objektkey, value);
            }

        }
        [XafDisplayName("Kategorie")]
        public boAttachmentkategorie Kategorie
        {
            get
            {
                return _kategorie;
            }
            set
            {
                SetPropertyValue("Kategorie", ref _kategorie, value);
            }
        }


        [XafDisplayName("Mandant")]
        [Association("boMandant-boAttachment")]
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


        [XafDisplayName("Uploader")]
        public PermissionPolicyUser Uploaduser
        {
            get
            {
                return _uploaduser;
            }
            set
            {
                SetPropertyValue("Uploaduser", ref _uploaduser, value);
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

        [XafDisplayName("Betreff")]
        public System.String Betreff
        {
            get
            {
                return _betreff;
            }
            set
            {
                SetPropertyValue("Betreff", ref _betreff, value);
            }
        }

        [XafDisplayName("Doktyp")]
        public enmAttatchmenttyp Typ
        {
            get
            {
                Type curType = this.GetType();
                if (curType == typeof(boANAttachment))
                    return enmAttatchmenttyp.Anlage;

                if (curType == typeof(boLGAttachment))
                    return enmAttatchmenttyp.Liegenschaft;

                if (curType == typeof(boMAAttachment))
                    return enmAttatchmenttyp.Massnahme;

                if (curType == typeof(fiMesstypAttachment))
                    return enmAttatchmenttyp.Mestyp;

                if (curType == typeof(fiMessungAttachment))
                    return enmAttatchmenttyp.Messung;

                if (curType == typeof(fiHerstellerAttachment))
                    return enmAttatchmenttyp.Herstelleranlage;

                if (curType == typeof(fiHerstellerProduktAttachment))
                    return enmAttatchmenttyp.Produktattachment;


                return enmAttatchmenttyp.Attachment;
            }
        }


        public string Extension
        {
            get
            {
                return _extension;
            }

            set
            {
                SetPropertyValue("Extension", ref _extension, value);
            }
        }

        
        

        [XafDisplayName("Dateiname")]
        [ReadOnly(true)]
        public string Filename
        {
            get
            {
                return _filename;
            }

            set
            {
                SetPropertyValue("Filename", ref _filename, value);
            }
        }

        public DateTime Lastchangedate
        {
            get
            {
                return _lastchangedate;
            }

            set
            {
                SetPropertyValue("Lastchangedate", ref _lastchangedate, value);
            }
        }

        public DateTime Uploaddate
        {
            get
            {
                return _uploaddate;
            }

            set
            {
                SetPropertyValue("Uploaddate", ref _uploaddate, value);
            }
        }

        [XafDisplayName("Bibliothek")]
        [DataSourceCriteria("Mandant.Oid='@this.Mandant.Oid'")]
        [Association("boAttachmentBibliothek-boAttachment")]
        [ImmediatePostData]
        [RuleRequiredField]
        public boAttachmentBibliothek Bibliothek
        {
            get
            {
                return _bibliothek;
            }
            set
            {
                SetPropertyValue("Bibliothek", ref _bibliothek, value);

            }
        }
        [XafDisplayName("Kategorie")]
        [Association("boAttachment-boAtachmentkategorie")]
        public boAttachmentkategorie Attachmentkategorie
        {
            get
            {
                return _attachmentkategorie;
            }
            set
            {
                SetPropertyValue("Attachmentkategorie", ref _attachmentkategorie, value);
            }
        }

        [XafDisplayName("Dateigröße")]
        [ReadOnly(true)]
            
        public int Filesize
        {
            get
            {
                return _filesize;
            }

            set
            {
                SetPropertyValue("Filesize", ref _filesize, value);
            }
        }

        /*
        [Association("boAttachment-fiAttachmentreference")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<fiAttachmentreference> lstAttachmentReferences
        {
            get
            {
                return GetCollection<fiAttachmentreference>("lstAttachmentReferences");
            }
        }
        */

    }
}