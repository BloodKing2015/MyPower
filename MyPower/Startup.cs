using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPower.Startup))]
namespace MyPower
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
