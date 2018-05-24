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
using FacilityInfo.Anlagen.BusinessObjects;

namespace FacilityInfo.Datenfeld.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlagenfeld")]
    public class fiAnlagenfeld : BaseObject
    {
        private boAnlage _anlage;
        private fiDatenfeldAntwort _datenfeldAntwort;
        private fiDatenfeldHerstellerprodukt _datenfeldHerstellerprodukt;
        public fiAnlagenfeld(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Antwort")]
        public fiDatenfeldAntwort DatenfeldAntwort
        {
            get
            {
                return _datenfeldAntwort;
            }
            set
            {
                SetPropertyValue("DatenfeldAntwort", ref _datenfeldAntwort, value);
            }
        }

        [XafDisplayName("Datenfeld (Herstellerprodukt)")]
        public fiDatenfeldHerstellerprodukt DatenfeldHerstellerprodukt
        {
            get
            {
                return _datenfeldHerstellerprodukt;
            }
            set
            {
                SetPropertyValue("DatenfeldHerstellerprodukt", ref _datenfeldHerstellerprodukt, value);
            }
        }
        [XafDisplayName("Anlage")]
        [Association("boAnlage-fiAnlagenfeld")]
        public boAnlage Anlage
        {
            get
            {
                return _anlage;
            }
            set
            {
                SetPropertyValue("Anlage", ref _anlage, value);
            }
        }
        #endregion
    }
}