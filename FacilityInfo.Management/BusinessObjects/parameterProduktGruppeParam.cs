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
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.Parameter.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Parameter (Produktgruppe)")]
   [ImageName("brick_16")]
    public class parameterProduktGruppeParam : BaseObject
    {
        private parameterParameterDefinition _parameterDefinition;
        private String _sollWert;
        private String _vorgabeWert;
        private fiHerstellerProduktgruppe _produktGruppe;
        public parameterProduktGruppeParam(Session session)
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

        [XafDisplayName("Vorgabewert")]
        public String VorgabeWert
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
        public String SollWert
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


        [XafDisplayName("Produktgruppe")]
        [Association("fiHerstellerProduktgruppe-parameterProduktGruppeParam")]
        public fiHerstellerProduktgruppe ProduktGruppe
        {
            get
            {
                return _produktGruppe;
            }
            set
            {
                SetPropertyValue("ProduktGruppe", ref _produktGruppe, value);
            }
        }

        #endregion

    }
}