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
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;

namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
  [XafDisplayName("Ansprechpartner")]
  [XafDefaultProperty("Kontakt")]
    [ImageName("vcard_16")]
    public class fiKontaktLiegenschaft : BaseObject
    {
        private boLiegenschaft _liegenschaft;
        private String _funktion;
        
        private boKontakt _kontakt;
        private String _notiz;
        private fiFachbereich _fachbereich;
        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public fiKontaktLiegenschaft(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Properties

        [XafDisplayName("Fachbereich")]
        public fiFachbereich Fachbereich
        {
            get
            {
                return _fachbereich;
            }
            set
            {
                SetPropertyValue("Fachbereich", ref _fachbereich, value);
            }
        }
        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-fiKontaktLiegenschaft")]
        public boLiegenschaft Liegenschaft
        {
            get
            {
                return _liegenschaft;
            }
            set
            {
                SetPropertyValue("Liegenschaft", ref _liegenschaft, value);
            }
        }
        [XafDisplayName("Kontakt")]
        public boKontakt Kontakt
        {
            get
            {
                return _kontakt;
            }
            set
            {
                SetPropertyValue("Kontakt", ref _kontakt, value);
            }
        }

        [XafDisplayName("Funktion")]
        public String Funktion
        {
            get
            {
                return _funktion;
            }
            set
            {
                SetPropertyValue("Funktion", ref _funktion, value);
            }
        }

        [XafDisplayName("Notiz")]
        [Size(-1)]
        public String Notiz
        {
            get
            {
                return _notiz;
            }
            set
            {
                SetPropertyValue("Notiz", ref _notiz, value);
            }
        }
        #endregion
    }
}