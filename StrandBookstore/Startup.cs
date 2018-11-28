using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StrandBookstore.Startup))]
namespace StrandBookstore
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
