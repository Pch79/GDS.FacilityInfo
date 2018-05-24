using System.Web;
using System.Web.Mvc;

namespace GDS_FacilityInfo_Portal {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}