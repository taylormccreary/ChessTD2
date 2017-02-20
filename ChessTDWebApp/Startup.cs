using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChessTDWebApp.Startup))]
namespace ChessTDWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
