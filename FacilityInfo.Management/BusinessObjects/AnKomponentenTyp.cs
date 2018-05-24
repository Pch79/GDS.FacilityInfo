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
    [XafDisplayName("Komponententyp")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("plugin_edit_16")]
    public class AnKomponentenTyp : BaseObject
    {
        private System.String _bezeichnung;

        public AnKomponentenTyp(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [ImageEditor(DetailViewImageEditorFixedHeight = 180, DetailViewImageEditorFixedWidth = 180, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 30, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        [XafDisplayName("Symbol")]
        public byte[] Symbol
        {
            get
            {
                return GetPropertyValue<byte[]>("Symbol");
            }
            set
            {
                SetPropertyValue<byte[]>("Symbol", value);
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
    }
}