using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalApplication.Web.Startup))]
namespace HospitalApplication.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
        }
    }
}
