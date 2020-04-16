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

namespace FacilityInfo.Core.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Portal-Account")]
    [ImageName("user_16")]
    
    public class corePortalAccount : BaseObject
    {
        private String _vorName;
        private String _nachName;
        private bool _isActive;
        private PermissionPolicyUser _systemBenutzer;
        private boHausverwalter _hausVerwalter;



        public corePortalAccount(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            this.IsActive = true;
        }
        #region Properties
        [XafDisplayName("Vorname")]
        public String VorName
        {
            get { return _vorName; }
            set { SetPropertyValue("VorName", ref _vorName, value); }
        }

        [XafDisplayName("Nachname")]
        public String NachName
        {
            get { return _nachName; }
            set { SetPropertyValue("NachName", ref _nachName, value); }
        }
        [XafDisplayName("Aktiv")]
        [CaptionsForBoolValues("Ja","Nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public bool IsActive
        {
            get { return _isActive; }
            set { SetPropertyValue("IsActive", ref _isActive, value); }
        }
        [XafDisplayName("Systembenutzer")]
        [RuleRequiredField]
        public PermissionPolicyUser SystemBenutzer
        {
            get { return _systemBenutzer; }
            set { SetPropertyValue("SystemBenutzer", ref _systemBenutzer, value); }
        }
        [XafDisplayName("Hausverwalter")]
        [Association("boHausVerwalter-corePortalAccount")]
        [RuleRequiredField]
        public boHausverwalter HausVerwalter
        {
            get { return _hausVerwalter; }
            set { SetPropertyValue("HausVerwalter", ref _hausVerwalter, value); }
        }
        #endregion
        //}
    }
}