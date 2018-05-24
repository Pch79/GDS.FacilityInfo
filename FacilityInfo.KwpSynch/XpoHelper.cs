using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System.Configuration;

namespace FacilityInfo.KwpSynch
{
   
    public static class XpoHelper
    {
        private static System.String connectionString = string.Empty;

        /*
        public static Session GetNewSession()
        {
            return new Session(DataLayer);
        }
        */
        public static Session GetNewSession(string connectionName)
        {
            switch (connectionName)
            {
                case "Fremdsystem":
                    connectionString = ConfigurationManager.ConnectionStrings["KWPConnectionString"].ConnectionString;
                    break;


                case "Hauptsystem":
                    connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    break;
            }
          
            return new Session(DataLayer);
        }


        public static UnitOfWork GetNewUnitOfWork()
        {
            return new UnitOfWork(DataLayer);
        }

        private readonly static object lockObject = new object();

        static volatile IDataLayer fDataLayer;
        static IDataLayer DataLayer
        {
            get
            {
                if (fDataLayer == null)
                {
                    lock (lockObject)
                    {
                        if (fDataLayer == null)
                        {
                            fDataLayer = GetDataLayer();
                        }
                    }
                }
                return fDataLayer;
            }
        }

        private static IDataLayer GetDataLayer()
        {
            XpoDefault.Session = null;
            // string conn = Properties.Settings.Default.ConnectionString;
            //string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            XPDictionary dict = new ReflectionDictionary();           
            IDataStore store = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema);
            IDataLayer dl = new ThreadSafeDataLayer(dict, store);

            //IDataLayer dl = new SimpleDataLayer(dict, store);
            return dl;
        }
    }

}
