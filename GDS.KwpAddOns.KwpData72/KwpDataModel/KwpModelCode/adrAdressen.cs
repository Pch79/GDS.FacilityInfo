using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using static GDS.KwpAddOns.KwpData72.StaticData.StaticValues;

namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class adrAdressen
    {
        public adrAdressen(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Eigene Properties
        [DevExpress.ExpressApp.DC.XafDisplayName("Adresse_Matchkey")]
        public String Adresse_Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var name = string.Empty;
                var vorname = string.Empty;
                var debitKreditnr = string.Empty;
                name = (this.Name != null) ? this.Name : "[Name]";
                vorname = (this.Vorname != null) ? this.Vorname : "[Vorname]";
                debitKreditnr = this.DebitKreditNr.ToString();
                retVal = string.Format("{0} - {1} - ({2})", name, vorname,debitKreditnr);
                return retVal;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Adresse_Selekt1")]
        public adrSelekts Adresse_Selekt1
        {
            get
            {

                adrSelekts retVal = this.Session.GetObjectByKey<adrSelekts>(this.Selekt1);

                return retVal;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Adresse_Selekt2")]
        public adrSelekts Adresse_Selekt2
        {
            get
            {

                adrSelekts retVal = this.Session.GetObjectByKey<adrSelekts>(this.Selekt2);

                return retVal;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Adresse_Anrede")]
        public adrAnreden Adresse_Anrede
        {
            get
            {

                adrAnreden retVal = this.Session.GetObjectByKey<adrAnreden>(this.Anrede);

                return retVal;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Adresse_Ort")]
        public adrOrte Adresse_Ort
        {
            get
            {

                adrOrte retVal = this.Session.GetObjectByKey<adrOrte>(this.Ort);

                return retVal;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Bank")]
        public adrBanken Adresse_Bank
        {
            get
            {
                adrBanken retVal = null;
                retVal = this.Session.GetObjectByKey<adrBanken>(this.Bank);


                return retVal;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Adressart")]
        public AdressArt Adresse_AdressArt
        {
            get
            {


                AdressArt adrArt = (AdressArt)Enum.ToObject(typeof(AdressArt), this.AdressArt);
                return adrArt;
                
            }
        }
        #endregion

    }

}
