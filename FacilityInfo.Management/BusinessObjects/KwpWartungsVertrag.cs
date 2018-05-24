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
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Fremdsystem.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Wartungsvertrag (KWP)")]
    [ImageName("page_white_wrench_16")]
    [XafDefaultProperty("VertragsNummer")]
    public class KwpWartungsVertrag : BaseObject
    {
        private boLiegenschaft _liegenschaft;
        private System.String _fremdsystemId;
        private System.String _vertragsText;
        private System.String _vertragsNummer;
        private System.String _linkKeyLiegenschaft;
        private System.String _bezeichnung;
        private System.DateTime _vertragsDatum;
        private DateTime _vertragsBeginn;
        private DateTime _vertragsEnde;
        private System.Boolean _vertragZurueck;
        private boMandant _mandant;
        private KwpWartungsAnlage _kwpAnlage;
        private DateTime _kuendigungsDatum;
        private String _KuendigungsGrund;
        




        public KwpWartungsVertrag(Session session)
            : base(session)
        {
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
          
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            
        }

        [XafDisplayName("Vertragsstatus")]
        public enmVertragsStatus VertragsStatus
        {
            get
            {

                enmVertragsStatus retVal;
                
                    if (this.KuendigungsDatum != null && this.KuendigungsDatum != DateTime.MinValue)
                    {
                        retVal = enmVertragsStatus.gekündigt;
                    }
                    else
                    {
                    if (this.VertragZurueck)
                    {
                        retVal = enmVertragsStatus.aktiv;
                    }
                    else
                    {
                        retVal = enmVertragsStatus.akquise;
                    }
                    }
                
                

                return retVal;
            }
            
        }
        [XafDisplayName("Kündigungsdatum")]
        public DateTime KuendigungsDatum
        {
            get
            {
                return _kuendigungsDatum;
            }
            set
            {
                SetPropertyValue("KuendigungsDatum", ref _kuendigungsDatum, value);
            }
        }
        [XafDisplayName("Kündigungsgrund")]
        public String KuendigungsGrund
        {
            get
            {
                return _KuendigungsGrund;
            }
            set {
                SetPropertyValue("KuendigungsGrund", ref _KuendigungsGrund, value);
            }
        }
        [XafDisplayName("Vertrag zurück")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public Boolean VertragZurueck
        {
        get
        {
                return _vertragZurueck;
        }
        set {
                SetPropertyValue("VertragZurueck", ref _vertragZurueck, value);
        }
        }

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

        [XafDisplayName("Vertragsende")]
        public DateTime VertragsEnde
        {
            get
            {
                return _vertragsEnde;
            }
            set
            {
                SetPropertyValue("VertragsEnde", ref _vertragsEnde, value);
            }
        }

        [XafDisplayName("Vertragsbeginn")]
        public DateTime VertragsBeginn
        {
            get
            {
                return _vertragsBeginn;
            }
            set
            {
                SetPropertyValue("VertragsBeginn", ref _vertragsBeginn, value);
            }
        }

        [XafDisplayName("Vertragsdatum")]
        public DateTime VertragsDatum
        {
            get
            {
                return _vertragsDatum;
            }
            set
            {
                SetPropertyValue("VertragsDatum", ref _vertragsDatum, value);
            }
        }

        [XafDisplayName("Bezeichnung")]
        [Size(200)]
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

        [XafDisplayName("LinkKeyLiegenschaft")]
        public System.String LinkKeyLiegenschaft
        {
            get
            {
                return _linkKeyLiegenschaft;
            }
            set
            {
                SetPropertyValue("LinkKeyLiegenschaft", ref _linkKeyLiegenschaft, value);
            }
        }

        [XafDisplayName("Vertragsnummer")]

        public System.String VertragsNummer
        {
            get
            {
                return _vertragsNummer;
            }
            set
            {
                SetPropertyValue("VertragsNummer", ref _vertragsNummer, value);
            }
        }
        [XafDisplayName("Vertragstext")]
        [Size(-1)]
        public System.String Vertragstext
        {
            get
            {
                return _vertragsText;
            }
            set
            {
                SetPropertyValue("Vertragstext", ref _vertragsText, value);
            }
        }
        [XafDisplayName("FremdsystemID")]
        public System.String FremdsystemId
        {
        get {
                return _fremdsystemId;
        }
        set {
                SetPropertyValue("FremdsystemId", ref _fremdsystemId, value);
        }
        }

        
        [Association("boLiegenschaft-KwpWartungsVertrag")]
        [XafDisplayName("Liegenschaft")]
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

        [XafDisplayName("Wartungsanlage")]
        public KwpWartungsAnlage WartungsAnlage
        {
          get {
                return _kwpAnlage;
          }
          set {
                SetPropertyValue("WartungsAnlage", ref _kwpAnlage, value);
          }
        }
    }
}