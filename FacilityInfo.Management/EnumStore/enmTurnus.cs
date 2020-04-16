using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmTurnus
    {

        [ImageName("")]
        [DisplayName("ohne")]
        none = 0,

        [ImageName("")]
        [DisplayName("Monate")]
        Monate = 1,

      

        [ImageName("")]
        [DisplayName("Tage")]
        Tage = 3,

        [ImageName("")]
        [DisplayName("Jahre")]
        Jahre = 4,

        [ImageName("")]
        [DisplayName("Stunden")]
        Stunden = 5,
            [ImageName("")]
        [DisplayName("Betriebsstunden")]
        Betriebsstunden = 6






    }
}
