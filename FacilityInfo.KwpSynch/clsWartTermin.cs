using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.KwpSynch
{
    class clsWartTermin
    {
        public String TerminKey { get; set; }
        public Int32 lfdNr { get; set; }
        public String AnlagenNr { get; set; }
        public Int32 MonatKw { get; set; }
        public Int32 WartungsJahr { get; set; }
        public Int32 Intervall { get; set; }
        public Int32 IntervallArt { get; set; }

        public String Monteur { get; set; }
        public String HauptMonteur { get; set; }
        public String InfoText { get; set; }
        public DateTime TerminDatum { get; set; }
        public DateTime TerminUhrzeit {get;set;}
        public Decimal PlanStunden { get; set; }



    }
}
