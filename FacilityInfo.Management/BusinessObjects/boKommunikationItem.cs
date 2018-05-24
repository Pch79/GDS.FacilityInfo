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
using FacilityInfo.GlobalObjects.EnumStore;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Kommunikationseintrag")]
    [XafDefaultProperty("Matchkey")]
    [ImageName("telephone_16")]
    
    public class boKommunikationItem : BaseObject
    {
        private enmKommunikationsArt _kontaktart;
        private enmKommunikationsTyp _kontakttyp;
        private System.String _bezeichnung;
        private System.String _kontaktvalue;


        public boKommunikationItem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Kontakttyp = enmKommunikationsTyp.dienstlich;
            this.Kontaktart = enmKommunikationsArt.none;

        }

        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                
                    var type = typeof(enmKommunikationsArt);
                    var memInfo = type.GetMember(this.Kontaktart.ToString());
                    var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)attributes[0]).Description;
                retVal = string.Format("{0} {1}", description, this.Kontakttyp.ToString());
                
                return retVal;
            }
        }

        [XafDisplayName("Anzeige")]
        public System.String Anzeige
        {
            get
            {
                var retVal = string.Empty;
                //matchkey + eintragswert
                var wert = string.Empty;
                wert = (this.Kontaktvalue != null) ? this.Kontaktvalue : "N/A";
                retVal = String.Format("{0}: {1}", this.Matchkey, wert);
                return retVal;
            }
        }

        [XafDisplayName("Kontakttyp")]
        public enmKommunikationsTyp Kontakttyp
        {
            get
            {
                return _kontakttyp;
            }
            set
            {
                SetPropertyValue("Kontakttyp", ref _kontakttyp, value);
            }
        }
        [XafDisplayName("Kontaktart")]
        public enmKommunikationsArt Kontaktart
        {
            get
            {
                return _kontaktart;
            }
            set
            {
                SetPropertyValue("Kontaktart", ref _kontaktart, value);

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
        [XafDisplayName("Eintrag")]
        public System.String Kontaktvalue
        {
            get
            {
                return _kontaktvalue;
            }
            set
            {
                SetPropertyValue("Kontaktvalue", ref _kontaktvalue, value);
            }
        }
    }
}