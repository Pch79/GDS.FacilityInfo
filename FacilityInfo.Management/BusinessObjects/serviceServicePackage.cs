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
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Servicepaket")]
    public class serviceServicePackage : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
        private boAnlagenArt _basisAnlagenArt;
        private boMandant _mandant;
        private Boolean _isActive;

        public serviceServicePackage(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
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
        [XafDisplayName("Basisanlage")]
        [RuleRequiredField]
        [ImmediatePostData(true)]
        public boAnlagenArt basisAnlagenArt
        {
            get
            {
                return _basisAnlagenArt;
            }
            set
            {
                SetPropertyValue("BasisAnlagenArt", ref _basisAnlagenArt, value);
            }
        }
        [XafDisplayName("Aktiv")]
        [CaptionsForBoolValues("ja", "nein")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        [ImmediatePostData(true)]
        public System.Boolean IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                SetPropertyValue("IsActive", ref _isActive, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        //ist ja eigentlich die Anlagenart
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

        //Welche Anlagenarten ghören dazu?
        [XafDisplayName("Anlagenarten")]
        [Association("serviceServicePackage-boAnlagenArt")]
        public XPCollection<boAnlagenArt> lstAnlagenarten
        {
            get
            {
                return GetCollection<boAnlagenArt>("lstAnlagenarten");
            }
        }

        #endregion
    }
}