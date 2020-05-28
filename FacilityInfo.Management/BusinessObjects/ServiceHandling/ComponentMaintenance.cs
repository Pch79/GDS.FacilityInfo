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
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.Management.ServiceHandling.BusinessObjects;

namespace FacilityInfo.Management.ServiceHandling.BusinessObjects
{
    [DefaultClassOptions]
  [XafDisplayName("Produktwartung")]
  [ImageName("gears_16")]
  [XafDefaultProperty("MatchKey")]
    public class ComponentMaintenance : MaintenanceSchedule
    {
        private fiHerstellerProdukt _herstellerProdukt;
        public ComponentMaintenance(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this.LinkKey == null)
            {
                if (this.HerstellerProdukt != null)
                {
                    this.LinkKey = this.HerstellerProdukt.Oid.ToString();
                    this.Save();
                    this.Session.CommitTransaction();
                }
            }

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch(propertyName)
            {
                case "HerstellerProdukt":
                    if(newValue != null)
                    {
                        this.LinkKey = ((fiHerstellerProdukt)newValue).Oid.ToString();
                    }
                    else
                    {
                        this.LinkKey = null;
                    }
                    break;
            }
        }
        #region Properties
        [XafDisplayName("Matchkey")]
        public String MatchKey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var art = string.Empty;
                bezeichnung = (this.Bezeichnung != null) ? this.Bezeichnung : "n.a.";
                art = (this.HerstellerProdukt != null) ? this.HerstellerProdukt.Bezeichnung : "n.a.";
                retVal = String.Format("{0} - {1}", art, bezeichnung);
                return retVal;
            }
        }
        [XafDisplayName("Produkt")]
        [Association("fiHerstellerProdukt-ComponentMaintenance")]
        public fiHerstellerProdukt HerstellerProdukt
        {
            get { return _herstellerProdukt; }
            set { SetPropertyValue("HerstellerProdukt", ref _herstellerProdukt, value); }
        }
        #endregion
    }
}