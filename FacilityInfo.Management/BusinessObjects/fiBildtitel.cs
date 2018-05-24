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
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Bildverarbeitung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Bildtitel")]
    public class fiBildtitel : BaseObject
    {
        private System.String _bildtitel;
        private boMandant _mandant;
        public fiBildtitel(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                return _mandant;
            }
            set
            {
                SetPropertyValue("Mandant", ref _mandant, value);
            }
        }
        public System.String Bildtitel
        {
            get
            {
                return _bildtitel;
            }
            set
            {
                SetPropertyValue("Bildtitel", ref _bildtitel, value);
            }
        }
        #endregion
    }
}