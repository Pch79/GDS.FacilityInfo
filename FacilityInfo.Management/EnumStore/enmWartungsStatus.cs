using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmWartungsStatus
    {
        [XafDisplayName("N/A")]
        none=0,
        [XafDisplayName("Besichtigung offen")]
        BesichtigungOffen =1,
        [XafDisplayName("keine Anfrage")]
        keineAnfrage =2,
        [XafDisplayName("keine Wartung")]
        keineWartung =3,
        [XafDisplayName("keine Wartung vorhanden")]
        keineWartungVorhanden =4,
        [XafDisplayName("nicht erwünscht")]
        nichtErwünscht =5,
        [XafDisplayName("offen")]
        offen =6,
        [XafDisplayName("Verträge offen")]
        VerträgeOffen = 7,
        [XafDisplayName("Wartung komplett")]
        WartungKomplett =8,
        [XafDisplayName("Wartung teilweise")]
        WartungTeilweise =9        
    }
}
