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

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Land")]
    [ImageName("globe_place_16")]
    [XafDefaultProperty("IsoKennzeichen")]
    
    public class boLand : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _isoKennzeichen;
        private System.String _key;
     
        private System.String _vorwahl;
        public boLand(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
         
        }
        [XafDisplayName("Vorwahl")]
        public System.String Vorwahl
        {
            get
            {
                return _vorwahl;
            }
            set
            {
                SetPropertyValue("Vorwahl", ref _vorwahl, value);
            }
        }

        [XafDisplayName("Bundesländer")]
        [Association("boLand-boBundesland"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boBundesland> lstBundeslands
        {
            get
            {
                return GetCollection<boBundesland>("lstBundeslands");
            }
        }

        [XafDisplayName("Flagge")]
        /*
        [ImageEditor(ListViewImageEditorMode =ImageEditorMode.PictureEdit,ImageSizeMode =ImageSizeMode.Zoom,ListViewImageEditorCustomHeight =60,DetailViewImageEditorFixedWidth =90,DetailViewImageEditorFixedHeight =90)]
        */
        public byte[] Flag
        {
            get
            {
                return GetPropertyValue<byte[]>("Flag");
            }
            set
            {
                SetPropertyValue("Flag", value);
            }
        }

        
        [XafDisplayName("Key")]
        public System.String Key
        {
            get
            {
                return _key;
            }
            set
            {
                SetPropertyValue("Key", ref _key, value);
            }
        }
        [XafDisplayName("IsoKennzeichen")]
        public System.String IsoKennzeichen
        {
            get
            {
                return _isoKennzeichen;
            }
            set
            {
                SetPropertyValue("IsoKennzeichen", ref _isoKennzeichen, value);
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