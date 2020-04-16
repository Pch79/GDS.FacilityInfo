using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum  enmServiceTyp
    {
        [ImageName("table_gear_16")]
        Sonstige = 0,
        [ImageName("table_key_16")]
        Komponentenservice = 1,
        [ImageName("table_lightning_16")]
        Anlagenservice = 2
         
    }
}
