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
using FacilityInfo.GlobalObjects.EnumStore;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Messung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Messtyp")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("tags_cloud")]
    [RuleObjectExists(DefaultContexts.Save,"Mandant.Oid = '@Mandant.Oid' AND Bezeichnung = '@Bezeichnung'",InvertResult =true)]
    public class boMesstyp : BaseObject
    {
        private System.String _bezeichnung;
        private System.Int32 _mindestprobenzahl;
        private System.Int32 _turnunsvalue;
        private enmTurnus _turnus;
        private boMandant _mandant;
        private boAnlagenArt _anlagenart;
        
        private System.String _beschreibung;

        public boMesstyp(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Anlagenart")]
        [Association("boMesstyp-boAnlagenArt")]
        public boAnlagenArt Anlagenart
        {
            get
            {
                return _anlagenart;
            }
            set
            {
                SetPropertyValue("Anlagenart", ref _anlagenart, value);
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

        [XafDisplayName("Beschereibung")]
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

        [XafDisplayName("Turnus")]
        public enmTurnus Turnus
        {
            get
            {
                return _turnus;
            }
            set
            {
                SetPropertyValue("Turnus", ref _turnus, value);
            }
        }
        [XafDisplayName("Turnuswert")]

        public System.Int32 Turnuswert
        {
            get
            {
                return -_turnunsvalue;
            }
            set
            {
                SetPropertyValue("Turnuswert", ref _turnunsvalue, value);
            }
        }
        [XafDisplayName("Mindestprobenzahl")]
        public System.Int32 Mindestprobenzahl
        {
            get
            {
                return _mindestprobenzahl;
            }
            set
            {
                SetPropertyValue("Mindestprobenzahl", ref _mindestprobenzahl, value);
            }
        }
       
        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField]
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

        [XafDisplayName("MessitemEntry")]
        [Association("boMesstyp-boMessitemEntry"),DevExpress.Xpo.Aggregated]
        public XPCollection<boMessitemEntry> lstMessItemEntries
        {
            get
            {
                return GetCollection<boMessitemEntry>("lstMessItemEntries");
            }
        }

        [XafDisplayName("Dokumente (Typspezifisch)")]
        [Association("boMesstyp-fiMesstypAttachment")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<fiMesstypAttachment> lstMesstypDokumente
        {
            get
            {
                return GetCollection<fiMesstypAttachment>("lstMesstypDokumente");
            }
        }

        //hier kommt das Gerät noch hinzu
        /*
        [XafDisplayName("Geräte")]
        [Association("boMesstyp-boGeraet")]
        public XPCollection<boGeraet> lstGeraete
        {
            get
            {
                return GetCollection<boGeraet>("lstGeraete");
            }

        }
        */
        #endregion
    }
}