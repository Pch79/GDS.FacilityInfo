using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum TCondition
    {
        [XafDisplayName("keine Zuordnung")]
        [ImageName("information_16")]
        NotAssigned = 0,

        [XafDisplayName("dringende Sanierung")]
        [ImageName("dringende_Sanierung_16")]
        UrgentRenovation = 1,

        [XafDisplayName("individuelle Sanierung")]
        [ImageName("individuelle_Sanierung_16")]
        IndividualRenovation = 2,

        [XafDisplayName("keine Sanierung")]
        [ImageName("keine_Sanierung_16")]
        NoRenovation = 3,

        [XafDisplayName("baldige Sanierung")]
        [ImageName("baldige_Sanierung_16")]
        SoonRenovation = 4
    }
}