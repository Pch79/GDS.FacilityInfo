using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class SteuerKonten
    {
        public SteuerKonten(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        [DevExpress.ExpressApp.DC.XafDisplayName("Matchkey")]
        public System.String SteuerKonten_MatchKey
        {
            get
            {
                var retVal = string.Empty;
                retVal = String.Format("{0}% - {1}", this.SteuerSatz.ToString(), this.LaenderKZ);
                return retVal;
            }
        }
    }

}
