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
   [XafDisplayName("Position")]
   [XafDefaultProperty("Bezeichnung")]
   [Serializable]
    public class boPosition : BaseObject
    { 
        private System.String _bezeichnung;
        private System.Boolean _vertretungsberechtigt;
        private System.Boolean _prokura;

        public boPosition(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
        #region
        [XafDisplayName("Vertretungsberechtigt")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public System.Boolean Vertretungsberechtigt
        {
            get
            {
                return _vertretungsberechtigt;
            }
            set
            {
                SetPropertyValue("Vertretungsberechtigt", ref _vertretungsberechtigt, value);
            }
        }

        [XafDisplayName("Prokura")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        [CaptionsForBoolValues("ja", "nein")]
        public System.Boolean Prokura
        {
            get
            {
                return _prokura;
            }
            set
            {
                SetPropertyValue("Prokura", ref _prokura, value);
            }
        }
        [XafDisplayName("Position")]
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
        #endregion
    }
}