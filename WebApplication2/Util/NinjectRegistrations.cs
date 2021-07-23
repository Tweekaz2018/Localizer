using Microsoft.Extensions.Localization;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IStringLocalizer>().To<Localizer>();
        }
    }
}