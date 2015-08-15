using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleIoc.Test
{
    [TestClass]
    public class ConstructorFactoryTest
    {
        [TestMethod]
        public void ConstructConstructorServiceFactory()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor));
            var type = typeof(ServiceWithDefaultConstructor);
            var constructor = type.GetConstructors().First();

            // Execute
            var serviceFactory = new ConstructorFactory(a_service: service, a_constructor: constructor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructConstructorServiceFactoryWithNullService()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor));
            var type = typeof(ServiceWithDefaultConstructor);
            var constructor = type.GetConstructors().First();

            // Execute
            var serviceFactory = new ConstructorFactory(a_service: null, a_constructor: constructor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructConstructorServiceFactoryWithNullConstructor()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor));
            var type = typeof(ServiceWithDefaultConstructor);
            var constructor = type.GetConstructors().First();

            // Execute
            var serviceFactory = new ConstructorFactory(a_service: service, a_constructor: null);
        }

        [TestMethod]
        public void CreateServiceWithNoDependencies()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor));
            var type = typeof(ServiceWithDefaultConstructor);
            var constructor = type.GetConstructors().First();
            var serviceFactory = new ConstructorFactory(a_service: service, a_constructor: constructor);

            // Execute
            var result = serviceFactory.Create();

            // Asssert
            Assert.AreEqual(0, serviceFactory.Dependencies?.Length);
            Assert.IsTrue(result is ServiceWithDefaultConstructor);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateServiceWithUnfulfilledDependency()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithOneConstructor));
            var type = typeof(ServiceWithOneConstructor);
            var constructor = type.GetConstructors().First();
            var serviceFactory = new ConstructorFactory(a_service: service, a_constructor: constructor);

            // Execute
            var result = serviceFactory.Create();
        }

        [TestMethod]
        public void CreateServiceWithFulfilledDependency()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();
            var service = new Service(container, typeof(ServiceWithOneConstructor));
            var type = typeof(ServiceWithOneConstructor);
            var constructor = type.GetConstructors().First();
            var serviceFactory = new ConstructorFactory(a_service: service, a_constructor: constructor);

            var fulfilled = serviceFactory.Fulfill(a_container: container);

            // Execute
            var result = serviceFactory.Create();

            Assert.IsTrue(fulfilled);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ServiceWithOneConstructor));
        }

    }
}
