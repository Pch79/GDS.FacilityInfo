using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.BusinessManagement.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Vertrag.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Vertrag")]
    [Serializable]
    [XafDefaultProperty("Vertragsnummer")]
    [ImageName("BO_Contract")]
    public class vertragVertragBase : BaseObject
    {

        private System.String _vertragsnummer;
        private Debitorenkonto _kunde;
        private boMandant _mandant;
        private System.DateTime _laufzeitbeginn;
        private System.DateTime _laufzeitende;
        private System.Int32 _laufzeit;
        private System.Int32 _mindestlaufzeit;
        private enmVertragsStatus _vertragsstatus;
        private System.DateTime _abschlussdatum;
        private System.DateTime _kuendigungsdatum;
        private System.Int32 _kuendigungsfrist;
        private boKuendigungsGrund _kuendigungsgrund;
        
        public vertragVertragBase(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.Vertragsstatus = enmVertragsStatus.akquise;           
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

        [XafDisplayName("Kündigungsgrund")]
        public boKuendigungsGrund Kuendigungsgrund
        {
            get
            {
                return _kuendigungsgrund;
            }
            set
            {
                SetPropertyValue("Kuendigungsgrund", ref _kuendigungsgrund, value);
            }
        }


        [XafDisplayName("Vertragsleistungen")]
        [Association("vertragVertragBase-vertragVertragsleistung")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<vertragVertragsleistung> lstVertragsleistungen
        {
            get
            {
                return GetCollection<vertragVertragsleistung>("lstVertragsleistungen");
            }
        }


        [XafDisplayName("Abschlussdatum")]
        public System.DateTime Abschlussdatum
        {
            get
            {
                return _abschlussdatum;
            }
            set
            {
                SetPropertyValue("Abschlussdatum", ref _abschlussdatum, value);
            }
        }

        [XafDisplayName("Vertragsnummer")]
        public System.String Vertragsnummer
        {
            get
            {
                return _vertragsnummer;
            }
            set
            {
                SetPropertyValue("Vetragsnummer", ref _vertragsnummer, value);
            }
        }


        [XafDisplayName("Kunde")]
        public Debitorenkonto Kunde
        {
            get
            {
                return _kunde;
            }
            set
            {
                SetPropertyValue("Kunde", ref _kunde, value);
            }
        }


        [XafDisplayName("Laufzeitbeginn")]
        public System.DateTime Laufzeitbeginn
        {
            get
            {
                return _laufzeitbeginn;
            }
            set
            {
                SetPropertyValue("Laufzeitbeginn", ref _laufzeitbeginn, value);
            }
        }



        [XafDisplayName("Laufzeit")]
        public System.Int32 Laufzeit
        {
            get
            {
                return _laufzeit;
            }
            set
            {
                SetPropertyValue("Laufzeit", ref _laufzeit, value);
            }
        }


        [XafDisplayName("Mindestlaufzeit")]
        public System.Int32 Mindestlaufzeit
        {
            get
            {
                return _mindestlaufzeit;
            }
            set
            {
                SetPropertyValue("Mindestlaufzeit", ref _mindestlaufzeit, value);
            }
        }


        [XafDisplayName("Vetragsstatus")]
        [ReadOnly(true)]
        public enmVertragsStatus Vertragsstatus
        {
            get
            {
                return _vertragsstatus;
            }
            set
            {
                SetPropertyValue("Vertragsstatus", ref _vertragsstatus, value);
            }
        }


        [XafDisplayName("Laufzeitende")]
        public System.DateTime Laufzeitende
        {
            get
            {
                return _laufzeitende;
            }
            set
            {
                SetPropertyValue("Laufzeitende", ref _laufzeitende, value);
            }
        }


        [XafDisplayName("Kündigungsfrist")]
        public System.Int32 Kuendigungsfrist
        {
            get
            {
                return _kuendigungsfrist;
            }
            set
            {
                SetPropertyValue("Kuendigungsfrist", ref _kuendigungsfrist, value);
            }
        }


        [XafDisplayName("Kündigungsdatum")]
        public System.DateTime Kuendigungsdatum
        {
            get
            {
                return _kuendigungsdatum;
            }
            set
            {
                SetPropertyValue("Kuendigungsdatum", ref _kuendigungsdatum, value);
            }
        }
        #endregion
    }
}