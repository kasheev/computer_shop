using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BasaDate.Startup))]
namespace BasaDate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
