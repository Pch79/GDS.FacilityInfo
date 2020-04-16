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
using FacilityInfo.Parameter.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.Parameter.BusinessObjects
{
    [DefaultClassOptions]
  [XafDisplayName("Produktparameter")]
  [ImageName("brick_add_16")]
    [XafDefaultProperty("ParameterItem")]

    public class parameterHerstellerProduktParameter : BaseObject
    {
        private parameterParameterDefinition _parameterItem;
        private fiHerstellerProdukt _herstellerProdukt;
        private String _defaultValue;
        private String _maxAllowedValue;
        private String _minAllowedValue;
        private String _value;

        private parameterParameterTyp _parameterTyp;
        public parameterHerstellerProduktParameter(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Produkt")]
        [Association("fiHerstellerProdukt-parameterHerstellerProduktParameter")]
        public fiHerstellerProdukt HerstellerProdukt
        {
            get { return _herstellerProdukt; }
            set { SetPropertyValue("HerstellerProdukt", ref _herstellerProdukt, value); }
        }
        [XafDisplayName("Parameter")]
        public parameterParameterDefinition ParameterItem
        {
            get { return _parameterItem; }
            set { SetPropertyValue("ParameterItem", ref _parameterItem, value); }
        }
        [XafDisplayName("DefaultWert")]
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