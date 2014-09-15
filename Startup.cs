using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Flw.Startup))]
namespace Flw
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
