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
using FacilityInfo.Artikelverwaltung.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;

namespace FacilityInfo.Service.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Serviceitem")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("to_do_list_16")]

    public class serviceServiceItem : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private serviceServicePaket _servicePaket;
        private artikelServiceArtikel _serviceArtikel;
        private Decimal _dauerNominell;

        //TODO Anlagenart hier einbauen
        private anlagenServicePunkt _servicePunkt;
        

        public serviceServiceItem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
          
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "ServiceArtikel":
                    if(newValue != null)
                    {
                        artikelServiceArtikel curArtikel = (artikelServiceArtikel)newValue;
                        this.DauerNominell = curArtikel.DauerNominell;
                    }
                    else
                    {
                        this.DauerNominell = 0;
                    }


                    break;
            }
        }

        #region Properties
        [XafDisplayName("Dauer (Nominell)")]
        public Decimal DauerNominell
        {
            get
            {
                return _dauerNominell;
            }
            set
            {
                SetPropertyValue("DauerNominell", ref _dauerNominell, value);
            }
        }
        [XafDisplayName("Serviceartikel")]
        public artikelServiceArtikel ServiceArtikel
        {
            get
            {
                return _serviceArtikel;
                
            }
            set
            {
                SetPropertyValue("ServiceArtikel", ref _serviceArtikel, value);
            }
        }

        [XafDisplayName("Servicepunkt")]
        public anlagenServicePunkt ServicePunkt
        {
            get
            {
                return _servicePunkt;
            }
            set
            {
                SetPropertyValue("ServicePunkt", ref _servicePunkt, value);
            }
        }
        [XafDisplayName("Servicepaket")]
        [Association("serviceServicePaket-serviceServiceItem")]
        public serviceServicePaket Servicepaket
        {
            get
            {
                return _servicePaket;
            }
            set
            {
                SetPropertyValue("ServicePaket", ref _servicePaket, value);
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


        /*
        [Association("boServicedefinition-boServiceItem")]
        public XPCollection<boServicedefinition> lstServiceDefinition
        {
            get
            {
                return GetCollection<boServicedefinition>("lstServiceDefinition");
            }
        }

        [XafDisplayName("Ergebnisliste")]
        [Association("boServiceItem-boServiceItemResult")]
        public XPCollection<boServiceItemResult> lstServiceItemResults
        {
            get
            {
                return GetCollection<boServiceItemResult>("lstServiceItemResults");
            }
        }
        */
        #endregion
    }
}