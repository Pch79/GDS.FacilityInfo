using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;

namespace FacilityInfo.GlobalObjects.EnumStore
{
    public enum enmServiceStatus
    {

        [XafDisplayName("offen")]
        offen = 0,
        [XafDisplayName("erledigt")]
        erledigt = 1,
        [XafDisplayName("zurückgestellt")]
        zurueckgestellt = 2,
        [XafDisplayName("fällig")]
        faellig = 2,


    }
}
