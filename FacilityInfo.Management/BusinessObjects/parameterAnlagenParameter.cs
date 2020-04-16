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
using FacilityInfo.Anlagen.BusinessObjects;
using DevExpress.ExpressApp.Utils;

namespace FacilityInfo.Parameter.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagenparameter")]
    [XafDefaultProperty("ParameterItem")]
    [ImageName("brick_link_16")]
    public class parameterAnlagenParameter : BaseObject
    {
        private parameterParameterDefinition _parameterItem;
        private boAnlage _anlage;
        private String _defaultValue;
        private String _maxAllowedValue;
        private String _minAllowedValue;
        private String _value;
        private Type _originType;
        

        public parameterAnlagenParameter(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Ursprung")]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        public Type OriginType
        {
            get { return _originType; }
            set { SetPropertyValue("OriginType", ref _originType, value); }
        }
        [XafDisplayName("Anlage")]
        [Association("boAnlage-parameterAnlagenParameter")]
        public boAnlage Anlage
        {
            get { return _anlage; }
            set { SetPropertyValue("Anlage", ref _anlage, value); }
        }
        [XafDisplayName("Parameter")]
        public parameterParameterDefinition ParameterItem
        {
            get { return _parameterItem; }
            set { SetPropertyValue("ParameterItem", ref _parameterItem, value); }
        }
        [XafDisplayName("Defaultwert")]
        public String DefaultValue
        {
            get { return _defaultValue; }
            set { SetPropertyValue("DefaultValue", ref _defaultValue, value); }
            
        }
        [XafDisplayName("Maximalwert")]
        public String MaxAllowedValue
        {
            get { return _maxAllowedValue; }
            set
            {
                SetPropertyValue("MaxAllowedValue", ref _maxAllowedValue, value);
            }
        }
        [XafDisplayName("Minimalwert")]
        public String MinAllowedValue
        {
            get { return _minAllowedValue; }
            set { SetPropertyValue("MinAllowedValue", ref _minAllowedValue, value); }


        }
        [XafDisplayName("Wert")]
        public string Value
        {
            get { return _value; }
            set { SetPropertyValue("Value", ref _value, value); }
        }
        #endregion
    }
}