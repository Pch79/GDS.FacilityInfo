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
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;

namespace FacilityInfo.Fremdsystem.BusinessObjects
{
    [DefaultClassOptions]
  [XafDisplayName("Wartungsauftrag (KWP)")]
  [ImageName("bell_16")]
    public class KwpWartungsAuftrag : BaseObject
    {
        private String _auftragsnrKwp;
        private String _fremdsystemId;
        private String _kwpAnlagenNummer;
        private DateTime _analagedatum;
        private String _betreff;
        private String _hautpmonteur;
        private String _monteuer;
        private DateTime _terminDatum;
        private DateTime _terminZeit;
        private Decimal _planstunden;
        private boMandant _mandant;
        
           
     // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public KwpWartungsAuftrag(Session session)
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
          get {
                return _mandant;
          }
          set {
                SetPropertyValue("Mandant", ref _mandant, value);
          }
        }
        
        [XafDisplayName("Auftragsnummer(KWP)")]
        public String AuftragsnummerKwp
        {
         get {
                return _auftragsnrKwp;
         }
         set {
                SetPropertyValue("AuftragsnummerKwp", ref _auftragsnrKwp, value);
         }
        }
        [XafDisplayName("FremdsystemID")]
        public String FremdsystemId
        {
        get {
                return _fremdsystemId;
        }
        set {
                SetPropertyValue("FremdsystemId", ref _fremdsystemId, value);
        }
        }
        [XafDisplayName("Anlagennummer (KWP)")]
        public String KwpAnlagenNummer
        {
        get {
                return _kwpAnlagenNummer;
        }
        set {
                SetPropertyValue("KwpAnlagenNummer", ref _kwpAnlagenNummer, value);
        }
        }
        [XafDisplayName("Betreff")]
        [Size(-1)]
        public String Betreff
        {
          get {
                return _betreff;
          }
          set {
                SetPropertyValue("Betreff", ref _betreff, value);
          }
        }
        [XafDisplayName("Hauptmonteuer")]
        public String HauptMonteuer
        {
        get {
                return _hautpmonteur;
        }
        set {
                SetPropertyValue("HauptMonteur", ref _hautpmonteur, value);
        }
        }
        [XafDisplayName("Monteuer")]
        public String Monteuer
        {
        get {
                return _monteuer;
        }
        set {
                SetPropertyValue("Monteur", ref _monteuer, value);
        }
        }
        [XafDisplayName("Termin (Datum)")]
        public DateTime TerminDatum
        {
        get {
                return _terminDatum;
        }
        set {
                SetPropertyValue("TerminDatum", ref _terminDatum, value);
        }
        }
        [XafDisplayName("Termin (Zeit)")]
        [Custom("DisplayFormat","0:t")]
        public DateTime TerminZeit
        {
        get {
                return _terminZeit;
        }
        set {
                SetPropertyValue("TerminZeit", ref _terminZeit, value);
        }
        }
        [XafDisplayName("Planstunden")]
        public Decimal Planstunden
        {
        get {
                return _planstunden;
        }
        set {
                SetPropertyValue("Planstunden", ref _planstunden, value);
        }
        }

        //die Liegenschaft ergibt sich aus der ANlage
      

        [XafDisplayName("KWP-Anlage")]
        public KwpWartungsAnlage KwpAnlage
        {
            get
            {
                KwpWartungsAnlage retVal = this.Session.FindObject<KwpWartungsAnlage>(new BinaryOperator("FremdsystemId", this.KwpAnlagenNummer, BinaryOperatorType.Equal));
                return retVal;
            }
        }
        #endregion
    }
}