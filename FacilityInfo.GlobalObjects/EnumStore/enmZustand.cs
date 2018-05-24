using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace FacilityInfo.GlobalObjects.EnumStore
{
    public enum enmZustand
    {
       // [XafDisplayName("keine Zuordnung")]
        [ImageName("information_16")]
        keineZuordnung =0,
        //[XafDisplayName("dringende Sanierung")]
        [ImageName("dringende_Sanierung_16")]
        dringendeSanierung =1,
       // [XafDisplayName("individuelle Sanierung")]
        [ImageName("individuelle_Sanierung_16")]
        individuelleSanierung =2,
       // [XafDisplayName("keine Sanierung")]
        [ImageName("keine_Sanierung_16")]
        keineSanierung =3,
       // [XafDisplayName("baldige Sanierung")]
        [ImageName("baldige_Sanierung_16")]
        baldigeSanierung =4
    }
}
