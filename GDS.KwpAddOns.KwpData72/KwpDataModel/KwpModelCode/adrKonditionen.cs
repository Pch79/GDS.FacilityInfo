using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;

namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class adrKonditionen
    {
    
        public adrKonditionen(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var nummer = string.Empty;
                var kurztext = string.Empty;
                nummer = this.KonditionsNr.ToString();
                kurztext = (this.SkontoKurzText != null) ? this.SkontoKurzText : "N/A";
                retVal = string.Format("{0} ({1})", nummer, kurztext);

                return retVal;
            }
        }
    }

}
