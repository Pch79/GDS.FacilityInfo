using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmBearbeitungsStatus
    {

        [XafDisplayName("neu")]
        [ImageName("clock_red_16")]
        neu =0,
        [XafDisplayName("bestätitgt")]
        [ImageName("clock_16")]
        bestaetigt =1,
        [XafDisplayName("in Arbeit")]
        [ImageName("clock_play_16")]
        inArbeit =2,
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
