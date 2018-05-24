using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.GlobalObjects.EnumStore
{
    public enum  enmGeschlecht
    {
        [XafDisplayName("N/A")]
        [ImageName("")]
        none = 0,
        [XafDisplayName("männlich")]
        [ImageName("male_16")]
        maennlich = 1,
        [XafDisplayName("weiblich")]
        [ImageName("female_16")]
        weiblich = 2
    }
}
