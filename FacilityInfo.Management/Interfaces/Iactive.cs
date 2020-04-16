using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.Interfaces
{
    [DomainComponent]
    public interface IActive
    {

          bool IsActive { get; set; }
        void Activate();
        void DeActivate();
        //die Domainlogik implementeiren
        

        
    }
    [DomainLogic(typeof(IActive))]
    public class IActiveLogic
    {

        public static void AfterConstruction(IActive active)
        {
            active.IsActive = true;
        }

        public static void Acitvate(IActive instance)
        {
            instance.IsActive = true;
        }

        public static void DeActivate(IActive instance)
        {
            instance.IsActive = false;
        }
    }
}
