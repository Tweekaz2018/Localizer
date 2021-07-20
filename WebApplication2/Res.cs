using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApplication2
{
    public static class Res
    {
        public static IStringLocalizer Localizer { get; set; }
    }
    public class Resource
    {
        public string Code { get; set; }
        public string Value { get; set; }

        public Resource(string code, string value)
        {
            Code = code;
            Value = value;
        }
    }
    public class Localizer : IStringLocalizer
    {
        private const string defaultCulture = "ru";

        private Dictionary<string, List<Resource>> _res = new Dictionary<string, List<Resource>> ()
        {
            {"Required", new List<Resource>(){new Resource("ru","Требуется"), new Resource("en", "Required") } },
            {"Login" , new List<Resource>(){new Resource("ru","Логин"), new Resource("en", "Login") } },
            {"Password", new List<Resource>(){new Resource("ru","Пароль"), new Resource("en", "Password") } },
        };

        public LocalizedString this[string name]
        {
            get
            {
                var culture = Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName;
                var hasRes = _res.TryGetValue(name, out var listOfResources);
                if (hasRes)
                {
                    var resource = _res[name].FirstOrDefault(x => x.Code == culture);
                    if (resource == default)
                        resource = _res[name].FirstOrDefault(x => x.Code == defaultCulture);

                    return new LocalizedString(name, resource.Value);
                }
                else
                    return new LocalizedString(name, string.Empty);
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