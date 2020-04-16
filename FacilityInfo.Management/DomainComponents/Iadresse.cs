using DevExpress.ExpressApp.DC;
using FacilityInfo.Adresse.BusinessObjects;
using System;
using System.ComponentModel;
using System.Linq;

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
