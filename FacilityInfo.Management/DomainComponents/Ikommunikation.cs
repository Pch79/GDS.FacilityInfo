
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;

namespace FacilityInfo.Management.DomainComponents
{
    [DomainComponent]
    public interface Ikommunikation
    {
        string telefon1 { get; set; }
        string telefon2 { get; set; }
        string mobil { get; set; }
        string fax { get; set; }
        string internet { get; set; }
        string mail { get; set; }

    }
}
