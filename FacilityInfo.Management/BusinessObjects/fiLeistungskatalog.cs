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

namespace FacilityInfo.Vertrag.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Leistungskatalog")]
    [Serializable]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("installer_box_16")]
    public class fiLeistungskatalog : BaseObject
    {
        private boMandant _mandant;
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private System.Boolean _aktiv;
        public fiLeistungskatalog(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
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



        [XafDisplayName("Mandant")]
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



        [XafDisplayName("Aktiv")]
        [ImagesForBoolValues("Action_Accept","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
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




        [XafDisplayName("Produkte")]
        [Association("boLeistungskatalog-boProdukt"), DevExpress.Xpo.Aggregated]
        public XPCollection<fiProdukt> lstProdukte
        {
            get
            {
                return GetCollection<fiProdukt>("lstProdukte");
            }
        }
        #endregion
    }
}