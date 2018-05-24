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

namespace FacilityInfo.Building.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Ebene")]
    [XafDefaultProperty("Bezeichnung")]
    public class fiEbene : BaseObject
    {
        private System.String _bezeichnung;
        private fiEbenenart _art;
        private System.Int32 _sortposition;
        private fiGebaeude _gebaeude;


        public fiEbene(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "Art":
                    if (this.Art != null)
                    {
                        this.Bezeichnung = this.Art.Bezeichnung;
                    }
                    else
                    {
                        this.Bezeichnung = string.Empty;
                    }
                    break;
            }
        }
        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                boMandant retVal;
                if (this.Gebaeude != null)
                {
                    if (this.Gebaeude.Liegenschaft != null)
                    {
                        retVal = (this.Gebaeude.Liegenschaft.Mandant != null) ? this.Gebaeude.Liegenschaft.Mandant : null;
                    }
                    else
                    {
                        retVal = null;
                    }
                }
                else
                {
                    retVal = null;
                }
                return retVal;
            }
        }

        [XafDisplayName("Sortposition")]
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
        [XafDisplayName("Art")]
        public fiEbenenart Art
        {
            get
            {
                return _art;
            }
            set
            {
                SetPropertyValue("Art", ref _art, value);
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

       [XafDisplayName("Gebäude")]
        [Association("fiGebaeude-fiEbene")]
        public fiGebaeude Gebaeude
        {
            get
            {
                return _gebaeude;
            }
            set
            {
                SetPropertyValue("Gebaeude", ref _gebaeude, value);
            }
        }

        [XafDisplayName("Räume")]
       
        [Association("fiEbene-fiRaum")]
        public XPCollection<fiRaum> lstRaeume
        {
            get
            {
                return GetCollection<fiRaum>("lstRaeume");
            }
        }
    }
}