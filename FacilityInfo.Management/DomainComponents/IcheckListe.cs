using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management.EnumStore;

namespace FacilityInfo.Management.DomainComponents
{
    [DomainComponent]
   public interface IcheckListe
    {
        [ReadOnly(true)]
        DateTime erstellungsdatum { get; set; }

        [ReadOnly(true)]
        PermissionPolicyUser ersteller { get; set; }

        enmBearbeitungsStatus status { get; set; }

        [ReadOnly(true)]
        string bezeichnung { get; set; }

        [ReadOnly(true)]
        DateTime datumFertigstellung { get; set; }

    }

  
}
