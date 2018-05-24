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

namespace FacilityInfo.Parameter.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Parameterart")]
    [XafDefaultProperty("Bezeichnung")]

    public class parameterParameterArt : BaseObject
    {
        private String _bezeichnung;
        private String _code;
        public parameterParameterArt(Session session)
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
        [XafDisplayName("Code")]
        public System.String Code
        {
            get
            {
                return _code;
            }
            set
            {
                SetPropertyValue("Code", ref _code, value);

            }
        }
     
        #endregion
    }
}