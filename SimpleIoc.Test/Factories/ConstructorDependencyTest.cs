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
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstrucWithNullConstract()
        {
            // Execute
            new ConstructorDependency(a_contract: null, a_paramName: "a_param");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullParamName()
        {
            // Execute
            new ConstructorDependency(a_contract: typeof (ServiceBase), a_paramName: null);
        }
    }
}
