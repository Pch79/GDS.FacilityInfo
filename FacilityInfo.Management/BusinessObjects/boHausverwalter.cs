using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using System;
using System.Linq;

namespace FacilityInfo.Liegenschaft.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Hausverwalter")]
    [ImageName("premium_support_16")]
    [XafDefaultProperty("Matchkey")]
    public class boHausverwalter : BaseObject

    {
        private boMandant _mandant;
        private PermissionPolicyUser _systembenutzer;
        private boAdresse _adresse;
        private enmLizenzStatus _lizenzstatus;
        private System.String _hausverwalterSelekt1;
        private System.String _hausverwalterSelekt2;
        private System.String _fremdsystemId;
        private System.String _kwpCode;
        private System.String _kuerzel;
        

        public boHausverwalter(Session session)
            : base(session)
        {
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this.Adresse == null)
            {
                this.Adresse = new boAdresse(this.Session);
            }
        }

        #region Methoden
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            //wenn sich der Lizenzstsatus ändert-> Aktion ausführen
            if (!this.Session.IsObjectToDelete(this))
            {
                switch (propertyName)
                {
                    case "Lizenzstatus":
                        if (newValue != null)
                        {
                            if ((enmLizenzStatus)newValue == enmLizenzStatus.aktiv)
                            {
                                //den entsprechenden Benutzer abfragen
                                aktivateUser();
                            }
                            else
                            {
                                deaktivateUser();
                            }
                        }
                        break;
                }
            }

          
        }

      
        private void aktivateUser()
        {
            PermissionPolicyUser curUser = this.Session.GetObjectByKey<PermissionPolicyUser>(this.Systembenutzer.Oid);
            curUser.IsActive = true;
            curUser.Save();

        }

        private void deaktivateUser()
        {
            PermissionPolicyUser curUser = this.Session.GetObjectByKey<PermissionPolicyUser>(this.Systembenutzer.Oid);
            curUser.IsActive = false;
            curUser.Save();

        }
        #endregion


        #region Properties
        [XafDisplayName("HV-Kürzel")]
        public System.String Kuerzel
        {
            get { return _kuerzel; }
            set { SetPropertyValue("Kuerzel", ref _kuerzel, value); }
        }
        [XafDisplayName("KwpCode")]
        public System.String KwpCode
        {
          get {
                return _kwpCode;
          }
          set {
                SetPropertyValue("KwpCode", ref _kwpCode, value);
          }
        }
        [XafDisplayName("FremdsystemID")]
        public System.String FremdsystemId
        {
        get {
                return _fremdsystemId;
        }
        set {
                SetPropertyValue("FremdsystemId", ref _fremdsystemId, value);
        }
        }

        [XafDisplayName("Hausverwalter Selekt1")]
        public System.String HausverwalterSelekt1
        {
         get
         {
                return _hausverwalterSelekt1;
         }
         set {
                SetPropertyValue("HausverwalterSelekt1", ref _hausverwalterSelekt1, value);
         }
        }

        [XafDisplayName("Hausverwalter Selekt2")]
        public System.String HausverwalterSelekt2
        {
            get
            {
                return _hausverwalterSelekt2;
            }
            set
            {
                SetPropertyValue("HausverwalterSelekt2", ref _hausverwalterSelekt2, value);
            }
        }

        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var adresse = string.Empty;
                adresse = (this.Adresse!= null)?this.Adresse.Anzeigename:"N/A";
                retVal = String.Format("{0}", adresse);
                return retVal;
            }
        }

        [XafDisplayName("Lizenzstatus")]
        public enmLizenzStatus Lizenzstatus
        {
            get
            {
                return _lizenzstatus;
            }
            set
            {
                SetPropertyValue("Lizenzstatus", ref _lizenzstatus, value);
            }
        }
        
        [XafDisplayName("Adresse")]
        public boAdresse Adresse
        {
            get
            {
                return _adresse;
            }
            set
            {
                SetPropertyValue("Adresse", ref _adresse, value);
            }
        }

        [XafDisplayName("Systembenutzer")]
        public PermissionPolicyUser Systembenutzer
        {
            get
            {
                return _systembenutzer;
            }
            set
            {
                SetPropertyValue("Systembenutzer", ref _systembenutzer, value);
            }
        }

      

        [XafDisplayName("Mandant")]
      
        [Association("boMandant-boHausverwalter")]
        public boMandant Mandant
        {
            get
            {
                return _mandant;
            }
            set
            {
                SetPropertyValue("Mandant", ref _mandant, value);
            }
        }
        [XafDisplayName("Liegenschaften")]
        [Association("boLiegenschaft-boHausverwalter"), DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<boLiegenschaft> Liegenschaften
        {
            get
            {
                return GetCollection<boLiegenschaft>("Liegenschaften");
            }
        }

        //Benutzer
        [XafDisplayName("Benutzer")]
        [Association("boHausVerwalter-corePortalAccount")]

        public XPCollection<corePortalAccount> lstBenutzer
        {
            get
            {
                return GetCollection<corePortalAccount>("lstBenutzer");
            }
        }

        //Anlagen
        [XafDisplayName("Anlagen")]
        public XPCollection<boAnlage> lstAnlagen
        {
            get
            {
                XPCollection<boAnlage> lstRetval = new XPCollection<boAnlage>(this.Session, new BinaryOperator("Hausverwalter.Oid", this.Oid, BinaryOperatorType.Equal));
                return lstRetval;
            }
        }

        //Haustechnikkomponenten
        //Anlagen
        [XafDisplayName("Anlagensysteme")]
        public XPCollection<LgHaustechnikKomponente> lstAnlagensysteme
        {
            get
            {
                XPCollection<LgHaustechnikKomponente> lstRetval = new XPCollection<LgHaustechnikKomponente>(this.Session, new BinaryOperator("Hausverwalter.Oid", this.Oid, BinaryOperatorType.Equal));
                return lstRetval;
            }
        }
        #endregion


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Lizenzstatus = enmLizenzStatus.inaktiv;
            //TODO: MAndantenzuordnung umbauen
            /*
            string curMandantID = clsStatic.loggedOnMandantOid;
            this.Mandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));
            */
            if (this.Adresse == null)
            {
                this.Adresse = new boAdresse(this.Session);
            }
        } 
    }
}