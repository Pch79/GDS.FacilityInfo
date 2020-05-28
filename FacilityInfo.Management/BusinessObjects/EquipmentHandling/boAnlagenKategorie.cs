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
using FacilityInfo.Management.BusinessObjects;


namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagenkategorie")]
    [ImageName("interface_preferences_16")]
    [XafDefaultProperty("Bezeichnung")]
    //Elektro/Heizung/Sanitär/Klima/Entwässerung
    public class boAnlagenKategorie : BaseObject
    {
        private System.String _bezeichnung;
        
        //private byte[] _symbol;
        private System.String _kuerzel;
        private boKostenGruppe _gewerk;
        private System.Boolean _aktiv;
        public boAnlagenKategorie(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
 
        }
        [XafDisplayName("Aktiv")]
        [CaptionsForBoolValues("ja","nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public System.Boolean Aktiv
        {
            get
            {
                return _aktiv;
            }
            set
            {
                SetPropertyValue("Aktiv", ref _aktiv, value);
            }
        }
        [XafDisplayName("Gewerk")]
        [Association("boKostenGruppe-boAnlagenKategorie")]
        public boKostenGruppe Gewerk
        {
            get
            {
                return _gewerk;
            }
            set
            {
                SetPropertyValue("Gewerk", ref _gewerk, value);
            }
        }
        [XafDisplayName("Anlagenart")]
        [Association("boAnlagenKategorie-boAnlagenart"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boAnlagenArt> lstAnlagenarten
        {
            get
            {
                return GetCollection<boAnlagenArt>("lstAnlagenarten");
            }
        }
        /*
        [ImageEditor(DetailViewImageEditorFixedHeight =180,DetailViewImageEditorFixedWidth =180, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 30, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        */
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
       [RuleRequiredField]
       public System.String Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnng", ref _bezeichnung, value);
            }
        }
        [XafDisplayName("Kürzel")]
        public System.String Kuerzel
        {
            get
            {
                return _kuerzel;
            }
            set
            {
                SetPropertyValue("Kuerzel", ref _kuerzel, value);
            }
        }
        /*
        [XafDisplayName("Servicepakete")]
        [Association("boAnlagenKategorie-serviceServicePaket")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection <serviceServicePaket> lstServicePakete
        {
            get
            {
                return GetCollection<serviceServicePaket>("lstServicePakete");
            }
        }
        */

        [XafDisplayName("Anlagen")]
        [Association("boAnlage-boAnlagenKategorie")]
        public XPCollection<boAnlage> lstAnlagen
        {
            get
            {
                return GetCollection<boAnlage>("lstAnlagen");
            }
        }

      
    }
}