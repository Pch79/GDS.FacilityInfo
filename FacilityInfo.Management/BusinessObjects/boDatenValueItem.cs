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
using FacilityInfo.GlobalObjects.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Datenfeldeintrag")]
    [Serializable]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("brick_link")]
    public class boDatenValueItem : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _kurzbezeichnung;
        private System.String _beschreibung;
        public boDatenValueItem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region properties
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
        [XafDisplayName("Kurzbezeichnung")]
        public System.String Kurzbezeichnung
        {
            get
            {
                return _kurzbezeichnung;
            }
            set
            {
                SetPropertyValue("Kurzbezeichnung", ref _kurzbezeichnung, value);
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
        [XafDisplayName("Datenfeld")]
        [Association("boDatenItem-boDatenValueItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boDatenItem> lstDatenItems
        {
            get
            {
                return GetCollection<boDatenItem>("lstDatenItems");
            }
        }
        #endregion

    }
}