using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.EnumStore
{
    public enum  enmPrioritaet
    {
        [ImageName("bullet_white_16")]
        [XafDisplayName("keine")]
        none=0,
        [ImageName("bullet_orange_16")]
        [XafDisplayName("niedrig")]
        niedrig =1,
        [ImageName("bullet_yellow_16")]
        [XafDisplayName("normal")]
        normal =2,
        [ImageName("bullet_purple_16")]
        [XafDisplayName("hoch")]
        hoch =3,
        [ImageName("bullet_red_16")]
        [XafDisplayName("Sofort!")]
        sofort =4
    }
}
