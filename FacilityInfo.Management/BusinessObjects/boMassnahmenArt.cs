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

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Bezeichnung")]
    [ImageName("table_heatmap")]
    [XafDefaultProperty("Bezeichnung")]

    public class boMassnahmenArt : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _beschreibung;
     

        public boMassnahmenArt(Session session)
            : base(session)
        {
        }
        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        #endregion

        #region Properties
        [XafDisplayName("Bezeichnung")]
        public System.String Bezeichnung
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
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
        {
            get
            {
                return _beschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
            }
        }
       

        [XafDisplayName("Datenfelder")]
        [Association("boMassnahmenArt-boMADatenItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boMADatenItem> lstDatenFelder
        {
            get
            {
                return GetCollection<boMADatenItem>("lstDatenFelder");
            }
        }

        #endregion
    }
}