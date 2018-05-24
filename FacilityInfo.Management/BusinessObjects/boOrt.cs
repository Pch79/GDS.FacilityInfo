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
using FacilityInfo.Management.BusinessObjects;

namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Ort")]
    [XafDefaultProperty("Matchkey")]
    [ImageName("google_map")]
    public class boOrt : BaseObject
    {
        
        private System.String _name;
        private System.String _plz;
        private System.String _keyNational;
        private System.String _keyInternational;
        private boBundesland _bundesland;
        private System.String _fremdsystemId;
        private boOrt _parentOrt;
        private boWartungszone _wartungsZone;
       


        public boOrt(Session session)
            : base(session)
        {
        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if(this.ParentOrt != null)
            {
                if (this.ParentOrt.Bundesland != null)
                {
                    if (this.Bundesland != null)
                    {
                        if (this.Bundesland.Oid != this.ParentOrt.Bundesland.Oid)
                        {
                            this.Bundesland = this.ParentOrt.Bundesland;
                            this.Save();
                            this.Session.CommitTransaction();
                        }
                    }
                    else
                    {
                        this.Bundesland = this.ParentOrt.Bundesland;
                        this.Save();
                        this.Session.CommitTransaction();
                    }
                }
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich der Hauptort ändert
            switch(propertyName)
            {
                case "ParentOrt":
                    if(!this.Session.IsObjectToDelete(this))
                    {
                        if(newValue!= null)
                        {
                            boBundesland retVal = null;
                            boOrt selectedOrt = (boOrt)newValue;
                            retVal = (selectedOrt.Bundesland != null)?selectedOrt.Bundesland:null;
                            this.Bundesland = retVal;
                                
                        }
                    }
                    break;
            }
        }


        #region Properties


        [XafDisplayName("Ortsteile")]
        [Association("boOrt-boOrt")]
        public XPCollection<boOrt> lstOrtsteile
        {
          get {
                //XPCollection<boOrt> lstRetVal = new XPCollection<boOrt>(this.Session, new BinaryOperator("ParentOrt.Oid", this.Oid.ToString(), BinaryOperatorType.Equal));
                //return lstRetVal;
                return GetCollection<boOrt>("lstOrtsteile");
            }
        }


        [XafDisplayName("Hauptort")]
        [Association("boOrt-boOrt")]
        public boOrt ParentOrt
        {
          get 
          {
                return _parentOrt;
          }
          set 
          {
                SetPropertyValue("ParentOrt", ref _parentOrt, value);
          }
        }


        [XafDisplayName("FremdsystemID")]
        public System.String FremdsystemID
        {
            get
            {
                return _fremdsystemId;
            }
            set
            {
                SetPropertyValue("FremdsystemID", ref _fremdsystemId, value);
            }
        }
            

        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {

                //wennn kein Hautptort dann PLZ - Ort
                //ansonsten  Name (PLZ - Ort)

                var retVal = String.Empty;
                var ort = string.Empty;
                var plz = string.Empty;
                var parentOrt = string.Empty;
                var name = string.Empty;

                if(this.ParentOrt != null)
                {
                    name = (this.Name != null) ? this.Name : string.Empty;
                    ort = (this.ParentOrt.Name != null)?this.ParentOrt.Name:string.Empty;
                    plz = (this.ParentOrt.PLZ != null) ? this.ParentOrt.PLZ : string.Empty;
                    retVal = string.Format("{0} {1} ({2})", plz, ort,name  );               
                }

                else 
                {
                    ort = (this.Name != null) ? this.Name : string.Empty;
                    plz = (this.PLZ != null) ? this.PLZ : string.Empty;
                    retVal = string.Format("{0} {1}", plz, ort);
                }
              
                return retVal;
            }
        }

       


        [XafDisplayName("Wartungszone")]
        [Association("boWartungszone-lstOrte")]
        public boWartungszone WartungsZone
        {
          get {
                return _wartungsZone;
          }
          set {
                SetPropertyValue("WartungsZone", ref _wartungsZone, value);
          }
        }



        [XafDisplayName("Bundesland")]
        [Association("boBundesland-boOrt")]
        public boBundesland Bundesland
        {
            get
            {
                return _bundesland;
            }
            set
            {
                SetPropertyValue("Bundesland", ref _bundesland, value);
            }
        }
       
        [XafDisplayName("Land")]
        public boLand Land
        {
            get
            {
                boLand retVal = null;
                if (this.Bundesland != null)
                {
                    retVal = (this.Bundesland.Land != null) ? this.Bundesland.Land : null;
                }
                return retVal;

            }
        }

       

        [XafDisplayName("Name")]
        [RuleRequiredField]
        public System.String Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue("Name", ref _name, value);
            }
        }

        [XafDisplayName("PLZ")]   
        public System.String PLZ
        {
            get
            {
                return _plz;
            }
            set
            {
                SetPropertyValue("PLZ", ref _plz, value);
            }
        }

        [XafDisplayName("Key-national")]
        public System.String KeyNational
        {
            get
            {
                return _keyNational;
            }
            set
            {
                SetPropertyValue("KeyNational", ref _keyNational, value);
            }
        }
        
        [XafDisplayName("Key-international")]
        public System.String KeyInternational
        {
            get
            {
                return _keyInternational;
            }
            set
            {
                SetPropertyValue("KeyInternational", ref _keyInternational, value);
            }
        }
        #endregion;
    }
}