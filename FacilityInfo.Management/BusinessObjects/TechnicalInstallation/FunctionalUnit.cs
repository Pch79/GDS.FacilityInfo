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
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Pkcs;
using FacilityInfo.Liegenschaft.BusinessObjects;
using DevExpress.ExpressApp.Security;
using FacilityInfo.Management.EnumStore;
using FacilityInfo.Anlagen.BusinessObjects;
using DevExpress.CodeParser;

namespace FacilityInfo.Management.BusinessObjects.TechnicalInstallation
{
    [DefaultClassOptions]
    [XafDisplayName("Funktionseinheit")]
    [ImageName("centos_16")]
    [XafDefaultProperty("Bezeichnung")]
    public class FunctionalUnit : BaseObject
    {
        private boLiegenschaft _realEstate;
        private TOperatingState _operatingState;
        private string _systemDesignation;
        private boAnlagenKategorie _classification;

        public FunctionalUnit(Session session)
            : base(session)
        {
        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [XafDisplayName("Codierung")]
        [ReadOnly(true)]
        public string SystemCode
        {
            get
            {
             string retVal = string.Empty;
             retVal = (this.Classification != null) ? this.Classification.Kuerzel : "n/a";
             return retVal;
            }
        }

        [ImmediatePostData(true)]
       [XafDisplayName("Anlagenkategorie")]
        public boAnlagenKategorie Classification
        {
            get { return this._classification; }
            set { SetPropertyValue("Classification", ref this._classification, value); }
        }

        [XafDisplayName("Systembezeichnung")]
        public string SystemDesignation
        {
            get { return this._systemDesignation; }
            set { SetPropertyValue("SystemDesignation", ref _systemDesignation, value); }
        }
        [XafDisplayName("Betriebszustand")]
        public TOperatingState OperatingState
        {
            get { return this._operatingState; }
            set { SetPropertyValue("OperatingState", ref _operatingState, value); }
        }

        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-FunctionalUnit")]
        public boLiegenschaft RealEstate
        {
            get { return this._realEstate; }
            set { SetPropertyValue("RealEstate", ref _realEstate, value); }
        }

        [Association("TechnicalInstallation-FunctionalUnit")]
        public XPCollection<TechnicalInstallation> lstTechnicalInstallations
        {
            get { return GetCollection<TechnicalInstallation>("lstTechnicalInstallations"); }
        }
       
    }
}