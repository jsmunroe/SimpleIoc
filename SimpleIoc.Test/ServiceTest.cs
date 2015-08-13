using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleIoc.Test
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void ConstructService()
        {
            // Execute
            var container = new Container();
            var service = new Service(container, typeof(String));
            
            // Assert
            Assert.AreSame(typeof (String), service.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructServiceWithNullType()
        {
            // Execute
            var container = new Container();
            new Service(a_container: container, a_type: null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructServiceWithNullContainer()
        {
            // Execute
            new Service(a_container: null, a_type: typeof(String));
        }

        [TestMethod]
        public void GetFactoriesForTypeWithoutContructor()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithNoConstructors));

            // Execute
            var factories = service.Factories;

            // Assert
            Assert.IsNotNull(factories);
            Assert.AreEqual(1, factories?.Length);
        }

        [TestMethod]
        public void GetFactoriesForTypeWithDefaultConstructor()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor));

            // Execute
            var factories = service.Factories;

            // Assert
            Assert.IsNotNull(factories);
            Assert.AreEqual(1, factories?.Length);
        }

        [TestMethod]
        public void GetFactoriesForTypeWithOneConstructor()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithOneConstructor));

            // Execute
            var factories = service.Factories;

            // Assert
            Assert.IsNotNull(factories);
            Assert.AreEqual(1, factories?.Length);
        }

        [TestMethod]
        public void GetFactoriesForTypeWithMultipleConstructors()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithMultipleConstructors));

            // Execute
            var factories = service.Factories;

            // Assert
            Assert.IsNotNull(factories);
            Assert.AreEqual(3, factories?.Length);
        }

        [TestMethod]
        public void ResolveServiceForTypeWithNoConstructors()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithNoConstructors));

            // Execute
            var serviceInstance = service.Resolve();

            // Assert
            Assert.IsNotNull(serviceInstance);
            Assert.IsInstanceOfType(serviceInstance, typeof(ServiceWithNoConstructors));
        }

        [TestMethod]
        public void ResolveServiceForTypeWithDefaultConstructor()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor));

            // Execute
            var serviceInstance = service.Resolve();

            // Assert
            Assert.IsNotNull(serviceInstance);
            Assert.IsInstanceOfType(serviceInstance, typeof(ServiceWithDefaultConstructor));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ResolveServiceForTypeWithOneConstructorWithoutFulfilling()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithOneConstructor));
            
            // Execute
            var serviceInstance = service.Resolve();

            // Assert
            Assert.IsNotNull(serviceInstance);
            Assert.IsInstanceOfType(serviceInstance, typeof(ServiceWithOneConstructor));
        }

        [TestMethod]
        public void ResolveServiceForTypeWithOneConstructor()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();
            var service = new Service(container, typeof(ServiceWithOneConstructor));

            // Execute
            var serviceInstance = service.Resolve();
        }

        [TestMethod]
        public void ResolveServiceForTypeWithMultipleConstructors()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();
            var service = new Service(container, typeof(ServiceWithMultipleConstructors));

            // Execute
            var serviceInstance = service.Resolve();

            // Assert
            Assert.IsNotNull(serviceInstance);
            Assert.IsInstanceOfType(serviceInstance, typeof(ServiceWithMultipleConstructors));
            var instance = serviceInstance as ServiceWithMultipleConstructors;
            Assert.AreEqual(".ctor(DependencyBase)", instance.Constructor);
        }

        [TestMethod]
        public void ResolveServiceForTypeWithMultipleConstructorsWithMaxDependenciesFulfilled()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();
            container.Register<Dependency2, Dependency2>();
            var service = new Service(container, typeof(ServiceWithMultipleConstructors));

            // Execute
            var serviceInstance = service.Resolve();

            // Assert
            Assert.IsNotNull(serviceInstance);
            Assert.IsInstanceOfType(serviceInstance, typeof(ServiceWithMultipleConstructors));
            var instance = serviceInstance as ServiceWithMultipleConstructors;
            Assert.AreEqual(".ctor(DependencyBase, Dependency2)", instance.Constructor);
        }

    }
}
