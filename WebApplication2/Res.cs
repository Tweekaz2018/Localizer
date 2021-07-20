using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public static class Res
    {
        public static IStringLocalizer Localizer { get; set; }
    }

    public class Localizer : IStringLocalizer
    {
        private Dictionary<string, string> _res = new Dictionary<string, string>()
        {
            {"Required", "Требуется" },
            {"Login" , "Логин"},
            {"Password", "Пароль" }
        };

        public LocalizedString this[string name]
        {
            get
            {
                /*
                var culture = Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName;
                _res[name].Where(x => x.Language == culture);
                 */

                return new LocalizedString(name, _res[name]);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }   
    }

    public class FakeStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FakeStringLocalizerFactory(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }


        public IStringLocalizer Create(Type resourceSource)
        {
            return (IStringLocalizer)_serviceProvider.GetService(typeof(IStringLocalizer));
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return (IStringLocalizer)_serviceProvider.GetService(typeof(IStringLocalizer));
        }
    }
}