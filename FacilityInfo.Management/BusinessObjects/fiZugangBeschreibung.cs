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
   [XafDisplayName("Zugangsbeschreibung")]
   [XafDefaultProperty("Zugangsbeschreibung")]
   [RuleObjectExists(DefaultContexts.Save, "Zugangsbeschreibung='@Zugangsbeschreibung'",InvertResult =true)]
    public class fiZugangBeschreibung : BaseObject
    {
        private System.String _zugangsbeschreibung;
        public fiZugangBeschreibung(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        [XafDisplayName("Beschreibung (Zugang)")]
        public System.String Zugangsbeschreibung
        {
            get
            {
                return _zugangsbeschreibung;
            }
            set
            {
                SetPropertyValue("Zugangsbeschreibung", ref _zugangsbeschreibung, value);
            }
        }
    }
}