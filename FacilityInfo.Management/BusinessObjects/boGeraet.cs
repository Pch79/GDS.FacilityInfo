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
using FacilityInfo.Messung.BusinessObjects;

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Gerät")]
    [XafDefaultProperty("Bezeichnung")]
    public class boGeraet : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _kuerzel;
        private boGeraeteart _geraeteart;
        private System.String _beschreibung;
        public boGeraet(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
        {
            get
            {
                return _beschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        [RuleRequiredField]
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
        [XafDisplayName("Kürzel")]
        public System.String Kuerzel
        {
            get
            {
                return _kuerzel;
            }
            set
            {
                SetPropertyValue("Kürzel", ref _kuerzel, value);
            }
        }
        [XafDisplayName("Geräteart")]
        [RuleRequiredField]
        public boGeraeteart Geraeteart
        {
            get
            {
                return _geraeteart;
            }
            set
            {
                SetPropertyValue("Geraeteart", ref _geraeteart, value);
            }
        }

        //den Messtyp hier verarzten
        //n;n Beziehung
        /*
        [Association("boMesstyp-boGeraet")]
        public XPCollection<boMesstyp> lstMesstyp
        {
            get
            {
                return GetCollection<boMesstyp>("lstMesstyp");
            }
        }
        */
        #endregion

    }
}