using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FacilityInfo.Management.EnumStore;
using System;
using System.Linq;

namespace FacilityInfo.Core.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Wasserzeichen")]
    [XafDefaultProperty("Wasserzeichen")]
    [ImageName("wrapping_behind_16")]
    public class boWatermark : BaseObject
    {
        //private System.Drawing.Image _wasserzeichen;
        private boMandant _mandant;
        private Int32 _hoehe;
        private Int32 _breite;
        //private Int32 _transparenz;
        private enmHorizontal _horizontal;
        private enmVertical _vertical;
        public boWatermark(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [XafDisplayName("Mandant")]
        public boMandant mandant
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


        [XafDisplayName("Horizontal")]
        public enmHorizontal Horizontal
        {
            get
            {
                return _horizontal;
            }
            set
            {
                SetPropertyValue("Horizontal", ref _horizontal, value);
            }
        }


        [XafDisplayName("Vertikal")]
        public enmVertical Vertical
        {
            get
            {
                return _vertical;
            }
            set
            {
                SetPropertyValue("Vertical", ref _vertical, value);
            }
        }

        [XafDisplayName("Höhe")]
        public System.Int32 Hoehe
        {
            get
            {
                return _hoehe;
            }
            set
            {
                SetPropertyValue("Hoehe", ref _hoehe, value);
            }
        }
        [XafDisplayName("Breite")]
        public System.Int32 Breite
        {
            get
            {
                return _breite;
            }
            set
            {
                SetPropertyValue("Breite", ref _breite, value);
            }
        }
       [XafDisplayName("Wasserzeichen")]
        [Size(SizeAttribute.Unlimited), ImageEditor]
        public byte[] Wasserzeichen
        {
            get
            {
                return GetPropertyValue<byte[]>("Wasserzeichen");
            }
            set
            {
                SetPropertyValue<byte[]>("Wasserzeichen",  value);
            }
        }
    }
}