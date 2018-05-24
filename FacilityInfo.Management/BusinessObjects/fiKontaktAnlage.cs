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
using FacilityInfo.Management.BusinessObjects;

namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Ansprechpartner")]
    [XafDefaultProperty("Kontakt")]
    [ImageName("vcard_16")]
    public class fiKontaktAnlage : BaseObject
    {
        private boAnlage _anlage;
        private String _funktion;
        private boKontakt _kontakt;
        private String _notiz;
        private fiFachbereich _fachbereich;

     
        public fiKontaktAnlage(Session session)
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
        [XafDisplayName("Anlage")]
        [Association("boAnlage-fiKontaktAnlage")]
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