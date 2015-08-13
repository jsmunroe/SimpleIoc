using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleIoc.Test
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void Construct()
        {
            // Execute
            new Container();
        }

        [TestMethod]
        public void RegisterService()
        {
            // Setup
            var container = new Container();

            // Execute
            container.Register<ServiceBase, ServiceWithNoConstructors>();
        }

        [TestMethod]
        public void ResolveService()
        {
            // Setup
            var container = new Container();
            container.Register<ServiceBase, ServiceWithNoConstructors>();

            // Execute
            var service = container.Resolve<ServiceBase>();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsTrue(service is ServiceWithNoConstructors);
        }

        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
        public void ResolveUnregisteredService()
        {
            // Setup
            var container = new Container();

            // Execute
            var service = container.Resolve<ServiceBase>();
        }

        [TestMethod]
        public void ResolveServiceWithName()
        {
            // Setup
            var container = new Container();
            container.Register<ServiceBase, ServiceWithNoConstructors>("MyService");

            // Execute
            var service = container.Resolve<ServiceBase>("MyService");

            // Assert
            Assert.IsNotNull(service);
            Assert.IsTrue(service is ServiceWithNoConstructors);
        }

        [TestMethod]
        public void ResolveServiceWithDependency()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();
            container.Register<ServiceBase, ServiceWithOneConstructor>();

            // Execute
            var service = container.Resolve<ServiceBase>();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsTrue(service is ServiceWithOneConstructor);
        }

        [TestMethod]
        public void ResolveComplexService()
        {
            // Setup
            var container = new Container();
            container.Register<ComplexService1, ComplexService1>();
            container.Register<ComplexService2, ComplexService2>();
            container.Register<ComplexService3, ComplexService3>();
            container.Register<DependencyBase, Dependency1>();

            // Execute
            var service = container.Resolve<ComplexService1>();

            // Assert
            Assert.IsNotNull(service);
        }
    }


}
