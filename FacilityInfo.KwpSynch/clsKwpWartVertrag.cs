using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.KwpSynch
{
    class clsKwpWartVertrag
    {
        #region Properties
        public System.String AnlagenNr { get; set; }
        public System.String Vertragsnummer { get; set; }
        public System.String Bezeichnung { get; set; }
        public System.String Text { get; set; }
        public System.DateTime Datum { get; set; }
        public System.DateTime Beginn { get; set; }
        public System.DateTime Ende { get; set; }
        public System.Int32 VertragZurueck { get; set; }

        public bool contractBack { get; }
    
        public System.String AnlagenAdr { get; set; }

        public String KuendigungsGrund { get; set; }
        public DateTime KuendigungsDatum { get; set; }


        #endregion

        public clsKwpWartVertrag()
        {
          
        }

        public bool ContractBack
        {
        get
        {
                bool retVal = false;
                retVal = (this.VertragZurueck == 0) ? false : true;
                return retVal;
        }
        }
    }
}
