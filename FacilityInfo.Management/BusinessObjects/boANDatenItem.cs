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
using FacilityInfo.Anlagen.BusinessObjects;

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Datenfeld (Anlagentyp)")]
    [ImageName("google_webmaster_tools")]
    [Serializable]
    
    public class boANDatenItem : BaseObject
    {

        //private boDatenItem _datenitem;
        private System.Int32 _sortindex;
        public boANDatenItem(Session session)
            : base(session)
        {

        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }


        #region Properties

        [XafDisplayName("Position")]
        public System.Int32 Sortindex
        {
            get
            {
                return _sortindex;
            }
            set
            {
                SetPropertyValue("Sortindex", ref _sortindex, value);
            }
        }
        /*

        [XafDisplayName("Datenitem")]
        //nur Anlagen
        //[DataSourceCriteria(CriteriaOperator.ToString(CriteriaOperator.t ContainsOperator("lstFIObjekte",new BinaryOperator("Objettyp",typeof(boAnlage),BinaryOperatorType.Equal)))]
        public boDatenItem DatenItem
        {
            get
            {
                return _datenitem;
            }
            set
            {
                SetPropertyValue("DatenItem", ref _datenitem, value);
            }
        }
        */

        [XafDisplayName("Anlagenarten")]
        [Association("boAnlagenArt-boANDatenItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boAnlagenArt> lstAnlagenArt
        {
            get
            {
                return GetCollection<boAnlagenArt>("lstAnlagenArt");
            }
        }

        #endregion
    }
}