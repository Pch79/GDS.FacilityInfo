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
    public class parameterParameterItem : BaseObject
    {
        private parameterParameterDefinition _parameterDefinition;
        private object _sollWert;
        private object _vorgabeWert;
        private String _bezeichnung;
        private boAnlagenArt _anlagenArt;
        private parameterParameterArt _parameterArt;
        //das item wird der Anlagenart zugeordent



        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public parameterParameterItem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Parameterart")]
        public parameterParameterArt ParameterArt
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

        [XafDisplayName("Vorgabewert")]
        public object VorgabeWert
        {
            get
            {
                return _vorgabeWert;
            }
            set
            {
                SetPropertyValue("VorgabeWert", ref _vorgabeWert, value);
            }
        }
        
        
        [XafDisplayName("Sollwert")]
        public object SollWert
        {
            get
            {
                return _sollWert;
            }
            set
            {
                SetPropertyValue("SollWert", ref _sollWert, value);
            }
        }
        /*
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
        */

        [XafDisplayName("Anlagengruppe")]
        [Association("boAnlagenArt-parameterParameterItem")]
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