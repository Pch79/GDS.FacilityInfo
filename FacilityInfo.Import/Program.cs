using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Import.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;
using System.Data;
using System.Data.OleDb;

namespace FacilityInfo.Import
{
    class Program
    {

        public static System.String logDirectory = string.Empty;
        public static System.String currentLogFile = string.Empty;
        public static CultureInfo currentCulture = new CultureInfo("de-DE");

        public static String curMandantKennung = string.Empty;
        public static boMandant curMandant = null;

        public static String curDocumentTypeKuerzel = string.Empty;
        public static importDokumentTyp curDocumentTyp = null;

        public static List<FileInfo> lstFileInfo = new List<FileInfo>();
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                //hier muss angegeben werden für welchen Mandanten das ganze ausgeführt wird
                

                //Argument 1 ist der Mandant
                curMandantKennung = args[0];

                //Argument 2 ist der Dokumenttyp der importiert werden soll
                if(args[1] != null)
                {
                    curDocumentTypeKuerzel = args[1];
                    initAppSettings();
                    readFileList();
                }

                //gibt es Mandant und Dokumenttyp?
                //prüfen ob es den Mandanten gibt

               
                //wenn ich den Mandanten habe und keine weiter Angabe???
                //Mandant und zweites Argument ??

            }
            else
            {
                curMandantKennung = "MAHA";
                curDocumentTypeKuerzel = "WartungTWA";
                initAppSettings();
                readFileList();
            }
        }

        public static void raiseError(string fehlerText)
        {

        }

            private static void initAppSettings()
            {
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
                logFileName = String.Format("LOG_{0}_{1}", appName, DateTime.Now.ToString("ddMMyyyyHHmm"));// ToShortDateString(), DateTime.Now.ToLongTimeString());
                fullLogFileName = String.Format("{0}\\{1}.txt", logDirectory, logFileName);
                currentLogFile = fullLogFileName;


                //curMandant = uow.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", Mandant, BinaryOperatorType.Equal));

                //Session workingSession = XpoHelper.GetNewSession(curMandantName);/// = HauptsystemHelper.GetNewSession();
                Session curSession = HauptsystemHelper.GetNewSession();

                if (curSession != null)
                {
                   
                    using (curSession)
                    {
                        //UnitOfWork uow = new UnitOfWork(curSession.DataLayer);
                        curMandant = curSession.FindObject<boMandant>(new BinaryOperator("Mandantenkennung", curMandantKennung, BinaryOperatorType.Equal));

                        if (curMandant != null)
                        {
                            //curSession = workingSession;
                            writeToLogSimpel(String.Format("Mandant {0} gültig", curMandant.Mandantenkennung));
                        //dann prüfen ob es für deisenMandanten den angegebnen Dokumenttyasp gibt
                        curDocumentTyp = curSession.FindObject<importDokumentTyp>(new GroupOperator(new BinaryOperator("Mandant.Oid", curMandant.Oid, BinaryOperatorType.Equal), new BinaryOperator("Kuerzel", curDocumentTypeKuerzel, BinaryOperatorType.Equal)));
                        if(curDocumentTyp != null)
                        {
                            writeToLogSimpel(String.Format("Dokumenttyp {0} gefunden.", curDocumentTyp.Bezeichnung));
                        }
                        else
                        {
                            writeToLogSimpel(String.Format("Dokumenttyp {0} für den Mandanten {1} nicht gefunden.", curDocumentTypeKuerzel, curMandant.Mandantenname));
                        }
                    }
                        else
                        {
                            writeToLogSimpel(String.Format("Mandant {0} nicht gefunden", curMandantKennung));
                        }                 
                    }
                }
                else
                {
                    writeToLogStart(String.Format("Sesion für Mandant {0} kann nicht erstellt werden", curMandantKennung));
                }
                curSession.Dispose();
            }


        private static void processFileList()
        {
            if(lstFileInfo != null)
            {
                for(int i=0;i<lstFileInfo.Count;i++)
                {
                    processSingleFile(lstFileInfo[i]);
                }
            }
            else
            {
                writeToLog("Keine Dateien zum verarbeiten in der Liste");
            }
        }

        private static void processSingleFile(FileInfo currentFile)
        {
            //was ist das für ein File?
            //passt die Endung zum aktuellen Objekt

            //Excel als DataSet einlesen da keinExcel installeirt ist auf dem kasten
            DataTable result = null;
            switch (currentFile.Extension)
            {
                case ".xlsx":
                     result = ReadExcelFile("WI_TW_HZG",currentFile.FullName);
                    processExcelResult(result);
                    break;

                case ".xls":
                    result = ReadExcelFile("WI_TW_HZG", currentFile.FullName);
                    processExcelResult(result);
                    break;

                case ".xlsm":
                    //result = GetExcel2010DataSet(currentFile, true);
                    result = ReadExcelFile("WI_TW_HZG", currentFile.FullName);
                    processExcelResult(result);

                    break;
            }
            
        }

        private static void processExcelResult(DataTable curDataSet)
        {
            //jetzt das Restultat der Excel Liste verasrbeiten
            //den aktuellen Dokumenttyp hab ich ja global
            //1. Feststellen welche Obejkte eingelesen werden sollen
            List<importDokumentObjekt> lstImportObjekte = curDocumentTyp.lstDokumentObjekte.Where(t => t.ImportAktiv == true).ToList<importDokumentObjekt>();
            if(lstImportObjekte != null)
            {
                //dann die einzelnen Objekte verarbeiten
                //jedes Objekt hat oimportProperteis
                for (int i = 0; i < lstImportObjekte.Count; i++)
                {
                    //alle durchgehen und dann die properties setzen
                    //diese müssen auch auf aktiv sein
                    var curImportObjekt = lstImportObjekte[i];
                    Type curImportObjektType = curImportObjekt.ImportObjekt.ObjektTyp;
                    //der Objekttyp -> Bsp Liegenschaft

                }
            }


            //throw new Exception(curDataSet.Rows.Count.ToString());
        }

        private static DataTable ReadExcelFile(string sheetName, string path)
        {

            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = path;
                string fileExtension = Path.GetExtension(Import_FileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                if (fileExtension == ".xlsm")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "$]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

    

        private static void readFileList()
        {
            //den Orderne aus der Definition suchen und die Dateien einlesen
            writeToLogSimpel(String.Format("Verzeichnis einlesen: {0}", curDocumentTyp.ImportDirectory.FullName));
            DirectoryInfo di = new DirectoryInfo(curDocumentTyp.ImportDirectory.FullName);
            var pattern = string.Empty;
            if(di.Exists)
            {
                try
                {
                    //hab ich eine Extension zum Eingrenzen
                    if (curDocumentTyp.FileType != null)
                    {
                        pattern = curDocumentTyp.FileType.Extension;
                    }


                    foreach (FileInfo item in di.GetFiles())
                    {
                            lstFileInfo.Add(item);
                        writeToLogSimpel(String.Format("Datei {0} hinzugefügt", item.Name));                        
                    }
                }
                catch(Exception ex)
                {
                    writeToLog("Fehler beim Einlesen der Dateien");
                    writeToLogSimpel(ex.Message);
                    if(ex.InnerException != null)
                    {
                        writeToLogSimpel(ex.InnerException.Message);
                    }
                }
                processFileList();
            }
            else
            {
                writeToLog(String.Format("Verzeichnis {0} nicht gefunden", curDocumentTyp.ImportDirectory.FullName));
            }      
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


        }
    }
