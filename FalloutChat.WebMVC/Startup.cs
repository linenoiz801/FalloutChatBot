using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FalloutChat.WebMVC.Startup))]
namespace FalloutChat.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
