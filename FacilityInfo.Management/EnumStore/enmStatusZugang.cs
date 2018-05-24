using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.EnumStore
{
    public enum  enmStatusZugang
    {
        [ImageName("bullet_white_16")]
        [DisplayName("k.A.")]
        keineAngabe = 0,

        [ImageName("bullet_red_16")]
        [DisplayName("nicht Ok")]
        nichtOK = 1,

        [ImageName("bullet_green_16")]
        [DisplayName("OK")]
        Ok = 2


    }
}
