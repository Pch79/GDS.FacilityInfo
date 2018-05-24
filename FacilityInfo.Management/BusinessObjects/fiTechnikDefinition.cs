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

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Technikdefinition")]
    public class fiTechnikDefinition : BaseObject
    {
        private System.Boolean _erforderlich;
        private System.Int32 _maximalzahl;
        private System.Int32 _sortposition;
        private boFIObjekt _fiObjekt;
        private boAnlagenArt _anlagenart;
        public fiTechnikDefinition(Session session)
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
        [XafDisplayName("Erforderliche Komponente")]
        [CaptionsForBoolValues("ja","nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public System.Boolean Erforderlich
        {
            get
            {
                return _erforderlich;
            }
            set
            {
                SetPropertyValue("Erforderlich", ref _erforderlich, value);
            }
        }
        [XafDisplayName("Maximalzahl")]
        public System.Int32 Maximalzahl
        {
            get
            {
                return _maximalzahl;
            }
            set
            {
                SetPropertyValue("Maximalzahl", ref _maximalzahl, value);
            }
        }

        [XafDisplayName("Sortposition")]
        public System.Int32 Sortposition
        {
            get
            {
                return _sortposition;
            }
            set
            {
                SetPropertyValue("Sortposition", ref _sortposition, value);
            }
        }
        [XafDisplayName("Fi-Objekt")]
        [Association("boFIObjekt-fitechnikDefinition")]
        public boFIObjekt FiObjekt
        {
            get
            {
                return _fiObjekt;
            }
            set
            {
                SetPropertyValue("FiObjekt", ref _fiObjekt, value);
            }
        }

        #endregion
    }
}