using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using System;

using System.Linq;

namespace FacilityInfo.Fremdsystem.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Wartungstermin (KWP)")]
    [ImageName("calendar_16")]

    public class KwpWartTermin : BaseObject
    {
        private String _fremdsystemId;
        private Int32 _laufendeNummer;
        private String _anlagenNummer;
        private Int32 _wartungsjahr;

        //das mus sich auch noch anders verwursten
        private Int32 _monatKw;
        //das Intervall muss als datenstruktur dargestellt werden
        private Int32 _intervall;
        private Int32 _intervallArt;

        private DateTime _terminDatum;
        private DateTime _terminUhrzeit;
        private String _monteur;
        private String _hauptMonteur;
        private String _infoText;
        private Decimal _planStunden;
        private boMandant _mandant;
        private KwpWartungsAnlage _kwpAnlage;
   
     

        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public KwpWartTermin(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
      
       
        [XafDisplayName("Infotext")]
        public String InfoTextAnlage
        {
            get
            {
                var retVal = string.Empty;
                var ort = string.Empty;
                var bem = string.Empty;
                ort = (this.KwpAnlage.AnlagenOrt != null) ? this.KwpAnlage.AnlagenOrt : "k.A.";
                bem = (this.KwpAnlage.Bemerkungen != null) ? this.KwpAnlage.Bemerkungen : "k.A.";
             
                retVal = String.Format("Anlagenort: {0}; Bemerkungen: {1}",ort,bem);
                 
                return retVal;
            }
        }

        [XafDisplayName("Liegenschaft")]
        
        public boLiegenschaft Liegenschaft
        {
            get
            {
                boLiegenschaft retVal = null;
                if (this.KwpAnlage != null)
                {
                    retVal = this.KwpAnlage.Liegenschaft;
                }
                return retVal;
            }
        }

        [XafDisplayName("Kwp-Anlage")]
        [Association("KwpWarttermin-KwpWartungsAnlage")]
        public KwpWartungsAnlage KwpAnlage
        {
            get
            {
                return _kwpAnlage;
            }
            set
            {
                SetPropertyValue("KwpAnlage", ref _kwpAnlage, value);
            }
        }

        //hier kann ich gleich den vertag auch ausgeben
        public KwpWartungsVertrag WartungsVertrag
        {
            get
            {
                KwpWartungsVertrag retVal = null;
                if(this.KwpAnlage != null)
                {
                    retVal = (this.KwpAnlage.WartungsVertrag != null) ? this.KwpAnlage.WartungsVertrag : null;
                }
                return retVal;

            }
        }

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
        [XafDisplayName("Planstunden")]
        public Decimal PlanStunden
        {
            get
            {
                return _planStunden;
            }
            set
            {
                SetPropertyValue("PlanStunden", ref _planStunden, value);
            }
        }

        [XafDisplayName("FremdsystemID")]
        public String FremdsystemId
        {
            get
            {
                return _fremdsystemId;
            }
            set
            {
                SetPropertyValue("FremdsystemId", ref _fremdsystemId, value);
            }
        }
        [XafDisplayName("Laufende Nummer")]
        public Int32 Laufendenummer
        {
            get
            {
                return _laufendeNummer;
            }
            set
            {
                SetPropertyValue("LaufendeNummer", ref _laufendeNummer, value);
            }
        }
        [XafDisplayName("Anlagennummer")]
        public String AnlagenNummer
        {
            get
            {
                return _anlagenNummer;
            }
            set
            {
                SetPropertyValue("AnlagenNummer", ref _anlagenNummer, value);
            }
        }
        [XafDisplayName("Wartungsjahr")]
        public Int32 WartungsJahr
        {
            get
            {
                return _wartungsjahr;
            }
            set
            {
                SetPropertyValue("WartungsJahr", ref _wartungsjahr, value);
            }
        }
        [XafDisplayName("Monat/KW")]
        public Int32 MonatKw
        {
            get
            {
                return _monatKw;
            }
            set
            {
                SetPropertyValue("MonatKw", ref _monatKw, value);
            }
        }
        [XafDisplayName("Intervall")]
        public Int32 Intervall
        {
            get
            {
                return _intervall;
            }
            set
            {
                SetPropertyValue("Intervall", ref _intervall, value);
            }
        }


        [XafDisplayName("IntervallArt(Int)")]
        public Int32 IntervallArt
        {
            get
            {
                return _intervallArt;
            }
            set
            {
                SetPropertyValue("IntervallArt", ref _intervallArt, value);
            }
        }
        [XafDisplayName("Turnus")]
        public enmTurnus Turnus
        {
            get
            {
                enmTurnus retVal = enmTurnus.none;
                retVal = (enmTurnus)this.IntervallArt;
                return retVal;


            }

        }
        [XafDisplayName("Termin (Datum)")]
        public DateTime TerminDatum
        {
            get
            {
                return _terminDatum;
            }
            set
            {
                SetPropertyValue("TerminDatum", ref _terminDatum, value);
            }
        }
        [XafDisplayName("Termin (Uhrzeit)")]
        [Custom("DisplayFormat", "0:t")]
        public DateTime TerminUhrzeit
        {
            get
            {
                return _terminUhrzeit;
            }
            set
            {
                SetPropertyValue("TerminUhrzeit", ref _terminUhrzeit, value);
            }
        }
        [XafDisplayName("Monteuer")]
        public String Monteur
        {
            get
            {
                return _monteur;
            }
            set
            {
                SetPropertyValue("Monteur", ref _monteur, value);
            }
        }
        [XafDisplayName("Hauptmonteuer")]
        public String HauptMonteuer
        {
            get
            {
                return _hauptMonteur;
            }
            set
            {
                SetPropertyValue("HauptMonteur", ref _hauptMonteur, value);
            }
        }

        [XafDisplayName("InfoText")]
        [Size(-1)]
        public String InfoText
        {
            get
            {
                return _infoText;
            }
            set
            {
                SetPropertyValue("InfoText", ref _infoText, value);
            }
        }

        // weitere ermine in der Liegenschaft
        [XafDisplayName("weitere Termine")]
        public XPCollection<KwpWartTermin> lstFurtherDates
        {
            get
            {

                XPCollection<KwpWartTermin> lstResult = new XPCollection<KwpWartTermin>(this.Session, new GroupOperator(new BinaryOperator("Liegenschaft.Oid", this.Liegenschaft.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", this.FremdsystemId, BinaryOperatorType.NotEqual)));
                    
                return lstResult;
                
            }
        }



        #endregion
    }
}