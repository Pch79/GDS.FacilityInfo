using DevExpress.Xpo;
using FacilityInfo.Core.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.Interfaces
{
    public interface IClientService
    {
        boMandant getClientByUserName(string userName, Session workingSession);
    }
}
