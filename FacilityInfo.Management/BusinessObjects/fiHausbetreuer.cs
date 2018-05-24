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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;

namespace FacilityInfo.Liegenschaft.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Hausbetreuer")]
   [XafDefaultProperty("Matchkey")]
   [ImageName("user_gray_16")]
    public class fiHausbetreuer : BaseObject
    {
        private boMandant _mandant;
        private PermissionPolicyUser _systembenutzer;
        private boAdresse _adresse;
        
        private System.String _hausbetreuerSelekt1;
        private System.String _hausbetreuerSelekt2;
        private System.String _fremdsystemId;
        private System.String _kwpCode;

        public fiHausbetreuer(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("KwpCode")]
        public System.String KwpCode
        {
            get
            {
                return _kwpCode;
            }
            set
            {
                SetPropertyValue("KwpCode", ref _kwpCode, value);
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

        [XafDisplayName("Hausbetreuer Selekt1")]
        public System.String HausbetreuerSelekt1
        {
            get
            {
                return _hausbetreuerSelekt1;
            }
            set
            {
                SetPropertyValue("HausbetreuerSelekt1", ref _hausbetreuerSelekt1, value);
            }
        }

        [XafDisplayName("Hausbetreuer Selekt2")]
        public System.String HausbetreuerSelekt2
        {
            get
            {
                return _hausbetreuerSelekt2;
            }
            set
            {
                SetPropertyValue("HausbetreuerSelekt2", ref _hausbetreuerSelekt2, value);
            }
        }

        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var adresse = string.Empty;
                adresse = (this.Adresse != null) ? this.Adresse.Matchkey : "N/A";
                retVal = String.Format("{0}", adresse);
                return retVal;
            }
        }

      

        [XafDisplayName("Adresse")]
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

        [XafDisplayName("Systembenutzer")]
        public PermissionPolicyUser Systembenutzer
        {
            get
            {
                return _systembenutzer;
            }
            set
            {
                SetPropertyValue("Systembenutzer", ref _systembenutzer, value);
            }
        }

        [XafDisplayName("Mandant")]
        [Association("boMandant-Hausbetreuer")]
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
        [XafDisplayName("Liegenschaften")]
        [Association("boLiegenschaft-fiHausbetreuer"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boLiegenschaft> Liegenschaften
        {
            get
            {
                return GetCollection<boLiegenschaft>("Liegenschaften");
            }
        }
        #endregion

    }
}