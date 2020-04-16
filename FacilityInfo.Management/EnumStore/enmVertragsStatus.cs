using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmVertragsStatus
    {
        [XafDisplayName("nicht bekannt")]
        [ImageName("bullet_white_16")]
        none =0,

        [XafDisplayName("Akquise")]
        [ImageName("bullet_blue_16")]
        akquise =1,

        [XafDisplayName("Zusage")]
        [ImageName("bullet_star_16")]
        zusage =2,

        [XafDisplayName("Freigabe")]
        [ImageName("bullet_go_16")]
        freigabe =3,

        [XafDisplayName("Aktiv")]
        [ImageName("bullet_green_16")]
        aktiv =4,

        [XafDisplayName("Pausiert")]
        [ImageName("bullet_yellow_16")]
        pausiert =5,

        [XafDisplayName("Gekündigt")]
        [ImageName("bullet_delete_16")]
        gekündigt =6,

        [XafDisplayName("beendet")]
        [ImageName("bullet_red_16")]
        beendet =7
    }
}
