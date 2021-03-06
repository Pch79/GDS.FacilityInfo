﻿using System;
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
using FacilityInfo.Liegenschaft.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [Serializable]
    [ImageName("building_edit")]
    [XafDisplayName("Objektdatenfeld")]
    
    public class boLGDatenEntry : BaseObject
    {
        private boDatenItem _datenfeld;
        private boLiegenschaft _liegenschaft;
        private System.String _eintragswertLang;
        private System.String _eintragswertKurz;
        private System.String _eintragswertSecure;
        private boDatenValueItem _eintragswertValueItem;

        public boLGDatenEntry(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

       
        #region Properties

        [XafDisplayName("Antwortkatalog")]
        [DataSourceProperty("Datenfeld.lstDatenValueItems")]
        //[DataSourceCriteria("CONTAINS(lstDatenItems[Oid = '@This.Datenfeld.Oid'])")]
        public boDatenValueItem EintragswertValueItem
        {
            get
            {
                return _eintragswertValueItem;
            }
            set
            {
                SetPropertyValue("EintragswertValueItem", ref _eintragswertValueItem, value);
            }
        }
        [XafDisplayName("Eintragswert (Langtext)")]
        [Size(500)]
        public System.String EintragswertLang
        {
            get
            {
                return _eintragswertLang;
            }
            set
            {
                SetPropertyValue("EintragswertLang", ref _eintragswertLang, value);
            }
        }
        [XafDisplayName("Eintragswert (Kurztext)")]
        [Size(100)]
        public System.String EintragswertKurz
        {
            get
            {
                return _eintragswertKurz;
            }
            set
            {
                SetPropertyValue("EintragswertKurz", ref _eintragswertKurz, value);
            }
        }

        [XafDisplayName("Eintragswert (Sicherheit)")]
        [Size(100)]
       
       
        public System.String EintragswertSecure
        {
            get
            {
                return _eintragswertSecure;
            }
            set
            {
                SetPropertyValue("EintragswertSecure", ref _eintragswertSecure, value);
            }
        }
        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-boLGDatenEntry")]
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
        [XafDisplayName("Datenfeld")]
        public boDatenItem Datenfeld
        {
            get
            {
                return _datenfeld;
            }
            set
            {
                SetPropertyValue("Datenfeld", ref _datenfeld, value);
            }
        }
        #endregion
    }
}