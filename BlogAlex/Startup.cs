using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogAlex.Startup))]
namespace BlogAlex
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
