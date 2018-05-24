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

namespace FacilityInfo.Core.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Geschäftsbereich")]
    [ImageName("chart_organisation_16")]
    [DefaultProperty("Bezeichnung")]

    public class coreBusinessUnit : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
        private Boolean _aktiv;
        private boMandant _mandant;
        private boMitarbeiter _bereichsLeiter;
        private String _kuerzel;
        //Tätigkeit kann später auch eingebaut werden

    // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public coreBusinessUnit(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            this.Aktiv = true;
        }
        #region Properties

        [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 30, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        [XafDisplayName("Firmenlogo")]
        [Delayed]
        public byte[] Logo
        {
            get
            {
                return GetDelayedPropertyValue<byte[]>("Logo");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("Logo", value);
            }
        }

        [XafDisplayName("Kürzel")]
        public String Kuerzel
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
            
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
        {
        get {
                return _bezeichnung;
        }
        set {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
        }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
        {
        get {
                return _beschreibung;
        }
        set {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
        }
        }
        [XafDisplayName("Aktiv")]
        [CaptionsForBoolValues("ja","nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public Boolean Aktiv
        {
        get {
                return _aktiv;
        }
        set
        {
                SetPropertyValue("Aktiv", ref _aktiv, value);
        }
        }
        [XafDisplayName("Mandant")]
        [Association("boMandant-coreBusinessUnit")]
        [RuleRequiredField]
        public boMandant Mandant
        {
        get {
                return _mandant;
        }
        set {
                SetPropertyValue("Mandant", ref _mandant, value);
        }
        }

        [XafDisplayName("Mitarbeiter")]
        [Association("coreBusinessUnit-boMitarbeiter"), DevExpress.Xpo.Aggregated]
        public XPCollection<boMitarbeiter> lstMitarbeiter
        {
            get
            {
                return GetCollection<boMitarbeiter>("lstMitarbeiter");    
            }
        }

        [DataSourceCriteria("")]
        public boMitarbeiter BereichsLeiter
        {
            get
            {
                return _bereichsLeiter;
            }
            set
            {
                SetPropertyValue("BereichsLeiter", ref _bereichsLeiter, value);
            }
        }
        #endregion
    }
}