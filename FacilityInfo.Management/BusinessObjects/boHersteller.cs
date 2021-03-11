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
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Management.BusinessObjects.TechnicalInstallation;

namespace FacilityInfo.Hersteller.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Hersteller")]
    [Serializable]
    [ImageName("factory_16")]
    [XafDefaultProperty("Bezeichnung")]
    [RuleObjectExists(DefaultContexts.Save,"Bezeichnung = '@Bezeichnung'",InvertResult =true)]
    public class boHersteller : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _kuerzel;
        private System.String _notiz;
        private System.String _internet;
        private System.String _telefon;
        private System.String _mail;
        

        public boHersteller(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        
        [XafDisplayName("Internet")]
        public System.String Internet
        {
            get
            {
                return _internet;
            }
            set
            {
                SetPropertyValue("Internet", ref _internet, value);
            }
        }

        [XafDisplayName("Telefon")]
        public System.String Telefon
        {
            get
            {
                return _telefon;
            }
            set
            {
                SetPropertyValue("Telefon", ref _telefon, value);
            }
        }

        [XafDisplayName("Mail")]
        public System.String Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                SetPropertyValue("Mail", ref _mail, value);
            }
        }

        [XafDisplayName("Notiz")]
        [Size(-1)]
        public System.String Notiz
        {
            get
            {
                return _notiz;
            }
            set
            {
                SetPropertyValue("Notiz", ref _notiz, value);
            }
        }
        [XafDisplayName("Name")]
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
      
        [XafDisplayName("Firmenlogo")]
        public byte[] Firmenlogo
        {
            get
            {
                return GetPropertyValue<byte[]>("Firmenlogo");
            }
            set
            {
                SetPropertyValue<byte[]>("Firmenlogo", value);
            }
        }


        [XafDisplayName("Kürzel")]
        private System.String Kuerzel
        {
            get
            {
                return _kuerzel;
            }
            set
            {
                SetPropertyValue("Kuerzel", ref _kuerzel, value);
            }
        }


        [XafDisplayName("Produkte")]
        [Association("fiHersteller-fiHerstellerProdukt"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<fiHerstellerProdukt> lstProdukte
        {
            get
            {
                return GetCollection<fiHerstellerProdukt>("lstProdukte");
            }
        }

        [Association("fiHersteller-TechnicalAssembly")]
        public XPCollection<TechnicalAssembly> lstTechnicalAssemblys
        {
            get
            {
                return GetCollection<TechnicalAssembly>("lstTechnicalAssemblys");
            }
        }

        [XafDisplayName("Herstellerdokumente")]
        [Association("boHersteller-fiHerstellerAttachment"), DevExpress.ExpressApp.DC.Aggregated]
   
        public XPCollection<fiHerstellerAttachment> lstHerstellerdokumente
        {
            get
            {
                return GetCollection<fiHerstellerAttachment>("lstHerstellerdokumente");
            }
        }
    }
}