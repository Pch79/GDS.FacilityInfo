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
    [XafDisplayName("Debitorenkonto")]
    [XafDefaultProperty("DebitorenNummer")]
    [ImageName("BO_Customer")]
    public class Debitorenkonto : BaseObject
    {
        private boAdresse _adresse;
        private System.String _debitorenNummer;
        private boMandant _mandant;
        private System.String _ustid;
        private System.String _fremdsystemId;
        private System.String _fremdsystemCode;
        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Debitorenkonto(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Matchkey")]
        public String Matchkey
        {
          get
          {
                var retVal = string.Empty;
                var adresse = string.Empty;
                var debitNr = string.Empty;
               
                return retVal;
          }
        }
        [XafDisplayName("FremdsystemID")]
        public System.String FremdsystemId
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
        [XafDisplayName("FremdsystemCode")]
        public System.String FremdsystemCode
        {
            get
            {
                return _fremdsystemCode;
            }
            set
            {
                SetPropertyValue("FremdsystemCode", ref _fremdsystemCode, value);
            }
        }
        [XafDisplayName("UstID")]
        public System.String UstID
        {
            get
            {
                return _ustid;
            }
            set
            {
                SetPropertyValue("UstID", ref _ustid, value);
            }
        }
        [XafDisplayName("Debitorennummer")]
        public System.String DebitorenNummer
        {
            get
            {
                return _debitorenNummer;
            }
            set
            {
                SetPropertyValue("DebitorenNummer", ref _debitorenNummer, value);
            }
        }
     



        [XafDisplayName("Adresse")]
        [Association("boAddresse-Debitorenkonto")]
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
        [XafDisplayName("Mandant")]
        [Association("boMandant-Debitorenkonto")]
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
        #endregion

    }
}