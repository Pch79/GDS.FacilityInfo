using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class Personal
    {
        public Personal(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Eigene Properties
        [DevExpress.ExpressApp.DC.XafDisplayName("Matchkey")]
        public System.String Personal_Matchkey
        {
            get
            {
                var retVal = string.Empty;
                retVal = string.Format("{0} - {1}", this.PersonalNr.ToString(), this.Name);
                return retVal;
            }
        }


        [DevExpress.ExpressApp.DC.XafDisplayName("SAP-Personalnummer")]
        public System.String Personal_SapPersonalNummer
        {
            get
            {
                var retVal = string.Empty;

                retVal = this.PersBemerkung1.PadLeft(6, '0');

                return retVal;
            }
        }
        #endregion

    }

}
