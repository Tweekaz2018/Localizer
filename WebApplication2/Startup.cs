using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Globalization;
using Ninject.Modules;
using WebApplication2.Util;
using Ninject;
using Ninject.Web.Mvc;

[assembly: OwinStartup(typeof(WebApplication2.Startup))]
namespace WebApplication2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            var services = new ServiceCollection();
            //var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            //DependencyResolver.SetResolver(resolver);
            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            ConfigureServices(services);
            Res.Localizer = kernel.GetService<IStringLocalizer>();

            var loc = kernel.GetService<IStringLocalizer>();
        }

        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
            services.AddTransient<IStringLocalizer, Localizer>();

            var mvc = services.AddMvc();
            mvc.AddViewLocalization()
                .AddDataAnnotationsLocalization(x => 
                    x.DataAnnotationLocalizerProvider = (y, factory) => factory.Create(null)
                );
            var stringLocalizer = services.FirstOrDefault(x => x.ServiceType == typeof(IStringLocalizer));
            var stringLocalizerFactory = services.FirstOrDefault(x => x.ServiceType == typeof(IStringLocalizerFactory));
            services.Remove(stringLocalizer);
            services.Remove(stringLocalizerFactory);
            services.AddLocalizationServices();
        }
    }
}