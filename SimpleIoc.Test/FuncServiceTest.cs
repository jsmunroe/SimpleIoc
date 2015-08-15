using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleIoc.Test
{
    [TestClass]
    public class FuncServiceTest
    {
        [TestMethod]
        public void ConstructFuncService()
        {
            // Execute
            new FuncService<ServiceBase>(() => new ServiceWithNoConstructors());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructFuncServiceWithNull()
        {
            // Execute
            new FuncService<ServiceBase>(a_func: null);
        }

        [TestMethod]
        public void CheckType()
        {
            // Setup
            var service = new FuncService<ServiceBase>(() => new ServiceWithNoConstructors());

            // Assert
            Assert.IsNotNull(service.Type);
            Assert.AreSame(service.Type, typeof (ServiceBase));
        }

        [TestMethod]
        public void CheckFunctions()
        {
            // Setup
            var service = new FuncService<ServiceBase>(() => new ServiceWithNoConstructors());

            // Assert
            Assert.IsNotNull(service.Factories);
            Assert.AreEqual(1, service.Factories.Length);
        }
    }
}
