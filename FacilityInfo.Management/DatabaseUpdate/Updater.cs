using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.Collections;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Vertrag.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Building.BusinessObjects;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Base.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Import.BusinessObjects;

namespace FacilityInfo.Management.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            /*
            string[] lstTypen = new string[] { "Telefonnummer", "Zugangscode", "Schlüsseldepot" };

            for(int i=0;i<lstTypen.Count();i++)
            {
                fiZugangsTyp curTyp = ObjectSpace.FindObject<fiZugangsTyp>(new BinaryOperator("Bezeichnung", lstTypen[i], BinaryOperatorType.Equal));
                  if(curTyp == null)
                {
                    curTyp = ObjectSpace.CreateObject<fiZugangsTyp>();
                    curTyp.Bezeichnung = lstTypen[i];
                    curTyp.Save();
                }
            }
            */

            Hashtable lstFileTypes = new Hashtable();
            lstFileTypes.Add("xls", "Excel 2003");
            lstFileTypes.Add("xlsx", "Excel");
            lstFileTypes.Add("xlm", "Excel mit Makros");

            foreach (DictionaryEntry item in lstFileTypes)
            {
                importFileType curFileType = ObjectSpace.FindObject<importFileType>(new BinaryOperator("Name", item.Value, BinaryOperatorType.Equal));
                if(curFileType == null)
                {
                    curFileType = ObjectSpace.CreateObject<importFileType>();
                    curFileType.Extension = item.Key.ToString();
                    curFileType.Name = item.Value.ToString();
                    curFileType.Save();
                }
            }

                boGeraeteart curGeraeteart = ObjectSpace.FindObject<boGeraeteart>(new BinaryOperator("Bezeichnung", "Armatur", BinaryOperatorType.Equal));
            if(curGeraeteart == null)
            {
                curGeraeteart = ObjectSpace.CreateObject<boGeraeteart>();
                curGeraeteart.Bezeichnung = "Armatur";
                curGeraeteart.Save();
            }

            //die Armaturarten auch gleich anlegen
            Hashtable lstArmaturarten = new Hashtable();
            lstArmaturarten.Add("EHM", "Einhebelmischer");
            lstArmaturarten.Add("ZGM", "Zweigriffarmatur");
            lstArmaturarten.Add("SA", "Sensorarmatur");
            lstArmaturarten.Add("THA", "Thermostatarmatur");
            lstArmaturarten.Add("KA", "Küchenarmatur");
            lstArmaturarten.Add("AZA", "Auszieharmatur");
            lstArmaturarten.Add("KFE", "Kessel Füll- und Entleerhahn");
            lstArmaturarten.Add("PV", "Probennahmeventil");
            lstArmaturarten.Add("VK", "Entleerventil/-hahn Kunststoff");
            lstArmaturarten.Add("VM", "Entleerventil/-hahn Messing");

            foreach(DictionaryEntry item in lstArmaturarten)
            {
                boGeraet curgeraet = ObjectSpace.FindObject<boGeraet>(new BinaryOperator("Bezeichnung", item.Value.ToString(), BinaryOperatorType.Equal));
                if(curgeraet == null)
                {
                    curgeraet = ObjectSpace.CreateObject<boGeraet>();
                    curgeraet.Bezeichnung = item.Value.ToString();
                    curgeraet.Kuerzel = item.Key.ToString();
                    curgeraet.Geraeteart = ObjectSpace.FindObject<boGeraeteart>(new BinaryOperator("Bezeichnung", "Armatur", BinaryOperatorType.Equal));
                    curgeraet.Save();
                }

                AnPruefanschluss curAnschluss = ObjectSpace.FindObject<AnPruefanschluss>(new BinaryOperator("Bezeichnung", item.Value.ToString(), BinaryOperatorType.Equal));
                if(curAnschluss == null)
                {
                    curAnschluss = ObjectSpace.CreateObject<AnPruefanschluss>();
                    curAnschluss.Bezeichnung = item.Value.ToString();
                    curAnschluss.Kuerzel = item.Key.ToString();
                    curAnschluss.Save();
                }
            }

            Hashtable lstRisikoGruppe = new Hashtable();
            lstRisikoGruppe.Add("1a", "1a Verwaltungsgebäude, Öffentliche Gebäude, Büro oder Geschäfte");
            lstRisikoGruppe.Add("1b", "Wohnanlagen,private Gebäude");
            lstRisikoGruppe.Add("2", "Schulen, Kindergarten, Hotels, Kasernen, Sportanlagen");
            lstRisikoGruppe.Add("3", "Altenheime, Kuranstalten, Rehab, (Zahn- oder Facharzt)");
            lstRisikoGruppe.Add("4", "Krankenanstalten");

            foreach (DictionaryEntry item in lstRisikoGruppe)
            {
                fiRisikoGruppe curGruppe = ObjectSpace.FindObject<fiRisikoGruppe>(new BinaryOperator("Kuerzel", item.Key.ToString(), BinaryOperatorType.Equal));
                if (curGruppe == null)
                {
                    curGruppe = ObjectSpace.CreateObject<fiRisikoGruppe>();
                    curGruppe.Bezeichnung = item.Value.ToString();
                    curGruppe.Kuerzel = item.Key.ToString();
                    curGruppe.Save();
                }

            }

            //Prüfpunkttypen anlegen
            Hashtable lstPruefpunktTyp = new Hashtable();
            lstPruefpunktTyp.Add("SP", "Spüle");
            lstPruefpunktTyp.Add("WT", "Waschtisch");
            lstPruefpunktTyp.Add("SPUD", "Speucher unteres Drittel");
            lstPruefpunktTyp.Add("SPA", "Speicher Austritt");
            lstPruefpunktTyp.Add("AH", "Auslaufhahn");
            lstPruefpunktTyp.Add("BWS", "Badewanne mit Schlauch und DK");
            lstPruefpunktTyp.Add("BW", "Badewanne");
            lstPruefpunktTyp.Add("RDU", "Regenduschanlage");
            lstPruefpunktTyp.Add("DUMO", "Dusche mit Schlauch ohne DK");
            lstPruefpunktTyp.Add("DU", "Dusche ohne Schlauch und DK");
            lstPruefpunktTyp.Add("DUSK", "Dusche mit Schlauch und DK");
            lstPruefpunktTyp.Add("ZIRKS", "Zirkulationssammelleitung");
            lstPruefpunktTyp.Add("ZIRKE", "Zirkulationseinzelstrang");
            lstPruefpunktTyp.Add("WWE", "Warmwasser-Einzelstrang");
            lstPruefpunktTyp.Add("KWE", "Kaltwasser-Einzelstrang");
            lstPruefpunktTyp.Add("KWA", "Kaltwasser Allgemein");
            lstPruefpunktTyp.Add("WWA", "Warmwasser Allgemein");
            lstPruefpunktTyp.Add("KWSP", "Kaltwasser-Speicher");
            lstPruefpunktTyp.Add("KWH", "Kaltwasser-Hauseintritt");
            lstPruefpunktTyp.Add("KODU", "Überkopfdusche");

            foreach (DictionaryEntry item in lstPruefpunktTyp)
            {
                AnPruefPunktTyp curpTyp = ObjectSpace.FindObject<AnPruefPunktTyp>(new BinaryOperator("Bezeichnung", item.Value.ToString(), BinaryOperatorType.Equal));
                if(curpTyp == null)
                {
                    curpTyp = ObjectSpace.CreateObject<AnPruefPunktTyp>();
                    curpTyp.Bezeichnung = item.Value.ToString();
                    curpTyp.Kuerzel = item.Key.ToString();
                    curpTyp.Save();
                }
                   
            }

            //die Ebebnenarten anlegen
            Hashtable lstEbnen = new Hashtable();
            lstEbnen.Add("UG", "Untergeschoss");
            lstEbnen.Add("EG", "Erdegeschoss");
            lstEbnen.Add("OG", "Obergeschoss");
            lstEbnen.Add("ZG", "Zwischengeschoss");
            lstEbnen.Add("DG", "Dachgeschoss");
            foreach (DictionaryEntry item in lstEbnen)
            {
                fiEbenenart curEbenenart = ObjectSpace.FindObject<fiEbenenart>(new BinaryOperator("Bezeichnung", item.Value.ToString(), BinaryOperatorType.Equal));
                if(curEbenenart == null)
                {
                    curEbenenart = ObjectSpace.CreateObject<fiEbenenart>();
                    curEbenenart.Kuerzel = item.Key.ToString();
                    curEbenenart.Bezeichnung = item.Value.ToString();
                    curEbenenart.Save();
                    
                }
            }
            //die Ruamarten anlegen
            Hashtable lstRaumarten = new Hashtable();
            lstRaumarten.Add("TR", "Technikraum");
            lstRaumarten.Add("WK", "Waschkücke");
            lstRaumarten.Add("K", "Küche");
            lstRaumarten.Add("WC", "Toilette");
            lstRaumarten.Add("HR", "Heizungsraum");
            
            foreach(DictionaryEntry item in lstRaumarten)
            {
                fiRaumart curRaumart = ObjectSpace.FindObject<fiRaumart>(new BinaryOperator("Bezeichnung", item.Value.ToString(), BinaryOperatorType.Equal));
                if(curRaumart == null)
                {
                    curRaumart = ObjectSpace.CreateObject<fiRaumart>();
                    curRaumart.Kuerzel = item.Key.ToString();
                    curRaumart.Bezeichnung = item.Value.ToString();
                    curRaumart.Save();
                }
            }

            //die Ruamarten anlegen
            Hashtable lstParameterArten = new Hashtable();
            lstParameterArten.Add("EP", "Einstellparameter");
            lstRaumarten.Add("SP", "Sollparameter");
            lstRaumarten.Add("PP", "Prüfparameter");
            

            foreach (DictionaryEntry item in lstParameterArten)
            {
                baseParameterArt curParamArt = ObjectSpace.FindObject<baseParameterArt>(new BinaryOperator("Code", item.Key.ToString(), BinaryOperatorType.Equal));
                if (curParamArt == null)
                {
                    curParamArt = ObjectSpace.CreateObject<baseParameterArt>();
                    curParamArt.Code = item.Key.ToString();
                    curParamArt.Bezeichnung = item.Value.ToString();
                    curParamArt.Save();
                }
            }

            //Zugangskategorie
            Hashtable lstZugangsKategorie = new Hashtable();
            lstZugangsKategorie.Add("Hausverwaltung", "1");
            lstZugangsKategorie.Add("Hausbetreuung", "1");
            lstZugangsKategorie.Add("Schlüssel", "1");
            lstZugangsKategorie.Add("Kontakt", "0");
            lstZugangsKategorie.Add("Sonstiges", "0");
            foreach(DictionaryEntry item in lstZugangsKategorie)
            {
                fiZugangKategorie curKategorie = ObjectSpace.FindObject<fiZugangKategorie>(new BinaryOperator("Bezeichnung", item.Key.ToString(), BinaryOperatorType.Equal));
                Int32 value = 0;
                if(curKategorie == null)
                {
                    curKategorie = ObjectSpace.CreateObject<fiZugangKategorie>();
                    curKategorie.Bezeichnung = item.Key.ToString();
                    value = Int32.Parse(item.Value.ToString());
                    curKategorie.DefaultStatus = (EnumStore.enmStatusZugang)value;
                    curKategorie.Save();
                }
            }
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
