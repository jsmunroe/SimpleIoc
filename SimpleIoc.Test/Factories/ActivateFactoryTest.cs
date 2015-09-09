using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Factories;

namespace SimpleIoc.Test.Factories
{
    [TestClass]
    public class ActivateFactoryTest
    {
        [TestMethod]
        public void ConstructConstructorActivateFactory()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), null);
            var type = typeof(ServiceWithDefaultConstructor);
            var constructor = type.GetConstructors().First();

            // Execute
            var factory = new ActivateFactory(a_service: service, a_constructor: constructor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructConstructorWithNullService()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), null);
            var type = typeof(ServiceWithDefaultConstructor);
            var constructor = type.GetConstructors().First();

            // Execute
            var factory = new ActivateFactory(a_service: null, a_constructor: constructor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructConstructorWithNullConstructor()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), null);
            var type = typeof(ServiceWithDefaultConstructor);
            var constructor = type.GetConstructors().First();

            // Execute
            var factory = new ActivateFactory(a_service: service, a_constructor: null);
        }

        [TestMethod]
        public void CreateServiceWithNoDependencies()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), null);
            var type = typeof(ServiceWithDefaultConstructor);
            var constructor = type.GetConstructors().First();
            var factory = new ActivateFactory(a_service: service, a_constructor: constructor);

            // Execute
            var result = factory.Create();

            // Asssert
            Assert.AreEqual(0, factory.Dependencies?.Length);
            Assert.IsTrue(result is ServiceWithDefaultConstructor);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateServiceWithUnfulfilledDependency()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithOneConstructor), typeof(ServiceBase), null);
            var type = typeof(ServiceWithOneConstructor);
            var constructor = type.GetConstructors().First();
            var factory = new ActivateFactory(a_service: service, a_constructor: constructor);

            // Execute
            var result = factory.Create();
        }

        [TestMethod]
        public void CreateServiceWithFulfilledDependency()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();
            var service = new Service(container, typeof(ServiceWithOneConstructor), typeof(ServiceBase), null);
            var type = typeof(ServiceWithOneConstructor);
            var constructor = type.GetConstructors().First();
            var factory = new ActivateFactory(a_service: service, a_constructor: constructor);

            var fulfilled = factory.Fulfill(a_container: container);

            // Execute
            var result = factory.Create();

            Assert.IsTrue(fulfilled);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ServiceWithOneConstructor));
        }

    }
}
