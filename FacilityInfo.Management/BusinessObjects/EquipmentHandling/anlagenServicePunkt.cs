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
    [XafDisplayName("Servicepunkt")]
    public class anlagenServicePunkt : BaseObject
    {
        private boAnlagenArt _anlagenArt;
        private String _bezeichnung;
        private Int32 _sortIndex;

        // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public anlagenServicePunkt(Session session)
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
        [Association("boAnlagenArt-anlagenServicePunkt")]
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
        [XafDisplayName("Sortorder")]
        public Int32 SortIndex
        {
            get
            {
                return _sortIndex;
            }
            set
            {
                SetPropertyValue("SortIndex", ref _sortIndex, value);
            }
        }

        //Servicepunkt hat resultate oder hat das ServiceItem Resultate??
        #endregion

    }
}