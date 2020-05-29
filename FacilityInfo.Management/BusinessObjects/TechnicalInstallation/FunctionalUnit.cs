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

namespace FacilityInfo.Management.BusinessObjects.TechnicalInstallation
{
    [DefaultClassOptions]
    [XafDisplayName("Funktionseinheit")]
    [ImageName("centos_16")]
    [XafDefaultProperty("Bezeichnung")]
    public class FunctionalUnit : BaseObject
    {

        private System.String _bezeichnung;
        private System.String _anlagennummer;
        private System.String _anlagencode;
        //private System.String _ansprechcode;
        private boAdresse _anlagenAdresse;
        private boAdresse _rechnungsadresse;
        private System.String _beschreibung;

        private boMandant _mandant;
       
        public FunctionalUnit(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [Association("TechnicalInstallation-FunctionalUnit")]
        public XPCollection<TechnicalInstallation> lstTechnicalInstallations
        {
            get { return GetCollection<TechnicalInstallation>("lstTechnicalInstallations"); }
        }
       
    }
}