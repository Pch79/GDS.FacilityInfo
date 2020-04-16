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

namespace FacilityInfo.Parameter.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Parameteritem")]
   [ImageName("brick_edit_16")]
   [XafDefaultProperty("ParameterDefinition")]
    public class parameterAnlagenArtParam : BaseObject
    {
        private parameterParameterDefinition _parameterDefinition;
        
        private String _allowedMaxValue;
        private String _allowedMinValue;
        private String _defaultValue;

      
        //private String _bezeichnung;
        private boAnlagenArt _anlagenArt;
        
        //das item wird der Anlagenart zugeordent



        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public parameterAnlagenArtParam(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
      
        [XafDisplayName("Parameterdefinition")]
        public parameterParameterDefinition ParameterDefinition
        {
            get
            {
                return _parameterDefinition;
            }
            set
            {
                SetPropertyValue("ParameterDefinition", ref _parameterDefinition, value);
            }
        }

        [XafDisplayName("Daefaultwert")]
        public String DefaultValue
        {
            get
            {
                return _defaultValue;
            }
            set
            {
                SetPropertyValue("DefaultValue", ref _defaultValue, value);
            }
        }
        
        
        [XafDisplayName("Maximalwert")]
        public String AllowedMaxValue
        {
            get
            {
                return _allowedMaxValue;
            }
            set
            {
                SetPropertyValue("AllowedMaxValue", ref _allowedMaxValue, value);
            }
        }

        [XafDisplayName("Minimalwert")]
        public String AllowedMinValue
        {
            get
            {
                return _allowedMinValue;
            }
            set
            {
                SetPropertyValue("AllowedMinValue", ref _allowedMinValue, value);
            }
        }


        [XafDisplayName("Anlagenart")]
        [Association("boAnlagenArt-parameterAnlagenArtParam")]
        public boAnlagenArt AnlagenArt
        {
            get
            {
                return _anlagenArt;
            }
            set
            {
                SetPropertyValue("AnlagenArt", ref _anlagenArt, value);
            }
        }

        #endregion
    }
}