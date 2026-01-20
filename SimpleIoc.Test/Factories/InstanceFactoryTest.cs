using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Factories;

namespace SimpleIoc.Test.Factories
{
    [TestClass]
    public class InstanceFactoryTest
    {

        [TestMethod]
        public void ConstructInstanceFactory()
        {
            // Setup
            var instance = new object();
            var instanceService = new InstanceService(typeof(object), instance);

            // Execute
            var factory = new InstanceFactory(instanceService);
        }

        [TestMethod]
        public void ConstructInstanceFactoryWithNullInstance()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Execute
                var factory = new InstanceFactory(a_instanceServiceService: null);
            });
        }

        [TestMethod]
        public void CreateServiceInstance()
        {
            // Setup
            var instance = new object();
            var instanceService = new InstanceService(typeof(object), instance);
            var factory = new InstanceFactory(instanceService);

            // Execute
            var result = factory.Create();

            // Assert
            Assert.AreSame(instance, result);

        }

        [TestMethod]
        public void CheckDependencyComplexity()
        {
            // Setup
            var instance = new object();
            var instanceService = new InstanceService(typeof(object), instance);
            var factory = new InstanceFactory(instanceService);

            // Assert
            Assert.AreEqual(0, factory.DependencyComplexity);
        }

        [TestMethod]
        public void CheckDependencies()
        {
            // Setup
            var instance = new object();
            var instanceService = new InstanceService(typeof(object), instance);
            var factory = new InstanceFactory(instanceService);

            // Assert
            Assert.IsNotNull(factory.Dependencies);
            Assert.AreEqual(0, factory.Dependencies.Length);
        }

        [TestMethod]
        public void CheckCanCreate()
        {
            // Setup
            var instance = new object();
            var instanceService = new InstanceService(typeof(object), instance);
            var factory = new InstanceFactory(instanceService);

            // Assert
            Assert.IsTrue(factory.CanCreate);
        }

        [TestMethod]
        public void FulfillFuncFactory()
        {
            // Setup
            var instance = new object();
            var instanceService = new InstanceService(typeof(object), instance);
            var factory = new InstanceFactory(instanceService);
            var container = new Container();

            // Execute
            var result = factory.Fulfill(container);

            // Assert
            Assert.IsTrue(result);
        }


    }
}
