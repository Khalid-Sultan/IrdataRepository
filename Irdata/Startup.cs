using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Irdata.Startup))]
namespace Irdata
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
