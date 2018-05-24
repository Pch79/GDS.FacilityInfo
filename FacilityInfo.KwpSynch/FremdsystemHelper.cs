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
  
    public static class FremdsystemHelper
    {

        public static Session GetNewSession()
        {
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
            string conn = ConfigurationManager.ConnectionStrings["FremdsystemConnection"].ConnectionString;

            XPDictionary dict = new ReflectionDictionary();

            IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);


          

            IDataLayer dl = new ThreadSafeDataLayer(dict, store);
            return dl;
        }
    }

}
