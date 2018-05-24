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

namespace FacilityInfo.GlobalObjects.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Datenkategorie")]
    [XafDefaultProperty("DatenKategorie_Bezeichnung")]
    [ImageName("brick")]
    public class boDatenKategorie : BaseObject
    {
        private System.String _datenKategorie_Bezeichnung;
        private System.String _datenKategorie_Kuerzel;
       
        public boDatenKategorie(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
    
            
        }
        #region Properties
        public System.String DatenKategorie_Bezeichnung
        {
            get
            {
                return _datenKategorie_Bezeichnung;
            }
            set
            {
                SetPropertyValue("DatenKategorie_Bezeichnung", ref _datenKategorie_Bezeichnung, value);
            }
        }
        public System.String DatenKategorie_Kuerzel
        {
            get
            {
                return _datenKategorie_Kuerzel;
            }
            set
            {
                SetPropertyValue("DatenKategorie_Kuerzel", ref _datenKategorie_Kuerzel, value);
            }
        }

       
        [XafDisplayName("Datenfelder")]
        [Association("boDatenItem-boDatenKategorie"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boDatenItem> lstDatenItems
        {
            get
            {
                return GetCollection<boDatenItem>("lstDatenItems");
            }
        }

        #endregion

    }
}