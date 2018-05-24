using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum  enmMassnahmenTyp
    {
        [ImageName("table_gear")]
        Sonstige = 0,
        [ImageName("table_key")]
        Liegenschaftsmassnahme = 1,
        [ImageName("table_lightning")]
        Anlagenmassnahme = 2
        
    }
}
