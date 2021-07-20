using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;

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
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(x =>
                    x.DataAnnotationLocalizerProvider = (y, factory) => factory.Create(typeof(IStringLocalizer))
                );
            var stringLocalizer = services.FirstOrDefault(x => x.ServiceType == typeof(IStringLocalizer));
            var stringLocalizerFactory = services.FirstOrDefault(x => x.ServiceType == typeof(IStringLocalizerFactory));
            services.Remove(stringLocalizer);
            services.Remove(stringLocalizerFactory);
            services.AddLocalizationServices();
        }
    }
}