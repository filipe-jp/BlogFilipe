using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogFilipe.Startup))]
namespace BlogFilipe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
