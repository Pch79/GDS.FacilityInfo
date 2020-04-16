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
using FacilityInfo.Management.EnumStore;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Wartung.BusinessObjects;
using DevExpress.ExpressApp.Utils;
using FacilityInfo.Management.Helpers;
using FacilityInfo.Artikelverwaltung.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;

namespace FacilityInfo.Action.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Maßnahme")]
    [ImageName("helmet_16")]
    public class actionActionBase : BaseObject
    {

        //gibt es einen Bezug zu einem Wartungsplan?
        private wartungWartungsPlan _wartungsPlan; 
        //zeitliche Planung
        private DateTime _datumFaellig;
        //geplante druchführung
        private DateTime _datumGeplant;
        //private DateTime _zeitGeplant;
        //reale Durchführung
        private DateTime _datumReal;
        //private DateTime _zeitReal;

        //Wiederholung


        //beschreibung
        private String _bezeichnung;
        private String _beschreibung;

        //Status
        private enmBearbeitungsStatus _status;

        //Wiederholung


        //Personalplanung
        private Int32 _anzahlTechniker;
        private boMitarbeiter _monteurDurchfuehrung;
        private boMitarbeiter _monteurGeplant;
        private boMitarbeiter _monteurVerantwortlich;

        private enmPrioritaet _prioritaet;
        //private enmServiceTyp _serviceTyp;
        private boLiegenschaft _liegenschaft;
        private enmActionClassification _actionClassification;

        //Servicetyp -> Wartung /Reparatur

        //hier gleich den Turnus angeben
        //dieser kann auch aus einem Vertrag resultieren
        private enmTurnus _turnus;
        private Int32 _turnusValue;


        public actionActionBase(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
         
        }

        public virtual void createNewAction(actionActionBase createdAction)
        {
            //einen neuen Service erstellen

            //TODO: Funktion für einen neuen Service

            createdAction.DatumFaellig = TurnusHelper.getNextDate(this.DatumReal, this.Turnus, this.TurnusValue);
            //was passiert
            if (this.WartungsPlan.GetType() == typeof(wartungWartungsPlanAnlagenArt))
            {
                createdAction.WartungsPlan = this.Session.GetObjectByKey<wartungWartungsPlanAnlagenArt>(this.WartungsPlan.Oid);
            }

            if (this.WartungsPlan.GetType() == typeof(wartungWartungsPlanProdukt))
            {
                createdAction.WartungsPlan = this.Session.GetObjectByKey<wartungWartungsPlanProdukt>(this.WartungsPlan.Oid);
            }

            createdAction.Liegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(this.Liegenschaft.Oid);
            createdAction.ActionClassification = this.ActionClassification;
            createdAction.AnzahlTechniker = this.AnzahlTechniker;

            createdAction.Beschreibung = this.Beschreibung;

            createdAction.Bezeichnung = this.Bezeichnung;
            createdAction.Prioritaet = this.Prioritaet;
            createdAction.Status = enmBearbeitungsStatus.neu;
            createdAction.Turnus = this.Turnus;
            createdAction.TurnusValue = this.TurnusValue;
            if (this.MonteurVerantwortlich != null)
            {
                createdAction.MonteurVerantwortlich = this.Session.GetObjectByKey<boMitarbeiter>(this.MonteurVerantwortlich.Oid);
            }
  
        


            createdAction.Save();

            //jetzt noch die Positionen übernehmen
            if(this.lstActionPosition != null)
            {
                actionActionPosition curActionPosition;
                actionActionPosition basePosition;
                for (int j = 0; j < this.lstActionPosition.Count; j++)
                {
                    basePosition = this.lstActionPosition[j];
                        curActionPosition = new actionActionPosition(this.Session);
                    curActionPosition.ActionBase = createdAction;
                        curActionPosition.WartungsPosition = this.Session.GetObjectByKey<wartungWartungsPosition>(basePosition.Oid);
                        curActionPosition.PositionsNummer = basePosition.PositionsNummer;
                        curActionPosition.PosLangText = basePosition.PosLangText;
                        curActionPosition.PosText = basePosition.PosText;
                        curActionPosition.SortIndex = basePosition.SortIndex;
                        curActionPosition.ZeitVorgabe = basePosition.ZeitVorgabe;
                        curActionPosition.AnzahlTechniker = basePosition.AnzahlTechniker;
                        curActionPosition.ArbeitsAnweisung = basePosition.ArbeitsAnweisung;
                        if (basePosition.Artikel != null)
                        {
                            curActionPosition.Artikel = this.Session.GetObjectByKey<artikelArtikelBase>(basePosition.Artikel.Oid);
                            curActionPosition.ArtikelMenge = basePosition.ArtikelMenge;
                        }
                        if (basePosition.Bauteil != null)
                        {
                            curActionPosition.Bauteil = this.Session.GetObjectByKey<fiBauteil>(basePosition.Bauteil.Oid);
                            curActionPosition.BauteilAnzahl = basePosition.BauteilAnzahl;
                        }
                        curActionPosition.Beschreibung = basePosition.Beschreibung;
                        curActionPosition.Notizen = basePosition.Notizen;
                        curActionPosition.Save();

                    
                }
            }
            this.Session.CommitTransaction();

        }

        #region Properties
        //Fällig
        [XafDisplayName("Jahr (Fällig)")]
        public Int32 JahrFaellig
        {
            get { return this.DatumFaellig.Year; }
        }
        [XafDisplayName("Monat (Fällig)")]
        public Int32 MonatFaellig
        {
            get
            {
                return this.DatumFaellig.Month;
            }
        }

        //geplant
        [XafDisplayName("Jahr (geplant")]
        public Int32 JahrGeplant
        {
            get
            {
                return this.DatumGeplant.Year;
            }
        }
        [XafDisplayName("Monat (geplant")]
        public Int32 MonatGeplant
        {
            get
            {
                return this.DatumGeplant.Month;
            }
        }
            
        //geplant
        [XafDisplayName("Anzahl Techniker")]
        public Int32 AnzahlTechniker
        {
            get { return _anzahlTechniker; }
            set { SetPropertyValue("AnzahlTechniker", ref _anzahlTechniker, value); }
        }


        [XafDisplayName("WartungsPlan")]
        public wartungWartungsPlan WartungsPlan
        {
            get { return _wartungsPlan; }
            set { SetPropertyValue("WartungsPlan", ref _wartungsPlan, value); }
        }
        [XafDisplayName("Serviceart")]
        public enmActionClassification ActionClassification
        {
            get { return _actionClassification; }
            set { SetPropertyValue("ActionClassification", ref _actionClassification, value); }
        }
        [XafDisplayName("Fällig")]
        public DateTime DatumFaellig
        {
            get { return _datumFaellig; }
            set { SetPropertyValue("DatumFaellig", ref _datumFaellig, value); }
        }

        [XafDisplayName("Turnus")]
        public enmTurnus Turnus
        {
            get { return _turnus; }
            set { SetPropertyValue("Turnus", ref _turnus, value); }
        }
        [XafDisplayName("Turnuswert")]
        public Int32 TurnusValue
        {
            get { return _turnusValue; }
            set { SetPropertyValue("TurnusValue", ref _turnusValue, value); }
        }

        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-actionActionBase")]
        public boLiegenschaft Liegenschaft
        {
            get { return _liegenschaft; }
            set { SetPropertyValue("Liegenschaft", ref _liegenschaft, value); }
        }
        

        [XafDisplayName("Priorität")]
        [ImmediatePostData(true)]
        public enmPrioritaet Prioritaet
        {
            get
            {
                return _prioritaet;
            }
            set
            {
                SetPropertyValue("Prioritaet", ref _prioritaet, value);
            }
        }


        [XafDisplayName("Verantwortlicher")]
        public boMitarbeiter MonteurVerantwortlich
        {
            get
            {
                return _monteurVerantwortlich;
            }
            set
            {
                SetPropertyValue("MonteurVerantwortlich", ref _monteurVerantwortlich, value);
            }
        }
        [XafDisplayName("Durchführender (geplant)")]
        public boMitarbeiter MonteurGeplant
        {
            get
            {
                return _monteurGeplant;
            }
            set
            {
                SetPropertyValue("MonteurGeplant", ref _monteurGeplant, value);
            }
        }
        [XafDisplayName("Durchführender (real)")]
        public boMitarbeiter MonteurDurchfuehrung
        {
            get
            {
                return _monteurDurchfuehrung;
            }
            set
            {
                SetPropertyValue("MonteurDurchfuehrung", ref _monteurDurchfuehrung, value);
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
        [XafDisplayName("Bezeichnung")]
        public String Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }
            set
            {
                SetPropertyValue("Bezeichnung", ref _bezeichnung, value);
            }
        }
        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public String Beschreibung
        {
            get
            {
                return _beschreibung;
            }
            set
            {
                SetPropertyValue("Beschreibung", ref _beschreibung, value);
            }
        }

        [XafDisplayName("Plandatum")]
        public DateTime DatumGeplant
        {
            get
            {
                return _datumGeplant;
            }
            set
            {
                SetPropertyValue("DatumGeplant", ref _datumGeplant, value);
            }
        }

        [XafDisplayName("Durchführungsdatum")]
        public DateTime DatumReal
        {
            get
            {
                return _datumReal;
            }
            set
            {
                SetPropertyValue("DatumReal", ref _datumReal, value);
            }
        }


        [XafDisplayName("Positionen")]
        [Association("actionActionBase-actionActionPosition")]
        public XPCollection<actionActionPosition> lstActionPosition
        {
            get { return GetCollection<actionActionPosition>("lstActionPosition"); }
        }

        [XafDisplayName("Typ")]
        [ValueConverter(typeof(TypeToStringConverter))]
        public Type SystemType
        {
            get
            {
                return this.GetType();
            }
        }

        #endregion

    }
}