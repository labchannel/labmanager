using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabManager.Startup))]
namespace LabManager
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
