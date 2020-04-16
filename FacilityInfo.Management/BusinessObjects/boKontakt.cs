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
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

namespace FacilityInfo.Adresse.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Kontakt")]
    [ImageName("vcard_16")]
    [XafDefaultProperty("Matchkey")]
    
    public class boKontakt : BaseObject,Ikommunikation
    {
        private System.String _vorname;
        private System.String _nachname;
        private boAdresse _adresse;
        private DateTime _geburtsDatum;
        private boPosition _position;
        private boAnrede _anrede;
        private String _notiz;
        private String _abteilung;
        private adresseTitel _titel;

        //Kommunikation
        private String _telefon1;
        private String _telefon2;
        private String _fax;
        private String _mobil;
        private String _mail;
        private String _internet;



        public boKontakt(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
     
               
                  

        #region Properties
          
        [XafDisplayName("Titel")]
        public adresseTitel Titel
        {
            get { return _titel; }
            set { SetPropertyValue("Titel", ref _titel, value); }
        }

        [XafDisplayName("Anzeigename")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var name = string.Empty;
                var vorname = string.Empty;
                var anrede = string.Empty;
                vorname = (this.Vorname != null) ? this.Vorname : string.Empty;
                name = (this.Nachname != null) ? this.Nachname : string.Empty;
                anrede = (this.Anrede != null) ? this.Anrede.Text : string.Empty;
                retVal = string.Format("{0} {1} {2}",anrede, vorname, name);
                return retVal;
            }
        }
        [XafDisplayName("Abteilung")]
        public String Abteilung
        {
            get
            {
                return _abteilung;
            }
            set
            {
                SetPropertyValue("Abteilung", ref _abteilung, value);
            }
        }


        [XafDisplayName("Notiz")]
        [Size(-1)]
        public String Notiz
        {
            get {
                return _notiz;
            }
            set
            {
                SetPropertyValue("Notiz", ref _notiz, value);
            }
        }


        [XafDisplayName("Mandant")]
        public boMandant Mandant
        {
            get
            {
                boMandant retVal;
                if (this.Adresse != null)
                {
                    retVal = (this.Adresse.Mandant != null) ? this.Adresse.Mandant : null;
                }
                else
                {
                    retVal = null;
                }
                return retVal;
            }
        }

        [XafDisplayName("Adresse")]
        [Association("boAdresse-boKontakt")]
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


        [XafDisplayName("Vorname")]
        [ImmediatePostData(true)]
        public System.String Vorname
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


        [XafDisplayName("Nachname")]
        [ImmediatePostData(true)]
        public System.String Nachname
        {
            get
            {
                return _nachname;
            }
            set
            {
                SetPropertyValue("Nachname", ref _nachname, value);
            }
        }

        [XafDisplayName("Geburtsdatum")]
        public System.DateTime Geburtsdatum
        {
            get
            {
                return _geburtsDatum;
            }
            set
            {
                SetPropertyValue("Geburtsdatum", ref _geburtsDatum, value);
            }
        }


        [XafDisplayName("Position")]  
        public boPosition Position
        {
            get
            {
                return _position;
            }
            set
            {
                SetPropertyValue("Position", ref _position, value);

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

        [XafDisplayName("Kommunikation")]
        [Association("boKontakt-boKontaktKommunikation")]
        public XPCollection<boKontaktKommunikation> lstKontaktKommunikation
        {
            get
            {
                return GetCollection<boKontaktKommunikation>("lstKontaktKommunikation");
            }
        }
        #region iKommunikation
        [XafDisplayName("Telefon1")]
        public string Telefon1
        {
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
        {
            get { return _mobil; }
            set { SetPropertyValue("Mobil", ref _mobil, value); }
        }



        [XafDisplayName("Fax")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
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

        #endregion
    }
}