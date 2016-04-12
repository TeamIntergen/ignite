using System.Collections;
using System.Reflection;
using NUnit.Framework;

namespace Ignite.Rules.Test
{
    public static class TestExtensionMethods
    {
        public static void AssertNoPropertiesAreNull(this object objectToCheck)
        {
            var collection = objectToCheck as IEnumerable;
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    item.AssertNoPropertiesAreNull();
                }
            }
            else
            {
                var typeWeAreChecking = objectToCheck.GetType();
                foreach (PropertyInfo pi in typeWeAreChecking.GetProperties())
                {
                    var msg = $"property {pi.Name} on {typeWeAreChecking} is null";
                    var currentPropertyObject = pi.GetValue(objectToCheck);
                    Assert.IsNotNull(currentPropertyObject, msg);

                    if (pi.PropertyType == typeof (string))
                    {
                        string value = (string) currentPropertyObject;
                        Assert.IsNotNull(value, msg);
                    }
                    else
                    {
                        currentPropertyObject.AssertNoPropertiesAreNull();
                    }
                }
            }
        }
    }
}