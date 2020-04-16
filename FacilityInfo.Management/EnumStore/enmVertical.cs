using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmVertical
    {
        [ImageName("align_above")]
        oben =1,
        [ImageName("align_bellow")]
        unten =2,
        [ImageName("align_middle")]
        mitte =3
    }
}
