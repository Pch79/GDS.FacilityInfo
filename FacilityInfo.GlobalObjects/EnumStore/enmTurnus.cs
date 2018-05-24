using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace FacilityInfo.GlobalObjects.EnumStore
{
    public enum enmTurnus
    {

        [ImageName("")]
        [DisplayName("none")]
        none = 0,

        [ImageName("")]
        [DisplayName("Monate")]
        monate = 1,

        [ImageName("")]
        [DisplayName("Kalenderwochen")]
        Kalenderwochen = 2,

        [ImageName("")]
        [DisplayName("Tage")]
        Tage = 3,

        [ImageName("")]
        [DisplayName("Jahre")]
        Jahre = 4,

        [ImageName("")]
        [DisplayName("Stunden")]
        Stunden = 5

       

       

        
    }
}
