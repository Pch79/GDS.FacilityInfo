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
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.BusinessObjects.ServiceHandling
{
    [DefaultClassOptions]
   [XafDisplayName("Service-Specification")]
    [ImageName("gearTool_16")]
    [XafDefaultProperty("MatchKey")]
    public class ServiceSpecification : BaseObject
    {
        private boAnlagenArt _anlagenArt;
        private String _bezeichnung;
        private String _beschreibung;
        private enmTurnus _turnus;
        private Int32 _turnusValue;

        public ServiceSpecification(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        #region Properties

        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField]
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


        [XafDisplayName("Turnus")]
        public enmTurnus Turnus
        {
            get { return _turnus; }
            set { SetPropertyValue("Turnus", ref _turnus, value); }
        }
        [XafDisplayName("Turnuswert")]
        public Int32 TurnusValue
        {
            get { return _turnusValue; }
            set { SetPropertyValue("TurnusValue", ref _turnusValue, value); }
        }
        [XafDisplayName("Matchkey")]
        public String MatchKey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var art = string.Empty;
                bezeichnung=(this.Bezeichnung != null)?this.Bezeichnung:"n.a.";
                art = (this.AnlagenArt != null)?this.AnlagenArt.Bezeichnung:"n.a.";
                retVal = String.Format("{0} - {1}", art, bezeichnung);
                return retVal;
            }
        }
        [XafDisplayName("Anlagenart")]
        [Association("boAnlagenArt-ServiceSpecification")]
        public boAnlagenArt AnlagenArt
        {
            get { return _anlagenArt; }
            set { SetPropertyValue("AnlagenArt", ref _anlagenArt, value); }
        }

        [XafDisplayName("Postionen")]
        [Association("ServiceSpecification-ServicePosition")]
        public XPCollection<ServicePosition> lstServicePosition
        {
            get { return GetCollection<ServicePosition>("lstServicePosition"); }

        }
        #endregion
    }
}