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
using FacilityInfo.Liegenschaft.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Massnahme (Liegenschaft)")]
   [ImageName("table_key_16")]

    public class boLGMassnahme : boMassnahme
    {
        private boLiegenschaft _liegenschaft;
        public boLGMassnahme(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-boLGMassnahme")]
        public boLiegenschaft Liegenschaft
        {
            get
            {
                return _liegenschaft;
            }
            set
            {
                SetPropertyValue("Liegenschaft", ref _liegenschaft, value);
            }
        }
        #endregion
    }
}