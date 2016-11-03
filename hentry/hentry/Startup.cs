using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hentry.Startup))]
namespace hentry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
