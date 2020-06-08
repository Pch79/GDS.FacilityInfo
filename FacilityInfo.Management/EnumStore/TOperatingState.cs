using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum TOperatingState
    {
        [XafDisplayName("Aktiv")]
        [ImageName("accept_16")]
        active = 0,
        [XafDisplayName("Betriebsbereit")]
        [ImageName("accept_16")]
        ReadyToUse = 1,
        [XafDisplayName("Wartung")]
        [ImageName("wrench_orange_16")]
        Maintenance = 2,
        [XafDisplayName("Revision")]
        [ImageName("zoom_16")]
        Revision = 3,
        [XafDisplayName("Notbetrieb")]
        [ImageName("roadworks_16")]
        EmergencyOperation = 4,
        [XafDisplayName("Stillgelegt")]
        [ImageName("cross_16")]
        ShutDown = 5,
    }
}