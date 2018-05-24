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

namespace FacilityInfo.Liegenschaft.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Risikogruppe")]
   [ImageName("dynamite_16")]
   [XafDefaultProperty("Bezeichnung")]
    public class fiRisikoGruppe : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
        private String _kuerzel;
        private Int32 _sortIndex;
        public fiRisikoGruppe(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Matchkey")]
        public String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var kuerzel = string.Empty;
                var bez = string.Empty;
                kuerzel = (this.Kuerzel != null)?this.Kuerzel:"k.A";
                bez = (this.Bezeichnung != null) ? this.Bezeichnung : "k.A";
                return retVal;
            }
        }
        [XafDisplayName("Sortindex")]
        public Int32 Sortindex
        {
            get
            {
                return _sortIndex;
            }
            set
            {
                SetPropertyValue("Sortindex", ref _sortIndex, value);
            }
        }
        [XafDisplayName("Kürzel")]
        [RuleRequiredField]
        public String Kuerzel
        {
            get
            {
                return _kuerzel;
            }
            set
            {
                SetPropertyValue("Kuerzel", ref _kuerzel, value);
            }
        }
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
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
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
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

        #endregion
    }
}