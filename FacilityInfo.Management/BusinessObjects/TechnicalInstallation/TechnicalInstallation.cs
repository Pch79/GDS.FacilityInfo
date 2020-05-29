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
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using DevExpress.CodeParser;
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects.TechnicalInstallation
{
    [DefaultClassOptions]
    [XafDisplayName("Anlage")]
    [ImageName("centos_16")]
    [XafDefaultProperty("AnlagenNummer")]
    public class TechnicalInstallation : BaseObject
    {

        private System.String _bezeichnung;
        private System.String _anlagennummer;
        private System.String _anlagencode;
        //private System.String _ansprechcode;
        private boAdresse _anlagenAdresse;
        private boAdresse _rechnungsadresse;
        private System.String _beschreibung;

        private boMandant _mandant;
        private TechnicalInstallation _parentInstallation;

        private FunctionalUnit _functionalUnit;

        public TechnicalInstallation(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Association("TechnicalInstallation-FunctionalUnit")]
        public FunctionalUnit FunctionalUnit
        {
            get { return this._functionalUnit; }
            set { SetPropertyValue("FunctionalUnit", ref _functionalUnit, value); }
        }

        [Association("TechnicalInstallation-TechnicalAssembly")]
        public XPCollection<TechnicalAssembly> lstTechnicalAssemblys
        {
            get { return GetCollection<TechnicalAssembly>("lstTechnicalAssemblys"); }
        }
       
    }
}