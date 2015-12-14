using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CyberSecurityAnalyser.Startup))]
namespace CyberSecurityAnalyser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
