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


namespace FacilityInfo.Vertrag.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Servicevertrag")]
    public class vertragServiceVertrag : vertragVertragBase
    {

        private String _FrmdsystemId;
        //vrertragspakete
        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vertragServiceVertrag(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("FremdsystemId")]
        public String FremdsystemId
        {
            get
            {
                return _FrmdsystemId;
            }
            set
            {
                SetPropertyValue("FremdsystemId", ref _FrmdsystemId, value);
            }
        }
        /*
        [XafDisplayName("Servicepakete")]
        public XPCollection<serviceServicePaket> lstServicePakete
        {
            get
            {
                return GetCollection<serviceServicePaket>("lstServicePakete");
            }
        }
        */
        #endregion
    }
}