using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmServiceStatus
    {
        [ImageName("bullet_white")]
        [XafDisplayName("offen")]
        offen = 0,
        [ImageName("bullet_green")]
        [XafDisplayName("erledigt")]
        erledigt = 1,
        [ImageName("bullet_black")]
        [XafDisplayName("zurückgestellt")]
        zurueckgestellt = 2,
        [ImageName("bullet_orange")]
        [XafDisplayName("fällig")]
        faellig = 3,



    }
}
