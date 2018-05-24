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

namespace FacilityInfo.Hersteller.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Bauteil")]
   [ImageName("controlbar_16")]
   [DefaultProperty("Bezeichnung")]
   [RuleObjectExists(DefaultContexts.Save,"Bezeichnung = '@this.Bezeichnung'",InvertResult =true)]
    public class fiBauteilkatalog : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _notiz;
        
        public fiBauteilkatalog(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

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
        #endregion
    }
}