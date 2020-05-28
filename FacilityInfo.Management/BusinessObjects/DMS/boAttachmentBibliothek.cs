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
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.DMS.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Dokumentbibliothek")]
    [ImageName("inbox_empty_16")]
    [XafDefaultProperty("Bezeichnung")]
    public class boAttachmentBibliothek : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private boMandant _mandant;
        private System.Int32 _sortposition;
        private String _key;
        //hier noch einbauen ob das Ding online stehen soll oder nicht
        private bool _online;

        public boAttachmentBibliothek(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        #region Properties
        [XafDisplayName("Key")]
        public String Key
        {
            get { return _key; }
            set { SetPropertyValue("Key", ref _key, value); }
        }
        [XafDisplayName("Online")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public Boolean Online
        { get
            {
                return _online;
            }
            set { SetPropertyValue("Online", ref _online, value); }
        }
        [XafDisplayName("Sortierposition")]
        public System.Int32 Sortposition
        {
            get
            {
                return _sortposition;
            }
            set
            {
                SetPropertyValue("Sortposition", ref _sortposition, value);
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


        [XafDisplayName("Mandant")]
        [Association("boMandant-boAttachmentBibliothek")]
       
        public boMandant Mandant
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


        [XafDisplayName("Dokumente")]
        [Association("boAttachmentBibliothek-boAttachment"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boAttachment> lstAttachments
        {
            get
            {
                return GetCollection<boAttachment>("lstAttachments");
            }
        }

        #endregion
    }
}