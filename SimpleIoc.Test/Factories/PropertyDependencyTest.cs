using System;
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
            // Execute
            var dependency = new PropertyDependency(typeof(ServiceBase), "Property");

            // Assert
            Assert.AreSame("Property", dependency.PropertyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstrucWithNullConstract()
        {
            // Execute
            new PropertyDependency(a_contract: null, a_propertyName: "PropertyName");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullParamName()
        {
            // Execute
            new PropertyDependency(a_contract: typeof(ServiceBase), a_propertyName: null);
        }

    }
}
