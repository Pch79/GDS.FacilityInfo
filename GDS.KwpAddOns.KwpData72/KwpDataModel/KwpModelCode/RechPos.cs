using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class RechPos
    {
        public RechPos(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        [DevExpress.ExpressApp.DC.XafDisplayName("RechnungsPosition_PositionsBetrag")]
        public Double RechnungsPosition_PositionsBetrag
        {
            get
            {
                Double retVal = 0;
                //vklohn + vkmat*mengen

                //Mengen-> float
                
                retVal = (Convert.ToDouble(this.VkLohn +this.VkMat)) * this.Mengen;
                
                return retVal;
            }
        }

        public Rechnung Rechnung
        {
            get
            {
                Rechnung retval;
                retval = this.Session.FindObject<Rechnung>(new BinaryOperator("RECHNR", this.VorgangsNr, BinaryOperatorType.Equal));
                return retval;
            }
        }
        public System.Double Steueranteil
        {
            get
            {
                double retVal;
                double steuerfaktor;
                double steueranteiL;
                if(this.Rechnung != null)
                {
                    steuerfaktor = this.Rechnung.MwstSatz / 100;
                    
                        retVal = this.RechnungsPosition_PositionsBetrag * steuerfaktor;
                    
                   
                }
                else
                {
                    retVal = 0;
                }
                return Math.Round(retVal,2);
            }
        }
        public Double PositionsbetragBrutto
        {
            get
            {
                
                Double retVal = 0;
                double mwstfaktor = (this.Rechnung.MwstSatz/100);
                double mwst = this.RechnungsPosition_PositionsBetrag * mwstfaktor;
                retVal = this.RechnungsPosition_PositionsBetrag + mwst;
                //vklohn + vkmat*mengen

                //Mengen-> float

                //retVal = (Convert.ToDouble(this.VkLohn) + Convert.ToDouble(this.VkMat)) * Convert.ToDouble(this.Mengen);

                return retVal;
            }
        }


       

        [DevExpress.ExpressApp.DC.XafDisplayName("S/H Kennzeichen")]
        public System.String SHKennzeichen
        {
            get
            {
                var retVal = string.Empty;
                retVal = (this.RechnungsPosition_PositionsBetrag >= 0) ? "H" : "S";
                return retVal;
            }
        }

       

    }

}
