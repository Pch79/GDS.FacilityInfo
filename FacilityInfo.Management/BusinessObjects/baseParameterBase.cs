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
    [XafDisplayName("Parameter(Base)")]
    //Basisklasse für Parameter (Anlagenparaneter, Serviceparameter)
    public class baseParameterBase : BaseObject
    {

        private String _parameterName;
        private baseParameterArt _parameterArt;

        public baseParameterBase(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Parametername")]
        public String ParameterName
        {
            get
            {
                return _parameterName;
            }
            set
            {
                SetPropertyValue("ParameterName", ref _parameterName, value);
            }
        }
        [XafDisplayName("Parameterart")]
        [Association("baseParameterbase-baseParameterArt")]
        public baseParameterArt ParameterArt
        {
            get
            {
                return _parameterArt;
            }
            set
            {
                SetPropertyValue("ParameterArt", ref _parameterArt, value);
            }
        }
            
        #endregion
    }
}