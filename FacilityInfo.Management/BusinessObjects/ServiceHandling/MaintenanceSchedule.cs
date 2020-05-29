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
using FacilityInfo.Management.EnumStore;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;
using DevExpress.ExpressApp.Utils;

namespace FacilityInfo.Management.BusinessObjects.ServiceHandling
{
  [DefaultClassOptions]
  [XafDisplayName("Wartungsplan")]
  [XafDefaultProperty("Bezeichnung")]
  [ImageName("gearSettings_16")]

    public class MaintenanceSchedule : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
      
        private Int32 _anzahlTechniker;
        //Turnus
        private enmTurnus _turnus;
        private Int32 _turnusValue;
        //Oid der Anlagenart oder des Produktes
        private String _linkKey;
        public MaintenanceSchedule(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
          
        }
        #region Properties
        [XafDisplayName("LinkKey")]
        public String LinkKey
        {
            get { return _linkKey; }
            set { SetPropertyValue("LinkKey", ref _linkKey, value); }
        }
            
        [XafDisplayName("Anzahl Techniker")]
        public Int32 AnzahlTechniker
        {
            get { return _anzahlTechniker; }
            set { SetPropertyValue("AnzahlTechniker", ref _anzahlTechniker, value); }
        }
      
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

        [XafDisplayName("Postionen")]
        [Association("MaintenanceSchedule-MaintenancePosition")]
        public XPCollection<MaintenancePosition> lstMaintenancePosition
        {
            get { return GetCollection<MaintenancePosition>("lstMaintenancePosition"); }

        }

        // TODO: Implement later on
        /*
        [XafDisplayName("Dauer (Vorgabe)")]
        public Decimal DauerVorgabe
        {
            get
            {
                decimal retval = 0;
                if (this.lstWartungPosition != null)
                {
                    retval = this.lstWartungPosition.Select(t => t.ZeitVorgabe).Sum();
                }
                return retval;
            }
        }
        */
        //den Typ anzeigen
        [XafDisplayName("Typ")]
        [ValueConverter(typeof(TypeToStringConverter))]
        public Type SystemType
        {
            get
            {
                return this.GetType();
            }
        }
        #endregion
    }
}