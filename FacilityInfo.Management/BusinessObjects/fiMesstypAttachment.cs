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
using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Messung.BusinessObjects;

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anhang")]
    public class fiMesstypAttachment : boAttachment
    {
        private boMesstyp _messtyp;
        public fiMesstypAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [XafDisplayName("Messtyp")]
        [Association("boMesstyp-fiMesstypAttachment")]
        public boMesstyp Messtyp
        {
            get
            {
                return _messtyp;
            }
            set
            {
                SetPropertyValue("Messtyp", ref _messtyp, value);
            }
        }
    }
}