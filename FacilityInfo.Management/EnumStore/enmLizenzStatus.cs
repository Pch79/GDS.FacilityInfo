using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmLizenzStatus
    {
        [XafDisplayName("inaktiv")]
        inaktiv=0,
        [XafDisplayName("aktiv")]
        aktiv=1,
        [XafDisplayName("gesperrt")]
        gesperrt = 2

    }
}
