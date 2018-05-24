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

namespace FacilityInfo.Import.BusinessObjects
{
 [DefaultClassOptions]
 [XafDisplayName("Filetype")]
 [ImageName("page_white_magnify_16")]
    public class importFileType : BaseObject
    {
        private String _name;
        private String _beschreibung;
        private String _extension;
        private bool _allowed;
        public String _application;

        
        public importFileType(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Allowed = true;
        }
        [XafDisplayName("Anwendung")]
        [RuleRequiredField]
        public String Application
        {
            get
            {
                return _application;
            }
            set {
                SetPropertyValue("Application", ref _application, value);
            }
        }

        [XafDisplayName("Name")]
        public String Name
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
        [Size(255)]
        public String Beschreibung
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

        [XafDisplayName("Extension")]
        public String Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                string retVal = value;
                //wenn der Punkt bereits eingegebn wirde nix machen ansonsten den Punkt voranstellen
                if(retVal.First<char>() != '.')
                {
                    retVal = string.Format(".{0}", retVal);
                }
                SetPropertyValue("Extension", ref _extension, retVal);
            }
        }
        [XafDisplayName("Allowed")]
        [CaptionsForBoolValues("ja","nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public bool Allowed
        {
            get
            {
                return _allowed;
            }
            set
            {
                SetPropertyValue("Allowed", ref _allowed, value);
            }
        }
    }
}