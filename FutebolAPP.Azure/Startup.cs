using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FutebolAPP.Azure.Startup))]

namespace FutebolAPP.Azure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}