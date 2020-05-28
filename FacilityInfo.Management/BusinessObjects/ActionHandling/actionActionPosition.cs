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
using FacilityInfo.Artikelverwaltung.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Action.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("progressbar_16")]
    [XafDisplayName("Position (Maßnahme)")]
    [XafDefaultProperty("Matchkey")]
    public class actionActionPosition : BaseObject
    {
   
        private System.String _beschreibung;
        private Int32 _anzahlTechniker;
        private String _notizen;
        private Int32 _positionsNummer;
        private String _posText;
        private String _posLangText;
        private Decimal _zeitVorgabe;

        private actionActionBase _actionBase;
  
        private Decimal _dauerNominell;

        //hier muss das bauteil noch eingebaut werden
        //und der Bezug zur Wartungsposition
       // private wartungWartungsPosition _wartungsPosition;
        private Int32 _sortIndex;

        //Bauteile die bei dieser Wartung benötigt werden
        private fiBauteil _bauteil;
        private Int32 _bauteilAnzahl;

        //Artikel mit einbauen
        private artikelArtikelBase _artikel;
        private Decimal _artikelMenge;

        private String _arbeitsAnweisung;

        //Status
        private enmBearbeitungsStatus _status;
        public actionActionPosition(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            this.Status = enmBearbeitungsStatus.neu;
        }
        #region Properties
        [XafDisplayName("Matchkey")]
        public String MatchKey
        {
            get
            {
                var retVal = string.Empty;
                var bezeichnung = string.Empty;
                var posNummer = string.Empty;
                bezeichnung = (this.PosText != null)?this.PosText:"n.a.";
                posNummer = this.PositionsNummer.ToString();
                retVal = string.Format("{0} - {1}", posNummer, bezeichnung);
                return retVal;
            }
        }

        [XafDisplayName("Status")]
        public enmBearbeitungsStatus Status
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
        /*
        [XafDisplayName("Wartungsposition")]
        public wartungWartungsPosition WartungsPosition
        {
            get
            {
                return _wartungsPosition;
            }
            set { SetPropertyValue("WartungsPosition", ref _wartungsPosition, value); }
        }
        */
        [XafDisplayName("Artikelmenge")]
        public decimal ArtikelMenge
        {
            get
            {
                return _artikelMenge;

            }
            set
            {
                SetPropertyValue("ArtikelMenge", ref _artikelMenge, value);
            }
        }
        [XafDisplayName("Serviceartikel")]
        public artikelArtikelBase Artikel
        {
            get
            {
                return _artikel;
            }
            set { SetPropertyValue("Artikel", ref _artikel, value); }
        }
        [XafDisplayName("Bauteilanzahl")]
        public Int32 BauteilAnzahl
        {
            get
            {
                return _bauteilAnzahl;
            }
            set
            {
                SetPropertyValue("BauteilAnzahl", ref _bauteilAnzahl, value);
            }
        }
        [XafDisplayName("Bauteil")]
        [DataSourceProperty("lstAvailableBauteile")]
        public fiBauteil Bauteil
        {
            get { return _bauteil; }
            set { SetPropertyValue("Bauteil", ref _bauteil, value); }
        }

        private List<fiBauteil> lstAvailableBauteile
        {
            get { return getAvailableBauteile(); }
        }
        public List<fiBauteil> getAvailableBauteile()
        {
            //für die bauteile muss ich auf d
            List<fiBauteil> lstBauteile = new List<fiBauteil>();
            if (this.ActionBase != null)
            {
                if (this.ActionBase.GetType() == typeof(actionActionAnlage))
                {
                    actionActionAnlage curAction  = (actionActionAnlage)this.ActionBase;
                    if(curAction.Anlage != null)
                    {
                        if(curAction.Anlage.Typ != null)
                        {
                            if (curAction.Anlage.Typ.lstBauteile != null)
                            {
                                lstBauteile.AddRange(curAction.Anlage.Typ.lstBauteile);
                            }
                        }
                                        }
                }
            }
            return lstBauteile;
        }

        [XafDisplayName("Arbeitsanweisung")]
        public String ArbeitsAnweisung
        {
            get { return _arbeitsAnweisung; }
            set { SetPropertyValue("ArbeitsAnweisung", ref _arbeitsAnweisung, value); }
        }
        [XafDisplayName("Nummer")]
        public Int32 PositionsNummer
        {
            get { return _positionsNummer; }
            set { SetPropertyValue("PositionsNummer", ref _positionsNummer, value); }
        }
        [XafDisplayName("SortIndex")]
        public Int32 SortIndex
        {
            get { return _sortIndex; }
            set { SetPropertyValue("SortIndex", ref _sortIndex, value); }
        }
        [XafDisplayName("Zeitvorgabe")]
        public Decimal ZeitVorgabe
        {
            get { return _zeitVorgabe; }
            set { SetPropertyValue("ZeitVorgabe", ref _zeitVorgabe, value); }
        }
        /*
        [XafDisplayName("Dauer geplant")]
        public Decimal DauerGeplant
        {
            get { return _dauerGeplant; }
            set { SetPropertyValue("DauerGeplant", ref _dauerGeplant, value); }
        }
        */
        [XafDisplayName("Anzahl Techniker")]
        public Int32 AnzahlTechniker
        {
            get { return _anzahlTechniker; }
            set { SetPropertyValue("AnzahlTechniker", ref _anzahlTechniker, value); }
        }
        [XafDisplayName("Notizen")]
        [Size(-1)]
        public String Notizen
        {
            get { return _notizen; }
            set { SetPropertyValue("Notizen", ref _notizen, value); }
        }

        [XafDisplayName("Wartung")]
        [Association("actionActionBase-actionActionPosition")]
        public actionActionBase ActionBase
        {
            get { return _actionBase; }
            set { SetPropertyValue("ActionBase", ref _actionBase, value); }
        }


        [XafDisplayName("Positionstext")]
        public String PosText
        {
            get { return _posText; }
            set { SetPropertyValue("PosText", ref _posText, value); }
        }

        [XafDisplayName("Langtext")]
        [Size(-1)]
        public String PosLangText
        {
            get
            {
                return _posLangText;
            }
            set { SetPropertyValue("PosLangText", ref _posLangText, value); }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
        {
            get { return _beschreibung; }
            set { SetPropertyValue("Beschreibung", ref _beschreibung, value); }
        }

        #endregion


    }
}