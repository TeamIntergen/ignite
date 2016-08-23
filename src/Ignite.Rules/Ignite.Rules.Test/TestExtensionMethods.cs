using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace Ignite.Rules.Test
{
    public static class TestExtensionMethods
    {
        public static void AssertNoPropertiesAreNull(this object objectToCheck)
        {
            var stringValue = objectToCheck as string; 
            var collection = objectToCheck as IEnumerable;
            if (collection != null && stringValue == null) 
            {
                // Dont treat string as IEnumerable
                foreach (var item in collection)
                {
                    item.AssertNoPropertiesAreNull();
                }
            }
            else
            {
                var typeWeAreChecking = objectToCheck.GetType();
                if (stringValue != null)
                {
                    Assert.IsNotNull(stringValue);
                }
                else if (typeWeAreChecking == typeof (DateTimeOffset))
                {
                    DateTimeOffset value = (DateTimeOffset)objectToCheck;
                    Assert.IsNotNull(value);
                }
                else if (typeWeAreChecking == typeof(int))
                {
                    int value = (int)objectToCheck;
                    Assert.IsNotNull(value);
                    Assert.That(value, Is.GreaterThan(0));
                }
                else
                {
                    foreach (PropertyInfo pi in typeWeAreChecking.GetProperties())
                    {
                        var msg = $"property {pi.Name} on {typeWeAreChecking} is null";
                        var currentPropertyObject = pi.GetValue(objectToCheck);
                        Assert.IsNotNull(currentPropertyObject, msg);
                        if (typeWeAreChecking.Name == "EvalaccessDto" ||
                            currentPropertyObject.GetType().Name == "String[]")
                        {
                            // Console.WriteLine("Skipping EvalaccessDto SessionTypes array null check as it is now value to have a null value.");
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
}