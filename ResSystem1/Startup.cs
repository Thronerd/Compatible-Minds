using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ResSystem1.Startup))]
namespace ResSystem1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
