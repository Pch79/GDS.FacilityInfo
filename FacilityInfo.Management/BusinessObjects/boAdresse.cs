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

using FacilityInfo.Management.EnumStore;

using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.BusinessManagement.BusinessObjects;
using FacilityInfo.Management;
using FacilityInfo.Management.Helpers;


namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
    [Serializable]
    [XafDefaultProperty("Anzeigename")]
    [ImageName("BO_Address")]
    [XafDisplayName("Adresse")]
    public class boAdresse : BaseObject, Iadresse, Ikommunikation
    {
        #region Fields
        private System.String _fremdsystemId;
        private System.String _fremdsystemCode;
        private System.String _firmenname;

        private String _kuerzelIntern;

        private System.String _vorname;
        private System.String _nachname;
        private System.String _strasse;
        private System.String _hausnummer;
        private System.String _zusatz;

        private boOrt _ort;
        private System.String _latitude;
        private System.String _longitude;
        private boAnrede _anrede;



        private System.String _notizen;
        private boMandant _mandant;

        private System.String curMandantID;
        private adresseTitel _titel;

        private String _kuerzel;

        private String _telefon1;
        private String _telefon2;
        private String _fax;
        private String _mobil;
        private String _mail;
        private String _internet;


        #endregion

        public boAdresse(Session session)
            : base(session)
        {
        }

      


        #region Wartungszone

        [XafDisplayName("Wartungszone")]
        public boWartungszone Wartungszone
        {
            get
            {
                boWartungszone retVal = null;
                if (this.Mandant != null)
                {
                    retVal = getWartungszone(this.Mandant);
                }

                return retVal;
            }
        }
        
        public boWartungszone getWartungszone(boMandant curMandant)
        {
            boWartungszone retVal;

            if(this.ort != null)
            {
                retVal = Session.FindObject<boWartungszone>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new ContainsOperator("lstOrte", new BinaryOperator("Name", this.ort.Name, BinaryOperatorType.Equal))));
             }
            else
            {
                retVal = null;
            }

            return retVal;
        }

        #endregion




        #region Properties
        [XafDisplayName("Titel")]
        public adresseTitel Titel
        {
            get { return _titel; }
            set { SetPropertyValue("Titel", ref _titel, value); }
        }

        [XafDisplayName("Kürzel (Intern)")]
        public String KuerzelIntern
        {
            get
            {
                return _kuerzelIntern;
            }
            set
            {
                SetPropertyValue("KuerzelIntern", ref _kuerzelIntern, value);
            }
        }
        [XafDisplayName("Mandant")]
        [ImmediatePostData(true)]
        //[RuleRequiredField]
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

        [XafDisplayName("Land")]
        public boLand Land
        {
            get
            {
                boLand retVal;
                //wenn kein Bundesland zugeordnet ist
                if(this.ort !=null)
                {
                    if(ort.Bundesland != null )
                    {
                        if(ort.Bundesland.Land != null)
                        {
                            retVal = ort.Bundesland.Land;
                        }
                        else
                        {
                            retVal = null;
                        }
                    }
                    else
                    {
                        retVal = null;
                    }
                }
                else
                {
                    retVal = null;
                }
                //retVal = (this.ort != null) ? this.ort.Bundesland.Land : null;
                return retVal;
                    
            }
        }

        [XafDisplayName("Bundesland")]
        public boBundesland Bundesland
        {
            get
            {
                boBundesland retVal;
                retVal = (this.ort != null) ? this.ort.Bundesland : null;
                return retVal;
            }
        }

        [XafDisplayName("PLZ")]
        public System.String Plz
        {
            get
            {
                String retVal = string.Empty;
                retVal = (this.ort != null) ? this.ort.PLZ : null;
                return retVal;
            }
        }
          
        
        
        [XafDisplayName("Hauptort")]
        //[DataSourceCriteria("Hauptort.Oid = '@this.ort.Oid'")]
       // [ImmediatePostData(true)]
        public boOrt Hauptort
        {
            get
            {
                boOrt retval = null;
                if (this.ort != null)
                {
                    retval = (this.ort.ParentOrt != null) ? this.ort.ParentOrt : this.ort;
                }
                return retval;
            }
          
        }
        
    

    
        [XafDisplayName("Zusatz")]
        public System.String Zusatz
        {
            get
            {
                return _zusatz;
            }
            set
            {
                SetPropertyValue("Zusatz", ref _zusatz, value);
            }
        }
        
       
        [XafDisplayName("Notizen")]
        [Size(-1)]
        public System.String Notizen
        {
            get
            {
                return _notizen;
            }
            set
            {
                SetPropertyValue("Notizen", ref _notizen, value);
            }
        }
        [XafDisplayName("Anrede")]
        public boAnrede Anrede
        {
            get
            {
                return _anrede;
            }
            set
            {
                SetPropertyValue("Anrede", ref _anrede, value);
            }
        }

        
        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var ort = string.Empty;
                if(this.firmenname != null)
                {
                    bezeichnung = this.firmenname;
                }
                else
                {
                    bezeichnung = string.Format("{0} - {1}", this.vorname, this.nachname);
                }

                ort = (this.ort != null) ? this.ort.Name : string.Empty;

                retVal = String.Format("{0} ({1})", bezeichnung,ort);
                return retVal;
            }
        }
        

        [XafDisplayName("Anzeigename")]
        public String Anzeigename
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var ort = string.Empty;
                if (this.firmenname != null)
                {
                    bezeichnung = this.firmenname;
                }
                else
                {
                    bezeichnung = string.Format("{0} {1}", this.vorname, this.nachname);
                }

               

                retVal = String.Format("{0}", bezeichnung);
                return retVal;
            }
        }


        public String FullName
        {
            get
            {
                var vorname = string.Empty;
                var nachname = string.Empty;
                var firmenname = string.Empty;
                var retVal = string.Empty;
                if(this.firmenname != null)
                {
                    retVal = this.firmenname;
                }
                else
                    {
                    vorname = (this.vorname !=null)?this.vorname:string.Empty;
                    nachname = (this.nachname != null) ? this.nachname : string.Empty;
                    retVal = String.Format("{0} {1}", vorname, nachname);

                }
                return retVal;
            }
        }

       

        [XafDisplayName("Kontakte")]
        [Association("boAdresse-boKontakt"), DevExpress.Xpo.Aggregated]
        public XPCollection<boKontakt> Kontakt
        {
            get
            {
                return GetCollection<boKontakt>("Kontakt");
            }
        }


        [XafDisplayName("Fremdsystem ID")]
        public System.String FremdsystemId
        {
            get
            {
                return _fremdsystemId;
            }
            set
            {
                SetPropertyValue("FremdsystemId", ref _fremdsystemId, value);
            }
        }

        [XafDisplayName("Fremdsystem-Code")]
        public System.String FremdsystemCode
        {
            get
            {
                return _fremdsystemCode;
            }
            set
            {
                SetPropertyValue("FremdsystemCode", ref _fremdsystemCode, value);
            }
        }

        public string firmenname
        {
            get
            {
                return _firmenname;
                
            }

            set
            {
                SetPropertyValue("firmenname", ref _firmenname, value);
               
            }
        }

        public string hausnummer
        {
            get
            {
                return _hausnummer;   
            }

            set
            {
                SetPropertyValue("hausnummer", ref _hausnummer, value);
            }
        }

      
        public string nachname
        {
            get
            {
                return _nachname;
            }

            set
            {
                SetPropertyValue("nachname", ref _nachname, value);
            }
        }

        [XafDisplayName("Ort")]
        [ImmediatePostData(true)]
        public boOrt ort
        {
            get
            {
                return _ort;
            }

            set
            {
                SetPropertyValue("ort", ref _ort, value);
            }
        }

      
        [XafDisplayName("Strasse")]
        public string strasse
        {
            get
            {
                return _strasse;
            }

            set
            {
                SetPropertyValue("strasse", ref _strasse, value);
            }
        }

        [XafDisplayName("Strasse/Nr")]
        public System.String StrasseNr
        {
            get
            {
                var retVal = string.Empty;
                var strasse = string.Empty;
                var nr = string.Empty;
                strasse = (this.strasse != null) ? this.strasse : "N/A";
                nr = (this.hausnummer != null) ? this.hausnummer : "N/A";
                retVal = string.Format("{0} {1}", strasse, nr);
                return retVal;
            }
        }
        public string vorname
        {
            get
            {
                return _vorname;
            }

            set
            {
                SetPropertyValue("Vorname", ref _vorname, value);
            }
        }

        public string longitude
        {
            get
            {
                return _longitude;
            }

            set
            {
                SetPropertyValue("longitude", ref _longitude, value);
            }
        }

        public string latitude
        {
            get
            {
                return _latitude;
            }

            set
            {
                SetPropertyValue("latitude", ref _latitude, value);
            }
        }

        [Association("boAdresse-boADRKommunikation"), DevExpress.Xpo.Aggregated]
        [XafDisplayName("Kommunikation")]
        public XPCollection<boADRKommunikation> lstADRKommunikation
        {
            get
            {
                return GetCollection<boADRKommunikation>("lstADRKommunikation");
            }
        }

        
        #endregion

        #region Methoden
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //TODO: MAndantenzuordnung umbauen
            /*
            curMandantID = clsStatic.loggedOnMandantOid;
            //hier gleich den Mandanten setzen
            this.Mandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));
            */
        }

      

        protected override void OnLoaded()
        {
            base.OnLoaded();

                   // GetGeocode(this); 
              
        }

    
        public static void GetGeocode(Iadresse item)
        {
            var requestString = string.Empty;
            try {
                requestString = String.Format("{0},{1},{2}", item.strasse, item.ort.PLZ, item.ort.Name);
                //den Request initiieren
                Dictionary<string, object> result = GoogleGeoHelper.getGeoCodes(requestString);
                object lat = null;
                object lng = null;
                if (result != null)
                {
                    if (result.ContainsKey("Fehler"))
                    {

                    }
                    else
                    {
                        if (result.TryGetValue("latitude", out lat))
                        {
                            item.latitude = lat.ToString();
                        }
                        if (result.TryGetValue("longitude", out lng))
                        {
                            item.longitude = lng.ToString();
                        }
                    }
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
             
            }
        }


        protected override void OnSaved()
        {
            base.OnSaved();
            //handelt es sich uim einen Privatkontakt?
            //Kontakt erstellen????
           
        }
          
               
    public void createKontakt()
    {

        boKontakt workingKontakt = null;
        if (this.Kontakt != null)
        {
            workingKontakt = this.Kontakt.Where(t => t.Vorname == this.vorname && t.Nachname == this.nachname).FirstOrDefault();
        }
        if(this.vorname != null && this.nachname!=null)
            { 
        if (workingKontakt == null)
        {
            workingKontakt = new boKontakt(this.Session);
            workingKontakt.Anrede = (this.Anrede != null) ? this.Session.GetObjectByKey<boAnrede>(this.Anrede.Oid) : null;
            workingKontakt.Titel = (this.Titel != null) ? this.Session.GetObjectByKey<adresseTitel>(this.Titel.Oid) : null;
            workingKontakt.Nachname = this.nachname;
            workingKontakt.Vorname = this.vorname;
            

            this.Kontakt.Add(workingKontakt);
                workingKontakt.Save();

                if (this.firmenname != null)
                {

                    resetNameFields();
                }
                
            }
            }
        }

        public void resetNameFields()
        {
            this.nachname = null;
            this.vorname = null;

            boAnrede workingAnrede = this.Session.FindObject<boAnrede>(new BinaryOperator("Text", "Firma", BinaryOperatorType.Equal));
            if (workingAnrede != null)
            {
                this.Anrede = workingAnrede;
            }
            else
            {
                this.Anrede = null;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
           
                GetGeocode(this);
            
            //Vorname und Nachname mit übergeben
            //if(this.vorname != null && this.nachname != null)
            createKontakt();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            
        }

        #endregion
        [Association("boAdresse-boLiegenschaft"), DevExpress.ExpressApp.DC.Aggregated]
        [XafDisplayName("Liegenschaften")]
       
        public XPCollection<boLiegenschaft> lstLiegenschaften
        {
            get
            {
                return GetCollection<boLiegenschaft>("lstLiegenschaften");
            }
        }

        [Association("boAddresse-Debitorenkonto")]
        [XafDisplayName("Debitorenkonten")]

        public XPCollection<Debitorenkonto> lstDebitorenkonten
        {
            get
            {
                return GetCollection<Debitorenkonto>("lstDebitorenkonten");
            }
        }

        [XafDisplayName("Kreditorenkonten")]
        [Association("boAdresse-Kreditorenkonto")]
        public XPCollection<Kreditorenkonto> lstKreditorenkonten
        {
            get
            {
                return GetCollection<Kreditorenkonto>("lstKreditorenkonten");
            }
        }
       


        #region iKommunikation
        [XafDisplayName("Telefon")]
        public string Telefon1 {
            get { return _telefon1; }
            set { SetPropertyValue("Telefon1", ref _telefon1, value); }
                }
        [XafDisplayName("Telefon2")]
        public string Telefon2
        {
            get { return _telefon2; }
            set { SetPropertyValue("Telefon2", ref _telefon2, value); }
        

        }
        [XafDisplayName("Mobil")]
        public string Mobil
        { get { return _mobil; }
            set { SetPropertyValue("Mobil", ref _mobil, value); }
            }


        [XafDisplayName("Fax")]
        public string Fax
        {
            get { return _fax; }
            set { SetPropertyValue("Fax", ref _fax, value); }
            }


        [XafDisplayName("Mail")]
        public string Mail
        {
            get { return _mail; }
            set { SetPropertyValue("Mail", ref _mail, value); }
        }
        [XafDisplayName("Internet")]
        public String Internet
        {
            get { return _internet; }
            set { SetPropertyValue("Internet", ref _internet, value); }
        }

        #endregion

    }
}