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

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Link-key (Haustechnikkomponente)")]
    [Description("Brennstoffart aus der KWP Anlage dients als Verbinder zur Haustechnikkomponente")]
    [ImageName("link_16")]
    [DefaultValue("FremdSystemValue")]
    [XafDefaultProperty("FremdSystemValue")]
    public class fiLinkKeyHtKomponente : BaseObject
    {
        private String _fremdSystemKey;
        private String _fremdSystemValue;
        public fiLinkKeyHtKomponente(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Key")]
        public String FremdSystemKey
        {
            get
            {
                return _fremdSystemKey;
            }
            set
            {
                SetPropertyValue("FremdSystemKey", ref _fremdSystemKey, value);
            }
        }
        [XafDisplayName("Value")]
        public String FremdSystemValue
        {
            get
            {
                return _fremdSystemValue; 
            }
            set
            {
                SetPropertyValue("FremdSystemValue", ref _fremdSystemValue, value);
            }
        }
        #endregion

    }
}