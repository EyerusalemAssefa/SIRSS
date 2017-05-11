using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIRSS.Web.Startup))]
namespace SIRSS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
