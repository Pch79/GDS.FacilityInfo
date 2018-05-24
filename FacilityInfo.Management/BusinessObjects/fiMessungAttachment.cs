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
using FacilityInfo.Messung.BusinessObjects;
using FacilityInfo.DMS.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Anhang (Messung)")]
    public class fiMessungAttachment : boAttachment
    {
        private boMessung _messung;
        public fiMessungAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        [XafDisplayName("Messung")]
        [Association("boMessung-fiMessungAttachment")]
        public boMessung Messung
        {
            get
            {
                return _messung;
            }
            set
            {
                SetPropertyValue("Messung", ref _messung, value);

            }
        }
    }
}