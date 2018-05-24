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
using FacilityInfo.Management.DomainComponents;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Bildverarbeitung.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Bildkategorie")]
    [XafDefaultProperty("Bezeichnung")]
    public class BildKategorie : BaseObject
    {
        private String _bezeichnung;
        private bool _aktiv;
        private boMandant _mandant;

    // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BildKategorie(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            this.Aktiv = true;
        }
        #region Properties

        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                return _mandant;
            }
            set
            {
                SetPropertyValue("Mandant", ref _mandant, value);
            }
        }
        [XafDisplayName("Aktiv")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        [CaptionsForBoolValues("ja","nein")]
        public bool Aktiv
        {
          get {
                return _aktiv;
          }
          set {
                SetPropertyValue("Aktiv", ref _aktiv, value);
          }
        }
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
        {
         get
         {
                return _bezeichnung;
         }
         set {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
         }
        }

        #endregion
    }
}