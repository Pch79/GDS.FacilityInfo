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
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Herstellerdokument")]
    public class fiHerstellerAttachment : boAttachment
    {
        private boHersteller _hersteller;
        public fiHerstellerAttachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "Hersteller":


                    if(newValue != null)
                    {
                        boHersteller selectedHersteller = (boHersteller)newValue;
                        this.Objektkey = selectedHersteller.Oid.ToString();
                    }
                    else
                    {
                        this.Objektkey = string.Empty;
                    }
                    break;
            }
        }
        #region Properties
        [XafDisplayName("Hersteller")]
        [Association("boHersteller-fiHerstellerProdukt")]
        public boHersteller Hersteller
        {
            get
            {
                return _hersteller;
            }
            set
            {
                SetPropertyValue("Hersteller", ref _hersteller, value);
            }
        }
        #endregion
    }
}