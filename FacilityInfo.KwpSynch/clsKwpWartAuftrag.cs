using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.KwpSynch
{
    class clsKwpWartAuftrag
    {
        //TODO: Anlagenart, Hausverwaltung, Intervall , Nächstes Wartungsdatum bitte mit einfügen
        

        #region Properties
        public String AuftragsNummerKwp { get; set; }
        public String FremdsystemId { get; set; }

        public String KwpAnlagenNummer { get; set; }
        public String Betreff { get; set; }
        public String Hauptmonteur { get; set; }
        public String Monteuer { get; set; }
        public Decimal Planstunden { get; set; }
        public DateTime Anlagedatum { get; set; }
        public DateTime TerminDatum { get; set; }
        public DateTime TerminZeit { get; set; }

        
        #endregion 

       public clsKwpWartAuftrag()
       {
         
       }
    }
}
