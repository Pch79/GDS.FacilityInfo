using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace GDS.KwpAddOns.KwpData72.KwpDataModel.BNWINS_Demo_new
{

    public partial class rebEinRechnung
    {
        public rebEinRechnung(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region eigene Properties
        [DevExpress.ExpressApp.DC.XafDisplayName("Buchungen")]
        public XPCollection<rebBuchung> rebEinRechnung_Buchungen
        {
            get
            {
                XPCollection<rebBuchung> lstRechBuchungen = new XPCollection<rebBuchung>(this.Session, new BinaryOperator("fkER", this.pkER, BinaryOperatorType.Equal));
                return lstRechBuchungen;
            }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName("Steuerkonto")]
        public SteuerKonten rebEinRechnung_SteuerKonto
        {
            get
            {
                SteuerKonten curSteuerKonto = this.Session.FindObject<SteuerKonten>(new BinaryOperator("SteuerSchl", this.fkMwst, BinaryOperatorType.Equal));
                return curSteuerKonto;
            }
        }
        #endregion
    }

}
