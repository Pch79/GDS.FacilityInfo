using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.KwpSynch
{
    class clsKwpWartAnlage
    {
       public String AnlagenNr { get; set; }
        public String AnlagenAdr { get; set; }
        public String HausverwalterAdr { get; set; }
        public String Bezeichnung { get; set; }

        public String AnlagenOrt { get; set; }
        public String Monteur { get; set; }
        //Brennstoffart == Anlagenart
        public String Brennstoffart { get; set; }
        public String InfoText { get; set; }
        public String Bemerkungen { get; set; }
        public String Selekt { get; set; }

        public string HausmeisterAdr { get; set; }


    }
}
