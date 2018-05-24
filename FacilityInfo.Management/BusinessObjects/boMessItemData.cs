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

namespace FacilityInfo.Messung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Messdateneintrag")]
    [XafDefaultProperty("MessItemEntry")]
    [ImageName("action_log")]

    public class boMessItemData : BaseObject
    {
        private boMessitemEntry _messitementry;
        private System.Decimal _messwert;
        private boMessprobe _messprobe;
        public boMessItemData(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        //gibt es einen Wertebereich??
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.Session.IsObjectToDelete(this))
            {
                switch (propertyName)
                {
                    case "Messwert":
                        //wenn der Wert geändert ist feststellen ob es hier einen definierten Wertebereich gibt
                        break;
                }
            }
        }
        #region Properties
        /*
        [XafDisplayName("Wertebereich")]
        public XPCollection<boWertebereich> lstWertebereich
        {
            
           
        }
        */
        [XafDisplayName("Probe")]
        [Association("boMessprobe-boMessItemData")]
        public boMessprobe Messprobe
        {
            get
            {
                return _messprobe;
            }
            set
            {
                SetPropertyValue("Messprobe", ref _messprobe, value);
            }
        }
        [XafDisplayName("MessItemEntry")]
        public boMessitemEntry MessItemEntry
        {
            get
            {
                return _messitementry;
            }
            set
            {
                SetPropertyValue("MessItemEntry", ref _messitementry, value);
            }
        }
        [XafDisplayName("Messwert")]
        public System.Decimal Messwert
        {
            get
            {
                return _messwert;
            }
            set
            {
                SetPropertyValue("Messwert", ref _messwert, value);
            }
        }
        #endregion
    }
}