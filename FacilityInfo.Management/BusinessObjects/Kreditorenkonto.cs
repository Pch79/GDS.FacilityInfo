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
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;

namespace FacilityInfo.BusinessManagement.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Kreditorenkonto")]
   [ImageName("BO_Vendor")]
    public class Kreditorenkonto : BaseObject
    {
        private boAdresse _adresse;
        private System.String _kreditorennummer;
        private boMandant _mandant;
     
        private System.String _eigeneKundenNummer;
        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Kreditorenkonto(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        [XafDisplayName("Eigene Kundennummer")]
        public System.String EigeneKundenNummer
        {
            get
            {
                return _eigeneKundenNummer;
            }
            set
            {
                SetPropertyValue("EigeneKundenNummer", ref _eigeneKundenNummer, value);
            }
        }
        [XafDisplayName("Adresse")]
        [Association("boAdresse-Kreditorenkonto")]
        public boAdresse Adresse
        {
            get
            {
                return _adresse;
            }
            set
            {
                SetPropertyValue("Adresse", ref _adresse, value);
            }
        }
        [XafDisplayName("Kreditorennummer")]
        public System.String Kreditorennummer
        {
            get
            {
                return _kreditorennummer;
            }
            set
            {
                SetPropertyValue("Kreditorennummer", ref _kreditorennummer, value);
            }
        }
        [XafDisplayName("Mandant")]
        [Association("boMandant-Kreditorenkonto")]
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

    }
}