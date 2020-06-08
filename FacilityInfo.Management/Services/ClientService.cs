using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using FacilityInfo.Core.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;
using FacilityInfo.Management.Interfaces;

namespace FacilityInfo.Management.Services
{
    public class ClientService : IClientService
    {
        public boMandant getClientByUserName(string userName, Session workingSession)
        {
            boMandant retVal = null;
            PermissionPolicyUser curUser = workingSession.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", userName, BinaryOperatorType.Equal));

            corePortalAccount curPortalAccount = workingSession.FindObject<corePortalAccount>(new BinaryOperator("SystemBenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));
            if (curPortalAccount != null)
            {
                //den hausverwalter rausfinden
                boHausverwalter curHausverwalter = workingSession.FindObject<boHausverwalter>(new BinaryOperator("Oid", curPortalAccount.HausVerwalter.Oid, BinaryOperatorType.Equal));
                retVal = curHausverwalter.Mandant;
            }

            boMitarbeiter curMitarbeiter = workingSession.FindObject<boMitarbeiter>(new BinaryOperator("Systembenutzer.Oid", curUser.Oid, BinaryOperatorType.Equal));

            if (curMitarbeiter != null)
            {
                retVal = curMitarbeiter.Mandant;
            }

            if (retVal == null)
            {
                boMandant curMandant = workingSession.FindObject<boMandant>(new BinaryOperator("IsDefault", "true", BinaryOperatorType.Equal));
                retVal = curMandant;
            }


            return retVal;
        }
    }
}