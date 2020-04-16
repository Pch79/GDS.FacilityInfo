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
    public class anlageBauGruppe : BaseObject
    {
        private System.String _bezeichnung;
        private System.String _beschreibung;
        private boAnlagenArt _anlagenArt;
        //default Serviceintervall


        //die Wertebereiche dann individuell bei der Anlage definieren
        


        public anlageBauGruppe(Session session)
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
        [Association("boAnlagenart-anlageBauGruppe")]
        public boAnlagenArt AnlagenArt
        {
            get
            {
                return _anlagenArt;
            }
            set
            {
                SetPropertyValue("AnlagenArt", ref _anlagenArt, value);
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
        

      
        
        #endregion
    }
}