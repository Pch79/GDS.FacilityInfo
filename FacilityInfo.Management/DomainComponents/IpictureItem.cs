using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using System.Drawing;


namespace FacilityInfo.Management.DomainComponents
{
    [DomainComponent]
    public interface IpictureItem
    {
        string bildtitel { get; }
        string beschreibung { get; }
        
        string id { get;  }
        Image bild { get;  }


    }
}
