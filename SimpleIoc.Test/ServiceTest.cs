using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Lifespan;

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
            var service = new Service(container, typeof(string), typeof(string), "service name", null);

            // Assert
            Assert.AreSame(typeof(string), service.Type);
            Assert.AreSame(typeof(string), service.Contract);
            Assert.AreSame("service name", service.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructServiceWithNullContractType()
        {
            // Execute
            var container = new Container();
            new Service(a_container: container, a_type: typeof(string), a_contract: null, a_lifespan: null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructServiceWithNullType()
        {
            // Execute
            var container = new Container();
            new Service(a_container: container, a_type: null, a_contract: null, a_lifespan: null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructServiceWithNullContainer()
        {
            // Execute
            new Service(a_container: null, a_type: typeof(string), a_contract: typeof(string), a_lifespan: null);
        }

        [TestMethod]
        public void GetFactoriesForTypeWithoutContructor()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithNoConstructors), typeof(ServiceBase), a_lifespan: null);

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
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), a_lifespan: null);

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
            var service = new Service(container, typeof(ServiceWithOneConstructor), typeof(ServiceBase), a_lifespan: null);

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
            var service = new Service(container, typeof(ServiceWithMultipleConstructors), typeof(ServiceBase), a_lifespan: null);

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
            var service = new Service(container, typeof(ServiceWithNoConstructors), typeof(ServiceBase), a_lifespan: null);

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
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), a_lifespan: null);

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
            var service = new Service(container, typeof(ServiceWithOneConstructor), typeof(ServiceBase), a_lifespan: null);
            
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
            var service = new Service(container, typeof(ServiceWithOneConstructor), typeof(ServiceBase), a_lifespan: null);

            // Execute
            var serviceInstance = service.Resolve();
        }

        [TestMethod]
        public void ResolveServiceForTypeWithMultipleConstructors()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();
            var service = new Service(container, typeof(ServiceWithMultipleConstructors), typeof(ServiceBase), a_lifespan: null);

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
            var service = new Service(container, typeof(ServiceWithMultipleConstructors), typeof(ServiceBase), a_lifespan: null);

            // Execute
            var serviceInstance = service.Resolve();

            // Assert
            Assert.IsNotNull(serviceInstance);
            Assert.IsInstanceOfType(serviceInstance, typeof(ServiceWithMultipleConstructors));
            var instance = serviceInstance as ServiceWithMultipleConstructors;
            Assert.AreEqual(".ctor(DependencyBase, Dependency2)", instance.Constructor);
        }

        [TestMethod]
        public void ResolveServiceForTypeWithRequiredProperty()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();
            var service = new Service(container, typeof(ServiceWithRequiredProperty), typeof(ServiceBase), a_lifespan: null);

            // Execute 
            var serviceInstance = service.Resolve();

            // Assert
            Assert.IsNotNull(serviceInstance);
            Assert.IsInstanceOfType(serviceInstance, typeof(ServiceWithRequiredProperty));
            var instance = serviceInstance as ServiceWithRequiredProperty;
            Assert.IsInstanceOfType(instance.Dependency, typeof (Dependency1));
        }


        [TestMethod]
        public void ResolveServiceTwiceWithNoLifespan()
        {
            // Setup
            var lifespan = new ContainerLifespan();
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), a_lifespan: null);

            // Execute 
            var serviceInstance1 = service.Resolve();
            var serviceInstance2 = service.Resolve();

            // Assert
            Assert.AreNotSame(serviceInstance1, serviceInstance2);
        }


        [TestMethod]
        public void ResolveServiceTwiceWithContainerLifespan()
        {
            // Setup
            var lifespan = new ContainerLifespan();
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), a_lifespan: lifespan);

            // Execute 
            var serviceInstance1 = service.Resolve();
            var serviceInstance2 = service.Resolve();

            // Assert
            Assert.AreSame(serviceInstance1, serviceInstance2);
        }


        [TestMethod]
        public void ResolveServiceTwiceWithCacheLifespan()
        {
            // Setup
            var lifespan = new CacheLifespan(TimeSpan.FromMinutes(5));
            var container = new Container();
            var service = new Service(container, typeof(ServiceWithDefaultConstructor), typeof(ServiceBase), a_lifespan: lifespan);

            // Execute 
            var serviceInstance1 = service.Resolve();
            var serviceInstance2 = service.Resolve();

            // Assert
            Assert.AreSame(serviceInstance1, serviceInstance2);
        }



    }
}
