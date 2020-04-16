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
using FacilityInfo.Management.Klassen;

namespace FacilityInfo.Parameter.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Parameterdefinition")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("bricks_16")]
    public class parameterParameterDefinition : BaseObject
    {
        private String _bezeichnung;
        private String _kuerzel;
        private boEinheit _einheit;
        private System.String _paramKey;
        private System.Int32 _sortIndex;
        private parameterParameterTyp _parameterTyp;
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

        #region Rules
      

        #endregion

        #region Properties
        [XafDisplayName("SortIndex")]
        public System.Int32 SortIndex
        {
            get
            {
                return _sortIndex;
            }
            set
            {
                SetPropertyValue("SortIndex", ref _sortIndex, value);
            }
        }

        [RuleUniqueValue("ParamKey = '@ParamKey'", DefaultContexts.Save, "Key darf nicht doppelt vergeben werden", InvertResult = false)]

        [XafDisplayName("Key")]
        [RuleRequiredField]
        public System.String ParamKey
        {
            get
            {
                return _paramKey;
            }
            set
            {
               
                SetPropertyValue("ParamKey", ref _paramKey, value);

            }
        }
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

        [XafDisplayName("Parametertyp")]
        [Association("parameterParameterDefinition-parameterParameterTyp")]
        public parameterParameterTyp ParameterTyp
        {
            get
            {
                return _parameterTyp;
            }
            set
            {
                SetPropertyValue("ParameterTyp", ref _parameterTyp, value);
            }
        }

        
     
      
       
        [XafDisplayName("Valuetype")]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(MyLocalizedClassInfoTypeConverter))]
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
         
        #endregion
    }
}