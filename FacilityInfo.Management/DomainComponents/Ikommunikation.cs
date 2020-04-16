
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
        string Telefon1 { get; set; }
        string Telefon2 { get; set; }
        string Mobil { get; set; }
        string Fax { get; set; }
        string Internet { get; set; }
        string Mail { get; set; }

    }
}
