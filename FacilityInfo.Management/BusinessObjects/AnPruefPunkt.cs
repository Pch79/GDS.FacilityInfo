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

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Prüfpunkt")]
    [XafDefaultProperty("Matchkey")]
    [ImageName("zoom_16")]
    public class AnPruefPunkt : BaseObject
    {
        private System.String _bezeichnung;
        //hier steht die Art der Entnahmestelle
        private AnPruefPunktTyp _pruefpunkttyp;
        //Prüfanschluss
        private AnPruefanschluss _pruefanschluss;
        private AnKomponente _anlagenkomponente;
        


        public AnPruefPunkt(Session session)
            : base(session)
        {
        }
        #region
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #endregion

        #region Properties
        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var typ = string.Empty;
                bezeichnung = (this.Bezeichnung != null) ? this.Bezeichnung : "N/A";
                typ = (this.Pruefpunkttyp != null) ? this.Pruefpunkttyp.Kuerzel : "N/A";
                retVal = string.Format("{0} - {1}", typ, bezeichnung);
                return retVal;
            }
        }

        [XafDisplayName("Anlagenkomponente")]
        [Association("AnPruefPunkt-AnKomponente")]
        public AnKomponente Anlagenkomponente
        {
            get
            {
                return _anlagenkomponente;
            }
            set
            {
                SetPropertyValue("Anlagenkomponente", ref _anlagenkomponente, value);
            }
        }
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
        [XafDisplayName("Prüfpunkttyp")]
        public AnPruefPunktTyp Pruefpunkttyp
        {
            get
            {
                return _pruefpunkttyp;
            }
            set
            {
                SetPropertyValue("Pruefpunkttyp", ref _pruefpunkttyp, value);
            }
        }

        [XafDisplayName("Prüfanschluss")]
        public AnPruefanschluss Pruefanschluss
        {
            get
            {
                return _pruefanschluss;
            }
            set
            {
                SetPropertyValue("Pruefanschluss", ref _pruefanschluss, value);
            }
        }
        #endregion
    }
}