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
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;

namespace FacilityInfo.Fremdsystem.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("WartungsAnlage")]
    [XafDefaultProperty("Anlagennummer")]
    [ImageName("mail_server_setting_16")]
    public class KwpWartungsAnlage : BaseObject
    {
        private boLiegenschaft _liegenschaft;
        private System.String _fremdsystemId;
        private String _anlagenNummer;
        private boAdresse _anlagenAdresse;
        private boHausverwalter _hausverwalter;
        private fiHausbetreuer _hausBetreuer;
        private String _bezeichnung;
        private String _anlagenOrt;
        private String _monteuer;
        private String _brennstoffart;
        private boMandant _mandant;
        private KwpWartungsVertrag _kwpWartungsVertrag;
        private String _bemerkungen;
        private String _kwpSelekt;


        public KwpWartungsAnlage(Session session)
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
        [XafDisplayName("Anlagennummer")]
        public String Anlagennummer
        {
          get {
                return _anlagenNummer;
          }
          set {
                SetPropertyValue("AnlagenNummer", ref _anlagenNummer, value);
          }
        }
       [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-kwpWartungsAnlage")]
        public boLiegenschaft Liegenschaft
       {
       get
       {
                return _liegenschaft;
       }
       set
       {
                SetPropertyValue("Liegenschaft", ref _liegenschaft, value);
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
        [XafDisplayName("Anlagenadresse")]
        public boAdresse AnlagenAdresse
        {
        get
        {
                return _anlagenAdresse;
        }
        set
        {
                SetPropertyValue("Anlagenadresse", ref _anlagenAdresse, value);
        }
        }
        [XafDisplayName("Hausverwalter")]
        public boHausverwalter Hausverwalter
        {
            get
            {
                return _hausverwalter;
            }
            set
            {
                SetPropertyValue("Hausverwalter", ref _hausverwalter, value);
            }
        }

        [XafDisplayName("Hausbetreuer")]
        public fiHausbetreuer HausBetreuer
        {
            get
            {
                return _hausBetreuer;
            }
            set
            {
                SetPropertyValue("HausBetreuer", ref _hausBetreuer, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        [Size(-1)]
        public String Bezeichnung
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
        [XafDisplayName("Anlagenort")]
        [Size(-1)]
        public String AnlagenOrt
        {
        get
        {
                return _anlagenOrt;
        }
        set
        {
                SetPropertyValue("AnlagenOrt", ref _anlagenOrt, value);
        }
        }
        [XafDisplayName("Monteuer")]
        public String Monteur
        {
        get
        {
                return _monteuer;
        }
        set
        {
                SetPropertyValue("Monteuer", ref _monteuer, value);
        }
        }
        [XafDisplayName("Brennstoffart")]
        public String BrennstoffArt
        {
        get
        {
                return _brennstoffart;
        }
        set
        {
                SetPropertyValue("BrennstoffArt", ref _brennstoffart, value);
        }
        }
        [XafDisplayName("Bemerkungen")]
        [Size(-1)]
        public String Bemerkungen
        {
            get
            {
                return _bemerkungen;
            }
            set
            {
                SetPropertyValue("Bemerkungen", ref _bemerkungen, value);
            }
        }
        [XafDisplayName("KwpSelekt")]
        public String KwpSelekt
        {
            get
            {
                return _kwpSelekt;
            }
            set
            {
                SetPropertyValue("KwpSelekt", ref _kwpSelekt, value);
            }
        }
        //Anlage hat einen Vertrag
        [XafDisplayName("Wartungsvertrag")]
        public KwpWartungsVertrag WartungsVertrag
        {
          get {
                return _kwpWartungsVertrag;
          }
          set {
                SetPropertyValue("WartungsVertrag", ref _kwpWartungsVertrag, value);
          }
        }
        //Anlage hat Aufträge
        [XafDisplayName("Wartungsaufträge")]
        public XPCollection<KwpWartungsAuftrag> lstWartungsAuftrag
        {
          get 
          {
                XPCollection<KwpWartungsAuftrag> lstRetVal = new XPCollection<KwpWartungsAuftrag>(this.Session, new BinaryOperator("KwpAnlagenNummer", this.Anlagennummer, BinaryOperatorType.Equal));
                return lstRetVal;
          }
        }
        //Anlage hat termine
        [XafDisplayName("Wartungstermine")]

        public XPCollection<KwpWartTermin> lstWartungsTermin
        {
            get
            {
                XPCollection<KwpWartTermin> lstRetVal = new XPCollection<KwpWartTermin>(this.Session, new BinaryOperator("AnlagenNummer", this.Anlagennummer, BinaryOperatorType.Equal));
                return lstRetVal;
            }
        }
        #endregion

    }
    }