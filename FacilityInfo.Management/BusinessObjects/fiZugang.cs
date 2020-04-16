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
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Zugang")]
    [ImageName("license_key_16")]
 
    public class fiZugang : BaseObject
    {
        //private fiZugangsTyp _typ;
        private fiZugangKategorie _zugangKategorie;
        private fiZugangKategorie _subKategorie;
        private fiZugangBezeichnung _zugangBezeichnung;
        private System.String _wert;
        private System.String _bemerkung;
        
        private String _keyCodeIntern;
        private String _keyList;
        private String _ort;
        private enmStatusZugang _status;
        
        private String _notiz;
  

        //private fiZugangBeschreibung _beschreibung;
        //TODO: Matchkey einbauen für den Zugang
        public fiZugang(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

       

  
        
        #region Properties
       
       
       
       
        [XafDisplayName("Notiz")]
        [Size(300)]
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
      
        [XafDisplayName("Status")]
       
        public enmStatusZugang Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetPropertyValue("Status", ref _status, value);
            }
        }
        [XafDisplayName("Ort")]
        [ImmediatePostData(true)]
        public String Ort
        {
            get
            {
                return _ort;
            }
            set
            {
                SetPropertyValue("Ort", ref _ort, value);
            }
        }
        [XafDisplayName("Schlüsselliste")]
        [Size(500)]
        public String KeyList
        {
            get
            {
                return _keyList;
            }
            set

            {
                SetPropertyValue("KeyList", ref _keyList, value);
            }

        }
        [XafDisplayName("Schl. Nr.")]
        [ImmediatePostData(true)]
        public String KeyCodeIntern
        {
            get
            {
                return _keyCodeIntern;
            }
            set
            {
                SetPropertyValue("KeyCodeIntern", ref _keyCodeIntern, value);
            }
        }
        
       

       
        
        
      

        [XafDisplayName("Kategorie")]
        [RuleRequiredField]
        [DataSourceCriteria("[ParentItem] is NULL")]
        [ImmediatePostData(true)]
        
        public fiZugangKategorie ZugangKategorie
        {
            get
            {
                return _zugangKategorie;
            }
            set
            {

                SetPropertyValue("ZugangKategorie", ref _zugangKategorie, value);
            }
        }

        [XafDisplayName("Unterkategorie")]
        [DataSourceCriteria("[ParentItem.Oid] ='@this.ZugangKategorie.Oid'")]
        [ImmediatePostData(true)]
        public fiZugangKategorie SubKategorie
        {
            get { return _subKategorie; }
            set { SetPropertyValue("SubKategorie", ref _subKategorie, value); }
        }

        [XafDisplayName("Bezeichnung")]
        public fiZugangBezeichnung ZugangBezeichnung
        {
            get
            {
                return _zugangBezeichnung;
            }
            set
            {
                SetPropertyValue("ZugangBezeichnung", ref _zugangBezeichnung, value);
            }
        }
        [XafDisplayName("Bemerkung")]
        [Size(-1)]
        public System.String Bemerkung
        {
            get
            {
                return _bemerkung;
            }
            set
            {
                SetPropertyValue("Bemerkung", ref _bemerkung, value);
            }
        }
        [XafDisplayName("Wert")]
        [ImmediatePostData(true)]
        public System.String Wert
        {
            get
            {
                return _wert;
            }
            set
            {
                SetPropertyValue("Wert", ref _wert, value);
            }
        }

     

        
        #endregion

    }
}