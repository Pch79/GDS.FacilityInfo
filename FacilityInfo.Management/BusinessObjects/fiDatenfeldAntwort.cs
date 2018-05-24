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

namespace FacilityInfo.Datenfeld.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Antwortkatalog")]
    [XafDefaultProperty("Antworttext")]
    [RuleObjectExists(DefaultContexts.Save, "Antworttext = '@this.Antworttext'", InvertResult = true)]
    public class fiDatenfeldAntwort : BaseObject
    {
        private System.String _antworttext;
        private System.Boolean _aktiv;

        public fiDatenfeldAntwort(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            this.Aktiv = true;
        }
        [XafDisplayName("Aktiv")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public System.Boolean Aktiv
        {
            get
            {
                return _aktiv;
            }
            set
            {
                SetPropertyValue("Aktiv", ref _aktiv, value);

            }
        }
        [XafDisplayName("Antworttext")]
        public System.String Antworttext
        {
            get
            {
                return _antworttext;
            }
            set
            {
                SetPropertyValue("Antworttext", ref _antworttext, value);
            }
        }

        //den Antworttext kann ich für mehrere Einträge verwaenden
        [Association("fiDatenFeld-fiDatenfeldAntwort")]
        public XPCollection<fiDatenfeld> lstDatenfelder
        {
            get
            {
                return GetCollection<fiDatenfeld>("lstDatenfelder");
            }
        }
    }
}