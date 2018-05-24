using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class ErlGrp
    {
        public ErlGrp(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        [DevExpress.ExpressApp.DC.XafDisplayName("Matchkey")]
        public System.String ErlGrp_MatchKey
        {
            get
            {
                var retVal = string.Empty;
               
                retVal = String.Format("{0} - {1}", this.ABTNR.ToString(), this.ABTTEXT);
                return retVal;
            }
        }
    }

}
