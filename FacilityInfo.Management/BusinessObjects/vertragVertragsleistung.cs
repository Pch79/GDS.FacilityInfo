using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FacilityInfo.Management.EnumStore;
using System;

using System.Linq;

namespace FacilityInfo.Vertrag.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Vertragsleistung")]
    [Serializable]
    [XafDefaultProperty("Name")]
    public class vertragVertragsleistung : BaseObject
    {
        private vertragVertragBase _vertrag;
        private System.DateTime _zugang;
        private System.DateTime _abgang;
        private fiProdukt _produkt;
        private System.String _name;
        private System.Decimal _vkfaktor;
        private System.Int32 _turnusvalue;
        private enmTurnus _leistungsturnus;
        private enmVertragsStatus _leistungsstatus;
        private System.Int32 _menge;
        private System.String _leistungsnummer;
        private enmTurnus _abrechnungsturnus;
        private System.Int32 _abrechnungturnusvalue;
        

        public vertragVertragsleistung(Session session)
            : base(session)
        {
        }
        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Zugang = DateTime.Now;
            this._leistungsstatus = enmVertragsStatus.aktiv;
            this.Menge = 1;
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "Produkt":
                    if(newValue != null)
                    {
                        this.Name = ((fiProdukt)newValue).Name;
                    }
                    else
                    {
                        this.Name = string.Empty;
                        
                    }

                    break;
            }
        }
        #endregion

        #region berechnete Felder



        [XafDisplayName("VK-Gesamt")]
        public System.Decimal VkGesamt
        {
            get
            {
                //Menge *VK
                return this.Menge * this.VkPreis;
            }
        }
        [XafDisplayName("EK-Gesamt")]
        public System.Decimal EkGesamt
        {
            get
            {
                return this.Menge * this.Produkt.EkPreis;
            }
        }

        [XafDisplayName("VK-Preis")]
        public System.Decimal VkPreis
        {
            get
            {
                System.Decimal retVal = 0;

                if (this.Produkt != null)
                {
                    if (this.VkFaktor != 0)
                    {
                        retVal = this.Produkt.VkPreis * this.VkFaktor;
                    }
                    else
                    {
                        retVal = this.Produkt.VkPreis;
                    }
                }
                return retVal;
            }
        }
        #endregion


        #region Properties

        [XafDisplayName("Abrechnungsturnus")]
        public enmTurnus Abrechnungsturnus
        {
            get
            {
                return _abrechnungsturnus;
            }
            set
            {
                SetPropertyValue("Abrechnugnsturnus", ref _abrechnungsturnus, value);
            }
        }

        [XafDisplayName("Turnuswert (Abrechnung)")]
        public System.Int32 Abrechnungsturnusvalue
        {
            get
            {
                return _abrechnungturnusvalue;
            }
            set
            {
                SetPropertyValue("Abrechnungsturnusvalue", ref _abrechnungturnusvalue, value);
            }
        }
        [XafDisplayName("Leistungsnummer")]
        public System.String Leistungsnummer
        {
            get
            {
                return _leistungsnummer;
            }
            set
            {
                SetPropertyValue("Leistungsnummer", ref _leistungsnummer, value);
            }
        }
        [XafDisplayName("Leistungsstatus")]
        public enmVertragsStatus Leistungsstatus
        {
            get
            {
                return _leistungsstatus;
            }
            set
            {
                SetPropertyValue("Leistungsstatus", ref _leistungsstatus, value);
            }
        }
        [XafDisplayName("Menge")]
        public System.Int32 Menge
        {
            get
            {
                return _menge;
            }
            set
            {
                SetPropertyValue("Menge", ref _menge, value);
            }
        }


        [XafDisplayName("Leistungsturnus")]
        public enmTurnus Leistungsturnus
        {
            get
            {
                return _leistungsturnus;
            }
            set
            {
                SetPropertyValue("Leistungsturnus", ref _leistungsturnus, value);
            }
        }
        [XafDisplayName("Turnuswert")]
        public System.Int32 Turnusvalue
        {
            get
            {
                return _turnusvalue;
            }
            set
            {
                SetPropertyValue("Turnusvalue", ref _turnusvalue, value);
            }
        }
        [XafDisplayName("Name")]
        public System.String Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue("Name", ref _name, value);
            }
        }


        [XafDisplayName("Faktor")]
        public System.Decimal VkFaktor
        {
            get
            {
                return _vkfaktor;
            }
            set
            {
                SetPropertyValue("VkFaktor", ref _vkfaktor, value);
            }
        }

        [XafDisplayName("Produkt")]
        [ImmediatePostData(true)]
        public fiProdukt Produkt
        {
            get
            {
                return _produkt;
            }
            set
            {
                SetPropertyValue("Produkt", ref _produkt, value);
            }
        }

        [XafDisplayName("Abgang")]
        public System.DateTime Abgang
        {
            get
            {
                return _abgang;
            }
            set
            {
                SetPropertyValue("Abgang", ref _abgang, value);
            }
        }
        [XafDisplayName("Zugang")]
        public System.DateTime Zugang
        {
            get
            {
                return _zugang;
            }
            set
            {
                SetPropertyValue("Zugang", ref _zugang, value);
            }
        }

        [XafDisplayName("Vertrag")]
        [Association("vertragVertragBase-vertragVertragsleistung")]
        public vertragVertragBase Vertrag
        {
            get
            {
                return _vertrag;
            }
            set
            {
                SetPropertyValue("Vertrag", ref _vertrag, value);
            }
        }
        #endregion
    }
}