using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Factories;

namespace SimpleIoc.Test.Factories
{
    [TestClass]
    public class ConstructorDependencyTest
    {
        [TestMethod]
        public void ConstructConstructorDependency()
        {
            // Execute
            var dependency = new ConstructorDependency(typeof (ServiceBase), "a_param");

            // Assert
            Assert.AreSame("a_param", dependency.ParamName);
        }

        [TestMethod]
        public void ConstrucWithNullConstract()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Execute
                new ConstructorDependency(a_contract: null, a_paramName: "a_param");
            });
        }

        [TestMethod]
        public void ConstructWithNullParamName()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Execute
                new ConstructorDependency(a_contract: typeof (ServiceBase), a_paramName: null);
            });
        }
    }
}
