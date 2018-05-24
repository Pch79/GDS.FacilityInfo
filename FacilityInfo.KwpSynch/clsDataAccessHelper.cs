using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.KwpSynch
{
    public class clsDataAccessHelper
    {
        
        public string connectionName { get; set; }
        public string connectionString { get; set; }

        public clsDataAccessHelper()
        {

        }

        public clsDataAccessHelper(string connectionName)
        {
            switch (connectionName)
            {
                case "Fremdsystem":
                    this.connectionString = ConfigurationManager.ConnectionStrings["KWPConnectionString"].ConnectionString;
                    break;

                case "Hauptsystem":
                    this.connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    break;
            }
        }

        public Session GetNewSession()
        {
             
            return new Session(DataLayer);
        }

        public  UnitOfWork GetNewUnitOfWork()
        {
            return new UnitOfWork(DataLayer);
        }

        private readonly static object lockObject = new object();

        static volatile IDataLayer fDataLayer;
        public IDataLayer DataLayer
        {
            get
            {
                fDataLayer = null;
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

        private IDataLayer GetDataLayer()
        {
            XpoDefault.Session = null;
            // string conn = Properties.Settings.Default.ConnectionString;
            //string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            XPDictionary dict = new ReflectionDictionary();
            IDataStore store = XpoDefault.GetConnectionProvider(this.connectionString, AutoCreateOption.DatabaseAndSchema);
            //IDataLayer dl = new ThreadSafeDataLayer(dict, store);

            IDataLayer dl = new SimpleDataLayer(dict, store);
            return dl;
        }
    }
}
