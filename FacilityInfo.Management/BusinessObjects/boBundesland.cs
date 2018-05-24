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

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
  [XafDisplayName("Bundesland")]
  [Serializable]
  [XafDefaultProperty("Matchkey")]
  [ImageName("location_pin")]
    public class boBundesland : BaseObject
    {
        private  boLand _land;
        private System.String _bezeichnungNational;
        private System.String _bezeichnungInternational;
        private System.String _kuerzelNational;
        private System.String _kuerzelInternational;
        private System.String _keyNational;
        private System.String _keyInternational;
        public boBundesland(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        #region Properties

        [XafDisplayName("Landkreise")]
        [Association("boBundesland-lstKreise"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boKreis> lstKreise
        {
            get
            {
                return GetCollection<boKreis>("lstKreise");
            }
        }



        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = String.Empty;
                var land = string.Empty;
                var bland = string.Empty;
                land = (this.Land != null) ? this.Land.IsoKennzeichen : "N/A";
                bland = (this.KuerzelNational != null) ? this.KuerzelNational : "N/A";
                retVal = String.Format("{0}-{1}", bland, land);
                return retVal;
            }
        }



        [XafDisplayName("Land")]
        [Association("boLand-boBundesland")]
        public boLand Land
        {
            get
            {
                return _land;
            }
            set
            {
                SetPropertyValue("Land", ref _land, value);
            }
        }



        [XafDisplayName("Orte")]
        [Association("boBundesland-boOrt"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boOrt> lstOrte
        {
            get
            {
                return GetCollection<boOrt>("lstOrte");
            }
        }




        [XafDisplayName("Bezeichnung (national)")]
        [RuleRequiredField]
        public System.String BezeichnungNational
        {
            get
            {
                return _bezeichnungNational;
            }
            set
            {
                SetPropertyValue("BezeichnungNational", ref _bezeichnungNational, value);

            }
        }




        [XafDisplayName("Bezeichnung (international)")]
        public System.String BezeichnungInternational
        {
            get
            {
                return _bezeichnungInternational;
            }
            set
            {
                SetPropertyValue("BezeichnungInternational", ref _bezeichnungInternational, value);

            }
        }



        [XafDisplayName("Kürzel (national")]
        [RuleRequiredField]
        public System.String KuerzelNational
        {
            get
            {
                return _kuerzelNational;
            }
            set
            {
                SetPropertyValue("KuerzelNational", ref _kuerzelNational, value);
            }
        }



        [XafDisplayName("Kürzel (international")]
        public System.String KuerzelInternational
        {
            get
            {
                return _kuerzelInternational;
            }
            set
            {
                SetPropertyValue("KuerzlInternational", ref _kuerzelInternational, value);
            }
        }


        [XafDisplayName("Key-International")]
        public System.String KeyInternational
        {
            get
            {
                return _keyInternational;
            }
            set
            {
                SetPropertyValue("KeyInternational", ref _keyInternational, value);
            }
        }

        [XafDisplayName("Key - National")]
        public System.String KeyNational
        {
            get
            {
                return _keyNational;
            }
            set
            {
                SetPropertyValue("KeyNational", ref _keyNational, value);
            }
        }

        #endregion
    }
}