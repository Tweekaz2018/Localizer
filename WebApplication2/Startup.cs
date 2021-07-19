using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

[assembly: OwinStartup(typeof(WebApplication2.Startup))]
namespace WebApplication2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolver);
            Res.Localizer = resolver.GetService<IStringLocalizer>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalizationServices();
            services.AddMvc().AddViewLocalization()
                .AddDataAnnotationsLocalization();
        }
    }
}