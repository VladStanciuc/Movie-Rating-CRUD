using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(proiect.Startup))]
namespace proiect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
