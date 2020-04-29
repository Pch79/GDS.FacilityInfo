using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.Fremdsystem.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.BusinessManagement.BusinessObjects;
using FacilityInfo.Anlagen.BusinessObjects;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.DMS.BusinessObjects;
using DevExpress.Persistent.BaseImpl;

namespace FacilityInfo.KwpSynch
{
    class Program
    {
        public static System.String logDirectory = string.Empty;
        public static string kwpContractFolder = string.Empty;
        public static string liegenschaftImageFolder = string.Empty;
        public static string anlagenImageFolder = string.Empty;
        public static string importRootFolder = string.Empty;
        public static List<FileInfo> lstImportFiles = new List<FileInfo>();
        public static System.String currentLogFile = string.Empty;
        public static CultureInfo currentCulture = new CultureInfo("de-DE");
        public static string Mandant = string.Empty;
        public static boMandant curMandant;
        public static List<clsKwpAdresse> lstLiegenschaften = new List<clsKwpAdresse>();
        public static List<clsKwpAdresse> lstHausverwalter = new List<clsKwpAdresse>();
        public static List<clsKwpAdresse> lstHausbetreuer = new List<clsKwpAdresse>();
        public static List<clsKwpAdresse> lstDebitoren = new List<clsKwpAdresse>();
        public static List<clsKwpAdresse> lstKreditoren = new List<clsKwpAdresse>();
        public static List<clsKwpAdresse> lstAdressen = new List<clsKwpAdresse>();
        public static List<boLiegenschaft> lstBoLiegenschaften = new List<boLiegenschaft>();
        public static List<clsKwpWartVertrag> lstkwpVertraege = new List<clsKwpWartVertrag>();
        public static List<clsKwpWartAuftrag> lstKwpWartAuftrag = new List<clsKwpWartAuftrag>();
        public static List<clsWartTermin> lstKwpWartTermine = new List<clsWartTermin>();
        public static List<clsKwpWartAnlage> lstKwpWartAnlagen = new List<clsKwpWartAnlage>();
        public static List<String> lstBrennstoffArt = new List<String>();
        public static Boolean isValidOption = false;

        static void Main(string[] args)
        {

            //die Daten aus dem Mandanten holen und dann loslegen
            //TODO: Meldung falls ein falscher Parameter übergeben wurde
            initAppSettings();
            writeToLogStart(String.Format("Ausführung für Mandant {0} gestartet", Mandant));
            //writeToLogSimpel(String.Format("Option: {0}", args[0].ToString()));
            if (args.Length > 0)
            {
                //den Import der Wartungsverträge anstossen
                if(args[0]=="Import_KwpContract")
                {
                    //ImportRootFolder\KwpContract
                    readImportFolder("KwpContract");
                    processKwpContracts();
                }
                if (args[0] == "Sync_Adresse")
                {
                    //3. Hausverwalter Sync                   
                    readKwpData("qry_Adressen");
                    processAdressen();
                }

                if (args[0] == "Sync_Hausverwalter")
                {
                   
                    readKwpData("qry_HausVerwalter");
                    processHausverwalter();
                }

                if (args[0] == "Sync_Hausbetreuer")
                {
                    //3. Hausverwalter
                    readKwpData("qry_Hausbetreuer");
                    processHausbetreuer();
                }

                if (args[0] == "Sync_Debitor")
                {
                    //1. Kundendaten generieren
                    readKwpData("qry_Debitoren");
                    processDebitoren();
                }
                if (args[0] == "Sync_Liegenschaft")
                {
                    //2. Die Liegenschaften
                    readKwpData("qry_Liegenschaften");
                    processLiegenschaften();
                }
                
                if (args[0] == "Sync_WartVertrag")
                {
                    //3. Hausverwalter
                    readKwpData("qry_KwpWartVertrag");
                    processKwpVertraege();
                }

                if (args[0] == "Sync_WartAuftrag")
                {
                    //3. Hausverwalter
                    readKwpData("WartAuftraege");
                    processKwpWartAuftraege();
                }

                if (args[0] == "Sync_WartTermin")
                {
                    //3. Hausverwalter
                    readKwpData("qry_WartTermine");
                    processKwpWartTermine();
                }

                if (args[0] == "Sync_WartAnlage")
                {
                    //3. Hausverwalter
                    readKwpData("WartAnlagen");
                    processKwpWartAnlagen();
                }

                if(args[0] =="Sync_Brennstoffart")
                {
                    readKwpData("qry_BrennstoffArt");
                    processBrennstoffArt();
                }

                #region Maintenance - Aufrufe
                if (args[0] == "Maintain_Hausverwalter")
                {
                    
                    Maintain("Hausverwalter");
                }

                if (args[0] == "Maintain_LgHausverwalter")
                {

                    Maintain("LgHausverwalter");
                }
                if (args[0] == "Maintain_Adressen")
                {

                    Maintain("Adressen");
                }

                if(args[0] =="Maintain_Betreuungsstatus")
                {
                    Maintain("Betreuungsstatus");
                }


                if(args[0] =="Maintain_WartungsAnlagen")
                {
                    readKwpData("WartAnlagen");
                    Maintain("WartungsAnlagen");
                }

                if (args[0] == "Maintain_WartungsTermine")
                {
                    readKwpData("qry_WartTermine");
                    Maintain("WartungsTermine");
                }

                #endregion
            }

            else 
            {
                readKwpData("qry_Liegenschaften");
                processLiegenschaften();
            }
        }

        private static void checkSystemUser(string userName)
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            PermissionPolicyUser retVal;
            retVal = curSession.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", userName, BinaryOperatorType.Equal));
            if(retVal == null)
            {
                retVal = new PermissionPolicyUser(curSession);
                retVal.UserName = userName;
                retVal.ChangePasswordOnFirstLogon = true;
                retVal.IsActive = false;
                curSession.BeginTransaction();
                retVal.Save();
                curSession.CommitTransaction();
            }

           // return retVal;

        }


        //Funktion um den Ordern für die Wartungsverträge auszulesen
        private static void readImportFolder(string path)
        {
            //die Liste erst löschen
            lstImportFiles.Clear();
            //1. Den Pfad zusammensetzen
            var workingPath = string.Empty;
            workingPath = string.Format("{0}\\{1}", importRootFolder, path);
            Console.WriteLine(workingPath);
            DirectoryInfo di = new DirectoryInfo(workingPath);
            if(di.Exists)
            {
                FileInfo[] fileList = di.GetFiles("*.pdf");
                foreach (FileInfo item in fileList)
                {
                    lstImportFiles.Add(item);
                }
            }
        }

        //das Log-File anpassen um eventuell den Fehler zu finden
        public static void processKwpContracts()
        {
            //jetzt die Liste der Import-Files durchgehen und die Dateibenennung zerlegen
            FileInfo workingFile;
            var fileName = string.Empty;
            string[] splitResult;
            char splitSign = '@';
            var anlagenNummer = string.Empty;
            var resultFileName = string.Empty;
            KwpWartungsVertrag workingKwpContract;
            KwpWartungsAnlage workingKwpAnlage;
            string targetFile = string.Empty;
            Session curContractSession = HauptsystemHelper.GetNewSession();
            bool fileOk = false;
            string targetDirectory = string.Empty;
          //  UnitOfWork uowContract = new UnitOfWork(curContractSession.DataLayer);
            curContractSession.BeginTransaction();
            if (lstImportFiles != null)
            {
                for(int i=0;i<lstImportFiles.Count;i++)
                {
                    workingFile = lstImportFiles[i];
                    //jetzt die Numme extrahieren und dann den Vertrag finden
                    //Bsp für Filename:1@W@AN14-0078@R@W@010@Rueckspuelfilter und  Druckminderer 
                    fileName = workingFile.Name;
                    Console.WriteLine(fileName);
                    splitResult = fileName.Split(splitSign);
                    anlagenNummer = splitResult[2];
                    resultFileName = String.Format("{0}_{1}", splitResult[2], splitResult[6]);
                    //jetzt die Anlage bzw den Vertrag mit der Anlage suchen und
                    Console.WriteLine(anlagenNummer);
                    //der vertrag hat die Anlagennummer
                    workingKwpContract = curContractSession.FindObject<KwpWartungsVertrag>(new BinaryOperator("FremdsystemId", anlagenNummer, BinaryOperatorType.Equal));
                    //Dokumentkategorie -> KWP Wartungsvertrag
                    boAttachmentkategorie chosenKategorie = curContractSession.FindObject<boAttachmentkategorie>(new BinaryOperator("Key", "KwpContract", BinaryOperatorType.Equal));
                    //hat der Vertrag schon ein Vertragsdokument

                    //wenn scheon eine Datei da ist diese löschen und mit der neuen ersetzen
                    docKwpVertragAttachment existingAttachment = curContractSession.FindObject<docKwpVertragAttachment>(new BinaryOperator("KwpAnlage.Anlagennummer", anlagenNummer, BinaryOperatorType.Equal));
                    if(existingAttachment != null)
                    {
                       
                        curContractSession.Delete(existingAttachment);
                       // curContractSession.CommitTransaction();
                    }
                    
                    //das file ok??

                    if(workingKwpContract != null)
                    {
                        
                        docKwpVertragAttachment workingAttachment = new docKwpVertragAttachment(curContractSession);
                        workingAttachment.Kategorie = chosenKategorie;

                        workingAttachment.KwpVertrag = workingKwpContract;

                        using (FileStream fs = workingFile.Open(FileMode.Open))
                        {

                            Console.WriteLine(resultFileName);



                            if (workingAttachment != null)
                            {
                                if (workingAttachment.File == null)
                                {
                                    workingAttachment.File = new FileData(curContractSession);
                                }
                                workingAttachment.File.LoadFromStream(resultFileName, fs);
                                fileOk = true;
                            }
                        }
                        //dann das Teil in den done Ordner kopioeren

                           
                        
                        workingAttachment.Save();
                        
                        workingKwpContract.Save();
                        
                        //das File dann in den done-Ordner verscheiben

                    }
                    else
                    {
                        fileOk = false;
                    }

                    if(fileOk)
                    {
                         targetDirectory = String.Format("{0}{1}", workingFile.DirectoryName, "\\done");
                        //ins Done -Verzeichnis

                       
                    }
                    else
                    {
                        targetDirectory = String.Format("{0}{1}", workingFile.DirectoryName, "\\error");
                    }

                    //ins Done -Verzeichnis
                    DirectoryInfo di = new DirectoryInfo(targetDirectory);
                    if(!di.Exists)
                    {
                        di.Create();
                    }
                    Console.WriteLine(targetDirectory);
                    targetFile = string.Format("{0}\\{1}", targetDirectory, workingFile.Name);
                    FileInfo targetFileInfo = new FileInfo(targetFile);
                    if (targetFileInfo.Exists)
                    {
                        targetFileInfo.Delete();
                    }
                    workingFile.MoveTo(targetFile);
                 }
            }
            
            curContractSession.CommitTransaction();
        }
        private static void Maintain(string targetItem)
        {
          switch(targetItem)
          {

                case "WartungsTermine":
                    Session curTerminSession = HauptsystemHelper.GetNewSession();
                    UnitOfWork uowTermine = new UnitOfWork(curTerminSession.DataLayer);
                    using (uowTermine)
                    {
                        List<String> lstTerminKeysToDelete = new List<string>();
                        List<String> lstFremdsystemIds = new List<string>();
                        List<String> lstKwpTerminKeys = new List<string>();
                        XPCollection<KwpWartTermin> lstKwpWarttermineFi = new XPCollection<KwpWartTermin>(uowTermine);
                        lstKwpWarttermineFi.Load();
                        lstFremdsystemIds = lstKwpWarttermineFi.Select(t => t.FremdsystemId).ToList();
                        //die Diffenrenzliste erstellen
                        lstKwpTerminKeys = lstKwpWartTermine.Select(t => t.TerminKey).ToList();

                        lstTerminKeysToDelete = (lstFremdsystemIds.Except(lstKwpTerminKeys)).ToList();
                        writeToLog("Termine löschen");
                        writeToLogSimpel("Anzahl Termine KWP: " + lstKwpWartTermine.Count.ToString());
                        writeToLogSimpel("Anzahl Termine FI: " + lstKwpWarttermineFi.Count.ToString());
                        writeToLogSimpel("Anzahl zu löschender termine: " + lstTerminKeysToDelete.Count.ToString());
                        for (int i =0;i<lstTerminKeysToDelete.Count;i++)
                        {
                            writeToLogSimpel("termin löschen: "+lstTerminKeysToDelete[i]);
                            deleteKwpWartTermin(lstTerminKeysToDelete[i]);
                        }
                    }
                        break;


                case "WartungsAnlagen":
                    Session curAnlagenSession = HauptsystemHelper.GetNewSession();
                    UnitOfWork uowAnlage = new UnitOfWork(curAnlagenSession.DataLayer);
                    using (uowAnlage)
                    {
                        List<String> lstAnlagenNummernToDelete = new List<string>();
                        //Liste mit FremdsystemIds 
                        List<String> lstFremdsystemIds = new List<string>();
                        XPCollection<KwpWartungsAnlage> lstKwpAnlagenFi = new XPCollection<KwpWartungsAnlage>(uowAnlage);
                        lstKwpAnlagenFi.Load();
                        lstFremdsystemIds = lstKwpAnlagenFi.Select(t => t.FremdsystemId).ToList();

                        //Liste mit KWPAnlagennummern

                        List<String> lstKwpAnlagenNummern = new List<string>();
                        lstKwpAnlagenNummern = lstKwpWartAnlagen.Select(t => t.AnlagenNr).ToList();

                        //Liste der Anlagen aus der Fi erstellen
                        lstAnlagenNummernToDelete = (lstFremdsystemIds.Except(lstKwpAnlagenNummern)).ToList();
                        
                        writeToLog("Anlagen zum löschen "+lstAnlagenNummernToDelete.Count.ToString());
                        writeToLog("AnlagenFi; " + lstFremdsystemIds.Count.ToString());
                        writeToLog("AnlagenKWP: " + lstKwpAnlagenNummern.Count.ToString());
                        
                        for(int i=0;i<lstAnlagenNummernToDelete.Count;i++)
                        {
                              
                            writeToLogSimpel(lstAnlagenNummernToDelete[i]);
                            //TODO Aufruf der Löschfunktion 
                            deleteAnlage(lstAnlagenNummernToDelete[i]);
                        }
                        

                        
                        
                    }
                        break;
                case "Hausverwalter":
                    Session curSession = HauptsystemHelper.GetNewSession();
                    UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
                    using (uow)
                    {
                        //
                        //  XPCollection<boHausverwalter> lstHausverwalter = new XPCollection<boHausverwalter>(uow, new NotOperator(new NullOperator("Adresse")));
                        XPCollection<boHausverwalter> lstHausverwalter = new XPCollection<boHausverwalter>(uow);
                        if (lstHausverwalter != null)
                        {
                          for(int i=0;i<lstHausverwalter.Count();i++)
                          {
                                boHausverwalter curHausverwalter = lstHausverwalter[i];
                                if(curHausverwalter.FremdsystemId == null)
                                {
                                    curHausverwalter.FremdsystemId = curHausverwalter.Adresse.FremdsystemId;
                                    //hier den Benutzeraccount erstellen
                                    
                                   
                                    curHausverwalter.Save();
                                }
                                /*
                                if (curHausverwalter.Systembenutzer == null)
                                {
                                    //feststellen ob für den Hausverawlater in dem Mandanten ein Account besteht
                                    checkSystemUser(curHausverwalter.FremdsystemId);
                                    PermissionPolicyUser curUser = uow.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", 
                                        curHausverwalter.FremdsystemId, BinaryOperatorType.Equal));

                                    PermissionPolicyRole curRole = uow.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Hausverwalter", BinaryOperatorType.Equal));
                                    PermissionPolicyRole defaultRole = uow.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default", BinaryOperatorType.Equal));
                                    if (curRole != null)
                                    {
                                        curRole.AddUser(curUser);
                                        curRole.Save();
                                        curUser.Save();
                                    }

                                    if(defaultRole != null)
                                    {
                                        defaultRole.AddUser(curUser);
                                        defaultRole.Save();
                                        curUser.Save();
                                    }

                                    curHausverwalter.Systembenutzer = curUser;
                                    //die Rolle finden
                                    
                                   
                                    curHausverwalter.Save();
                                }
                                */

                                //curHausverwalter.KwpCode = curHausverwalter.Adresse.
                            }
                        }
                        uow.CommitChanges();
                    }
                        break;

                case ("LgHausverwalter"):
                   {
                        //die Zuordnung suchen_Adressen
                        setHausverwalter();

                   }
                        break;

                case "Adressen":
                    //alle Adrsssen durchgehen und Firmennamen setzen wo nötig
                    Session curAdressSession = HauptsystemHelper.GetNewSession();
                    UnitOfWork uowAdress = new UnitOfWork(curAdressSession.DataLayer);
                    using (uowAdress)
                    {
                        XPCollection<boAdresse> lstAdressen = new XPCollection<boAdresse>(uowAdress);
                        if (lstAdressen != null)
                        {
                            for (int i = 0; i < lstAdressen.Count(); i++)
                            {
                                boAdresse curAdresse = uowAdress.GetObjectByKey<boAdresse>(lstAdressen[i].Oid);
                                if(curAdresse.vorname == string.Empty)
                                {
                                    curAdresse.firmenname = curAdresse.nachname;
                                    curAdresse.nachname = string.Empty;
                                    curAdresse.Save();
                                }
                            }
                        }
                        uowAdress.CommitChanges();
                    }
                    break;

                  
                case "Betreuungsstatus":
                    Session curStatusSession = HauptsystemHelper.GetNewSession();
                    UnitOfWork uowStatus = new UnitOfWork(curStatusSession.DataLayer);
                    using (uowStatus)
                    {
                        //alle Liegenschaften durchgehen udn feststellen ob ein aktiver vertreag da ist.
                        XPCollection<boLiegenschaft> lstMaintainLg = new XPCollection<boLiegenschaft>(uowStatus);
                        int vertragCount = 0;
                        for (int i = 0; i < lstMaintainLg.Count; i++)
                        {
                            boLiegenschaft curLiegenschaft = uowStatus.GetObjectByKey<boLiegenschaft>(lstMaintainLg[i].Oid);
                            if (curLiegenschaft.lstKwpVertrag != null)
                            {
                                vertragCount = curLiegenschaft.lstKwpVertrag.Where(y => y.VertragZurueck == true).Count();
                                //wenn der count 1 0der höher ist ist der Betreuungsstatus aktiv
                                if (vertragCount > 0)
                                {
                                    curLiegenschaft.Betreuungsstatus = Management.EnumStore.enmBetreuungsStatus.aktiv;
                                }
                                else
                                {
                                    curLiegenschaft.Betreuungsstatus = Management.EnumStore.enmBetreuungsStatus.inaktiv;
                                }
                            }
                            else
                            {
                                curLiegenschaft.Betreuungsstatus = Management.EnumStore.enmBetreuungsStatus.unbekannt;
                            }
                            curLiegenschaft.Save();
                        }
                        uowStatus.CommitChanges();
                    }
                        break;
          }
        }

        


        public static void setHausverwalter()
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            Console.WriteLine("setHausverwalter");
            using (uow)
            {
               
                XPCollection<boLiegenschaft> lstLiegenschaften = new XPCollection<boLiegenschaft>(uow,new BinaryOperator("Mandant.Oid",curMandant.Oid,BinaryOperatorType.Equal));
                Console.WriteLine("Count: "+lstLiegenschaften.Count().ToString());
                writeToLog("Maintain (set Hausverwalter");
                if(lstLiegenschaften != null)
                {
                  for(int i=0;i<lstLiegenschaften.Count(); i++)
                  {
                        writeToLogSimpel(lstLiegenschaften[i].HausverwlaterSelekt+ lstLiegenschaften[i].Bezeichnung);
                        boLiegenschaft curLiegenschaft = lstLiegenschaften[i];

                        if(curLiegenschaft.HausverwlaterSelekt != null && curLiegenschaft.HausverwlaterSelekt != string.Empty)
                        
                        {
                            //gibt es für den Selekt einen Eintrag in den Hausverwaltern
                            boHausverwalter relatedHausverwalter = uow.FindObject<boHausverwalter>(new BinaryOperator("KwpCode", curLiegenschaft.HausverwlaterSelekt, BinaryOperatorType.Equal));
                            //hab ich einen gefunden??

                            if (relatedHausverwalter != null)
                            {
                              //ist das der gleiche als der bei der Liegenschaft???
                              if(curLiegenschaft.Hausverwalter!= null) 
                              {
                                if(curLiegenschaft.Hausverwalter.Oid != relatedHausverwalter.Oid)
                                {
                                        curLiegenschaft.Hausverwalter = relatedHausverwalter;
                                        curLiegenschaft.Save();
                                }
                              }
                              else {
                                    curLiegenschaft.Hausverwalter = relatedHausverwalter;
                                    curLiegenschaft.Save();
                              }
                            }

                        } 
                        else
                        {
                            curLiegenschaft.Hausverwalter = null;
                            curLiegenschaft.Save();
                        }
                  }
                }
                uow.CommitChanges();
            }
        }




        /// <summary>
        /// Initilaisierungsfunktion
        /// </summary>
        private static void initAppSettings()
        {

            //den Import-Pfad setzen
            var importSetting = ConfigurationManager.AppSettings.Get("ImportRootFolder").ToString();
            importRootFolder = importSetting;
            //Settings für die Log-Datei
            Console.WriteLine("Initalisierung");
            var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //gibt es schon ein Log-Directory?
            DirectoryInfo diLog = new DirectoryInfo(String.Format("{0}\\{1}", workingDirectory, "Log"));
            if (!diLog.Exists)
            {
                diLog.Create();
            }
            logDirectory = diLog.FullName;
            var logFileName = string.Empty;
            var fullLogFileName = string.Empty;
            var appName = string.Empty;

            appName = ConfigurationManager.AppSettings.Get("AppName").ToString();
            Mandant = ConfigurationManager.AppSettings.Get("Mandant").ToString();
            //curMandant = uow.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
            
             Session curSession = HauptsystemHelper.GetNewSession();
            using (curSession)
            {
                //UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
                curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
            }
            logFileName = String.Format("LOG_{0}_{1}", appName, DateTime.Now.ToString("ddMMyyyyHHmm"));// ToShortDateString(), DateTime.Now.ToLongTimeString());
            fullLogFileName = String.Format("{0}\\{1}.txt", logDirectory, logFileName);
            currentLogFile = fullLogFileName;



        }

        #region ins Log schreiben
        public static void writeToLogSimpel(string message)
        {
            //Block hinzufügen
            //**********************************************
            //Datum + Uhrzeit
            //Message
            //**********************************************
            var trennlinie = string.Empty;
            var timeLine = string.Empty;

            trennlinie = String.Format("{0}", "*").PadLeft(30, '*');
            timeLine = string.Format("{0} - {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
            StreamWriter logWriter;
            if (!File.Exists(currentLogFile))
            {

                logWriter = new StreamWriter(currentLogFile);
            }
            else
            {
                logWriter = File.AppendText(currentLogFile);
            }

            //den streamWriter brauch ich noch

            //logWriter.WriteLine(trennlinie);
            //logWriter.WriteLine(timeLine);
            logWriter.WriteLine(message);
            //logWriter.WriteLine(trennlinie);
            logWriter.Close();

        }
        public static void writeToLogStart(string message)
        {
            //Block hinzufügen
            //**********************************************
            //Datum + Uhrzeit
            //Message
            //**********************************************
            var trennlinie = string.Empty;
            var timeLine = string.Empty;

            trennlinie = String.Format("{0}", "*").PadLeft(30, '*');
            timeLine = string.Format("{0} - {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
            StreamWriter logWriter;
            if (!File.Exists(currentLogFile))
            {

                logWriter = new StreamWriter(currentLogFile);
            }
            else
            {
                logWriter = File.AppendText(currentLogFile);
            }

            //den streamWriter brauch ich noch

            logWriter.WriteLine(trennlinie);
            logWriter.WriteLine(timeLine);
            logWriter.WriteLine(message);
            //logWriter.WriteLine(trennlinie);
            logWriter.Close();

        }

        public static void writeToLogEnd(string message)
        {
            //Block hinzufügen
            //**********************************************
            //Datum + Uhrzeit
            //Message
            //**********************************************
            var trennlinie = string.Empty;
            var timeLine = string.Empty;

            trennlinie = String.Format("{0}", "*").PadLeft(30, '*');
            timeLine = string.Format("{0} - {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
            StreamWriter logWriter;
            if (!File.Exists(currentLogFile))
            {

                logWriter = new StreamWriter(currentLogFile);
            }
            else
            {
                logWriter = File.AppendText(currentLogFile);
            }

            //den streamWriter brauch ich noch

            logWriter.WriteLine(trennlinie);
            // logWriter.WriteLine(timeLine);
            logWriter.WriteLine(message);
            logWriter.WriteLine(trennlinie);
            logWriter.Close();

        }



        public static void writeToLog(string message)
        {
            //Block hinzufügen
            //**********************************************
            //Datum + Uhrzeit
            //Message
            //**********************************************
            var trennlinie = string.Empty;
            var timeLine = string.Empty;

            trennlinie = String.Format("{0}", "*").PadLeft(30, '*');
            timeLine = string.Format("{0} - {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
            StreamWriter logWriter;
            if (!File.Exists(currentLogFile))
            {

                logWriter = new StreamWriter(currentLogFile);
            }
            else
            {
                logWriter = File.AppendText(currentLogFile);
            }

            //den streamWriter brauch ich noch

            logWriter.WriteLine(trennlinie);
            logWriter.WriteLine(timeLine);
            logWriter.WriteLine(message);
            logWriter.WriteLine(trennlinie);
            logWriter.Close();

        }

        #endregion

        #region WartTermine
        private static void processKwpWartTermine()
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            KwpWartungsAnlage workingAnlage = null;
            using (uow)
            {

                //erst mal den Mandanten finden  

                boMandant curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
                if (curMandant != null)
                {
                    writeToLogSimpel(String.Format("Hausverwalter für den Mandanten: {0} schreiben", curMandant.Mandantenname));
                    if (lstKwpWartTermine != null)
                    {
                        //alle durchgehen und feststellen welche es in der Fi noch nicht gibt
                        //hier brauch cih die Session mit dem normalen ConnectionString

                        //Session KwpXpoSession = XpoHelper.GetNewSession();
                        //den Kunden suchen
                        KwpWartTermin curKwpWartTermin = null;
                        for (int i = 0; i < lstKwpWartTermine.Count(); i++)
                        {
                            clsWartTermin item = lstKwpWartTermine[i];
                            try {
                                curKwpWartTermin = uow.FindObject<KwpWartTermin>(new BinaryOperator("FremdsystemId", item.TerminKey, BinaryOperatorType.Equal));
                            }
                            catch (Exception teminEx)
                            {

                            }
                            if (curKwpWartTermin == null)
                            {
                               createKwpWartTermin(item);
                            }
                            else 
                            {
                                updateKwpWartTermin(item, curKwpWartTermin);
                            }
                        }
                    }
                }
            }
        }

        private static void createKwpWartTermin(clsWartTermin curTermin)
        {
            
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            KwpWartungsAnlage workingAnlage = null;
         
            writeToLogSimpel("Ausführung Wartugnsverträge" + workingMandant.Mandantenkennung);
            using (uow)
            {
                if (curTermin.AnlagenNr != null)
                {
                    workingAnlage = uow.FindObject<KwpWartungsAnlage>(new BinaryOperator("FremdsystemId", curTermin.AnlagenNr, BinaryOperatorType.Equal));
                  

                }
                
                // writeToLogSimpel("Liegenschaft " + workingLiegenschaft.FremdsystemID);
               
                    //nur dann den vertrag erstellen
                    KwpWartTermin curWartungsTermin = new KwpWartTermin(uow);
                    curWartungsTermin.Mandant = workingMandant;
                    curWartungsTermin.AnlagenNummer = curTermin.AnlagenNr;
                    curWartungsTermin.FremdsystemId = curTermin.TerminKey;
                    curWartungsTermin.Monteur = curTermin.Monteur;
                    curWartungsTermin.HauptMonteuer = curTermin.HauptMonteur;
                    curWartungsTermin.Laufendenummer = curTermin.lfdNr;
                    curWartungsTermin.MonatKw = curTermin.MonatKw;
                    curWartungsTermin.Intervall = curTermin.Intervall;
                    curWartungsTermin.IntervallArt = curTermin.IntervallArt;
                    curWartungsTermin.WartungsJahr = curTermin.WartungsJahr;
                    curWartungsTermin.InfoText = curTermin.InfoText;
                    curWartungsTermin.TerminDatum = curTermin.TerminDatum;
                    curWartungsTermin.TerminUhrzeit = curTermin.TerminUhrzeit;
                    curWartungsTermin.PlanStunden = curTermin.PlanStunden;
                curWartungsTermin.KwpAnlage = (workingAnlage != null) ? workingAnlage : null;
              
                    curWartungsTermin.Save();    
                if(workingAnlage != null)
                {
                    workingAnlage.lstWartungsTermine.Add(curWartungsTermin);
                }
                                    
               
                uow.CommitChanges();
            }

        }

        public static void deleteKwpWartTermin(string kwpTerminKey)
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            KwpWartTermin workingTermin;
            
            using (uow)
            {
                workingTermin = uow.FindObject<KwpWartTermin>(new BinaryOperator("FremdsystemId", kwpTerminKey, BinaryOperatorType.Equal));
                if (workingTermin != null)
                {

                    uow.Delete(workingTermin);
                    uow.CommitChanges();
                }
                //1. Anlage löschen

            }

        }

        public static void updateKwpWartTermin(clsWartTermin curTermin, KwpWartTermin curKwpWartTermin)
        {
           
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            KwpWartTermin workingTermin = uow.GetObjectByKey<KwpWartTermin>(curKwpWartTermin.Oid);
            KwpWartungsAnlage workingAnlage = null;
            KwpWartungsVertrag workingVertrag = null;
            //wenn sich Änderungen ergeben haben dann diese abspeichern
            writeToLogSimpel(String.Format("KwpVertrag für den Mandanten: {0} updaten", curMandant.Mandantenname));
            using (uow)
            {

                if (curTermin.AnlagenNr != null)
                {
                    workingAnlage = uow.FindObject<KwpWartungsAnlage>(new BinaryOperator("FremdsystemId", curTermin.AnlagenNr, BinaryOperatorType.Equal));
                    if (workingAnlage != null)
                    {
                        workingVertrag = (workingAnlage.WartungsVertrag != null) ? workingAnlage.WartungsVertrag : null;
                    }
                }

                if(workingAnlage != null)
                {
                   if(workingTermin.KwpAnlage != null)
                   {
                     if(workingTermin.KwpAnlage.Oid != workingAnlage.Oid)
                     {
                            workingTermin.KwpAnlage = workingAnlage;
                            workingTermin.Save();
                     }
                   }
                   else {
                        workingTermin.KwpAnlage = workingAnlage;
                        workingTermin.Save();
                   }
                }
               

                if (workingTermin.InfoText != curTermin.InfoText)
                {
                    workingTermin.InfoText = curTermin.InfoText;
                    workingTermin.Save();
                }

                if (workingTermin.TerminDatum != curTermin.TerminDatum)
                {
                    workingTermin.TerminDatum = curTermin.TerminDatum;
                    workingTermin.Save();
                }

                if (workingTermin.TerminUhrzeit != curTermin.TerminUhrzeit)
                {
                    workingTermin.TerminUhrzeit = curTermin.TerminUhrzeit;
                    workingTermin.Save();
                }

                if (workingTermin.Monteur != curTermin.Monteur)
                {
                    workingTermin.Monteur = curTermin.Monteur;
                    workingTermin.Save();
                }

                if (workingTermin.HauptMonteuer != curTermin.HauptMonteur)
                {
                    workingTermin.HauptMonteuer = curTermin.HauptMonteur;
                    workingTermin.Save();
                }

                if (workingTermin.PlanStunden != curTermin.PlanStunden)
                {
                    workingTermin.PlanStunden = curTermin.PlanStunden;
                    workingTermin.Save();
                }

                if (workingTermin.MonatKw != curTermin.MonatKw)
                {
                    workingTermin.MonatKw = curTermin.MonatKw;
                    workingTermin.Save();
                }

                if (workingTermin.WartungsJahr != curTermin.WartungsJahr)
                {
                    workingTermin.WartungsJahr = curTermin.WartungsJahr;
                    workingTermin.Save();
                }


                if (workingTermin.Intervall != curTermin.Intervall)
                {

                    workingTermin.Intervall = curTermin.Intervall;
                    workingTermin.Save();
                }

                if (workingTermin.IntervallArt != curTermin.IntervallArt)
                {

                    workingTermin.IntervallArt = curTermin.IntervallArt;
                    workingTermin.Save();
                }

                uow.CommitChanges();
            }
        }
        #endregion

        #region WartAnlagen
        private static void processKwpWartAnlagen()
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            using (curSession)
            {
                //erst mal den Mandanten finden  
                
                //beide gleiche Oid

                boMandant curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
                if (curMandant != null)
                {
                    writeToLogSimpel(String.Format("WartAnlagen für den Mandanten: {0} schreiben", curMandant.Mandantenname));
                    var test = "Exists";
                    char first = test.First();
                 

                    if (lstKwpWartAnlagen != null)
                    {
                        //alle durchgehen und feststellen welche es in der Fi noch nicht gibt
                        //hier brauch cih die Session mit dem normalen ConnectionString

                        //Session KwpXpoSession = XpoHelper.GetNewSession();


                        //den Kunden suchen
                        KwpWartungsAnlage curKwpAnlage = null;
                        for (int i = 0; i < lstKwpWartAnlagen.Count(); i++)
                        {
                            clsKwpWartAnlage item = lstKwpWartAnlagen[i];

                            try {
                                curKwpAnlage = curSession.FindObject<KwpWartungsAnlage>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", item.AnlagenNr, BinaryOperatorType.Equal)));

                            }
                            catch(Exception anlagenEx)
                            {

                            }

                            if (curKwpAnlage == null)
                            {
                                createKwpWartAnlage(item);
                            }
                            else
                            {
                                updateKwpWartAnlage(item, curKwpAnlage);
                            }
                        }
                    }
                }
            }
        }

        private static void createKwpWartAnlage(clsKwpWartAnlage curAnlage)
        {
           
            //es handelt sich um eine neue Anlage
            //
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            boLiegenschaft workingLiegenschaft = null;
            boHausverwalter workingHausverwalter = null;
            //TODO: Hausbetreuer integrieren
            fiHausbetreuer workingHausbetreuer = null;
            KwpWartungsVertrag workingKwpVertrag = null;

            writeToLogSimpel("Ausführung Wartugnsverträge" + workingMandant.Mandantenkennung);
            using (uow)         
            {
            
                if (curAnlage.AnlagenAdr != null && curAnlage.AnlagenAdr != string.Empty)
                {
                    workingLiegenschaft = uow.FindObject<boLiegenschaft>(new BinaryOperator("FremdsystemId", curAnlage.AnlagenAdr, BinaryOperatorType.Equal));
                }
                if(curAnlage.HausverwalterAdr != null && curAnlage.HausverwalterAdr != string.Empty)
                {
                    workingHausverwalter = uow.FindObject<boHausverwalter>(new BinaryOperator("FremdsystemId", curAnlage.HausverwalterAdr, BinaryOperatorType.Equal));
                }

                if(curAnlage.HausmeisterAdr != null && curAnlage.HausmeisterAdr != string.Empty)
                {
                    workingHausbetreuer = uow.FindObject<fiHausbetreuer>(new BinaryOperator("FremdsystemId", curAnlage.HausmeisterAdr, BinaryOperatorType.Equal));
                }

                workingKwpVertrag = uow.FindObject<KwpWartungsVertrag>(new BinaryOperator("FremdsystemId", curAnlage.AnlagenNr, BinaryOperatorType.Equal));
                // writeToLogSimpel("Liegenschaft " + workingLiegenschaft.FremdsystemID);
               
                    //nur dann den vertrag erstellen
                    KwpWartungsAnlage curWartungsAnlage = new KwpWartungsAnlage(uow);
                    curWartungsAnlage.FremdsystemId = curAnlage.AnlagenNr;
                    curWartungsAnlage.Anlagennummer = curAnlage.AnlagenNr;
                    curWartungsAnlage.AnlagenOrt = curAnlage.AnlagenOrt;
                    curWartungsAnlage.Bezeichnung = curAnlage.Bezeichnung;
                    curWartungsAnlage.BrennstoffArt = curAnlage.Brennstoffart;
                    //gibt es bei bei der Liegenschaft eine Haustechnikkomponente die als Syastembezeichnung die Brennstoffart drin hat

                curWartungsAnlage.Bemerkungen = curAnlage.Bemerkungen;
                curWartungsAnlage.KwpSelekt = curAnlage.Selekt;

                   curWartungsAnlage.Liegenschaft = (workingLiegenschaft != null) ? workingLiegenschaft : null; 
                    //den Hausverwalter extra raussuchen
                    curWartungsAnlage.Hausverwalter = (workingHausverwalter != null)?workingHausverwalter :null;
                    curWartungsAnlage.Monteur = curAnlage.Monteur;

                    curWartungsAnlage.Mandant = workingMandant;
                //hier kann ich den Vertrag auch gleich suchen
              
                     curWartungsAnlage.WartungsVertrag = (workingKwpVertrag != null) ? workingKwpVertrag : null;
                    writeToLogSimpel(curWartungsAnlage.FremdsystemId);
                    curWartungsAnlage.Save();

                  

                
               
                uow.CommitChanges();
            }
           // curSession.CommitTransaction();

        }

        public static void updateKwpWartAnlage(clsKwpWartAnlage curAnlage,  KwpWartungsAnlage curKwpAnlage)
        {
       

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            KwpWartungsAnlage workingAnlage = uow.GetObjectByKey<KwpWartungsAnlage>(curKwpAnlage.Oid);
            boHausverwalter workingHausverwalter = null;
            fiHausbetreuer workingHausbetreuer = null;
            boLiegenschaft workingLiegenschaft = null;
            KwpWartungsVertrag workingKwpVertrag = null;
            //wenn sich Änderungen ergeben haben dann diese abspeichern
            writeToLogSimpel(String.Format("KwpAnlage für den Mandanten: {0} updaten", curMandant.Mandantenname));
            using (uow)
            {
                workingKwpVertrag = uow.FindObject<KwpWartungsVertrag>(new BinaryOperator("FremdsystemId", curAnlage.AnlagenNr, BinaryOperatorType.Equal));

                if (workingAnlage.Monteur != curAnlage.Monteur)
                {
                    workingAnlage.Monteur = curAnlage.Monteur;
                    workingAnlage.Save();
                }
                if (workingAnlage.Bezeichnung != curAnlage.Bezeichnung)
                {
                    workingAnlage.Bezeichnung = curAnlage.Bezeichnung;
                    workingAnlage.Save();
                }
                if (workingAnlage.AnlagenOrt != curAnlage.AnlagenOrt)
                {
                    workingAnlage.AnlagenOrt = curAnlage.AnlagenOrt;
                    workingAnlage.Save();
                }

                //Brennstoffart == Anlagenart
                if (workingAnlage.BrennstoffArt != curAnlage.Brennstoffart)
                {
                    workingAnlage.BrennstoffArt = curAnlage.Brennstoffart;
                    workingAnlage.Save();
                }
                if(workingAnlage.Bemerkungen != curAnlage.Bemerkungen)
                {
                    workingAnlage.Bemerkungen = curAnlage.Bemerkungen;
                    workingAnlage.Save();
                }
               
                if(workingAnlage.KwpSelekt != curAnlage.Selekt)
                {
                    workingAnlage.KwpSelekt = curAnlage.Selekt;
                    workingAnlage.Save();
                }

                if (workingKwpVertrag != null)
                {
                    if (workingAnlage.WartungsVertrag != null)
                    {
                      if(workingAnlage.WartungsVertrag.Oid != workingKwpVertrag.Oid)
                      {
                            workingAnlage.WartungsVertrag = workingKwpVertrag;
                            workingAnlage.Save();
                      }
                    }
                    else
                    {
                        workingAnlage.WartungsVertrag = workingKwpVertrag;
                        workingAnlage.Save();
                    }
                }
                //wenn sich der Hausverwalter ändert
               
                  if(curAnlage.HausverwalterAdr != null)
                  {
                        workingHausverwalter = uow.FindObject<boHausverwalter>(new BinaryOperator("FremdsystemId", curAnlage.HausverwalterAdr, BinaryOperatorType.Equal));
                        if(workingHausverwalter != null)
                        {
                        if (workingAnlage.Hausverwalter != null)
                        {
                            if (workingAnlage.Hausverwalter.Oid != workingHausverwalter.Oid)
                            {
                                workingAnlage.Hausverwalter = workingHausverwalter;
                            }
                        }
                        else {
                            workingAnlage.Hausverwalter = workingHausverwalter;
                        }
                        }
                        else 
                        {
                            workingAnlage.Hausverwalter = null;
                        }
                  }
                //wenn sich der Hausbetreuer ändert (Ist im KWP die Hausmeisteradresse)
                if (curAnlage.HausmeisterAdr != null)
                {
                    workingHausbetreuer = uow.FindObject<fiHausbetreuer>(new BinaryOperator("FremdsystemId", curAnlage.HausmeisterAdr, BinaryOperatorType.Equal));
                    if (workingHausbetreuer != null)
                    {
                        if (workingAnlage.HausBetreuer != null)
                        {
                            if (workingAnlage.HausBetreuer.Oid != workingHausbetreuer.Oid)
                            {
                                workingAnlage.HausBetreuer = workingHausbetreuer;
                            }
                        }
                        else
                        {
                            workingAnlage.HausBetreuer = workingHausbetreuer;
                        }
                    }
                    else
                    {
                        workingAnlage.HausBetreuer = null;
                    }
                }

                //wenn sich die Liegenschaft ändert

                if (curAnlage.AnlagenAdr != null)
                    {
                        workingLiegenschaft = uow.FindObject<boLiegenschaft>(new BinaryOperator("FremdsystemId", curAnlage.AnlagenAdr, BinaryOperatorType.Equal));
                    if (workingLiegenschaft != null)
                    {
                        if (workingAnlage.Liegenschaft != null)
                        {
                        
                            if (workingAnlage.Liegenschaft.Oid != workingLiegenschaft.Oid)
                            {
                                workingAnlage.Liegenschaft = workingLiegenschaft;
                            }

                        }
                        else {
                            workingAnlage.Liegenschaft = workingLiegenschaft;
                        }
                    }
                    else
                    {
                        workingAnlage.Liegenschaft = null;
                    }
                    }
                

                uow.CommitChanges();
            }
        }

        public static void deleteAnlage(string anlagenNummerKwp)
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            KwpWartungsAnlage workingAnlage;
            KwpWartungsVertrag workingVertrag;
            using (uow)
            {
                workingAnlage = uow.FindObject<KwpWartungsAnlage>(new BinaryOperator("FremdsystemId", anlagenNummerKwp, BinaryOperatorType.Equal));
                if(workingAnlage != null)
                {
                    workingVertrag = uow.FindObject<KwpWartungsVertrag>(new BinaryOperator("WartungsAnlage.FremdsystemId", anlagenNummerKwp, BinaryOperatorType.Equal));
                    if (workingVertrag != null)
                    {
                        uow.Delete(workingVertrag);
                    }
                        uow.Delete(workingAnlage);
                    uow.CommitChanges();
                }
                //1. Anlage löschen
                
            }
        }
        #endregion

        #region Brennstoffart
        private static void processBrennstoffArt()
        {
            //gibt es sie Brennstoffart schon? wenn nein anlegen sonst nix machen
            Session curSession = HauptsystemHelper.GetNewSession();
            
            if (lstBrennstoffArt.Count > 0)
            {
                for (int i = 0; i < lstBrennstoffArt.Count(); i++)
                {
                    fiLinkKeyHtKomponente curLinkKey = curSession.FindObject<fiLinkKeyHtKomponente>(new BinaryOperator("FremdSystemValue", lstBrennstoffArt[i], BinaryOperatorType.Equal));
                    if(curLinkKey == null)
                    {
                        curLinkKey = new fiLinkKeyHtKomponente(curSession);
                        curLinkKey.FremdSystemValue = lstBrennstoffArt[i];
                        curLinkKey.FremdSystemKey = lstBrennstoffArt[i];
                        curLinkKey.Save();
                    }
                 }
                curSession.BeginTransaction();
                curSession.CommitTransaction();
             }
        }
        #endregion
        #region Wartungsauftrag

        private static void processKwpWartAuftraege()
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            using (curSession)
            {
                //erst mal den Mandanten finden  

                boMandant curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
                if (curMandant != null)
                {
                    writeToLogSimpel(String.Format("Hausverwalter für den Mandanten: {0} schreiben", curMandant.Mandantenname));
                    if (lstKwpWartAuftrag != null)
                    {
                        

                        //Session KwpXpoSession = XpoHelper.GetNewSession();

                        KwpWartungsAuftrag curKwpWartAuftrag = null;
                        
                        for (int i = 0; i < lstKwpWartAuftrag.Count(); i++)
                        {
                            clsKwpWartAuftrag item = lstKwpWartAuftrag[i];

                            try {
                                curKwpWartAuftrag = curSession.FindObject<KwpWartungsAuftrag>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", item.AuftragsNummerKwp, BinaryOperatorType.Equal)));
                            }
                            catch (Exception auftragException)
                            {
                              
                            }

                            if (curKwpWartAuftrag == null)
                            {
                                createKwpWartAuftrag(item);
                            }
                            else 
                            {
                                updateKwpWartAuftrag(item, curKwpWartAuftrag);
                            }
                        }
                    }
                }
            }
        }

        private static void createKwpWartAuftrag(clsKwpWartAuftrag curAuftrag)
        {
            //TODO: Wartungsaufträge Create
            //es handelt sich um eine neue Anlage
            //
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
           

            writeToLogSimpel("Ausführung Wartugnsverträge" + workingMandant.Mandantenkennung);
            using (uow)

            {
              
                //nur dann den vertrag erstellen
                KwpWartungsAuftrag curWartungsAuftrag = new KwpWartungsAuftrag(uow);

                curWartungsAuftrag.FremdsystemId = curAuftrag.AuftragsNummerKwp;
                curWartungsAuftrag.AuftragsnummerKwp = curAuftrag.AuftragsNummerKwp;
                curWartungsAuftrag.Betreff = curAuftrag.Betreff;
                curWartungsAuftrag.HauptMonteuer = curAuftrag.Hauptmonteur;
                curWartungsAuftrag.KwpAnlagenNummer = curAuftrag.KwpAnlagenNummer;
                curWartungsAuftrag.Mandant = workingMandant;
                curWartungsAuftrag.Monteuer = curAuftrag.Monteuer;
                curWartungsAuftrag.Planstunden = curAuftrag.Planstunden;
                curWartungsAuftrag.TerminDatum = curAuftrag.TerminDatum;
                curWartungsAuftrag.TerminZeit = curAuftrag.TerminZeit;
                
               
               
                curWartungsAuftrag.Save();

                uow.CommitChanges();
            }
            // curSession.CommitTransaction();


        }

        public static void updateKwpWartAuftrag(clsKwpWartAuftrag curAuftrag,KwpWartungsAuftrag curWartAuftrag)
        {
            //TODO: Wartungsaufträge update
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);

            KwpWartungsAuftrag workingAuftrag = uow.GetObjectByKey<KwpWartungsAuftrag>(curWartAuftrag.Oid);
            
            //wenn sich Änderungen ergeben haben dann diese abspeichern
            writeToLogSimpel(String.Format("KwpAnlage für den Mandanten: {0} updaten", curMandant.Mandantenname));
            using (uow)
            {

                if (workingAuftrag.Betreff != curAuftrag.Betreff)
                {
                    workingAuftrag.Betreff = curAuftrag.Betreff;
                    workingAuftrag.Save();
                }

                if (workingAuftrag.TerminDatum != curAuftrag.TerminDatum)
                {
                    workingAuftrag.TerminDatum = curAuftrag.TerminDatum;
                    workingAuftrag.Save();
                }

                if (workingAuftrag.TerminZeit != curAuftrag.TerminZeit)
                {
                    workingAuftrag.TerminZeit = curAuftrag.TerminZeit;
                    workingAuftrag.Save();
                }

                if (workingAuftrag.Monteuer != curAuftrag.Monteuer)
                {
                    workingAuftrag.Monteuer = curAuftrag.Monteuer;
                    workingAuftrag.Save();
                }

                if (workingAuftrag.Planstunden != curAuftrag.Planstunden)
                {
                    workingAuftrag.Planstunden = curAuftrag.Planstunden;
                    workingAuftrag.Save();
                }




                uow.CommitChanges();
            }
        }
        #endregion

        #region Wartungsvertrag - getestet
        private static void processKwpVertraege()
        {
            //was ist mit den Verträgen die es im KWP nicht mehr gibt

            Session curSession = HauptsystemHelper.GetNewSession();
            using (curSession)

            {
                //erst mal den Mandanten finden  

                boMandant curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
                if (curMandant != null)
                {
                    writeToLogSimpel(String.Format("Hausverwlater für den Mandanten: {0} schreiben", curMandant.Mandantenname));
                    if (lstkwpVertraege != null)
                    {
                        //alle durchgehen und feststellen welche es in der Fi noch nicht gibt
                        //hier brauch cih die Session mit dem normalen ConnectionString

                        //Session KwpXpoSession = XpoHelper.GetNewSession();

                        
                        //den Kunden suchen
                        for (int i = 0; i < lstkwpVertraege.Count(); i++)
                        {
                            clsKwpWartVertrag item = lstkwpVertraege[i];
                            
                            KwpWartungsVertrag curKwpVertrag = curSession.FindObject<KwpWartungsVertrag>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", item.AnlagenNr, BinaryOperatorType.Equal)));


                            if (curKwpVertrag == null)
                            {
                                createKwpVertrag(item);
                            }
                            else {
                                updateKwpVertrag(item,curKwpVertrag);
                                
                            }

                        }
                    }

                }
            }

            //jetzt muss ich die Verträge löschen die nicht mehr im KWP sind
        }

        

        public static void createKwpVertrag(clsKwpWartVertrag curKwpVertrag)
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            boLiegenschaft workingLiegenschaft = null;
            KwpWartungsAnlage workingWartungsAnlage = null;

            writeToLogSimpel("Ausführung Wartugnsverträge" + workingMandant.Mandantenkennung);
            using (uow)
            {
                workingWartungsAnlage = uow.FindObject<KwpWartungsAnlage>(new BinaryOperator("Anlagennummer", curKwpVertrag.AnlagenNr, BinaryOperatorType.Equal));

                if (curKwpVertrag.AnlagenAdr != null)
                {
                    workingLiegenschaft = uow.FindObject<boLiegenschaft>(new BinaryOperator("FremdsystemId", curKwpVertrag.AnlagenAdr, BinaryOperatorType.Equal));
                }
               // writeToLogSimpel("Liegenschaft " + workingLiegenschaft.FremdsystemID);
                if (workingLiegenschaft != null)
                {
                    //nur dann den vertrag erstellen
                    KwpWartungsVertrag curWartungsVertrag = new KwpWartungsVertrag(uow);
                    curWartungsVertrag.VertragsNummer = curKwpVertrag.Vertragsnummer;
                    curWartungsVertrag.FremdsystemId = curKwpVertrag.AnlagenNr;
                    curWartungsVertrag.Vertragstext = curKwpVertrag.Text;
                    curWartungsVertrag.Bezeichnung = curKwpVertrag.Bezeichnung;
                    curWartungsVertrag.VertragsDatum = curKwpVertrag.Datum;
                    curWartungsVertrag.VertragsBeginn = curKwpVertrag.Beginn;
                    curWartungsVertrag.VertragsEnde = curKwpVertrag.Ende;
                    curWartungsVertrag.LinkKeyLiegenschaft = curKwpVertrag.AnlagenAdr;
                    curWartungsVertrag.Liegenschaft = workingLiegenschaft;
                    curWartungsVertrag.VertragZurueck = (curKwpVertrag.VertragZurueck == 1) ? true : false;
                    curWartungsVertrag.Mandant = workingMandant;
                    curWartungsVertrag.WartungsAnlage = (workingWartungsAnlage != null) ? workingWartungsAnlage : null;

                    curWartungsVertrag.KuendigungsDatum = curKwpVertrag.KuendigungsDatum;
                    curWartungsVertrag.KuendigungsGrund = curKwpVertrag.KuendigungsGrund;
                    writeToLogSimpel(curWartungsVertrag.FremdsystemId);
                    curWartungsVertrag.Save();
                    //hier den Status setzen

                    curWartungsVertrag.VertragsStatus = getStatus(curWartungsVertrag);
                    curWartungsVertrag.Save();
                 
                }
                else
                {
                    writeToLogSimpel(String.Format("Keine Liegenschaft für {0} gefunden", curKwpVertrag.AnlagenNr));
                }
                uow.CommitChanges();
            }
        }

        public static void updateKwpVertrag(clsKwpWartVertrag curKwpVertrag,KwpWartungsVertrag curKwpWartungsVertrag )
        {
            //was ändert sich?
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            KwpWartungsVertrag workingVertrag = uow.GetObjectByKey<KwpWartungsVertrag>(curKwpWartungsVertrag.Oid);
            KwpWartungsAnlage workingWartungsAnlage = null;
            boLiegenschaft workingLiegenschaft = null;
            //wenn sich Änderungen ergeben haben dann diese abspeichern
            writeToLogSimpel(String.Format("KwpVertrag für den Mandanten: {0} updaten", curMandant.Mandantenname));
            using (uow)
            {
                workingWartungsAnlage = uow.FindObject<KwpWartungsAnlage>(new BinaryOperator("Anlagennummer", curKwpVertrag.AnlagenNr, BinaryOperatorType.Equal));

                workingLiegenschaft = uow.FindObject<boLiegenschaft>(new BinaryOperator("FremdsystemId", curKwpVertrag.AnlagenAdr, BinaryOperatorType.Equal));

                //es kann sich auch die Liegenschaft ändern
                if (workingVertrag.Liegenschaft != null)
                {
                    if(workingLiegenschaft != null)
                    {
                        if(workingVertrag.Liegenschaft.Oid != workingLiegenschaft.Oid)
                        {
                            workingVertrag.Liegenschaft = workingLiegenschaft;
                            workingVertrag.Save();
                        }
                    }
                }
                else
                {

                    workingVertrag.Liegenschaft = workingLiegenschaft;
                    workingVertrag.Save();

                }
                if (workingWartungsAnlage != null)
                {
                    
                    if (workingVertrag.WartungsAnlage != null)
                    {
                        //dann prüfen 
                        if(workingVertrag.WartungsAnlage.Oid != workingWartungsAnlage.Oid)
                        {
                            workingVertrag.WartungsAnlage = workingWartungsAnlage;
                            workingVertrag.Save();
                        }
                       

                    }
                    else
                    {
                        workingVertrag.WartungsAnlage = workingWartungsAnlage;
                        workingVertrag.Save();
                    }
                }


                    if (workingVertrag.Bezeichnung != curKwpVertrag.Bezeichnung)
                {
                    workingVertrag.Bezeichnung = curKwpVertrag.Bezeichnung;
                    workingVertrag.Save();
                }

                if (curKwpVertrag.Text != null)
                {
                    if (workingVertrag.Vertragstext != curKwpVertrag.Text)
                    {
                        workingVertrag.Vertragstext = curKwpVertrag.Text;
                        workingVertrag.Save();
                    }
                    else {
                        workingVertrag.Vertragstext = curKwpVertrag.Text;
                        workingVertrag.Save();
                    }
                }
                ///wenn Vertrag zurück
                
                if(workingVertrag.VertragZurueck != curKwpVertrag.ContractBack)
                {
                    workingVertrag.VertragZurueck = curKwpVertrag.ContractBack;
                    workingVertrag.Save();
                    
                    //hier die Termine löschen
                }

                if(workingVertrag.VertragsDatum != curKwpVertrag.Datum)
                {
                    workingVertrag.VertragsDatum = curKwpVertrag.Datum;
                    workingVertrag.Save();
                }

                if(workingVertrag.VertragsBeginn != curKwpVertrag.Beginn)
                {
                    workingVertrag.VertragsBeginn = curKwpVertrag.Beginn;
                    workingVertrag.Save();
                }
                
                //Kündigung
                if(workingVertrag.KuendigungsDatum != curKwpVertrag.KuendigungsDatum)
                {
                    workingVertrag.KuendigungsDatum = curKwpVertrag.KuendigungsDatum;
                    workingVertrag.Save();
                }

                if(workingVertrag.KuendigungsGrund != curKwpVertrag.KuendigungsGrund)
                {
                    workingVertrag.KuendigungsGrund = curKwpVertrag.KuendigungsGrund;
                    workingVertrag.Save();
                }
                workingVertrag.VertragsStatus = getStatus(workingVertrag);
                workingVertrag.Save();
             
                uow.CommitChanges();
                if (workingVertrag.VertragZurueck == false)
                {
                    deleteTermine(workingVertrag);
                }
            }
        }
        private static void deleteTermine(KwpWartungsVertrag curVertrag)
        {
            //die Anlage dazu
            KwpWartungsAnlage workingAnlage = null;
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            workingAnlage = uow.GetObjectByKey<KwpWartungsAnlage>(curVertrag.WartungsAnlage.Oid);
            if(workingAnlage != null)
            {
                uow.Delete(workingAnlage.lstWartungsTermine);
                uow.CommitChanges();
            }
            workingAnlage.Save();
            uow.CommitChanges();

        }
        private static Management.EnumStore.enmVertragsStatus getStatus(KwpWartungsVertrag workingVertrag)
        {

            Management.EnumStore.enmVertragsStatus retVal = Management.EnumStore.enmVertragsStatus.none;
            //wenn K
            if (workingVertrag.KuendigungsDatum != null && workingVertrag.KuendigungsDatum != DateTime.MinValue)
            {
                if (workingVertrag.KuendigungsDatum > DateTime.Now)
                {
                    retVal = Management.EnumStore.enmVertragsStatus.gekündigt;
                }
                else
                {
                    retVal = Management.EnumStore.enmVertragsStatus.beendet;
                }
            }
            else
            {
                if (workingVertrag.VertragZurueck)
                {
                    retVal = Management.EnumStore.enmVertragsStatus.aktiv;
                }
                else
                {
                    retVal = Management.EnumStore.enmVertragsStatus.akquise;
                }
            }

            return retVal;

        }

        #endregion

        #region Liegenschaften
        public static void processLiegenschaften()
        {

            writeToLogStart("Aufruf der Funktion processLiegenschaften");


            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
          
                bool goOn = true;
            using (uow)
            {
                if (curMandant != null)
                {
                    if (lstLiegenschaften != null)
                    {
                        //alle durchgehen und feststellen welche es in der Fi noch nicht gibt
                        //hier brauch cih die Session mit dem normalen ConnectionString

                        //Session KwpXpoSession = XpoHelper.GetNewSession();

                        //foreach (clsKwpAdresse item in lstLiegenschaften)

                        for (int i = 0; i < lstLiegenschaften.Count; i++)
                        {


                            clsKwpAdresse item = lstLiegenschaften[i];
                            writeToLogSimpel(String.Format("KwpAdressen: {0}", item.AdrNrGes));

                            //die Liegenschaft für den angemeldetenMandanten sind hier wichtig alles andere ist unwichtig

                            //boLiegenschaft curLiegenschaft = uow.FindObject<boLiegenschaft>(new GroupOperator(new BinaryOperator("FremdsystemID", item.AdrNrGes, BinaryOperatorType.Equal),new BinaryOperator("Mandant.Oid",curMandant.Oid,BinaryOperatorType.Equal)));
                            boLiegenschaft curLiegenschaft = uow.FindObject<boLiegenschaft>(new BinaryOperator("FremdsystemId", item.AdrNrGes, BinaryOperatorType.Equal));
                            Debitorenkonto curKunde = uow.FindObject<Debitorenkonto>(new BinaryOperator("FremdsystemId", item.AdrNrGes, BinaryOperatorType.Equal));

                            if (curMandant != null)
                            {
                                goOn = true;
                            }
                            else {
                                goOn = false;
                            }



                            //den Kunden noch prüfen

                            if (goOn)
                            {
                                if (curKunde != null)
                                {
                                    if (goOn)
                                    {
                                        goOn = true;
                                    }
                                }
                                else
                                {
                                    goOn = false;
                                }
                            }
                      

                            if (goOn)
                            {
                                if (curLiegenschaft != null)
                                {
                                    //die Update-Funktion aufrufen
                                    updateLiegenschaft(curLiegenschaft, curKunde, item);
                                }
                                else 
                                {
                                    createLiegenschaft(curKunde, item);
                                }
                            }
                        }
                    }
                }
            }                      
         }
                
        
        
        public static void updateLiegenschaft(boLiegenschaft workingLiegenschaft, Debitorenkonto workingKunde, clsKwpAdresse adressItem)
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boLiegenschaft curLiegenschaft = uow.GetObjectByKey<boLiegenschaft>(workingLiegenschaft.Oid);
            Debitorenkonto curKunde = uow.GetObjectByKey<Debitorenkonto>(workingKunde.Oid);
            using (uow)
            {
                
                if (adressItem.Objektnummer != null)
                {
                    if (curLiegenschaft.ObjektNummer != adressItem.Objektnummer)
                    {
                        curLiegenschaft.ObjektNummer = adressItem.Objektnummer;
                        writeToLogSimpel(String.Format("Objektnummer:Liegenschaft {0} oldValue {1} newValue {2}", curLiegenschaft.Bezeichnung, curLiegenschaft.ObjektNummer, adressItem.Objektnummer));
                        curLiegenschaft.Save();
                    }
                }

                if (curLiegenschaft.Bezeichnung != adressItem.Name1)
                {
                    curLiegenschaft.Bezeichnung = adressItem.Name1;
                    writeToLogSimpel(String.Format("Bezeichnung:Liegenschaft {0} oldValue {1} newValue {2} ", curLiegenschaft.Bezeichnung, curLiegenschaft.Bezeichnung, adressItem.Name1));
                    curLiegenschaft.Save();
                }

                if(curLiegenschaft.DebitorenKonto  == null)
                {
                    curLiegenschaft.DebitorenKonto = curKunde;
                    curLiegenschaft.Save();
                    curLiegenschaft.Liegenschaftsadresse = uow.GetObjectByKey<boAdresse>(curKunde.Adresse.Oid);
                    curLiegenschaft.Save();
                }
                 
                else 
                {
                  if(curLiegenschaft.DebitorenKonto.Oid != curKunde.Oid)
                  {
                        curLiegenschaft.DebitorenKonto = curKunde;
                        curLiegenschaft.Save();   
                  }
                    curLiegenschaft.Liegenschaftsadresse = uow.GetObjectByKey<boAdresse>(curKunde.Adresse.Oid);
                    curLiegenschaft.Save();
                }
                //die Adresse aus dem Debitorenkonto nehmen
                
                if(curLiegenschaft.Liegenschaftsnummer == null)
                {
                    curLiegenschaft.createNumber();
                    writeToLog(string.Format("Liegenschaftsnummer generiert: {0} {1}", curLiegenschaft.Liegenschaftsnummer, curLiegenschaft.Bezeichnung));
                   
                }
               
                curLiegenschaft.HausverwlaterSelekt = adressItem.strSelekt2;
                curLiegenschaft.Save();
               
                uow.CommitChanges();
            }
        }
   
        public static void createLiegenschaft(Debitorenkonto curKunde,clsKwpAdresse adressItem)
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            Debitorenkonto workingDebitorenkonto = uow.GetObjectByKey<Debitorenkonto>(curKunde.Oid);
            //die Kundenadresse anlegen falls noch nicht vorhanden

            if (workingMandant != null)
            {

                using (uow)
                {
                    //die Adressdaten laden
                    writeToLogStart(String.Format("Neuanlage:Liegenschaft {0} ", adressItem.AdrNrGes));
                    boLiegenschaft curLiegenschaft = new boLiegenschaft(uow);
                    curLiegenschaft.Mandant = workingMandant;
                    curLiegenschaft.FremdsystemId = adressItem.AdrNrGes;
                    curLiegenschaft.Bezeichnung = adressItem.Name1;
                    writeToLogSimpel(String.Format("Neuanlage:Liegenschaft {0} Bezeichnung {1}", adressItem.AdrNrGes, curLiegenschaft.Bezeichnung));
                    //curLiegenschaft.Wohneinheiten = adressItem.Wohneinheiten;
                    // writeToLogSimpel(String.Format("Neuanlage:Liegenschaft {0} Wohneinheiten {1}", adressItem.AdrNrGes, curLiegenschaft.Wohneinheiten));
                    curLiegenschaft.ObjektNummer = adressItem.Objektnummer;
                    writeToLogSimpel(String.Format("Neuanlage:Liegenschaft {0} Objektnummer {1}", adressItem.AdrNrGes, curLiegenschaft.ObjektNummer));

                    curLiegenschaft.HausverwlaterSelekt = adressItem.strSelekt2;
                    curLiegenschaft.DebitorenKonto = workingDebitorenkonto;
                    curLiegenschaft.Liegenschaftsadresse = uow.GetObjectByKey<boAdresse>(workingDebitorenkonto.Adresse.Oid);

                    curLiegenschaft.Save();

                    writeToLogSimpel(String.Format("Neuanlage:Liegenschaft {0} HausverwlaterSelekt {1}", adressItem.strSelekt2, curLiegenschaft.HausverwlaterSelekt));

                    uow.CommitChanges();
                }
            }
        }
        #endregion


        #region Adressen allgemein
        

        #endregion

        #region Hausverwalter
        public static void processHausverwalter()
        {

            Session curSession = HauptsystemHelper.GetNewSession();
            using (curSession)
            {
                //erst mal den Mandanten finden  

                boMandant curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
                if (curMandant != null)
                {
                    writeToLogSimpel(String.Format("Hausverwlater für den Mandanten: {0} schreiben", curMandant.Mandantenname));
                    if (lstHausverwalter != null)
                    {
                        //alle durchgehen und feststellen welche es in der Fi noch nicht gibt
                        //hier brauch cih die Session mit dem normalen ConnectionString

                        //Session KwpXpoSession = XpoHelper.GetNewSession();

                        boOrt curOrt = null;
                        //den Kunden suchen
                        for (int i = 0; i < lstHausverwalter.Count(); i++)
                        {
                            clsKwpAdresse item = lstHausverwalter[i];
                            boHausverwalter curHausverwalter = curSession.FindObject<boHausverwalter>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", item.AdrNrGes, BinaryOperatorType.Equal)));


                            if (curHausverwalter == null)
                            {
                                createHausverwalter(item);

                            }
                            else {
                                updateHausverwalter(curHausverwalter, item);
                            }

                        }
                    }

                }
            }
        }

    public static void createHausverwalter(clsKwpAdresse workingAdresse)
    {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            boAdresse relatedAdresse = uow.FindObject<boAdresse>(new BinaryOperator("FremdsystemId", workingAdresse.AdrNrGes, BinaryOperatorType.Equal));
            using (uow)
            {
                boHausverwalter  curHausverwalter = new boHausverwalter(uow);

                curHausverwalter.FremdsystemId = workingAdresse.AdrNrGes;
                curHausverwalter.Mandant = workingMandant;
                curHausverwalter.Adresse = relatedAdresse;
                curHausverwalter.FremdsystemId = workingAdresse.AdrNrGes;
                curHausverwalter.KwpCode = workingAdresse.AdrNrA;
                curHausverwalter.HausverwalterSelekt1 = workingAdresse.strSelekt1;
                curHausverwalter.HausverwalterSelekt2 = workingAdresse.strSelekt2;
                curHausverwalter.Save(); 
                
                //den Benutzer kann ich heir auch gleich erstellen
                //Benutzername 
                //die Rolle abfragen


                uow.CommitChanges();
            }
        }

    private static void updateHausverwalter(boHausverwalter curHausverwalter, clsKwpAdresse curAdresse)
    {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boHausverwalter workingHausverwalter = uow.GetObjectByKey<boHausverwalter>(curHausverwalter.Oid);
            //wenn sich Änderungen ergeben haben dann diese abspeichern
            writeToLogSimpel(String.Format("Hausverwlater für den Mandanten: {0} updaten", curMandant.Mandantenname));
            using (uow)
            {
           
            if(workingHausverwalter.KwpCode == null)
            {
                    workingHausverwalter.KwpCode = curAdresse.AdrNrA;
                    workingHausverwalter.Save();
            }

            if(workingHausverwalter.HausverwalterSelekt1 != null)
            {
              if(workingHausverwalter.HausverwalterSelekt1 != curAdresse.strSelekt1)
              {
                        workingHausverwalter.HausverwalterSelekt1 = curAdresse.strSelekt1;
                        workingHausverwalter.Save();
              }
            }
            else
            {
                    workingHausverwalter.HausverwalterSelekt1 = curAdresse.strSelekt1;
                    workingHausverwalter.Save();
                }

                if (workingHausverwalter.HausverwalterSelekt2 != null)
                {
                    if (workingHausverwalter.HausverwalterSelekt2 != curAdresse.strSelekt2)
                    {
                        workingHausverwalter.HausverwalterSelekt2 = curAdresse.strSelekt2;
                        workingHausverwalter.Save();
                    }
                }
                else
                {
                    workingHausverwalter.HausverwalterSelekt2 = curAdresse.strSelekt2;
                    workingHausverwalter.Save();
                }
                //die Selekts neu setzen wenn nötig

                uow.CommitChanges();
            }

        }

        #endregion

        #region Hausbetreuer
        public static void processHausbetreuer()
        {

            Session curSession = HauptsystemHelper.GetNewSession();
            using (curSession)
            {
                //erst mal den Mandanten finden  

                boMandant curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
                if (curMandant != null)
                {
                    writeToLogSimpel(String.Format("Hausbetreuer für den Mandanten: {0} schreiben", curMandant.Mandantenname));
                    if (lstHausbetreuer != null)
                    {
                        //alle durchgehen und feststellen welche es in der Fi noch nicht gibt
                        //hier brauch cih die Session mit dem normalen ConnectionString

                        //Session KwpXpoSession = XpoHelper.GetNewSession();
                        writeToLogSimpel(String.Format("Anzahl: {0} verarbeiten", lstHausbetreuer.Count.ToString()));
                        boOrt curOrt = null;
                        //den Kunden suchen
                        for (int i = 0; i < lstHausbetreuer.Count(); i++)
                        {
                            clsKwpAdresse item = lstHausbetreuer[i];
                            fiHausbetreuer curHausbetreuer = curSession.FindObject<fiHausbetreuer>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", item.AdrNrGes, BinaryOperatorType.Equal)));


                            if (curHausbetreuer == null)
                            {
                                createHausbetreuer(item);

                            }
                            else {
                                updateHausbetreuer(curHausbetreuer, item);
                            }

                        }
                    }
                }
            }
        }

        private static void updateHausbetreuer(fiHausbetreuer curHausbetreuer, clsKwpAdresse curAdresse)
        {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
           fiHausbetreuer workingHausbetreuer = uow.GetObjectByKey<fiHausbetreuer>(curHausbetreuer.Oid);
            //wenn sich Änderungen ergeben haben dann diese abspeichern
            writeToLogSimpel(String.Format("Hausbetreuer für den Mandanten: {0} updaten", curMandant.Mandantenname));
            using (uow)
            {
                writeToLogSimpel(String.Format("Update Hausbetreuer {0}", curHausbetreuer.FremdsystemId));
                if (workingHausbetreuer.KwpCode == null)
                {
                    workingHausbetreuer.KwpCode = curAdresse.AdrNrA;
                    workingHausbetreuer.Save();
                }


                if (workingHausbetreuer.HausbetreuerSelekt1 != null)
                {
                    if (workingHausbetreuer.HausbetreuerSelekt1 != curAdresse.strSelekt1)
                    {
                        workingHausbetreuer.HausbetreuerSelekt1 = curAdresse.strSelekt1;
                        workingHausbetreuer.Save();
                    }
                }
                else
                {
                    workingHausbetreuer.HausbetreuerSelekt1 = curAdresse.strSelekt1;
                    workingHausbetreuer.Save();
                }

                if (workingHausbetreuer.HausbetreuerSelekt2 != null)
                {
                    if (workingHausbetreuer.HausbetreuerSelekt2 != curAdresse.strSelekt2)
                    {
                        workingHausbetreuer.HausbetreuerSelekt2 = curAdresse.strSelekt2;
                        workingHausbetreuer.Save();
                    }
                }
                else
                {
                    workingHausbetreuer.HausbetreuerSelekt2 = curAdresse.strSelekt2;
                    workingHausbetreuer.Save();
                }
                uow.CommitChanges();
            }

        }

        public static void createHausbetreuer(clsKwpAdresse workingAdresse)
        {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            boAdresse relatedAdresse = uow.FindObject<boAdresse>(new BinaryOperator("FremdsystemId", workingAdresse.AdrNrGes, BinaryOperatorType.Equal));
            writeToLogSimpel(String.Format("Hausbetreuer {0} anlegen", workingAdresse.AdrNrGes));
            using (uow)
            {
                fiHausbetreuer curHausbetreuer = new fiHausbetreuer(uow);

                curHausbetreuer.FremdsystemId = workingAdresse.AdrNrGes;
                curHausbetreuer.Mandant = workingMandant;
                curHausbetreuer.Adresse = relatedAdresse;
                curHausbetreuer.FremdsystemId = workingAdresse.AdrNrGes;
                curHausbetreuer.KwpCode = workingAdresse.AdrNrA;
                curHausbetreuer.HausbetreuerSelekt1 = workingAdresse.strSelekt1;
                curHausbetreuer.HausbetreuerSelekt2 = workingAdresse.strSelekt2;
                curHausbetreuer.Save();
                uow.CommitChanges();
            }
        }


        #endregion

        #region Adressen
        public static boOrt createOrt(string plz, string ort)
        {
            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boOrt retVal;
            boOrt curOrt;
            using (uow)
            {

                curOrt = new boOrt(curSession);
                curOrt.PLZ = plz;
                curOrt.Name = ort;

                curOrt.Save();
                uow.CommitChanges();
            }
            retVal = curSession.GetObjectByKey<boOrt>(curOrt.Oid);
            return retVal;

        }
        public static void updateAdresse(boAdresse curAdresse, clsKwpAdresse adressItem,boOrt curOrt)
        {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boAdresse workingAdresse = uow.GetObjectByKey<boAdresse>(curAdresse.Oid);
            //wenn sich Änderungen ergeben haben dann diese abspeichern
            boOrt workingOrt = uow.GetObjectByKey<boOrt>(curOrt.Oid);
            using (uow)
            {
              //gibt es einen Vornamen?
              
                if (workingAdresse.vorname != null)
                {
                    if (workingAdresse.vorname != adressItem.Vorname)
                    {
                        workingAdresse.vorname = adressItem.Vorname;
                        workingAdresse.Save();
                    }
                }
                else {
                    workingAdresse.vorname = adressItem.Vorname;
                    workingAdresse.Save();
                }




                if (workingAdresse.vorname != string.Empty)
                {
                    if (workingAdresse.nachname != adressItem.Name1)
                    {
                        workingAdresse.nachname = adressItem.Name1;
                        workingAdresse.Save();
                    }
                    else {
                        if (workingAdresse.firmenname != adressItem.Name1)
                        {
                            workingAdresse.firmenname = adressItem.Name1;
                            workingAdresse.Save();
                        }

                    }
                }

                //Firmenname wenn keinVorname gesetzt wurde

                if(workingAdresse.FremdsystemCode == null || workingAdresse.FremdsystemCode == string.Empty)
                {
                    workingAdresse.FremdsystemCode = adressItem.AdrNrA;
                    workingAdresse.Save();
                }
                else
                {
                   if(workingAdresse.FremdsystemCode != adressItem.AdrNrA)
                   {
                        workingAdresse.FremdsystemCode = adressItem.AdrNrA;
                        workingAdresse.Save();
                   }
                }

                

                if (workingAdresse.Zusatz != null)
                {
                    if (workingAdresse.Zusatz != adressItem.Zusatz)
                    {
                        workingAdresse.Zusatz = adressItem.Zusatz;
                        workingAdresse.Save();
                    }
                }
                else {
                    workingAdresse.Zusatz = adressItem.Zusatz;
                    workingAdresse.Save();

                }
                if (workingAdresse.ort != null)
                {
                    if (workingAdresse.ort.Oid != workingOrt.Oid)
                    {
                        workingAdresse.ort = workingOrt;
                    }
                }
                else {
                    workingAdresse.ort = workingOrt;
                }
                uow.CommitChanges();
            }
        }

        public static void createAdresse(clsKwpAdresse workingAdresse, boOrt workingOrt)
        {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            using (uow)
            {
                boAdresse curAdresse = new boAdresse(uow);
                //curAdresse.DebitorenNummer = workingAdresse.DebitKreditNr.ToString();
                curAdresse.FremdsystemId = workingAdresse.AdrNrGes;
                curAdresse.FremdsystemCode = workingAdresse.AdrNrA;
                curAdresse.strasse = workingAdresse.Strasse;
                curAdresse.Mandant = workingMandant;
                curAdresse.vorname = workingAdresse.Vorname;
                if (workingAdresse.Vorname != string.Empty)
                {
                    //Name1 = Nachname
                    curAdresse.nachname = workingAdresse.Name1;

                }
                else 
                {
                    //Name 1 ist Firmenname
                    curAdresse.firmenname = workingAdresse.Name1;
                }
               
                curAdresse.nachname = workingAdresse.Name1;
                curAdresse.Zusatz = workingAdresse.Zusatz;
                
                   
                if (workingOrt != null)
                {
                    curAdresse.ort = uow.GetObjectByKey<boOrt>(workingOrt.Oid);
                }
                curAdresse.Save();
                uow.CommitChanges();
            }
        }

        public static void createAdresseSimpel(clsKwpAdresse workingAdresse)
        {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            using (uow)
            {
                boAdresse curAdresse = new boAdresse(uow);
                //curAdresse.DebitorenNummer = workingAdresse.DebitKreditNr.ToString();
               
                curAdresse.FremdsystemId = workingAdresse.AdrNrGes;
                curAdresse.FremdsystemCode = workingAdresse.AdrNrA;
                curAdresse.firmenname = workingAdresse.Name1;
                curAdresse.Mandant = workingMandant;                          
                curAdresse.Save();
                uow.CommitChanges();
            }
        }


        public static void processAdressen()
        {
            writeToLogStart("Aufruf der Funktion processAdressen");

            Session curSession = HauptsystemHelper.GetNewSession();
            using (curSession)
            {
                //erst mal den Mandanten finden  

                boMandant curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
                if (curMandant != null)
                {
                    writeToLogSimpel(String.Format("Adressen für den Mandanten: {0} schreiben", curMandant.Mandantenname));
                    if (lstAdressen != null)
                    {
                        //alle durchgehen und feststellen welche es in der Fi noch nicht gibt
                        //hier brauch cih die Session mit dem normalen ConnectionString

                        //Session KwpXpoSession = XpoHelper.GetNewSession();

                        boOrt curOrt = null;
                        //den Kunden suchen
                        for (int i = 0; i < lstAdressen.Count(); i++)
                        {
                            clsKwpAdresse item = lstAdressen[i];
                            boAdresse curAdresse = curSession.FindObject<boAdresse>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", item.AdrNrGes, BinaryOperatorType.Equal)));

                            if (item.Plz != string.Empty && item.Ort != string.Empty)
                            {
                                curOrt = curSession.FindObject<boOrt>(new GroupOperator(new BinaryOperator("PLZ", item.Plz, BinaryOperatorType.Equal), new BinaryOperator("Name", item.Ort, BinaryOperatorType.Equal)));
                                if (curOrt == null)
                                {
                                    curOrt = createOrt(item.Plz, item.Ort);
                                }
                                if (curAdresse == null)
                                {
                                    createAdresse(item, curOrt);

                                }
                                else {
                                    updateAdresse(curAdresse, item,curOrt);
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion

       


        #region Debitor
        public static void updateDebitorenkonto(Debitorenkonto curKunde,clsKwpAdresse adressItem)
    {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            Debitorenkonto workingKunde = uow.GetObjectByKey<Debitorenkonto>(curKunde.Oid);
            boAdresse relatedAdresse = curSession.FindObject<boAdresse>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", adressItem.AdrNrGes, BinaryOperatorType.Equal)));
            if (relatedAdresse == null)
            {
                createAdresseSimpel(adressItem);
            }

            relatedAdresse = curSession.FindObject<boAdresse>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", adressItem.AdrNrGes, BinaryOperatorType.Equal)));

            //wenn sich Änderungen ergeben haben dann diese abspeichern
            using (uow)
            {

                if (workingKunde.FremdsystemCode != null)
                {
                    if (workingKunde.FremdsystemCode != adressItem.AdrNrA)
                    {
                        workingKunde.FremdsystemCode = adressItem.AdrNrA;
                        workingKunde.Save();
                    }
                }
                else
                {
                    workingKunde.FremdsystemCode = adressItem.AdrNrA;
                    workingKunde.Save();
                }
                if (relatedAdresse != null)
                {
                    if (workingKunde.Adresse != null)
                    {
                      //update bei Unterschied   
                      if(workingKunde.Adresse.Oid != relatedAdresse.Oid)
                      {
                            workingKunde.Adresse = uow.GetObjectByKey<boAdresse>(relatedAdresse.Oid);
                            workingKunde.Save();
                        }
                    }
                    else {
                        workingKunde.Adresse = uow.GetObjectByKey<boAdresse>(relatedAdresse.Oid);
                        workingKunde.Save();
                    }
                } 
                uow.CommitChanges();
            }
        }

    public static void createDebitorenkonto(clsKwpAdresse workingAdresse)
    {

            Session curSession = HauptsystemHelper.GetNewSession();
            UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
            boMandant workingMandant = uow.GetObjectByKey<boMandant>(curMandant.Oid);
            //die Adresse suchen falls vorhanden
            //hier muss eine Adresse ausgewählt und zugeordent bzw erstellt und zugeoadnet werden
            boAdresse relatedAdresse = curSession.FindObject<boAdresse>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", workingAdresse.AdrNrGes, BinaryOperatorType.Equal)));
            if(relatedAdresse == null)
            {
                createAdresseSimpel(workingAdresse);
            }
           
            relatedAdresse = curSession.FindObject<boAdresse>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("FremdsystemId", workingAdresse.AdrNrGes, BinaryOperatorType.Equal)));

            using (uow)
            {
               Debitorenkonto curKunde = new Debitorenkonto(uow);
               //gibt es hierfür schon einen Adresseintrag?

                curKunde.DebitorenNummer = workingAdresse.DebitKreditNr.ToString();
                curKunde.FremdsystemId = workingAdresse.AdrNrGes;
                curKunde.FremdsystemCode = workingAdresse.AdrNrA;
                curKunde.Adresse = uow.GetObjectByKey<boAdresse>(relatedAdresse.Oid);

                curKunde.Mandant = workingMandant;
                
            
                curKunde.Save();
                uow.CommitChanges();
            }
        }
        


        
        public static void processDebitoren()
        {
            writeToLogStart("Aufruf der Funktion processDebitoren");
          
                Session curSession = HauptsystemHelper.GetNewSession();
                using (curSession)
                {
                    //erst mal den Mandanten finden  

                    boMandant curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));
                    if (curMandant != null)
                    {
                        writeToLogSimpel(String.Format("Debitoren für den Mandanten: {0} schreiben", curMandant.Mandantenname));
                        if (lstDebitoren != null)
                        {
                            //alle durchgehen und feststellen welche es in der Fi noch nicht gibt
                            //hier brauch cih die Session mit dem normalen ConnectionString

                            //Session KwpXpoSession = XpoHelper.GetNewSession();

                           
                            //den Kunden suchen
                            for (int i = 0; i < lstDebitoren.Count(); i++)
                            {
                                clsKwpAdresse item = lstDebitoren[i];
                                Debitorenkonto curKunde = curSession.FindObject<Debitorenkonto>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("DebitorenNummer", item.DebitKreditNr, BinaryOperatorType.Equal)));
                            if (curKunde == null)
                            {
                                createDebitorenkonto(item);

                            }
                            else {
                                updateDebitorenkonto(curKunde, item);
                            }

                           
                            }
                        }
                    }
                }
            
            }
        #endregion

        public static void processKreditoren()
        {

        }


        public static void readKwpData(string tablename)
        {
            writeToLogStart(String.Format("Ausführung readKwpData mit Parameter {0}",tablename));

            //die Adressen aus dem View auslesen und 
            //1. Liegenschaften
            //das SQl command für die Adressen bauen 1. nur View und dannabgleichen ob die Adresse als Liegenschaft vorhanden ist
            var sqlCommand = string.Empty;

            
            Session curSession = FremdsystemHelper.GetNewSession();
            using (curSession)
            {
              
                writeToLogSimpel(String.Format("ConnectionString: {0}", curSession.ConnectionString));
                SelectedData adressResult;

                switch (tablename)
                {
                    case "qry_Liegenschaften":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstLiegenschaften == null)
                        {
                            lstLiegenschaften = new List<clsKwpAdresse>();
                        }
                        else
                        {
                            lstLiegenschaften.Clear();
                        }

                        sqlCommand = string.Format("Select AdrNrGes,Name,Objektnummer,Selekt2,AdrNrA from {0}", tablename);
                        writeToLogSimpel(String.Format("ConnectionString: {0}", curSession.ConnectionString));
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        //Name der Liegenschaft = Firmenname der Adresse  -> das Krieg ich über die Debitorennummer geregelt

                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            //die Liste befüllenp
                            clsKwpAdresse curAdresse = new clsKwpAdresse();
                            curAdresse.AdrNrGes = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;
                            writeToLogStart(string.Format("AdrNrGes {0}", curAdresse.AdrNrGes));
                            curAdresse.Name1 = (row.Values[1] != null) ? row.Values[1].ToString() : string.Empty;
                            writeToLogSimpel(string.Format("Name1 {0}", curAdresse.Name1));
                            curAdresse.Objektnummer = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            writeToLogEnd(string.Format("Objektnummer {0}", curAdresse.Objektnummer));
                            // eine neue Liegenschaft -> eine Adresse und einen Debitor erstellen

                          
                            curAdresse.strSelekt2 = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;
                            writeToLogEnd(string.Format("Selekt2 {0}", curAdresse.strSelekt2));
                            curAdresse.AdrNrA = (row.Values[4] != null) ? row.Values[4].ToString() : string.Empty;
                            writeToLogEnd(string.Format("AdrnrA {0}", curAdresse.AdrNrA));
                            curAdresse.Adresstyp = clsKwpAdresse.AdressTypFI.Liegenschaft;

                            //in die Liste der Liegenschaften 
                            lstLiegenschaften.Add(curAdresse);
                        }
                        
                       
                        curSession.Dispose();
                        break;
                    case "qry_HausVerwalter":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstHausverwalter == null)
                        {
                            lstHausverwalter = new List<clsKwpAdresse>();
                        }
                        else
                        {
                            lstHausverwalter.Clear();
                        }
                    
                        sqlCommand = string.Format("Select AdrNrGes,Name,AdrNrA,Selekt1,Selekt2 from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            //die Liste befüllen
                            clsKwpAdresse curAdresse = new clsKwpAdresse();                          
                            curAdresse.AdrNrGes = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;
                            curAdresse.Name1 = (row.Values[1] != null) ? row.Values[1].ToString() : string.Empty;
                            curAdresse.AdrNrA = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            curAdresse.strSelekt1 = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;
                            curAdresse.strSelekt2 = (row.Values[4] != null) ? row.Values[4].ToString() : string.Empty;
                            curAdresse.Adresstyp = clsKwpAdresse.AdressTypFI.Hausverwalter;

                            //in die Liste der Liegenschaften 
                            lstHausverwalter.Add(curAdresse);
                        }
                        
                        break;
                        
                    case "qry_Hausbetreuer":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstHausbetreuer == null)
                        {
                            lstHausbetreuer = new List<clsKwpAdresse>();
                        }
                        else
                        {
                            lstHausbetreuer.Clear();
                        }

                        sqlCommand = string.Format("Select AdrNrGes,Name,AdrNrA,Selekt1,Selekt2 from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            //die Liste befüllen
                            clsKwpAdresse curAdresse = new clsKwpAdresse();
                            curAdresse.AdrNrGes = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;
                            curAdresse.Name1 = (row.Values[1] != null) ? row.Values[1].ToString() : string.Empty;
                            curAdresse.AdrNrA = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            curAdresse.strSelekt1 = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;
                            curAdresse.strSelekt2 = (row.Values[4] != null) ? row.Values[4].ToString() : string.Empty;
                            curAdresse.Adresstyp = clsKwpAdresse.AdressTypFI.Hausbetreuer;

                            //in die Liste der Liegenschaften 
                            lstHausbetreuer.Add(curAdresse);
                        }

                        break;

                    case "qry_Debitoren":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if(lstDebitoren == null)
                        {
                            lstDebitoren = new List<clsKwpAdresse>();
                        }
                        else
                        {
                            lstDebitoren.Clear();
                        }
                        sqlCommand = string.Format("Select AdrNrGes,Debitorennummer,Land,PLZ,Ort,Strasse,Vorname,Name,Zusatz,AdrNrA from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            //die Liste befüllen
                            clsKwpAdresse curAdresse = new clsKwpAdresse();
                            curAdresse.AdrNrGes = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;
                            curAdresse.DebitKreditNr = (row.Values[1] != null) ? double.Parse(row.Values[1].ToString()) :0;
                            curAdresse.Land= (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            curAdresse.Plz = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;
                            curAdresse.Ort = (row.Values[4] != null) ? row.Values[4].ToString() : string.Empty;
                            curAdresse.Strasse = (row.Values[5] != null) ? row.Values[5].ToString() : string.Empty;

                            curAdresse.Vorname = (row.Values[6] != null) ? row.Values[6].ToString() : string.Empty;                            
                            curAdresse.Name1 = (row.Values[7] != null) ? row.Values[7].ToString() : string.Empty;
                            curAdresse.Zusatz = (row.Values[8] != null) ? row.Values[8].ToString() : string.Empty;
                            curAdresse.AdrNrA = (row.Values[9] != null) ? row.Values[9].ToString() : string.Empty;
                            writeToLogEnd(string.Format("AdrnrA {0}", curAdresse.AdrNrA));

                            //in die Liste der Liegenschaften 
                            lstDebitoren.Add(curAdresse);
                        }
                       
                        break;
                    case "qry_Kreditoren":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstKreditoren == null)
                        {
                            lstKreditoren = new List<clsKwpAdresse>();
                        }
                        else
                        {
                            lstKreditoren.Clear();
                        }
                        sqlCommand = string.Format("Select AdrNrGes,Kreditorennummer,Land,PLZ,Ort,Strasse,Vorname from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            //die Liste befüllen
                            clsKwpAdresse curAdresse = new clsKwpAdresse();
                            curAdresse.AdrNrGes = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;
                            curAdresse.DebitKreditNr = (row.Values[1] != null) ? double.Parse(row.Values[1].ToString()) : 0;
                            curAdresse.Land = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            curAdresse.Plz = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;
                            curAdresse.Ort = (row.Values[4] != null) ? row.Values[4].ToString() : string.Empty;
                            curAdresse.Strasse = (row.Values[5] != null) ? row.Values[5].ToString() : string.Empty;
                            curAdresse.AdrNrA = (row.Values[6] != null) ? row.Values[6].ToString() : string.Empty;
                            writeToLogEnd(string.Format("AdrnrA {0}", curAdresse.AdrNrA));
                            //in die Liste der Liegenschaften 
                            lstDebitoren.Add(curAdresse);
                        }
                       
                        break;

                    case "qry_Adressen":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstAdressen == null)
                        {
                            lstAdressen = new List<clsKwpAdresse>();
                        }
                        else
                        {
                            lstAdressen.Clear();
                        }
                        sqlCommand = string.Format("Select AdrNrGes,Land,PLZ,Ort,Strasse,Vorname,Name,Zusatz,AdrNrA from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            //die Liste befüllen
                            clsKwpAdresse curAdresse = new clsKwpAdresse();
                            curAdresse.AdrNrGes = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;
                            
                            curAdresse.Land = (row.Values[1] != null) ? row.Values[1].ToString() : string.Empty;
                            curAdresse.Plz = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            curAdresse.Ort = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;
                            curAdresse.Strasse = (row.Values[4] != null) ? row.Values[4].ToString() : string.Empty;

                            curAdresse.Vorname = (row.Values[5] != null) ? row.Values[5].ToString() : string.Empty;
                            curAdresse.Name1 = (row.Values[6] != null) ? row.Values[6].ToString() : string.Empty;
                            curAdresse.Zusatz = (row.Values[7] != null) ? row.Values[7].ToString() : string.Empty;

                            curAdresse.AdrNrA = (row.Values[8] != null) ? row.Values[8].ToString() : string.Empty;


                            //in die Liste der Liegenschaften 
                            lstAdressen.Add(curAdresse);
                        }

                        break;


                    case "qry_KwpWartVertrag":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstkwpVertraege == null)
                        {
                            lstkwpVertraege = new List<clsKwpWartVertrag>();
                        }
                        else
                        {
                            lstkwpVertraege.Clear();
                        }
                        sqlCommand = string.Format("Select Vertragsnummer,AnlagenNr,Bezeichnung,Text,Datum,Beginn,Ende,VertragZurueck,AnlagenAdr,KuendDatum,KuendigungsGrund from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            //die Liste befüllen
                            clsKwpWartVertrag curVertrag = new clsKwpWartVertrag();

                            curVertrag.Vertragsnummer = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;

                            curVertrag.AnlagenNr = (row.Values[1] != null) ? row.Values[1].ToString() : string.Empty;
                            curVertrag.Bezeichnung = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            curVertrag.Text = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;

                            curVertrag.Datum = (row.Values[4] != null) ? (DateTime)row.Values[4] : DateTime.MinValue;

                            curVertrag.Beginn = (row.Values[5] != null) ? (DateTime)row.Values[5] : DateTime.MinValue;
                            curVertrag.Ende = (row.Values[6] != null) ? (DateTime)row.Values[6]: DateTime.MinValue;
                            curVertrag.VertragZurueck = Convert.ToInt32(row.Values[7]); 

                            curVertrag.AnlagenAdr = (row.Values[8] != null) ? row.Values[8].ToString() : string.Empty;

                            curVertrag.KuendigungsDatum = (row.Values[9] != null) ? (DateTime)row.Values[9] : DateTime.MinValue;
                            curVertrag.KuendigungsGrund = (row.Values[10] != null) ? row.Values[10].ToString() : string.Empty;


                            //in die Liste der Liegenschaften 
                            lstkwpVertraege.Add(curVertrag);
                        }

                        break;

                    case "WartAuftraege":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstkwpVertraege == null)
                        {
                            lstkwpVertraege = new List<clsKwpWartVertrag>();
                        }
                        else
                        {
                            lstkwpVertraege.Clear();
                        }
                        sqlCommand = string.Format("Select AuftragsNr,AnlagenNr,Betreff,Hauptmonteur,Monteur,Anlagedatum,TerminDatum,TerminUhrZeit from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            //die Liste befüllen
                            clsKwpWartAuftrag curWartAuftrag = new clsKwpWartAuftrag();

                            curWartAuftrag.AuftragsNummerKwp = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;

                            curWartAuftrag.KwpAnlagenNummer = (row.Values[1] != null) ? row.Values[1].ToString() : string.Empty;
                            curWartAuftrag.Betreff = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            curWartAuftrag.Hauptmonteur = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;

                            curWartAuftrag.Monteuer = (row.Values[4] != null) ? row.Values[4].ToString() : string.Empty;

                            curWartAuftrag.Anlagedatum = (row.Values[5] != null) ? (DateTime)row.Values[5] : DateTime.MinValue;
                            curWartAuftrag.TerminDatum = (row.Values[6] != null) ? (DateTime)row.Values[6] : DateTime.MinValue;
                            curWartAuftrag.TerminZeit = (row.Values[7] != null) ? (DateTime)row.Values[7] : DateTime.MinValue;

                            //in die Liste der Liegenschaften 
                            lstKwpWartAuftrag.Add(curWartAuftrag);
                        }

                        break;

                    case "qry_WartTermine":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstKwpWartTermine == null)
                        {
                            lstKwpWartTermine = new List<clsWartTermin>();
                        }
                        else
                        {
                            lstKwpWartTermine.Clear();
                        }
                        sqlCommand = string.Format("Select TerminKey,lfdNr,AnlagenNr,MonatKW,WartungsJahr,Interval,IntervallArt,InfoText,TerminDatum,TerminUhrzeit,Monteur,HauptMonteur,PlanStunden from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];

                            //die Liste befüllen
                            clsWartTermin curTermin = new clsWartTermin();
                            curTermin.TerminKey = row.Values[0].ToString();
                            curTermin.lfdNr = Convert.ToInt32(row.Values[1]);

                            curTermin.AnlagenNr = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            
                            curTermin.MonatKw = Convert.ToInt32(row.Values[3]);
                            curTermin.WartungsJahr = Convert.ToInt32(row.Values[4]);
                            curTermin.Intervall = Convert.ToInt32(row.Values[5]);
                            curTermin.IntervallArt = Convert.ToInt32(row.Values[6]);
                            curTermin.InfoText = (row.Values[7] != null) ? row.Values[7].ToString() : string.Empty;
                            curTermin.TerminDatum = (row.Values[8] != null) ? (DateTime)row.Values[8] : DateTime.MinValue;
                            curTermin.TerminUhrzeit = (row.Values[9] != null) ? (DateTime)row.Values[9] : DateTime.MinValue;
                            curTermin.Monteur = (row.Values[10] != null) ? row.Values[10].ToString() : string.Empty;
                            curTermin.HauptMonteur = (row.Values[11] != null) ? row.Values[11].ToString() : string.Empty;
                            curTermin.PlanStunden = Convert.ToDecimal(row.Values[12]);

                            //in die Liste der Liegenschaften 
                            lstKwpWartTermine.Add(curTermin);
                        }

                        break;
                    case "WartAnlagen":
                        writeToLogSimpel(string.Format("Verarbeitung Tabelle {0}", tablename));
                        if (lstKwpWartAnlagen == null)
                        {
                            lstKwpWartAnlagen = new List<clsKwpWartAnlage>();
                        }
                        else
                        {
                            lstKwpWartAnlagen.Clear();
                        }
                        sqlCommand = string.Format("Select AnlagenNr,AnlagenAdr,HausverwalterAdr,Bezeichnung,InfoText,Brennstoffart,AnlagenOrt,Monteur,Bemerkungen,Selekt,HausmeisterAdr from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for (int r = 0; r < adressResult.ResultSet[0].Rows.Count(); r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];

                            //die Liste befüllen
                            clsKwpWartAnlage curAnlage = new clsKwpWartAnlage();
                            curAnlage.AnlagenNr = (row.Values[0] != null) ? row.Values[0].ToString() : string.Empty;
                            curAnlage.AnlagenAdr = (row.Values[1] != null) ? row.Values[1].ToString() : string.Empty;
                            curAnlage.HausverwalterAdr = (row.Values[2] != null) ? row.Values[2].ToString() : string.Empty;
                            curAnlage.Bezeichnung = (row.Values[3] != null) ? row.Values[3].ToString() : string.Empty;
                            curAnlage.InfoText = (row.Values[4] != null) ? row.Values[4].ToString() : string.Empty;
                            curAnlage.Brennstoffart = (row.Values[5] != null) ? row.Values[5].ToString() : string.Empty;
                            curAnlage.AnlagenOrt = (row.Values[6] != null) ? row.Values[6].ToString() : string.Empty;
                            curAnlage.Monteur = (row.Values[7] != null) ? row.Values[7].ToString() : string.Empty;
                            curAnlage.Bemerkungen = (row.Values[8] != null) ? row.Values[8].ToString() : string.Empty;
                            curAnlage.Selekt = (row.Values[9] != null) ? row.Values[9].ToString() : string.Empty;
                            curAnlage.HausmeisterAdr = (row.Values[10] != null) ? row.Values[10].ToString() : string.Empty;
                            lstKwpWartAnlagen.Add(curAnlage);
                        }

                        break;
                    case "qry_BrennstoffArt":
                        if (lstBrennstoffArt == null)
                        {
                            lstBrennstoffArt = new List<String>();
                        }
                        else
                        {
                            lstBrennstoffArt.Clear();
                        }
                        sqlCommand = string.Format("Select * from {0}", tablename);
                        adressResult = curSession.ExecuteQuery(sqlCommand);
                        for(int r=0;r<adressResult.ResultSet[0].Rows.Count();r++)
                        {
                            SelectStatementResultRow row = adressResult.ResultSet[0].Rows[r];
                            lstBrennstoffArt.Add((row.Values[0] != null?row.Values[0].ToString():string.Empty));
                        }
                        break;
                    default:
                        curSession.Dispose();

                        break;
                }
                curSession.Dispose();
            }
            writeToLogEnd("KWP-Daten gelesen");
        }
            }
}
