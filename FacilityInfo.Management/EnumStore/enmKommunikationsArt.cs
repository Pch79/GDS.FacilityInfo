using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.Management.EnumStore
{
    public enum enmKommunikationsArt
    {
        [XafDisplayName("N/A")]
        [Description("keine Zuordnung")]
        none=0,
        [XafDisplayName("Telefon")]
        [ImageName("phone_vintage_16")]
        [Description("Tel")]
        
        telefon =1,
        [XafDisplayName("Mobil")]
        [ImageName("phone_16")]
        [Description("Mobil")]
        mobil =2,
        [XafDisplayName("Fax")]
        [ImageName("fax_16")]
        [Description("Fax")]
        fax =3,
        [XafDisplayName("Mail")]
        [ImageName("at_sign_16")]
        [Description("Mail")]
        mail =4,
        [XafDisplayName("Skype")]
        [ImageName("skype_16")]
        [Description("Skype")]
        skype = 5,

        [XafDisplayName("Internet")]
        [ImageName("www_page_16")]
        [Description("Internet")]
        internet = 6
    }
}
