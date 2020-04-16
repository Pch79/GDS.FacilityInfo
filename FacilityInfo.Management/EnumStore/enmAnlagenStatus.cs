using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmAnlagenStatus
    {

        [XafDisplayName("Aktiv")]
        [ImageName("accept_16")]
        Aktiv=0,
        [XafDisplayName("Betriebsbereit")]
        [ImageName("accept_16")]
        Betriebsbereit = 1,
        [XafDisplayName("Wartung")]
        [ImageName("wrench_orange_16")]
        Wartung = 2,
        [XafDisplayName("Revision")]
        [ImageName("zoom_16")]
        Revision = 3,
        [XafDisplayName("Notbetrieb")]
        [ImageName("roadworks_16")]
        Notbetrieb = 4,
        [XafDisplayName("Stillgelegt")]
        [ImageName("cross_16")]
        Stillgelegt = 5
        

    }
}
