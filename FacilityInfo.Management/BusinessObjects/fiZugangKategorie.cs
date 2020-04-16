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
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Kategorie (Zugang)")]
    [XafDefaultProperty("Bezeichnung")]
    [ImageName("key_go_16")]
    [RuleObjectExists(DefaultContexts.Save, "Bezeichnung = '@Bezeichnung'", InvertResult = true)]

    public class fiZugangKategorie : BaseObject
    {
        private String _bezeichnung;
        private String _beschreibung;
        private Int32 _sortIndex;
        private enmStatusZugang _defaultStatus;

        //parent-kategorie einrichten
        private fiZugangKategorie _parentItem;

        public fiZugangKategorie(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        #region Properties
        [XafDisplayName("Übergeordnete Kategorie")]
        public fiZugangKategorie ParentItem
        {
            get { return _parentItem; }
            set { SetPropertyValue("ParentItem", ref _parentItem, value); }
        }

         [XafDisplayName("Defaultstatus")]
         public enmStatusZugang DefaultStatus
        {
            get
            {
                return _defaultStatus;
            }
            set
            {
                SetPropertyValue("DefaultStatus", ref _defaultStatus, value);
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
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
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
        [XafDisplayName("Sortindex")]
        public Int32 Sortindex
        {
            get
            {
                return _sortIndex;
            }
            set
            {
                SetPropertyValue("Sortindex", ref _sortIndex, value);
            }
        }

        [XafDisplayName("Unterkategorien")]
        public XPCollection<fiZugangKategorie> lstSubCategories
        {
            get
            {
                XPCollection<fiZugangKategorie> lstRetVal = new XPCollection<fiZugangKategorie>(this.Session, new BinaryOperator("ParentItem.Oid",this.Oid,BinaryOperatorType.Equal));
                return lstRetVal;
            }
        }
        #endregion
    }
}