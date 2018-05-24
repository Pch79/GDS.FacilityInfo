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
using DevExpress.Xpo.Metadata;
using System.Drawing;

namespace FacilityInfo.Messung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Wertebereich")]
    [ImageName("traffic_lights")]
    public class boWertebereich : BaseObject
    {
        private System.String _bezeichnung;
    
        private boMessitemEntry _messitementry;
        private System.Decimal _minwert;
        private System.Decimal _maxwert;
        private System.Drawing.Color _farbe;
        public boWertebereich(Session session)
            : base(session)
        {
        }

        public class ColorValueConverter : ValueConverter
        {
            public override Type StorageType
            {
                get
                {
                    return typeof(Int32);
                }
            }
            public override object ConvertToStorageType(object value)
            {
                if (!(value is Color)) return null;
                return ((Color)value).ToArgb();
            }
            public override object ConvertFromStorageType(object value)
            {
                if (!(value is Int32)) return null;
                return Color.FromArgb((Int32)value);
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        #region Properties

        [XafDisplayName("Farbe")]
        [ValueConverter(typeof(ColorValueConverter))]
        public System.Drawing.Color Farbe
        {
            get
            {
                return _farbe;
            }
            set
            {
                SetPropertyValue("Farbe", ref _farbe, value);
            }
        }

        [XafDisplayName("Min-Wert")]
        public System.Decimal MinWert
        {
            get
            {
                return _minwert;
            }
            set
            {
                SetPropertyValue("MinWert", ref _minwert, value);
            }
        }
        [XafDisplayName("Max-Wert")]
        public System.Decimal MaxWert
        {
            get
            {
                return _maxwert;
            }
            set
            {
                SetPropertyValue("MaxWert", ref _maxwert, value);
            }
        }


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
       
        [XafDisplayName("MessitemEntry")]
        [Association("boMessitemEntry-boWertebereich")]
        public boMessitemEntry MessitemEntry
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

        #endregion
    }
}