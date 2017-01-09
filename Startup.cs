using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PSO.Startup))]
namespace PSO
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
