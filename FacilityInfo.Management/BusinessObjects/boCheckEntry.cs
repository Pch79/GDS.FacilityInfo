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
using FacilityInfo.Management.DomainComponents;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("highliter_text")]
    [XafDisplayName("PrüfpunktEntry")]
    
    public class boCheckEntry : BaseObject,IcheckEntry
    {
        private boCheckItem _checkItem;
        private enmCheckResult _checkResult;
        private System.String _checkValue;
        private PermissionPolicyUser _erfasser;
        private System.DateTime _erfassungsdatum;
        
        public boCheckEntry(Session session)
            : base(session)
        {
        }

        public boCheckItem checkItem
        {
            get
            {
                return _checkItem;
            }

            set
            {
                SetPropertyValue("chekItem", ref _checkItem, value);
            }
        }

        public enmCheckResult checkResult
        {
            get
            {
                return _checkResult;
            }

            set
            {
                SetPropertyValue("checkResult", ref _checkResult, value);
            }
        }

        public string checkValue
        {
            get
            {
                return _checkValue;
            }

            set
            {
                SetPropertyValue("checkValue", ref _checkValue, value);
            }
        }

        public PermissionPolicyUser erfasser
        {
            get
            {
                return _erfasser;
            }

            set
            {
                SetPropertyValue("erfasser", ref _erfasser, value);
            }
        }

        public DateTime erfassungsdatum
        {
            get
            {
                return _erfassungsdatum;
            }

            set
            {
                SetPropertyValue("erfassungsdatum", ref _erfassungsdatum, value);
            }
        }

        public IList<IcheckListe> lstChecklisten
        {
            get
            {
                return GetList<IcheckListe>("lstchecklisten");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
       
    }
}