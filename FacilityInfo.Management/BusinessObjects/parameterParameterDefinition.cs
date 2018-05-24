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
using FacilityInfo.Base.BusinessObjects;
using DevExpress.ExpressApp.Utils;

namespace FacilityInfo.Parameter.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Parameterdefinition")]
    [XafDefaultProperty("Bezeichnung")]
    public class parameterParameterDefinition : BaseObject
    {
        private String _bezeichnung;
        private String _kuerzel;
        private boEinheit _einheit;

        //das sollte ein .Net typ sein INT oder String


        private System.Type _valueType;

        public parameterParameterDefinition(Session session)
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
        public String Bezeichnung
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
        [XafDisplayName("Kürzel")]
        public String Kuerzel
        {
            get
            {
                return _kuerzel;
            }
            set
            {
                SetPropertyValue("Kuerzel", ref _kuerzel, value);
            }
        }
        [XafDisplayName("Einheit")]
        public boEinheit Einheit
        {
            get
            {
                return _einheit;
            }
            set
            {
                SetPropertyValue("Einheit", ref _einheit, value);
            }
        }



        /*
        [XafDisplayName("Value")]
        public String Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetPropertyValue("Value", ref _value, value);
            }
        }
      
       
        [XafDisplayName("Valuetype")]
       // [ValueConverter(typeof(TypeToStringConverter))]
       // [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]


      
        public Type ValueType
        {
            get
            { 
               
                return _valueType;
            }
            set
            {
                SetPropertyValue("ValueType", ref _valueType, value);
            }
        }
          */



        #endregion
    }
}