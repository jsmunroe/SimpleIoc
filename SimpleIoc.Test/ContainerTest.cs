using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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
        public void ResolveServiceWithTypeObject()
        {
            // Setup
            var container = new Container();
            container.Register<ServiceBase, ServiceWithNoConstructors>();

            // Execute
            var service = container.Resolve(typeof(ServiceBase));

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
        public void ResolveServiceWithNameAndTypeObject()
        {
            // Setup
            var container = new Container();
            container.Register<ServiceBase, ServiceWithNoConstructors>("MyService");

            // Execute
            var service = container.Resolve(typeof(ServiceBase), "MyService");

            // Assert
            Assert.IsNotNull(service);
            Assert.IsTrue(service is ServiceWithNoConstructors);
        }

        [TestMethod]
        public void ResolveFuncServiceWithName()
        {
            // Setup
            var container = new Container();
            container.Register<ServiceBase>(() => new ServiceWithNoConstructors(), "MyService");

            // Execute
            var service = container.Resolve(typeof(ServiceBase), "MyService");

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

        [TestMethod]
        public void RegisterFuncService()
        {
            // Setup
            var container = new Container();

            // Execute
            container.Register<ServiceBase>(() => new ServiceWithNoConstructors());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterFuncServiceWithNull()
        {
            // Setup
            var container = new Container();

            // Execute
            container.Register<ServiceBase>(a_func: null);
        }

        [TestMethod]
        public void ResolveFuncService()
        {
            // Setup
            var container = new Container();
            container.Register<ServiceBase>(() => new ServiceWithNoConstructors());

            // Execute
            var result = container.Resolve<ServiceBase>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ServiceWithNoConstructors));
        }

        [TestMethod]
        public void RegisterTheSameContractMoreThanOnceWithDifferentNames()
        {
            // Setup
            var container = new Container();

            // Execute
            container.Register<DependencyBase, Dependency1>("Dependency1");
            container.Register<DependencyBase, Dependency2>("Dependency2");
        }

        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
        public void RegisterTheSameContractWithNoNames()
        {
            // Setup
            var container = new Container();

            // Execute
            container.Register<DependencyBase, Dependency1>();
            container.Register<DependencyBase, Dependency2>();
        }


        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
        public void RegisterTheSameContractWithSameName()
        {
            // Setup
            var container = new Container();

            // Execute
            container.Register<DependencyBase, Dependency1>("Dependency1");
            container.Register<DependencyBase, Dependency2>("Dependency1");
        }

        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
        public void RegisterTheSameContractWithOnlyFirstNamed()
        {
            // Setup
            var container = new Container();

            // Execute
            container.Register<DependencyBase, Dependency1>("Dependency1");
            container.Register<DependencyBase, Dependency2>();
        }

        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
        public void RegisterTheSameContractWithOnlySecondNamed()
        {
            // Setup
            var container = new Container();

            // Execute
            container.Register<DependencyBase, Dependency1>();
            container.Register<DependencyBase, Dependency2>("Dependency2");
        }


        [TestMethod]
        public void RegisterInstance()
        {
            // Setup
            var container = new Container();
            var service = new ServiceWithNoConstructors();

            // Execute
            container.RegisterInstance<ServiceBase>(service);

            // Assert
            var result = container.Resolve<ServiceBase>();
            Assert.AreSame(result, service);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterInstanceWithNull()
        {
            // Setup
            var container = new Container();

            // Execute
            container.RegisterInstance<ServiceBase>(a_instance: null);
        }

        [TestMethod]
        public void RegisterInstanceWithName()
        {
            // Setup
            var container = new Container();
            var service = new ServiceWithNoConstructors();

            // Execute
            container.RegisterInstance<ServiceBase>(service, "noconstructors");

            // Assert
            var result = container.Resolve<ServiceBase>("noconstructors");
            Assert.AreSame(result, service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterInstanceWithNameAndNullInstance()
        {
            // Setup
            var container = new Container();

            // Execute
            container.RegisterInstance<ServiceBase>(a_instance: null, a_name: "noconstructors");
        }

        [TestMethod]
        public void ResolveMultipleServices()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>("Dependency1");
            container.Register<DependencyBase, Dependency2>("Dependency2");

            // Execute
            var services = container.ResolveAll<DependencyBase>();

            // Assert
            Assert.AreEqual(2, services.Count());
            Assert.AreEqual(1, services.OfType<Dependency1>().Count());
            Assert.AreEqual(1, services.OfType<Dependency2>().Count());
        }

        [TestMethod]
        public void ResolveMultipleServicesWithTypeObject()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>("Dependency1");
            container.Register<DependencyBase, Dependency2>("Dependency2");

            // Execute
            var services = container.ResolveAll(typeof(DependencyBase));

            // Assert
            Assert.AreEqual(2, services.Count());
            Assert.AreEqual(1, services.OfType<Dependency1>().Count());
            Assert.AreEqual(1, services.OfType<Dependency2>().Count());
        }

        [TestMethod]
        public void ResolveAnUnregisteredService()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();

            // Execute
            var service = container.Resolve<ServiceWithOneConstructor>();

            // Assert
            Assert.IsNotNull(service);
            Assert.IsTrue(service != null);
        }


        [TestMethod]
        public void CreateChildContainer()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();

            // Execute
            var child = container.CreateChild();

            // Assert
            Assert.IsNotNull(child);
        }


        [TestMethod]
        public void CreateChildContainerAndRegisterInParent()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency1>();

            // Execute
            var child = container.CreateChild();
            container.Register<ServiceBase, ServiceWithNoConstructors>();
            var result = child.Resolve<ServiceBase>();

            // Assert
            Assert.IsNotNull(child);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (ServiceWithNoConstructors));
        }



    }


}
