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

namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
 [XafDisplayName("Titel")]
 [ImageName("")]
 [XafDefaultProperty("Kuerzel")]
    public class adresseTitel : BaseObject
    {
        private String _bezeichnung;
        private String _kuerzel;
        

        public adresseTitel(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
       [XafDisplayName("Kürzel")]
       [RuleRequiredField]
       public String Kuerzel
        {
            get { return _kuerzel; }
            set { SetPropertyValue("Kuerzel", ref _kuerzel, value); }
        }
       [XafDisplayName("Bezeichnung")]
       public String Bezeichnung
        {
            get { return _bezeichnung; }
            set { SetPropertyValue("Bezeichnung", ref _bezeichnung, value); }
        }
    }
}