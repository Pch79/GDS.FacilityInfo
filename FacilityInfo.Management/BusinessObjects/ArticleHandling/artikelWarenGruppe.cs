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

namespace FacilityInfo.Artikelverwaltung.BusinessObjects
{
    [DefaultClassOptions]
   [XafDisplayName("Warengruppe")]
   [XafDefaultProperty("Bezeichnung")]
   [ImageName("box_search_16")]
    public class artikelWarenGruppe : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
        public artikelWarenGruppe(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
        {
            get { return _bezeichnung; }
            set { SetPropertyValue("Bezeichnung", ref _bezeichnung, value); }
                    
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
        [XafDisplayName("Artikel")]
        [Association("artikelArtikelBase-artikelWarenGruppe")]
        public XPCollection<artikelArtikelBase> lstArtikelBase
        {
            get { return GetCollection<artikelArtikelBase>("lstSrtikelBase"); }
        }
        #endregion
    }
}