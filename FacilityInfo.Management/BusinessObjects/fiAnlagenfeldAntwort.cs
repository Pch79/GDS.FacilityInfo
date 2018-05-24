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
  [XafDisplayName("Antwort (Anlagenfeld)")]
    [XafDefaultProperty("Antworttext")]
    [RuleObjectExists(DefaultContexts.Save, "Antworttext = '@this.Antworttext'", InvertResult = true)]
    public class fiAnlagenfeldAntwort : BaseObject
    {
        private System.String _antworttext;

        public fiAnlagenfeldAntwort(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        
        
    }
}