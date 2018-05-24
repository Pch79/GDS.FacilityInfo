using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using FacilityInfo.Adresse.BusinessObjects;
using FacilityInfo.GlobalObjects.Helpers;
using FacilityInfo.Management.BusinessObjects;

namespace FacilityInfo.Management.DomainComponents
{
    [DomainComponent]
    public interface Iadresse
    {
       
       
        boOrt ort { get; set; }

       
        string strasse { get; set; }
        string hausnummer { get; set; }
      

        [ReadOnly(true)]
        string longitude { get; set; }
        [ReadOnly(true)]
        string latitude { get; set; }
        
        string internet { get; set; }
     
        
        //hier kann ich das ganze Locationgedönse einbauen
    }


    [DomainLogic(typeof(Iadresse))]
    public class AdressLogic
    {
        Iadresse instance;
        public AdressLogic(Iadresse instance)
        {
            
        }
      
           
    }
}
