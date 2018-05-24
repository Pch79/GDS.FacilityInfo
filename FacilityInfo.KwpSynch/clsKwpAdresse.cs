using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.KwpSynch
{
    class clsKwpAdresse
    {
        #region Properties
        private System.String _kredDebiFlag;
        public System.Int32 AdressArt { get; set; }
        public System.String Bukr { get; set; }
        public System.String Ident_Firma { get; set; }
        public System.String FibuNummer { get; set; }
        public double DebitKreditNr { get; set; }
        private System.String _anrede;
        public Int32 refAnrede { get; set; }
        public System.String Vorname { get; set; }
        public System.String Name1 { get; set; }
        public System.String AdrNrGes { get; set; }
        public System.String AdrNrA { get; set; }

        public System.String Name2 { get; set; }
        public System.String Name3 { get; set; }
        public System.String Name4 { get; set; }
        public System.String Strasse { get; set; }
        public System.String Plz { get; set; }
        public System.String Ortsteil { get; set; }
        public System.String Ort { get; set; }

        public Int32 refOrt { get; set; }
        public System.String Postfach { get; set; }
        public System.String PLZ_Postfach { get; set; }
        public Int32 refPostfachOrt { get; set; }
        public System.String Land { get; set; }
        public System.String Telefon { get; set; }
        public System.Int32 refTelefon { get; set; }

        public System.String Mobiltelefon { get; set; }
        public System.Int32 refMobiltelefon { get; set; }
        public System.String eMail { get; set; }
        public System.Int32 refMail { get; set; }
        public System.String Fax { get; set; }
        public System.Int32 refFax { get; set; }
        public System.String Branchenschluessel { get; set; }
        public System.Int32 Selekt1 { get; set; }
        public System.Int32 Selekt2 { get; set; }

        public System.String strSelekt1 { get; set; }
        public System.String strSelekt2 { get; set; }
        public System.String UstID { get; set; }
        public System.String Steuernummer { get; set; }
        public System.String IBAN { get; set; }
        public System.String BIC { get; set; }
        public System.Int32 refBank { get; set; }
        public System.String Mandatsreferenz { get; set; }
        public System.String Buchungssperre_allg { get; set; }
        public System.String Buchungssperre_bukrs { get; set; }
        public System.String Loeschvormerkung_allg { get; set; }
        public System.String Loeschvormerkung_bukrs { get; set; }
        public System.String Zahlwege { get; set; }
        public System.String Kreditlimit { get; set; }
        public System.String Bearbeitergruppe { get; set; }
        public System.String Risikoklasse { get; set; }
        public System.String Kreditlaufzeit { get; set; }
        public System.String Kreditsperre { get; set; }
        public System.Boolean Fehlerhaft { get; set; }
        public System.String SORTL { get; set; }
        public System.String SteuerKZ { get; set; }
        public AdressTypFI Adresstyp { get; set; }
        public System.String Objektnummer { get; set; }
        public System.Int32 Wohneinheiten { get; set; }
        public System.String Zusatz { get; set; }


        #endregion

       
        public  enum AdressTypFI
        {
            [DisplayName("Liegenschaft")]
            Liegenschaft = 1,
            [DisplayName("Hausverwalter")]
            Hausverwalter = 2,
            [DisplayName("Hausbetreuer")]
            Hausbetreuer = 3,
            [DisplayName("N/A")]
            unknown = 0
        }
        public clsKwpAdresse()
        {
            this.Adresstyp = AdressTypFI.unknown;
        }

        #region Methoden

        public System.String KredDebiFlag
        {
            get
            {
                return _kredDebiFlag;
            }
            set
            {
                this._kredDebiFlag = value;
                switch (_kredDebiFlag)
                {
                    case "D":
                        this.AdressArt = 0;
                        break;
                    case "K":
                        this.AdressArt = 1;
                        break;
                }
            }
        }

        public System.String Anrede
        {
            get
            {
                return _anrede;
            }
            set
            {
                this._anrede = Anrede;
                //die entsprechende Anrede gleich ausm KWP suchen


            }
        }
        //

        #endregion


    }
}
