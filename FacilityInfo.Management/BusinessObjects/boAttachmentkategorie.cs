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

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Attachmentkategorie")]
    [XafDefaultProperty("Bezeichnung")]

    public class boAttachmentkategorie : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private boAttachment _attachment;
        private System.Boolean _online;
        public boAttachmentkategorie(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Online = false;
        }
        #region Properties
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
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
        {
            get
            {
                return _beschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
            }
        }
        [XafDisplayName("Attachments")]
        [DevExpress.Xpo.Aggregated]
        [Association("boAttachment-boAtachmentkategorie")]
        
        public XPCollection<boAttachment> lstAttachments
        {
            get
            {
                return GetCollection<boAttachment>("lstAttachments");
            }
        }

        [XafDisplayName("Online")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public System.Boolean Online
        {
            get
            {
                return _online;
            }
            set
            {
                SetPropertyValue("Online", ref _online, value);
            }
        }
            
        #endregion
    }
}