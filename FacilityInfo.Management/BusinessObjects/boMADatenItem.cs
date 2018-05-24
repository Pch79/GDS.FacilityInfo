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

namespace FacilityInfo.Management.BusinessObjects
{
    
    [DefaultClassOptions]
    [XafDisplayName("Datenfeld (Maßnahmen)")]
    [Serializable]
    [ImageName("google_adsense")]

    public class boMADatenItem : BaseObject
    {
        private boDatenItem _datenitem;
        public boMADatenItem(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
        #region Properties

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

        [XafDisplayName("Massnahmenarten")]
        [Association("boMassnahmenArt-boMADatenItem"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boMassnahmenArt> lstMassnahmenArt
        {
            get
            {
                return GetCollection<boMassnahmenArt>("lstMassnahmenArt");
            }
        }

        #endregion

    }
}