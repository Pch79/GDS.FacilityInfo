using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.DomainComponents
{
    [DomainComponent]
    public interface IcheckEntry
    {
        boCheckItem checkItem { get; set; }
        enmCheckResult checkResult { get; set; }
        System.String checkValue { get; set; }

        [ReadOnly(true)]
        PermissionPolicyUser erfasser { get; set; }

        [ReadOnly(true)]
        DateTime erfassungsdatum { get; set; }

       


    }

    [DomainLogic(typeof(IcheckEntry))]
    public class CheckEntryLogic
    {

        


    }

}
