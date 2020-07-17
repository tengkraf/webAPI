using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IntegrationTests.Helpers
{
    public class NoIdSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var propertyInfo = request as PropertyInfo;
            if (propertyInfo != null &&
                propertyInfo.Name.Contains("Id", StringComparison.OrdinalIgnoreCase) &&
                propertyInfo.PropertyType == typeof(int))
            {
                return new OmitSpecimen();
            }

            return new NoSpecimen();
        }
    }
}
