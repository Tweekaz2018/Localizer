using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Attributes
{
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes,
              Type containerType, Func<object> modelAccessor,
              Type modelType, string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType,
                    modelAccessor, modelType, propertyName);

            if (attributes.OfType<LocalizedDisplayAttribute>().ToList().Count > 0)
            {
                modelMetadata.DisplayName = GetValueFromLocalizationAttribute(attributes.OfType<LocalizedDisplayAttribute>().ToList()[0]);
            }

            return modelMetadata;
        }

        private string GetValueFromLocalizationAttribute(LocalizedDisplayAttribute attribute)
        {
            return attribute.GetName();
        }
    }
}