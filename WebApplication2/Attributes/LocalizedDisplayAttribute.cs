using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Attributes
{
    public class LocalizedDisplayAttribute : Attribute
    {
        public LocalizedDisplayAttribute() { ; }
        public string Name { get; set; }
        public string GetName()
        {
            return Res.Localizer[Name];
        }
    }
}