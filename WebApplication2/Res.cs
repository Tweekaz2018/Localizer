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
        private const string defaultCulture = "en";

        private Dictionary<string, List<Resource>> _res = new Dictionary<string, List<Resource>> ()
        {
            {"Required", new List<Resource>(){new Resource("rus","Требуется"), new Resource("eng", "Required") } },
            {"Login" , new List<Resource>(){new Resource("rus","Логин"), new Resource("eng", "Login") } },
            {"Password", new List<Resource>(){new Resource("rus","Пароль"), new Resource("eng", "Password") } },
            {"Not valid", new List<Resource>(){new Resource("rus","Не подходит"), new Resource("eng", "Not valid") } },
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

        public LocalizedString this[string name, params object[] arguments] => this[name];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }   
    }

    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return CreateStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return CreateStringLocalizer();
        }

        private IStringLocalizer CreateStringLocalizer()
        {
            return new Localizer();
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