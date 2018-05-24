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

namespace FacilityInfo.Hersteller.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Hersteller")]
    [Serializable]
    [ImageName("factory")]
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


        #region Properties
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
        [XafDisplayName("Bezeichnung")]
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
        [ImageEditor]
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

        //Dokumente die als Objektkey das Hersteller 
        [XafDisplayName("Dokumente (Alle)")]
        public XPCollection<boAttachment> lstDokumente
        {
            get
            {
                
                XPCollection<boAttachment> lstRetVal = new XPCollection<boAttachment>(this.Session, new BinaryOperator("Objektkey", this.Oid.ToString(), BinaryOperatorType.Equal));
                return lstRetVal;
            }
        }

        [XafDisplayName("Herstellerdokumente")]
        [Association("boHersteller-fiHerstellerProdukt")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<fiHerstellerAttachment> lstHerstellerdokumente
        {
            get
            {
                return GetCollection<fiHerstellerAttachment>("lstHerstellerdokumente");
            }
        }

        //Dokumente die in den Produkten verzeichner sind anzeigen
        /*
        public XPCollection<boAttachment> lstDokumente
        {
            get
            {
             
            }
        }
        */


        #endregion
    }
}