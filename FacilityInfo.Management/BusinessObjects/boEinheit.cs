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

namespace FacilityInfo.Base.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Einheit")]
    [XafDefaultProperty("Einheit_Bezeichnung")]
    
    public class boEinheit : BaseObject
    {
        private System.String _einheit_bezeichnung;
        private System.String _einheit_kuerzel;
        private System.String _einheit_kuerzelInternat;
        public boEinheit(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region
        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField]
        public System.String Einheit_Bezeichnung
        {
            get
            {
                return _einheit_bezeichnung;
            }
            set
            {
                SetPropertyValue("Einheit_Bezeichnung", ref _einheit_bezeichnung, value);
            }
        }

        [XafDisplayName("Kürzel")]
        [RuleRequiredField]
        public System.String Einheit_Kuerzel
        {
            get
            {
                return _einheit_kuerzel;
            }
            set
            {
                SetPropertyValue("Einheit_Kuerzel", ref _einheit_kuerzel, value);
            }
        }

        [XafDisplayName("Kürzel internat.")]
        public System.String Einheit_KuerzelInternat
        {
            get
            {
                return _einheit_kuerzelInternat;
            }
            set
            {
                SetPropertyValue("Einheit_KuerzelInternat", ref _einheit_kuerzelInternat, value);
            }
        }

        #endregion

    }
}