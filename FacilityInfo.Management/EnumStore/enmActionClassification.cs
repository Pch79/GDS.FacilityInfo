using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmActionClassification
    {
        [ImageName("")]
        [XafDisplayName("Wartung (geplant)")]
        Wartung=1,

        [ImageName("")]
        [XafDisplayName("Kontrolle")]
        Kontrolle = 2,

        [ImageName("")]
        [XafDisplayName("Probenentnahme")]
        Probenentnahme = 3,

        [ImageName("")]
        [XafDisplayName("Reparatur")]
        Reparatur = 4,

        [ImageName("")]
        [XafDisplayName("Instandsetzung")]
        Instandsetzung = 5,
        [ImageName("")]
        [XafDisplayName("Wartung (nicht geplant)")]
        WartungNichtGeplant = 6

    }
}
