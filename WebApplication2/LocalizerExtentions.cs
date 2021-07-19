using Microsoft.Extensions.Localization;
using WebApplication2;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizerExtentions
    {
        public static IServiceCollection AddLocalizationServices(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizer, Localizer>();
            services.AddSingleton<IStringLocalizerFactory, FakeStringLocalizerFactory>();
            return services;
        }
    }
}