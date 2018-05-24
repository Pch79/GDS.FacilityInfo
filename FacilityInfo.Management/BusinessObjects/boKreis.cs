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

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Landkreis/Bezirk")]
    [XafDefaultProperty("Bezeichnung")]
    [Serializable]
    public class boKreis : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _keyNational;
        private System.String _keyInternational;
        private System.String _kfzKennzeichen;
        private boBundesland _bundesland;
        public boKreis(Session session)
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
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var bundesland = string.Empty;
                 
                return retVal;
            }
        }
        [XafDisplayName("Bundesland")]
        [Association("boBundesland-lstKreise")]
        public boBundesland Bundesland
        {
            get
            {
                return _bundesland;
            }
            set
            {
                SetPropertyValue("Bundesland", ref _bundesland, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField]
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
        [XafDisplayName("Key-National")]
        public System.String KeyNational
        {
            get
            {
                return _keyNational;
            }
            set
            {
                SetPropertyValue("KeyNational", ref _keyNational, value);
            }
        }
        [XafDisplayName("Key-International")]
        public System.String KeyInternational
        {
            get
            {
                return _keyInternational;
            }
            set
            {
                SetPropertyValue("KeyInternational", ref _keyInternational, value);
            }
        }
        [XafDisplayName("KFZ-Kennzeichen")]
        public System.String KfzKennzeichen
        {
            get
            {
                return _kfzKennzeichen;
            }
            set
            {
                SetPropertyValue("KfzKennzeichen", ref _kfzKennzeichen, value);
            }
        }
        #endregion
    }
}