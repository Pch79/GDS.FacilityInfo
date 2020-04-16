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

namespace FacilityInfo.Action.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Maßnahmenpakt")]
    public class actionActionPackage : BaseObject
    {
        private boAnlagenArt _basisAnlagenArt;
        private String _bezeichnung;
        private String _beschreibung;
        private Boolean _isActive;
        
        public actionActionPackage(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Aktiv")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public Boolean IsActive
        {
            get { return _isActive; }
            set { SetPropertyValue("IsActive", ref _isActive, value); }
        }
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
        {
            get { return _bezeichnung; }
            set { SetPropertyValue("Bezeichnung", ref _bezeichnung, value); }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
        {
            get { return _beschreibung; }
            set { SetPropertyValue("Beschreibung", ref _beschreibung, value); }
        }
        [XafDisplayName("Anlagenart (Basis)")]
        [Association("boAnlagenArt-actionActionPackage")]
        public boAnlagenArt basisAnlagenArt
        {
            get
            {
                return _basisAnlagenArt;
            }
            set { SetPropertyValue("BasisAnlagenArt", ref _basisAnlagenArt, value); }
        }

        //alle Maßnahmen die zu der basisanlage gehören -> alle Maßnahmen der Anlagengruppe

        
        #endregion

    }
}