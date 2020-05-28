using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using FacilityInfo.Action.BusinessObjects;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Artikelverwaltung.BusinessObjects;
using FacilityInfo.Bildverarbeitung.BusinessObjects;
using FacilityInfo.Building.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;

using FacilityInfo.DMS.BusinessObjects;
using FacilityInfo.Hersteller.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management.EnumStore;
using FacilityInfo.Management.Helpers;
using FacilityInfo.Messung.BusinessObjects;
using FacilityInfo.Parameter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace FacilityInfo.Anlagen.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Anlage")]
    [ImageName("centos_16")]
    [XafDefaultProperty("AnlagenNummer")]

    public class boAnlage : BaseObject ,ITreeNode,ITreeNodeImageProvider
    {


        private System.String _bezeichnung;
        private System.String _anlagennummer;
        private System.String _anlagencode;
        //private System.String _ansprechcode;
        private boAdresse _anlagenAdresse;
        private boAdresse _rechnungsadresse;
        private System.String _beschreibung;

        private boMandant _mandant;
        private boAnlage _parentAnlage;
        private System.String _fremdsystemId;

        //Klassifizeirung der Anlage
        private boAnlagenKategorie _anlagenKategorie;
        private boAnlagenArt _anlagenArt;

        private boLiegenschaft _liegenschaft;
        //Zustand und Status
        private enmAnlagenStatus _anlagenstatus;
        private enmZustand _zustand;
        private System.String _seriennummer;
        //technische Daten global
        private Int32 _baujahr;
        private System.DateTime _einbaudatum;
        private System.String _typbezeichnung;
        private fiHerstellerProdukt _typ;
        private boHersteller _hersteller;
        private boAdresse _anlagenbetreuer;
        private System.String _notiz;

        private LgHaustechnikKomponente _lghaustechnikkomponente;


        //Standort
        private fiGebaeude _gebaeude;
        private fiEbene _ebene;
        private fiRaum _raum;

        //einfache Standortimplementierung

        private fiGebaeude _building;
        private fiRaumart _roomType;
        private String _roomDesignation;

        private String _floorDesignation;
        private fiEbenenart _floorType;

        public boAnlage(Session session)
            : base(session)
        {
           
        }



        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
            this.Anlagenstatus = enmAnlagenStatus.Aktiv;

            this.Mandant = getMandantByUser(SecuritySystem.CurrentUserName);
            createAnlagenCode();
            //TODO: MAndantenzuordnung umbauen - > Erstellung des Anlagencodes hängt daran !!!!!
            /*
            curMandantID = clsStatic.loggedOnMandantOid;
            this.Mandant = this.Session.FindObject<boMandant>(new BinaryOperator("Oid", curMandantID, BinaryOperatorType.Equal));

            createAnlagenCode();
            */

        }
        public boMandant getMandantByUser(string userName)
        {
            boMandant retVal = null;
            PermissionPolicyUser curUser = this.Session.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", userName, BinaryOperatorType.Equal));

            corePortalAccount curPortalAccount = this.Session.FindObject<corePortalAccount>(new BinaryOperator("SystemBenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));
            if (curPortalAccount != null)
            {
                //den hausverwalter rausfinden
                boHausverwalter curHausverwalter = this.Session.FindObject<boHausverwalter>(new BinaryOperator("Oid", curPortalAccount.HausVerwalter.Oid, BinaryOperatorType.Equal));
                retVal = curHausverwalter.Mandant;
            }

            boMitarbeiter curMitarbeiter = this.Session.FindObject<boMitarbeiter>(new BinaryOperator("Systembenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));

            if (curMitarbeiter != null)
            {
                retVal = curMitarbeiter.Mandant;
            }

            if (retVal == null)
            {
                boMandant curMandant = this.Session.FindObject<boMandant>(new BinaryOperator("IsDefault", "true", BinaryOperatorType.Equal));
                retVal = curMandant;
            }


            return retVal;
        }
        /*
        public void checkAnlagenActions()
        {
            //TODO die benötigten Maßnahmen laden;
            //Maßnahmen der Anlagenart
            wartungWartungsPlanAnlagenArt curWartungAnlage;
            wartungWartungsPlanProdukt curWartungProdukt;
            actionActionAnlage curActionAnlage;
            wartungWartungsPosition curWartungsPosition;
            actionActionPosition curActionPosition;
            if(this.AnlagenArt.lstWartungsPlan != null)
            {
                for(int i=0;i<this.AnlagenArt.lstWartungsPlan.Count;i++)
                {
                    curWartungAnlage = (wartungWartungsPlanAnlagenArt)this.AnlagenArt.lstWartungsPlan[i];
                    //gibt es hierzu eine Action?? 
                    //wenn nicht muss diese angelegt werden
                    curActionAnlage = this.lstActionAnlage.Where(t => t.WartungsPlan.Oid == curWartungAnlage.Oid).FirstOrDefault();
                    if(curActionAnlage == null)
                    {
                        //create
                        curActionAnlage = new actionActionAnlage(this.Session);
                        curActionAnlage.Anlage = this;
                        curActionAnlage.WartungsPlan = this.Session.GetObjectByKey<wartungWartungsPlanAnlagenArt>(curWartungAnlage.Oid);
                        curActionAnlage.Liegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(this.Liegenschaft.Oid);
                        curActionAnlage.AnzahlTechniker = curWartungAnlage.AnzahlTechniker;
                        curActionAnlage.Beschreibung = curWartungAnlage.Beschreibung;
                        curActionAnlage.Bezeichnung = curWartungAnlage.Bezeichnung;
                        curActionAnlage.ActionClassification = enmActionClassification.Wartung;
                        curActionAnlage.Prioritaet = enmPrioritaet.normal;
                        curActionAnlage.Status = enmBearbeitungsStatus.neu;
                        curActionAnlage.Turnus = curWartungAnlage.Turnus;
                        curActionAnlage.TurnusValue = curWartungAnlage.TurnusValue;
                        curActionAnlage.Save();
                       
                     
                        if(curWartungAnlage.lstWartungPosition != null)
                        {
                            for(int j=0;j<curWartungAnlage.lstWartungPosition.Count;j++)
                            {
                                curWartungsPosition = (wartungWartungsPosition)curWartungAnlage.lstWartungPosition[j];
                                if(curWartungsPosition != null)
                                {
                                    curActionPosition = new actionActionPosition(this.Session);
                                    curActionPosition.ActionBase = curActionAnlage;
                                    curActionPosition.WartungsPosition = this.Session.GetObjectByKey<wartungWartungsPosition>(curWartungsPosition.Oid);
                                    curActionPosition.PositionsNummer = curWartungsPosition.PositionsNummer;
                                    curActionPosition.PosLangText = curWartungsPosition.PosLangText;
                                    curActionPosition.PosText = curWartungsPosition.PosText;
                                    curActionPosition.SortIndex = curWartungsPosition.SortIndex;
                                    curActionPosition.ZeitVorgabe = curWartungsPosition.ZeitVorgabe;
                                    curActionPosition.AnzahlTechniker = curWartungsPosition.AnzahlTechniker;
                                    curActionPosition.ArbeitsAnweisung = curWartungsPosition.ArbeitsAnweisung;
                                    if(curWartungsPosition.Artikel != null)
                                    {
                                        curActionPosition.Artikel = this.Session.GetObjectByKey<artikelArtikelBase>(curWartungsPosition.Artikel.Oid);
                                        curActionPosition.ArtikelMenge = curWartungsPosition.ArtikelMenge;
                                    }
                                    if(curWartungsPosition.Bauteil != null)
                                    {
                                        curActionPosition.Bauteil = this.Session.GetObjectByKey<fiBauteil>(curWartungsPosition.Bauteil.Oid);
                                        curActionPosition.BauteilAnzahl = curWartungsPosition.BauteilAnzahl;
                                    }
                                    curActionPosition.Beschreibung = curWartungsPosition.Beschreibung;
                                    curActionPosition.Notizen = curWartungsPosition.Notizen;
                                    curActionPosition.Save();
                                   
                                }
                            }
                        }
                        
                    }
                    this.Session.CommitTransaction();
                }
            }

         
           
           
        }
        */

        /*
        public void checkProduktActions()
        {
            //TODO die benötigten Maßnahmen laden;
            //Maßnahmen der Anlagenart
            
            wartungWartungsPlanProdukt curWartungProdukt;
            actionActionAnlage curActionAnlage;
            wartungWartungsPosition curWartungsPosition;
            actionActionPosition curActionPosition;

            //hab ich einen Hersteller??
            if (this.Typ != null)
            {
                if (this.Typ.lstWartungsPlan != null)
                {
                    for (int i = 0; i < this.Typ.lstWartungsPlan.Count; i++)
                    {
                        curWartungProdukt = (wartungWartungsPlanProdukt)this.Typ.lstWartungsPlan[i];
                        //gibt es hierzu eine Action?? 
                        //wenn nicht muss diese angelegt werden
                        curActionAnlage = this.lstActionAnlage.Where(t => t.WartungsPlan.Oid == curWartungProdukt.Oid).FirstOrDefault();
                        if (curActionAnlage == null)
                        {
                            //create
                            curActionAnlage = new actionActionAnlage(this.Session);
                            curActionAnlage.Anlage = this;
                            curActionAnlage.WartungsPlan = this.Session.GetObjectByKey<wartungWartungsPlanProdukt>(curWartungProdukt.Oid);
                            curActionAnlage.Liegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(this.Liegenschaft.Oid);
                            curActionAnlage.AnzahlTechniker = curWartungProdukt.AnzahlTechniker;
                            curActionAnlage.Beschreibung = curWartungProdukt.Beschreibung;
                            curActionAnlage.Bezeichnung = curWartungProdukt.Bezeichnung;
                            curActionAnlage.ActionClassification = enmActionClassification.Wartung;
                            curActionAnlage.Prioritaet = enmPrioritaet.normal;
                            curActionAnlage.Status = enmBearbeitungsStatus.neu;
                            curActionAnlage.Turnus = curWartungProdukt.Turnus;
                            curActionAnlage.TurnusValue = curWartungProdukt.TurnusValue;
                            curActionAnlage.Save();

                            //TODO: die Positionen noch verarbeiten
                            if (curWartungProdukt.lstWartungPosition != null)
                            {
                                for (int j = 0; j < curWartungProdukt.lstWartungPosition.Count; j++)
                                {
                                    curWartungsPosition = (wartungWartungsPosition)curWartungProdukt.lstWartungPosition[j];
                                    if (curWartungsPosition != null)
                                    {
                                        curActionPosition = new actionActionPosition(this.Session);
                                        curActionPosition.ActionBase = curActionAnlage;
                                        curActionPosition.WartungsPosition = this.Session.GetObjectByKey<wartungWartungsPosition>(curWartungsPosition.Oid);
                                        curActionPosition.PositionsNummer = curWartungsPosition.PositionsNummer;
                                        curActionPosition.PosLangText = curWartungsPosition.PosLangText;
                                        curActionPosition.PosText = curWartungsPosition.PosText;
                                        curActionPosition.SortIndex = curWartungsPosition.SortIndex;
                                        curActionPosition.ZeitVorgabe = curWartungsPosition.ZeitVorgabe;
                                        curActionPosition.AnzahlTechniker = curWartungsPosition.AnzahlTechniker;
                                        curActionPosition.ArbeitsAnweisung = curWartungsPosition.ArbeitsAnweisung;
                                        if (curWartungsPosition.Artikel != null)
                                        {
                                            curActionPosition.Artikel = this.Session.GetObjectByKey<artikelArtikelBase>(curWartungsPosition.Artikel.Oid);
                                            curActionPosition.ArtikelMenge = curWartungsPosition.ArtikelMenge;
                                        }
                                        if (curWartungsPosition.Bauteil != null)
                                        {
                                            curActionPosition.Bauteil = this.Session.GetObjectByKey<fiBauteil>(curWartungsPosition.Bauteil.Oid);
                                            curActionPosition.BauteilAnzahl = curWartungsPosition.BauteilAnzahl;
                                        }
                                        curActionPosition.Beschreibung = curWartungsPosition.Beschreibung;
                                        curActionPosition.Notizen = curWartungsPosition.Notizen;
                                        curActionPosition.Save();

                                    }
                                }
                            }

                        }
                       // this.Session.CommitTransaction();
                    }
                }
            }
        }
        */

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.Session.IsObjectToDelete(this))
            {
                //wenn es sich 
                switch (propertyName)
                {
                    case "Liegenschaft":

                        boLiegenschaft workingLg = null;
                        if (newValue != null)
                        {
                           workingLg = ((boLiegenschaft)newValue);
                        }
                        if (this.Rechnungsadresse == null)
                        {
                            if (newValue != null)
                            {

                                setRechnungsAdresse(((boLiegenschaft)newValue).Liegenschaftsadresse);
                            }
                        }

                        if (this.AnlagenAdresse == null)
                        {
                            if (newValue != null)
                            {
                                setAnlagenAdresse(((boLiegenschaft)newValue).Liegenschaftsadresse);
                            }
                        }

                        //TODO: hier auch den Mandanten noch prüfen
                     
                        if(workingLg != null)

                        {
                            this.Mandant = Session.GetObjectByKey<boMandant>(workingLg.Mandant.Oid);
                            this.Save();
                        }                       
                        break;
                    case "AnlagenArt":
                
                        deleteAnlagenparameter();

                        //die parameter löschen

                        if (newValue != null)
                        {
                            boAnlagenArt curAnlagenArt = (boAnlagenArt)newValue;
                            this.AnlagenKategorie = ((boAnlagenArt)newValue).AnlagenKategorie;
                            this.AnlagenNummer = completeNumber();
                                                    

                            //die Anlagenparameter schreiben
                            generateAnlagenparameter((boAnlagenArt)newValue);
                            //wenn sich die Anlagenart ändert ändert sich dann auch der Anlagentyp???
                            // completeNumber();
                            //auch hier die Wartungen noch schreiben
                          //  List<actionActionAnlage> lstActionsToDelete = this.lstActionAnlage.Where(t => t.WartungsPlan.GetType() == typeof(wartungWartungsPlanAnlagenArt)).ToList();
                           
                            /*
                            if (lstActionsToDelete != null)
                            {
                                this.Session.Delete(lstActionsToDelete);
                                this.Save();
                                this.Session.CommitTransaction();
                            }
                            */

                           // checkAnlagenActions();
                        }
                        break;

                    case "Hersteller":
                       
                        this.Typ = null;
                       // GetAvailableProducts();
                        break;

                    //wenn sich der Typ ändert dann die Felder anschreiben
                    case "Typ":
                        if (!this.Session.IsObjectsSaving)
                        {
                            var lstResult = this.lstAnlagenParameter.Where(t => t.OriginType == typeof(parameterHerstellerProduktParameter)).ToList().FirstOrDefault();
                        if (lstResult != null)
                        {
                            this.Session.Delete(lstResult);
                            this.Save();
                            this.Session.CommitTransaction();
                        }



                        if (newValue != null)
                        {
                            fiHerstellerProdukt curProdukt = (fiHerstellerProdukt)newValue;
                                if (curProdukt.Hersteller != null)
                                {
                                    this.Hersteller = this.Session.GetObjectByKey<boHersteller>(curProdukt.Hersteller.Oid);
                                }
                            generateAnlagenparameter(curProdukt);

                                //die Maßnahmen updaten
                                //alle Maßnahmen aus der vorigen Zuorndung löschen
                                /*
                                List<actionActionAnlage> lstActionsToDelete = this.lstActionAnlage.Where(t => t.WartungsPlan != null && t.WartungsPlan.GetType() == typeof(wartungWartungsPlanProdukt)).ToList();
                                if (lstActionsToDelete != null)
                                        {
                                            this.Session.Delete(lstActionsToDelete);
                                            this.Save();
                                            this.Session.CommitTransaction();
                                        }
                                */
                                
                              //  checkProduktActions();
                        }
                            this.Session.CommitTransaction();
                        

                        
                }
                break;
                    case "Mainimage":
                        if (!this.IsLoading)
                        {
                            if (newValue != null)
                            {
                                setMainThumbnail();
                                //hier kan ich auch gleich die Web-Implementierung erstellen
                                setMainImageWeb();
                            }
                            if (newValue == null)
                            {
                                this.MainImageThumb = null;
                                this.MainImageWeb = null;
                            }
                        }
                        break;
                    case "Mandant":
                        //wenn sich der Mandant ändert muss die Anlagennummer neu vergeben werden
                        if(newValue != null)
                        {
                            if (!this.IsLoading)
                            {
                                if (!this.Session.IsNewObject(this))
                                {
                                    createAnlagenCode();
                                }
                            }
                            }
                        break;
                    
                }
            }         
        }

      

        public void deleteAnlagenparameter()
        {
            if (this.lstAnlagenParameter != null)
            {
                this.Session.Delete(this.lstAnlagenParameter);
                this.Save();
               // this.Session.CommitTransaction();
            }
        }

        public void generateAnlagenparameter(boAnlagenArt curAnlagenArt)
        {
            
            if(curAnlagenArt.lstParameterItem!= null && curAnlagenArt.lstParameterItem.Count >0)
            { 
                for(int i=0;i<curAnlagenArt.lstParameterItem.Count;i++)
                {
                    parameterAnlagenArtParam paramToAdd = this.Session.GetObjectByKey<parameterAnlagenArtParam>(curAnlagenArt.lstParameterItem[i].Oid);

                    if (this.lstAnlagenParameter.Where(t => t.ParameterItem.ParamKey == paramToAdd.ParameterDefinition.ParamKey).Count() <= 0)
                    {
                        parameterAnlagenParameter curParam = new parameterAnlagenParameter(this.Session);
                        curParam.OriginType = paramToAdd.GetType();
                        curParam.ParameterItem = paramToAdd.ParameterDefinition;
                        curParam.DefaultValue = paramToAdd.DefaultValue;
                        curParam.Value = paramToAdd.DefaultValue;
                        curParam.Save();
                        this.lstAnlagenParameter.Add(curParam);
                        this.Save();
                        //this.Session.CommitTransaction();
                        //TODO hier die Verbindung zu den Herstellerprodukten schaffen
                   }
                    }
            }
        }


        

        public void generateAnlagenparameter(fiHerstellerProdukt curAnlagenTyp)
        {

            if (curAnlagenTyp.lstProduktParameter != null && curAnlagenTyp.lstProduktParameter.Count > 0)
            {
                for (int i = 0; i < curAnlagenTyp.lstProduktParameter.Count; i++)
                {
                    parameterHerstellerProduktParameter paramToAdd = this.Session.GetObjectByKey<parameterHerstellerProduktParameter>(curAnlagenTyp.lstProduktParameter[i].Oid);

                    if (this.lstAnlagenParameter.Where(t => t.ParameterItem.ParamKey == paramToAdd.ParameterItem.ParamKey).Count() <= 0)
                    {
                        parameterAnlagenParameter curParam = new parameterAnlagenParameter(this.Session);

                  
                        curParam.OriginType = paramToAdd.GetType();
                        curParam.ParameterItem = paramToAdd.ParameterItem;
                        curParam.DefaultValue = paramToAdd.DefaultValue;
                        curParam.Value = paramToAdd.DefaultValue;
                        curParam.MaxAllowedValue = paramToAdd.MaxAllowedValue;
                        curParam.MinAllowedValue = paramToAdd.MinAllowedValue;
                        curParam.Save();
                        this.lstAnlagenParameter.Add(curParam);
                        this.Save();
                       // this.Session.CommitTransaction();
                        //TODO hier die Verbindung zu den Herstellerprodukten schaffenö
                    }
                }
            }
        }

        
      


        public void createAnlagenGruppe()
    {
        if (this.HaustechnikKomponente == null)
        {
            if (this.Liegenschaft != null)
            {
                boLiegenschaft workingLiegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(this.Liegenschaft.Oid);
                boAnlagenArt curAnlagenArt = this.Session.GetObjectByKey<boAnlagenArt>(this.AnlagenArt.Oid);
                fiTechnikeinheit workingUnit;
                XPCollection<fiTechnikeinheit> lstTechnikUnits = new XPCollection<fiTechnikeinheit>(this.Session, new BinaryOperator("Basisanlage.Oid", curAnlagenArt.Oid, BinaryOperatorType.Equal));
                workingUnit = lstTechnikUnits.FirstOrDefault();
                if (workingUnit != null)
                {
                    LgHaustechnikKomponente workingAnlagenGruppe = new LgHaustechnikKomponente(this.Session);

                    workingAnlagenGruppe.Liegenschaft = workingLiegenschaft;
                    workingAnlagenGruppe.Technikeinheit = this.Session.GetObjectByKey<fiTechnikeinheit>(workingUnit.Oid);
                    workingAnlagenGruppe.lstAnlagen.Add(this);
                    workingAnlagenGruppe.lstAnlagen.AddRange(this.lstUnteranlagen);
                    workingAnlagenGruppe.Gebaeude = (this.Gebaeude != null) ? this.Session.GetObjectByKey<fiGebaeude>(this.Gebaeude.Oid) : null;
                    workingAnlagenGruppe.Ebene = (this.Ebene != null) ? this.Session.GetObjectByKey<fiEbene>(this.Ebene.Oid) : null;
                    workingAnlagenGruppe.Raum = (this.Raum != null) ? this.Session.GetObjectByKey<fiRaum>(this.Raum.Oid) : null;
                    workingAnlagenGruppe.Notiz = "automatisch generiert";
                    workingAnlagenGruppe.Save();
                }
            }
            this.Session.CommitTransaction();
        }
    }

      
        protected override void OnLoading()
        {
            base.OnLoading();
        }


        protected override void OnLoaded()
        {
            base.OnLoaded();

           
           
            if(this.Anlagencode == null || this.Anlagencode== string.Empty)
            {
                if(this.Mandant != null)
                {
                    createAnlagenCode();
                }
                else
                {
                    this.Mandant = getMandantByUser(SecuritySystem.CurrentUserName);
                    createAnlagenCode();
                }
            }

            if (this.AnlagenNummer == null || this.AnlagenNummer == string.Empty)
            {
                this.AnlagenNummer = completeNumber();
                this.Save();
               // Session.CommitTransaction();
            }
          if(this.Liegenschaft == null)
            {
                //gibt es eine parent-Anlage??
                if(this.ParentAnlage != null)
                {
                    if(this.ParentAnlage.Liegenschaft != null)
                    {
                        this.Liegenschaft = this.Session.GetObjectByKey<boLiegenschaft>(this.ParentAnlage.Liegenschaft.Oid);
                        this.Save();
                    }
                }
            }
        }

     

        protected override void OnSaved()
        {
            base.OnSaved();
            if(this.FloorType != null && (this.FloorDesignation == null || this.FloorDesignation == string.Empty))
            {
                this.FloorDesignation = (this.FloorType.Kuerzel != null)?this.FloorType.Kuerzel:"n.a.";
            }
            if (this.RoomType != null && (this.RoomDesignation == null || this.RoomDesignation == string.Empty))
            {
                this.RoomDesignation = (this.RoomType.Kuerzel != null)?this.RoomType.Kuerzel:"n.a.";
            }

        }

        protected override void OnSaving()
        {
            base.OnSaving();
            
        }


        public void createAnlagenCode()
        {
            Type curType = this.GetType();
            boMandant curMandant;
            //die Anlage sollte einemMandanten zugeweisen werden
            if (this.Mandant != null)
            {
                curMandant = this.Session.GetObjectByKey<boMandant>(this.Mandant.Oid);

                
                var nummer = this.Session.FindObject<boNummernkreis>(new GroupOperator(new BinaryOperator("Objekt", curType, BinaryOperatorType.Equal), new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal),
                         new BinaryOperator("GueltigAb", DateTime.Now, BinaryOperatorType.LessOrEqual),
                         new BinaryOperator("GueltigBis", DateTime.Now, BinaryOperatorType.GreaterOrEqual)));
                var retVal = string.Empty;



                //dann noch den Acode dazuholen

                if (nummer != null)
                {
                    //jetzt das Teil zusammenbauen
                    if (nummer.Suffix != null && nummer.Suffix != string.Empty)
                    {
                        retVal = string.Format("{0}{1}{2}", nummer.Praefix, nummer.FortlaufendeNummer, nummer.Suffix);
                    }
                    else
                    {
                        retVal = string.Format("{0}{1}", nummer.Praefix, nummer.FortlaufendeNummer, nummer.Suffix);

                    }
                    //retVal = nummer.NextNumber;
                    nummer.FortlaufendeNummer = nummer.FortlaufendeNummer + 1;
                    nummer.Save();
                }
                this.Anlagencode = retVal;
               
                //this.AnlagenNummer = retVal;
              
                this.Save();
                //wenn ich eine ANlagenart habe gleich die Nummer generieren
                if (this.AnlagenArt != null)
                {
                  this.AnlagenNummer =   completeNumber();
                    this.Save();
                    //this.Session.CommitTransaction();
                }
                //this.DebitKreditNr = retVal;
            }
           
          //this.Session.CommitTransaction();
           
        }

        public String completeNumber()
        {
            //die bestehende nummer um den Anlagencode ergänzen
            var Ansprechcode = string.Empty;
            var retVal = string.Empty;
            if (this.Anlagencode!= null)
            {
                if (this.Anlagencode != string.Empty)
                {
                    String[] splitResult = this.Anlagencode.Split('-');
                    if (splitResult != null)
                    {
                        //jetzt den Avcode aus der Anlagenart holen
                        if (this.AnlagenArt != null)
                        {
                            if (this.AnlagenArt.Ansprechcode != null)
                            {
                                Ansprechcode = this.AnlagenArt.Ansprechcode.PadRight(4, ' ');
                                retVal = string.Format("{0}-{1}-{2}", splitResult[0], Ansprechcode, splitResult[1]);
                                
                              
                            }
                        }
                    }
                }
            
            }
            return retVal;
           
        }







    
       
        private void setRechnungsAdresse(boAdresse curAdresse)
        {
            this.Rechnungsadresse = curAdresse;
        }

        private void setAnlagenAdresse(boAdresse curAdresse)
        {
            this.AnlagenAdresse = curAdresse;
        }

        #region einfache Standortimplementierung
        [XafDisplayName("Gebäude")]
        [DataSourceProperty("lstBuildings")]
        public fiGebaeude Building
        {
            get { return _building; }
            set { SetPropertyValue("Building", ref _building, value); }
        }
        [XafDisplayName("Raumbezeichnung")]
        public String RoomDesignation
        {
            get { return _roomDesignation; }
            set { SetPropertyValue("RoomDesignation", ref _roomDesignation, value); }
        }
        [XafDisplayName("Raumart")]
       
        public fiRaumart RoomType
        {
            get { return _roomType; }
            set { SetPropertyValue("RoomType", ref _roomType, value); }
        }

        [XafDisplayName("Ebenenbezeichnung")]
        public String FloorDesignation
        {
            get { return _floorDesignation; }
            set { SetPropertyValue("FloorDesignation", ref _floorDesignation, value); }
        }
        [XafDisplayName("Ebenenart")]
      
        public fiEbenenart FloorType
        {
            get { return _floorType; }
            set { SetPropertyValue("FloorType", ref _floorType, value); }
        }
        #endregion

        [XafDisplayName("Anlagenparameter")]
        [Association("boAnlage-parameterAnlagenParameter"),DevExpress.Xpo.Aggregated]
        public XPCollection<parameterAnlagenParameter> lstAnlagenParameter
        {
            get
            {
                return GetCollection<parameterAnlagenParameter>("lstAnlagenParameter"); 
            }
        }

        [XafDisplayName("Anlagenstatus")]
        public enmAnlagenStatus Anlagenstatus
        {
            get
            {
                return _anlagenstatus;

            }
            set
            {
                SetPropertyValue("Anlagenstatus", ref _anlagenstatus, value);
            }
        }

        [XafDisplayName("Notiz")]
        public System.String Notiz
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
        [XafDisplayName("Seriennummer")]
        public System.String Seriennummer
        {
            get
            {
                return _seriennummer;
            }
            set
            {
                SetPropertyValue("Seriennummer", ref _seriennummer, value);
            }
        }
        [XafDisplayName("Gebäude")]
        [DataSourceProperty("lstBuildings")]
        [ImmediatePostData(true)]
        public fiGebaeude Gebaeude
        {
            get
            {
                return _gebaeude;
            }
            set
            {
                SetPropertyValue("Gebaeude", ref _gebaeude, value);
            }
        }

        #region Filterproperties
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public List<fiGebaeude> lstBuildings
        {
            get
            {
                return lstAvailableBuildings();
            }
        }
        
        public List<fiGebaeude> lstAvailableBuildings()
        {
           List<fiGebaeude> lstRetVal = new List<fiGebaeude>();
            lstRetVal.AddRange(this.Liegenschaft.lstGebaeude);
            return lstRetVal;
        }

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public List<fiEbene> lstFloors
        {
            get
            {
                return lstAvailableEbenen();
            }
        }

        public List<fiEbene> lstAvailableEbenen()
        {
            List<fiEbene> lstRetVal = new List<fiEbene>();
            if (this.Gebaeude != null)
            {
                lstRetVal.AddRange(this.Gebaeude.lstEbenen);
            }
            return lstRetVal;
        }
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public List<fiRaum> lstRooms
        {
            get
            {
                return lstAvailableRooms();
            }
        }

        public List<fiRaum> lstAvailableRooms()
        {
            List<fiRaum> lstRetVal = new List<fiRaum>();
            if (this.Gebaeude != null)
            {
                lstRetVal.AddRange(this.Gebaeude.lstRaeume);
            }
            return lstRetVal;
        }

        #endregion

        [XafDisplayName("Ebene")]
        //[DataSourceCriteria("Gebaeude.Oid='@this.Gebaeude.Oid'")]
        [DataSourceProperty("lstFloors")]
        [ImmediatePostData(true)]
        public fiEbene Ebene
        {
            get
            {
                return _ebene;
            }
            set
            {
                SetPropertyValue("Ebene", ref _ebene, value);
            }
        }
        [XafDisplayName("Raum")]
        //[DataSourceCriteria("Ebene.Oid='@this.Ebene.Oid'")]
        [DataSourceProperty("lstRooms")]
        [ImmediatePostData(true)]
        public fiRaum Raum
        {
            get
            {
                return _raum;
            }
            set
            {
                SetPropertyValue("Raum", ref _raum, value);
            }


        }


        [Association("boAnlage-boZugangAnlage")]
        [XafDisplayName("Zugangsinformationen")]
        public XPCollection<fiZugangAnlage> lstZugangAnlage
        {
            get
            {
                return GetCollection<fiZugangAnlage>("lstZugangAnlage");
            }
        }


        /*
   


      [XafDisplayName("Originalbild")]
      [VisibleInDetailView(false)]
      [VisibleInListView(false)]
      [ReadOnly(true)]
      [ImageEditor]
      [Delayed(true)]


      public byte[] MainImageOriginal
      {
          get
          {
              return GetDelayedPropertyValue<byte[]>("MainImageOriginal");
          }
          set
          {
              SetDelayedPropertyValue<byte[]>("MainImageOriginal", value);
          }
      }
      */





        [XafDisplayName("Wartungszone")]
       // [Delayed(true)]
        public boWartungszone Wartungszone
        {
            get
            {
                boWartungszone retVal = null;
                
                    if (this.AnlagenAdresse != null && this.Liegenschaft != null)
                    {
                        if (this.AnlagenAdresse.getWartungszone(this.Liegenschaft.Mandant) != null)
                        {
                            retVal = this.AnlagenAdresse.getWartungszone(this.Liegenschaft.Mandant);
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
                
                return retVal;
            }

        }

        [XafDisplayName("Mandant")]
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



        [XafDisplayName("Husverwalter")]
        public boHausverwalter Hausverwalter
        {
            get
            {
                boHausverwalter retVal;
                if (this.Liegenschaft != null)
                {

                    retVal = (this.Liegenschaft.Hausverwalter != null) ? retVal = this.Liegenschaft.Hausverwalter : null;
                }
                else
                {
                    retVal = null;
                }
                return retVal;
            }
        }


        [XafDisplayName("Anlagenbetreuer")]
        public boAdresse Anlagenbetreuer
        {
            get
            {
                return _anlagenbetreuer;
            }
            set
            {
                SetPropertyValue("Anlagenbetreuer", ref _anlagenbetreuer, value);
            }
        }


        [XafDisplayName("Rechnungsadresse")]
        public boAdresse Rechnungsadresse
        {
            get
            {
                return _rechnungsadresse;
            }
            set
            {
                SetPropertyValue("Rechnungsadresse", ref _rechnungsadresse, value);
            }
        }

        [XafDisplayName("Baujahr")]
        [ModelDefault("DisplayFormat", "0:D0")]
     
        public System.Int32 Baujahr
        {
            get
            {
                return _baujahr;
            }
            set
            {
                SetPropertyValue("Baujahr", ref _baujahr, value);
            }
        }

        [XafDisplayName("Einbaudatum")]
        public System.DateTime Einbaudatum
        {
            get
            {
                return _einbaudatum;
            }
            set
            {
                SetPropertyValue("Einbaudatum", ref _einbaudatum, value);
            }
        }


        [XafDisplayName("Typbezeichnung")]
        public System.String Typbezeichnung
        {
            get
            {
                return _typbezeichnung;
            }
            set
            {
                SetPropertyValue("Typbezeichnung", ref _typbezeichnung, value);
            }
        }

        [XafDisplayName("Produkttyp")]
        [DataSourceProperty("AvailableProducts")]
      
        
        [ImmediatePostData]
        public fiHerstellerProdukt Typ
        {
            get
            {
                return _typ;
            }
            set
            {
                SetPropertyValue("Typ", ref _typ, value);
            }
        }
        [XafDisplayName("Hersteller")]
        [ImmediatePostData]
        public boHersteller Hersteller
        {
            get
            {
                return _hersteller;
            }
            set
            {
                SetPropertyValue("Hersteller", ref _hersteller, value);
                RefreshAvailableProducts();
            }
        }

       private XPCollection<fiHerstellerProdukt> _availableProducts;
        [Browsable(false)]
        public XPCollection<fiHerstellerProdukt> AvailableProducts
        {
            get
            {
               if(_availableProducts == null)
                {
                    _availableProducts = new XPCollection<fiHerstellerProdukt>(this.Session);
                    
                    RefreshAvailableProducts();
                }
                return _availableProducts;
            }
        }

        private void RefreshAvailableProducts()
        {
            if(_availableProducts == null)
            {
                return;
            }
            if(this.Hersteller != null)
            {
                _availableProducts.Criteria = new BinaryOperator("Hersteller.Oid", this.Hersteller.Oid, BinaryOperatorType.Equal);
            }
            else
            {
                _availableProducts.Criteria = new BinaryOperator(1, 1, BinaryOperatorType.Equal);
            }
            
          
        }



        [XafDisplayName("Zustand")]
        public enmZustand Zustand
        {
            get
            {
                return _zustand;
            }
            set
            {
                SetPropertyValue("Zustand", ref _zustand, value);
            }
        }
        [XafDisplayName("Matchkey")]
        public System.String Matchkey
        {
            get
            {
                var retVal = string.Empty;
                var nummer = string.Empty;
                var bezeichnung = string.Empty;
                nummer = (this.AnlagenNummer != null) ? this.AnlagenNummer : "N/A";
                bezeichnung = (this.Bezeichnung != null) ? this.Bezeichnung : "N/A";
                retVal = String.Format("{0} - {1}", nummer, bezeichnung);
                return retVal;

            }
        }


        [XafDisplayName("Liegenschaft")]
        [Association("boLiegenschaft-boAnlage")]
       // [DataSourceCriteria("Mandant.Oid = '@this.curMandantID'")]
        [RuleRequiredField]
        public boLiegenschaft Liegenschaft
        {
            get
            {
                return _liegenschaft;
            }
            set
            {
                SetPropertyValue("Liegenschaft", ref _liegenschaft, value);
            }
        }

        [XafDisplayName("Anlagenbilder")]
        [Association("boAnlage-boAnlagenBild"), DevExpress.Xpo.Aggregated]
  
        public XPCollection<boAnlagenBild> lstAnlagenBilds
        {
            get
            {
                return GetCollection<boAnlagenBild>("lstAnlagenBilds");
            }
        }

        [XafDisplayName("Anlagenart")]
        [RuleRequiredField]
        [ImmediatePostData(true)]
        [Association("boAnlage-boAnlagenArt")]
        //TODO Anlagenkategorie 
        //[DataSourceCriteria("AnlagenKategorie.Oid = '@this.AnlagenKategorie.Oid'")]
        public boAnlagenArt AnlagenArt
        {
            get
            {
                return _anlagenArt;
            }
            set
            {
                SetPropertyValue("AnlagenArt", ref _anlagenArt, value);
            }
        }

        [XafDisplayName("Anlagenkategorie")]
        [ReadOnly(true)]
        [ImmediatePostData(true)]
        [Association("boAnlage-boAnlagenKategorie")]
        public boAnlagenKategorie AnlagenKategorie
        {
            get
            {
                return _anlagenKategorie;
            }
            set
            {
                SetPropertyValue("AnlagenKategorie", ref _anlagenKategorie, value);
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

        /*
        [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 30, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        [XafDisplayName("Titelbild")]
        [Delayed(true)]
        */
        [Delayed(true)]
        public byte[] Mainimage
        {
            get
            {
                //return GetPropertyValue<byte[]>("Mainimage");
                return GetDelayedPropertyValue<byte[]>("Mainimage");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("Mainimage", value);
            }
        }
        /*
        [ImageEditor(DetailViewImageEditorFixedHeight = 240, DetailViewImageEditorFixedWidth = 240, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, ListViewImageEditorCustomHeight = 30, ListViewImageEditorMode = ImageEditorMode.PictureEdit)]
        */
        [XafDisplayName("Vorschaubild")]
        public byte[] MainImageThumb
        {
            get
            {
                return GetPropertyValue<byte[]>("MainImageThumb");
            }
            set { SetPropertyValue<byte[]>("MainImageThumb",value); }


        }
        [XafDisplayName("Titelbild (Web)")]
        public byte[] MainImageWeb
        {
            get
            {

                return GetPropertyValue<byte[]>("MainImageWeb");
            }
            set
            {
                SetPropertyValue<byte[]>("MainImageWeb", value);
            }
        }

        //[Action(Caption = "Thumbnail erstellen")]
        //TODO: Den Funktionsaufruf in einen Controller packen (Web und Desktop)
        public void makeMainThumbnail()
        {
            setMainThumbnail();
            setMainImageWeb();
        }
        public void setMainThumbnail()
        {
            if (this.Mainimage != null)
            {
                this.MainImageThumb = PictureHelper.getThumbnailByteArray(this.Mainimage);
                this.Save();
                this.Session.CommitTransaction();
            }
        }

        public void setMainImageWeb()
        {
            if (this.Mainimage != null)
            {
                Image workingImage = PictureHelper.ImageFromByteArray(this.Mainimage);
                this.MainImageWeb = PictureHelper.ResizePicByWidth(workingImage, 400);
                this.Save();
                this.Session.CommitTransaction();
            }
        }


        [XafDisplayName("Beschreibung")]
        [Size(-1)]
        public System.String Beschreibung
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

        [XafDisplayName("Anlagenadresse")]

        public boAdresse AnlagenAdresse
        {
            get
            {
                return _anlagenAdresse;
            }
            set
            {
                SetPropertyValue("AnlagenAdresse", ref _anlagenAdresse, value);
            }
        }

        [XafDisplayName("Anlagencode")]
        [ReadOnly(true)]
        public System.String Anlagencode
        {
            get
            {
                return _anlagencode;
            }
            set
            {
                SetPropertyValue("Anlagencode", ref _anlagencode, value);
            }
        }




        [XafDisplayName("Anlagennummer")]

        public System.String AnlagenNummer
        {
            get
            {
                return _anlagennummer;
            }
            set
            {
                SetPropertyValue("AnlagenNummer", ref _anlagennummer, value);
            }
        }


        [XafDisplayName("Bezeichnung")]
        public System.String Bezeichnung
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

        [XafDisplayName("Dateien und Dokumente")]
        [Association("boAnlage-boANAttachment"), DevExpress.Xpo.Aggregated]
        public XPCollection<boANAttachment> lstANAttachments
        {
            get
            {
                return GetCollection<boANAttachment>("lstANAttachments");
            }
        }

        [XafDisplayName("Messungen")]
        [Association("boAnlage-boMessung"), DevExpress.Xpo.Aggregated]
        public XPCollection<boMessung> lstMessungen
        {
            get
            {
                return GetCollection<boMessung>("lstMessungen");
            }
        }

        //Maßnahmen
        [XafDisplayName("Maßnahmen")]
        [Association("boAnlage-actionActionAnlage")]
        public XPCollection<actionActionAnlage> lstActionAnlage
        {
            get { return GetCollection<actionActionAnlage>("lstActionAnlage"); }
        }


      


        [XafDisplayName("Geräte")]
        [Association("boAnlage-fiAnlagengeraet")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<fiAnlagenGeraet> lstAnlagengeraete
        {
            get
            {
                return GetCollection<fiAnlagenGeraet>("lstAnlagengeraete");
            }
        }

        

        [XafDisplayName("Baugruppen")]
        [Association("boAnlage-anlageAnlagenbaugruppe")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<anlageAnlagenbaugruppe> lstBauGruppe
        {
            get
            {
                return GetCollection<anlageAnlagenbaugruppe>("lstBauGruppe");
            }
        }
        
        //TODO: hier kann ich sicher noch was optimieren

        [XafDisplayName("Hauptanlage")]
        [DataSourceCriteria("Liegenschaft.Oid ='@this.Liegenschaft.Oid' AND Oid != '@this.Oid'")]
        [Association("parentAnlage-subAnlagen")]
        public boAnlage ParentAnlage
        {
            get
            {
                return _parentAnlage;
            }
            set
            {
                SetPropertyValue("ParentAnlage", ref _parentAnlage, value);
            }
        }

        [XafDisplayName("abhängige Anlagen")]
        [Association("parentAnlage-subAnlagen")]
        public XPCollection<boAnlage> lstUnteranlagen
        {
            get
            {
                return GetCollection<boAnlage>("lstUnteranlagen");
            }
        }

        #region ITreeNode
        IBindingList ITreeNode.Children
        {
            get { return this.lstUnteranlagen; }
        }

        string ITreeNode.Name
        {
            get { return this.Bezeichnung; }
        }

        ITreeNode ITreeNode.Parent
        {
            get
            {
                return this.ParentAnlage;
            }
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            if (this.lstUnteranlagen != null && this.lstUnteranlagen.Count > 0)
            {
                imageName = "BO_Category";
            }
            else
            {
                imageName = "BO_Product";
            }
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
        #endregion


        [XafDisplayName("Anlagengruppe")]
        [Association("LgHaustechnikKomponente-boAnlage")]
        [DataSourceCriteria("Liegenschaft.Oid ='@this.Liegenschaft.Oid' AND Oid != '@this.Oid'")]
        public LgHaustechnikKomponente HaustechnikKomponente
        {
            get
            {
                return _lghaustechnikkomponente;
            }
            set
            {
                SetPropertyValue("HaustechnikKomponente", ref _lghaustechnikkomponente, value);
            }
        }

        [XafDisplayName("Ansprechpartner")]
        [Association("boAnlage-fiKontaktAnlage")]
        public XPCollection<fiKontaktAnlage> lstKontakte
        {
            get
            {
                return GetCollection<fiKontaktAnlage>("lstKontakte");
            }
        }

       
    }
}