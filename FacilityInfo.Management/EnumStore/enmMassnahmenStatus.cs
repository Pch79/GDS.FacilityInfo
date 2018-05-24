using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmMassnahmenStatus
    {
        [XafDisplayName("offen")]
        [ImageName("clock_red_16")]
        offen =0,
        [XafDisplayName("erfasst")]
        [ImageName("clock_16")]
        erfasst =1,
        [XafDisplayName("in Bearbeitung")]
        [ImageName("clock_play_16")]
        inBearbeitung =2,
        [XafDisplayName("zurückgestellt")]
        [ImageName("clock_pause_16")]
        zurueckgestellt =3,
        [XafDisplayName("warten")]
        [ImageName("clock_stop_16")]
        warten =4,
        [XafDisplayName("erledigt")]
        [ImageName("accept_16")]
        erledigt = 5
    }
}
