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
    [XafDisplayName("Komponente")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("plugin_16")]
    public class AnKomponente : BaseObject
    {
        private System.String _bezeichnung;
        private System.Boolean _messpunkt;
        private AnKomponentenTyp _komponententyp;
        private System.String _beschreibung;
        private boAnlagenArt _anlagenart;

        //die Wertebereiche dann individuell bei der Anlage definieren
        


        public AnKomponente(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties
        [XafDisplayName("Anlagenart")]
        [Association("boAnlagenart-AnKomponente")]
        public boAnlagenArt Anlagenart
        {
            get
            {
                return _anlagenart;
            }
            set
            {
                SetPropertyValue("Anlagenart", ref _anlagenart, value);
            }
        }
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
        [XafDisplayName("Messpunkt")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public System.Boolean Messpunkt
        {
            get
            {
                return _messpunkt;
            }
            set
            {
                SetPropertyValue("Messpunkt", ref _messpunkt, value);
            }
        }

        [XafDisplayName("Komponententyp")]
        public AnKomponentenTyp Komponententyp
        {
            get
            {
                return _komponententyp;
            }
            set
            {
                SetPropertyValue("Komponententyp", ref _komponententyp, value);
            }
        }

        [XafDisplayName("Prüfpunkte")]
        [Association("AnPruefPunkt-AnKomponente")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<AnPruefPunkt> lstPruefpunkte
        {
            get
            {
                return GetCollection<AnPruefPunkt>("lstPruefpunkte");
            }
        }
        
        #endregion
    }
}