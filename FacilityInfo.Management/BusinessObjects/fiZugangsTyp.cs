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
    [XafDisplayName("Zugangstyp")]
    [XafDefaultProperty("Bezeichnung")]
    [RuleObjectExists(DefaultContexts.Save, "Bezeichnung = '@Bezeichnung'", InvertResult = true)]
    public class fiZugangsTyp : BaseObject
    {
        private System.String _bezeichnung;
        
        
        public fiZugangsTyp(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #region Properties
        [XafDisplayName("Bezeichnung")]
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