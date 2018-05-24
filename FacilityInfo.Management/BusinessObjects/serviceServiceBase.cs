using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using System;

namespace FacilityInfo.Service.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Service")]
    [ImageName("setting_tools_16")]
    public class serviceServiceBase : BaseObject
    {
        private DateTime _datumGeplant;
        private DateTime _zeitGeplant;
        private DateTime _datumReal;
        private DateTime _zeitReal;

        private String _betreff;
        private String _beschreibung;

        //Status
        private enmServiceStatus _status;
        //wer soll das machen
        private boMitarbeiter _monteurDurchfuehrung;
        private boMitarbeiter _monteurGeplant;
        private boMitarbeiter _monteurVerantwortlich;


        //was soll gemacht werden 
        private serviceServicePaket _servicePaket;
        
        //Abrechung erfolgt dann über einen Vetrag
        //der Servicevertrag hat dann eine Kombination aus diversen Servicepaketen



        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public serviceServiceBase(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Verantwortlicher")]
        public boMitarbeiter MonteurVerantwortlich
        {
            get
            {
                return _monteurVerantwortlich;
            }
            set
            {
                SetPropertyValue("MonteurVerantwortlich", ref _monteurVerantwortlich, value);
            }
        }
        [XafDisplayName("Durchführender (Real)")]
        public boMitarbeiter MonteurGeplant
        {
            get
            {
                return _monteurGeplant;
            }
            set
            {
                SetPropertyValue("MonteurGeplant", ref _monteurGeplant, value);
            }
        }
        [XafDisplayName("Durchführender (geplant)")]
        public boMitarbeiter MonteurDurchfuehrung
        {
            get
            {
                return _monteurDurchfuehrung;
            }
            set
            {
                SetPropertyValue("MonteurDurchfuehrung", ref _monteurDurchfuehrung, value);
            }
        }
        [XafDisplayName("Servicepaket")]
        public serviceServicePaket ServicePaket
        {
            get
            {
                return _servicePaket;
            }
            set
            {
                SetPropertyValue("ServicePaket", ref _servicePaket, value);
            }

        }
        [XafDisplayName("Status")]
        public enmServiceStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetPropertyValue("Status", ref _status, value);
            }
        }
        [XafDisplayName("Betreff")]
        public String Betreff
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
        [XafDisplayName("Beschreibung")]
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

        [XafDisplayName("Plandatum")]
        public DateTime DatumGeplant
        {
            get
            {
                return _datumGeplant;
            }
            set
            {
                SetPropertyValue("DatumGeplant", ref _datumGeplant, value);
            }
        }
        [XafDisplayName("Planzeit")]
        public DateTime ZeitGeplant
        {
            get
            {
                return _zeitGeplant;
            }
            set
            {
                SetPropertyValue("ZeitGeplant", ref _zeitGeplant, value);
            }
        }
        [XafDisplayName("Ist-Datum")]
        public DateTime DatumReal
        {
            get
            {
                return _datumReal;
            }
            set
            {
                SetPropertyValue("DatumReal", ref _datumReal, value);
            }
        }

        [XafDisplayName("Ist-Zeit")]
     
        public DateTime ZeitReal
        {
            get
            {
                return _zeitReal;
            }
            set
            {
                SetPropertyValue("ZeitReal", ref _zeitReal, value);
            }
        }

        //TODO Die Serviceleistungen aus dem Paket ermitteln und dann das Wergebnis eintragen

        //die Dauer 

        #endregion
    }
}