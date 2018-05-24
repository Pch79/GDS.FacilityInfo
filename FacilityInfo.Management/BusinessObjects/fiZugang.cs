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
    [XafDefaultProperty("Bezeichnung")]
    public class fiZugang : BaseObject
    {
        //private fiZugangsTyp _typ;
        private fiZugangKategorie _zugangKategorie;
        private fiZugangBezeichnung _zugangBezeichnung;
        private System.String _wert;
        private System.String _bemerkung;
        private boKontakt _kontakt;
        private String _keyCodeIntern;
        private String _keyList;
        private String _ort;
        private enmStatusZugang _status;
        private boAdresse _zugangAdresse;
        private String _notiz;
        private XPCollection<boKontakt> _lstAvailableContacts;

        //private fiZugangBeschreibung _beschreibung;

        public fiZugang(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Status = enmStatusZugang.nichtOK;

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            
            switch (propertyName)
            {
                case "ZugangKategorie":
                    if (!this.Session.IsObjectToDelete(this))
                    {
                        if (newValue != null)
                        {
                            fiZugangKategorie curKategorie = (fiZugangKategorie)(newValue);
                            if(curKategorie != null)
                            {
                                this.Status = curKategorie.DefaultStatus;
                                
                            }                            
                        }
                        this.Save();
                        if(!IsLoading)
                        {
                            //  this.Session.CommitTransaction();
                            saveAndLoad();
                        }
                    }
                        break;

                case "ZugangAdresse":
                    //filterKotnakt.
                    
                     
                    break;
            }

      
            
       
            
            
        }

        private void saveAndLoad()
        {
            if(this.Session.IsObjectToSave(this))
            {
                this.Save();
                this.Session.CommitTransaction();
            }
            this.Session.Reload(this);

           
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
        [XafDisplayName("Schlüsselnummer (intern)")]
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
        
        [XafDisplayName("Adresse")]
        [Association("boAdresse-fiZugang")]
        [ImmediatePostData(true)]
        
        public boAdresse ZugangAdresse
        {
            get
                {
                return _zugangAdresse;
            }
            set
            {
                SetPropertyValue("ZugangAdresse", ref _zugangAdresse, value);
                if (!IsLoading)
                {
                    Kontakt = null;
                }
          
                //RefreshContacts();



            }
        }


        [XafDisplayName("Kontakt")]
        //[DataSourceCriteria("[Adresse.Oid] = '@this.ZugangAdresse.Oid'")]
        //[DataSourceProperty("lstAvailableContacts",DataSourcePropertyIsNullMode.SelectNothing)]
        
        //[DataSourceProperty("filterKontakt")]
        
        //[ImmediatePostData(true)]
            
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
        
      

        [XafDisplayName("Kategorie")]
        [RuleRequiredField]
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

   
        [XafDisplayName("AvailableContacts")]
        [Browsable(false)]
        public XPCollection<boKontakt> lstAvailableContacts
        {
            get
            {
                if(_lstAvailableContacts == null)
                {
                    _lstAvailableContacts = new XPCollection<boKontakt>(this.Session);
                    RefreshContacts();
                }
             
                return _lstAvailableContacts;
                
            }
        }

        private void RefreshContacts()
        {
            if(_lstAvailableContacts == null)
            {
              
              return;
            }

            if(this.ZugangAdresse != null)
            {
                
                _lstAvailableContacts.Criteria = new BinaryOperator("Adresse.Oid", this.ZugangAdresse.Oid, BinaryOperatorType.Equal);
            }
            else
            {
                _lstAvailableContacts.Criteria = new BinaryOperator("1", "1", BinaryOperatorType.Equal);

            }

            this.Kontakt = null;

        }
        
        
       
        
        #endregion

        #region CriteriaOperators
        public CriteriaOperator filterKontakt
        {
            get
            {

                if (this.ZugangAdresse != null)
                {
                    return CriteriaOperator.Parse("[Adresse].[Oid] = '@this.ZugangAdresse.Oid'");
                }

          
                else
                {
                    return CriteriaOperator.Parse("[Oid] != null");
                }
           
            }
        }
        #endregion
    }
}