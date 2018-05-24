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
using FacilityInfo.GlobalObjects.BusinessObjects;
using FacilityInfo.Base.BusinessObjects;

namespace FacilityInfo.Datenfeld.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Datenfeld (neu)")]
    [XafDefaultProperty("Matchkey")]
    public class fiDatenfeld : BaseObject
    {
        private System.String _bezeichnung;
        private boEinheit _einheit;

        public fiDatenfeld(Session session)
            : base(session)
        {
        }

        #region Properties
        [XafDisplayName("Bezeichnung")]
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



        [XafDisplayName("Einheit")]
        public boEinheit Einheit
        {
            get
            {
                return _einheit;
            }
            set
            {
                SetPropertyValue("Einheit", ref _einheit, value);
            }
        }

        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var einheit = string.Empty;
               
                var notAssignedString = "N/A";
                bezeichnung = (this.Bezeichnung != null) ? this.Bezeichnung : notAssignedString;

                einheit = (this.Einheit != null) ? this.Einheit.Einheit_Kuerzel : notAssignedString;
                //kategorie = (this.DatenItem_Kategorie != null) ? this.DatenItem_Kategorie.DatenKategorie_Bezeichnung : notAssignedString;
                if (einheit == notAssignedString)
                {
                    retVal = string.Format("{0}", bezeichnung);
                }
                else
                {
                    retVal = string.Format("{0}[{1}]", bezeichnung, einheit);
                }
                return retVal;
            }
        }
        [Association("fiDatenFeld-fiDatenfeldAntwort")]
        public XPCollection<fiDatenfeldAntwort> lstDatenfeldAntworten
        {
            get
            {
                return GetCollection<fiDatenfeldAntwort>("lstDatenfeldAntworten");
            }
        }

        #endregion

        #region Properties
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #endregion


    }
}