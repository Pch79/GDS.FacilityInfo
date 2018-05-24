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
using FacilityInfo.Management.DomainComponents;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Mitarbeiter")]
    [Serializable]
    [ImageName("BO_Employee")]
    [DefaultProperty("DisplayName")]
    
    [RuleObjectExists(DefaultContexts.Save, "Systembenutzer.Oid ='@Systembenutzer.Oid'",InvertResult =true)]
    public class boMitarbeiter : BaseObject
    {
        private System.Boolean _externerMitarbeiter;
        private System.String _kuerzel;
        //private boMandant _mandant;
        private boAdresse _adresse;
        private System.String _personalnummer;
        private System.String _fremdsystemID;
        private System.DateTime _eintrittsdatum;
        private System.DateTime _austrittsdatum;
        private System.DateTime _geburtsdatum;
        private coreBusinessUnit _businessUnit;
        //der Systembenutzer
        private PermissionPolicyUser _systembenutzer;

        [XafDisplayName("Anzeigename")]
        public String DisplayName
        {
            get
            {
                var retVal = string.Empty;

                if(this.Adresse != null)
                {
                retVal =  this.Adresse.FullName;
                }
                else
                {
                    retVal = null;
                }
                return retVal;
            }
        }
        [XafDisplayName("Systembenutzer")]
        [RuleRequiredField]
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
        [XafDisplayName("Geburtsdatum")]
        public System.DateTime Geburtsdatum
        {
            get
            {
                return _geburtsdatum;
            }
            set
            {
                SetPropertyValue("Geburtsdatum", ref _geburtsdatum, value);
            }
        }
        public boMitarbeiter(Session session)
            : base(session)
        {
        }

        [XafDisplayName("Eintrittsdatum")]
        [RuleRequiredField]
        public System.DateTime Eintrittsdatum
        {
            get
            {
                return _eintrittsdatum;
            }
            set
            {
                SetPropertyValue("Eintrittsdatum", ref _eintrittsdatum, value);
            }
        }

        [XafDisplayName("Austrittsdatum")]
        public System.DateTime Austrittsdatum
        {
            get
            {
                return _austrittsdatum;
            }

            set
            {
                SetPropertyValue("Austrittsdatum", ref _austrittsdatum, value);
            }
        }
        
        [XafDisplayName("FremdsystemID")]
        public System.String FremdsystemID
        {
            get
            {
                return _fremdsystemID;
            }
            set
            {
                SetPropertyValue("FremdsystemID", ref _fremdsystemID, value);
            }
        }
        [XafDisplayName("Personalnummer")]
        [RuleRequiredField]
        public System.String Personalnummer
        {
            get
            {
                return _personalnummer;

            }
            set
            {
                SetPropertyValue("Personalnummer", ref _personalnummer, value);
            }
        }

        [XafDisplayName("Geschäftsbereich")]
        [Association("coreBusinessUnit-boMitarbeiter")]
        public coreBusinessUnit BusinessUnit
        {
            get
            {
                return _businessUnit;
            }
            set
            {
                SetPropertyValue<coreBusinessUnit>("BusinessUnit", ref _businessUnit, value);
            }
        }

        //der Mandant resultiert aus dem Geschäftsbereich

        [XafDisplayName("Mandant")]
        
        public boMandant Mandant
        {
            get
            {
                boMandant retVal = null;
                if(this.BusinessUnit != null)
                {
                    retVal = this.BusinessUnit.Mandant;
                }
                return retVal;
            }
            
        }
        
        [XafDisplayName("Kürzel")]
        [Size(4)]
        public System.String Kuerzel
        {
            get
            {
                return _kuerzel;
            }
            set
            {
                SetPropertyValue("Kuerzel", ref _kuerzel, value);

            }
        }
        [XafDisplayName("externer Mitarbeiter")]
        [CaptionsForBoolValues("ja","nein")]
        [ImagesForBoolValues("Action_Grant","Action_Deny")]
        public System.Boolean Extern
        {
            get
            {
                return _externerMitarbeiter;
            }
            set
            {
                SetPropertyValue("Extern", ref _externerMitarbeiter, value);
            }
        }
        
        [XafDisplayName("Adresse")]
        [RuleRequiredField]
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


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //nachschauen ob ein Nummernkreis da ist
           

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "Mandant":
                    if (newValue != null)
                    {
                        if (this.Session.IsNewObject(this))
                        {
                            createNumber((boMandant)newValue);
                        }
                    }
                    else
                    {
                        this.Personalnummer = null;
                    }
                    break;
            
               
            }
        }

        private void createNumber(boMandant selectedMandant)
        {
            Type curType = this.GetType();
            //var nummer = this.Session.FindObject<boNummernkreis>(new GroupOperator(new BinaryOperator("Objekt", typeof(boMitarbeiter), BinaryOperatorType.Equal), new BinaryOperator("Mandant.Oid",selectedMandant.Oid,BinaryOperatorType.Equal),
            // new BinaryOperator("ValidFrom", DateTime.Now, BinaryOperatorType.LessOrEqual),
            // new BinaryOperator("ValidTo", DateTime.Now, BinaryOperatorType.GreaterOrEqual)));
            var nummer = this.Session.FindObject<boNummernkreis>(new GroupOperator(new BinaryOperator("Objekt", curType, BinaryOperatorType.Equal), new BinaryOperator("Mandant.Oid", selectedMandant.Oid, BinaryOperatorType.Equal),
                     new BinaryOperator("GueltigAb", DateTime.Now, BinaryOperatorType.LessOrEqual),
                     new BinaryOperator("GueltigBis", DateTime.Now, BinaryOperatorType.GreaterOrEqual)));


            if (nummer != null)
            {
                this.Personalnummer = nummer.NextNumber;
                nummer.FortlaufendeNummer = nummer.FortlaufendeNummer + 1;
                nummer.Save();
            }
        }

    }
}