using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GDS_FacilityInfo_Portal.Startup))]

// Files related to ASP.NET Identity duplicate the Microsoft ASP.NET Identity file structure and contain initial Microsoft comments.

namespace GDS_FacilityInfo_Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}