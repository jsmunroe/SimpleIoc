using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Contracts;

namespace SimpleIoc.Test
{
    [TestClass]
    public class FuncFactoryTest
    {
        [TestMethod]
        public void ConstructFuncFactory()
        {
            // Execute
            new FuncFactory<ServiceBase>(() => new ServiceWithDefaultConstructor());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructFuncFactoryWithNull()
        {
            // Execute
            new FuncFactory<ServiceBase>(a_func: null);
        }

        [TestMethod]
        public void ResolveServiceInstance()
        {
            // Setup
            var factory = new FuncFactory<ServiceBase>(() => new ServiceWithDefaultConstructor());

            // Execute
            var result = factory.Create();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (ServiceWithDefaultConstructor));
        }

        [TestMethod]
        public void CheckDependencyComplexity()
        {
            // Setup
            var factory = new FuncFactory<ServiceBase>(() => new ServiceWithDefaultConstructor());

            // Assert
            Assert.AreEqual(0, factory.DependencyComplexity);
        }

        [TestMethod]
        public void CheckDependencies()
        {
            // Setup
            var factory = new FuncFactory<ServiceBase>(() => new ServiceWithDefaultConstructor());

            // Assert
            Assert.IsNotNull(factory.Dependencies);
            Assert.AreEqual(0, factory.Dependencies.Length);
        }

        [TestMethod]
        public void CheckCanCreate()
        {
            // Setup
            var factory = new FuncFactory<ServiceBase>(() => new ServiceWithDefaultConstructor());

            // Assert
            Assert.IsTrue(factory.CanCreate);

        }

        [TestMethod]
        public void FulfillFuncFactory()
        {
            // Setup
            var factory = new FuncFactory<ServiceBase>(() => new ServiceWithDefaultConstructor());
            var container = new Container();

            // Execute
            var result = factory.Fulfill(container);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
