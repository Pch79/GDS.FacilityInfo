using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmKommunikationsTyp
    {
        [XafDisplayName("N/A")]
        none=0,
        [XafDisplayName("privat")]
        privat =1,
        [XafDisplayName("dienstlich")]
        dienstlich =2,
        [XafDisplayName("notfall")]
        notfall=3
            
    }
}
