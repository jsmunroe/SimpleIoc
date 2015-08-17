using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Factories;

namespace SimpleIoc.Test.Factories
{
    [TestClass]
    public class PropertyDependencyTest
    {
        [TestMethod]
        public void ConstructPropertyTest()
        {
            // Setup
            var property = typeof (ServiceWithRequiredProperty).GetProperty("Dependency");

            // Execute
            var dependency = new PropertyDependency(typeof(ServiceBase), property);

            // Assert
            Assert.AreSame(property, dependency.Property);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstrucWithNullConstract()
        {
            // Setup
            var property = typeof(ServiceWithRequiredProperty).GetProperty("Dependency");

            // Execute
            new PropertyDependency(a_contract: null, a_property: property);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullParamName()
        {
            // Execute
            new PropertyDependency(a_contract: typeof(ServiceBase), a_property: null);
        }
    }
}
