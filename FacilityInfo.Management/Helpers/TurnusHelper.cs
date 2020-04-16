using FacilityInfo.Management.EnumStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.Helpers
{
    public static class TurnusHelper
    {
        public static DateTime getNextDate(DateTime baseDate,enmTurnus curTurnus,Int32 curTurnusValue)
        {
            DateTime retVal = new DateTime();
            DateTime workingDate = (baseDate == DateTime.MinValue) ? DateTime.Now : baseDate;
           
            switch (curTurnus)
            {
                case enmTurnus.Stunden:
                    retVal = workingDate.AddHours(curTurnusValue);
                    break;
                case enmTurnus.Tage:
                    retVal = workingDate.AddDays(curTurnusValue);
                    break;
               
                case enmTurnus.Monate:
                    retVal = workingDate.AddMonths(curTurnusValue);
                    break;
                case enmTurnus.Jahre:
                    retVal = workingDate.AddYears(curTurnusValue);
                    break;
              
               
               
             
            }
            return retVal;
        }
    }
}
