using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;

namespace FacilityInfo.GlobalObjects.EnumStore
{
    public enum enmAttatchmenttyp
    {
        [XafDisplayName("Dokumente allgemein")]
        Attachment =0,
        [XafDisplayName("Liegenschaftsdokument")]
        Liegenschaft =1,
        [XafDisplayName("Anlagendokument")]
        Anlage =3,
        [XafDisplayName("Maßnahmendokument")]
        Massnahme =4,
        [XafDisplayName("Messtypdokument")]
        Mestyp = 5,
        [XafDisplayName("Messungsdokument")]
        Messung = 6,
            [XafDisplayName("Produktanlage")]
        Produktattachment = 7,
            [XafDisplayName("Herstelleranlage")]
        Herstelleranlage = 8



    }
}
