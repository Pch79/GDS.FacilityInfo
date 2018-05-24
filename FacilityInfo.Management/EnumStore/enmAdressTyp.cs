using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmAdressTyp
    {
        [ImageName("Action_About")]
        Sonstige =0,
        [ImageName("BO_Address")]
        Adresse = 1,
        [ImageName("BO_Lead")]
        Interessent = 2,
        [ImageName("BO_Customer")]
        Kunde = 3,
        [ImageName("BO_Vendor")]
        Lieferant = 4,    
        [ImageName("BO_Employee")]
        Mitarbeiter =6
        

    }
}
