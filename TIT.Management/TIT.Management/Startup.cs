using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TIT.Management.Startup))]
namespace TIT.Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
