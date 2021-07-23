using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication2.Attributes
{
    public class LocalizedRegularExpressionAttribute : RegularExpressionAttribute
    {
        public LocalizedRegularExpressionAttribute(string pattern) : base(pattern) {; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(Res.Localizer[ErrorMessage]);
            if (Regex.IsMatch((string)value, Pattern)) return ValidationResult.Success;
            else return new ValidationResult(Res.Localizer[ErrorMessage]);
        }
    }
}