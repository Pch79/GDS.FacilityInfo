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
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.Artikelverwaltung.BusinessObjects;

namespace FacilityInfo.Management.BusinessObjects.ServiceHandling
{
    [DefaultClassOptions]
    [XafDisplayName("Wartungsposition")]
    [ImageName("gears1_16")]
    [XafDefaultProperty("MatchKey")]

    public class MaintenancePosition : BaseObject
    {
        private String _posText;
        private String _posLangText;
        private Decimal _zeitVorgabe;
       
        private Int32 _anzahlTechniker;
      
        private Int32 _positionsNummer;
        
        private String _arbeitsAnweisung;

        //TODO: müssen spezielle Werkzeuge mitgenommen werden????
        private MaintenanceSchedule _maintenanceSchedule;
        private Int32 _sortIndex;

        //Bauteile dioe bei dieser Wartung benötigt werden
        private fiBauteil _bauteil;
        private Int32 _bauteilAnzahl;

        //Artikel mit einbauen
        private artikelArtikelBase _artikel;
        private Decimal _artikelMenge;




        public MaintenancePosition(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "WartungsPlan":
                    int posCount = 0;
                    //Anzahl bestimmen und dann eins hochzählen
                    if((MaintenanceSchedule)newValue != null)
                    {
                        var curWartung = (MaintenanceSchedule)newValue;
                        if (curWartung.lstMaintenancePosition != null)
                        {
                            posCount = curWartung.lstMaintenancePosition.Count();
                            posCount++;
                        }
                        else
                        {
                            posCount = 0;
                        }
                        //die Anzahl Techniker gleich mitübernehmen
                        this.AnzahlTechniker = this.MaintenanceSchedule.AnzahlTechniker;
                    }
                    else
                    {
                       posCount = 0;
                    }

                    this.SortIndex = posCount;
                    this.PositionsNummer = posCount;
                   
                    break;
            }
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
                bezeichnung = (this.PosText != null) ? this.PosText : "n.a.";
                posNummer = this.PositionsNummer.ToString();
                retVal = string.Format("{0} - {1}", posNummer, bezeichnung);
                return retVal;
            }
        }
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
            List<fiBauteil> lstBauteile = new List<fiBauteil>();
            if (this.MaintenanceSchedule != null)
            {
                if (this.MaintenanceSchedule.GetType() == typeof(ComponentMaintenance))
                {
                    ComponentMaintenance curPlan = (ComponentMaintenance)this.MaintenanceSchedule;
                    if (curPlan.HerstellerProdukt != null)
                    {
                        if (curPlan.HerstellerProdukt.lstBauteile != null)
                        {
                            lstBauteile.AddRange(curPlan.HerstellerProdukt.lstBauteile);
                        }

                    }
                }
            }
            return lstBauteile;
        }
        

        [XafDisplayName("Arbeitsanweisung")]
        [Size(-1)]
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
         
        [XafDisplayName("Wartung")]
        [Association("MaintenanceSchedule-MaintenancePosition")]
        public MaintenanceSchedule MaintenanceSchedule
        {
            get { return _maintenanceSchedule; }
            set { SetPropertyValue("MaintenanceSchedule", ref _maintenanceSchedule, value); }
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
      
        #endregion
    }
}