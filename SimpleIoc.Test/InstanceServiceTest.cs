using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleIoc.Test
{
    [TestClass]
    public class InstanceServiceTest
    {
        [TestMethod]
        public void ConstructInstanceService()
        {
            // Setup
            var instance = new object();

            // Execute
            var service = new InstanceService(typeof(object), instance, "instance");

            // Assert
            Assert.AreEqual(typeof (object), service.Contract);
            Assert.AreEqual(typeof (object), service.Type);
            Assert.AreEqual("instance", service.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullInstance()
        {
            // Setup
            var instance = new object();

            // Execute
            new InstanceService(a_contract: null, a_instance: instance, a_name: "instance");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullContract()
        {
            // Setup
            var instance = new object();

            // Execute
            new InstanceService(a_contract: typeof(object), a_instance: null, a_name: "instance");
        }

        [TestMethod]
        public void ConstructWithNullName()
        {
            // Setup
            var instance = new object();

            // Execute
            var service = new InstanceService(a_contract: typeof(object), a_instance: instance, a_name: null);

            // Assert
            Assert.AreEqual(typeof(object), service.Contract);
            Assert.AreEqual(typeof(object), service.Type);
            Assert.IsNull(service.Name);
        }


        [TestMethod]
        public void ConstructWithoutName()
        {
            // Setup
            var instance = new object();

            // Execute
            var service = new InstanceService(a_contract: typeof(object), a_instance: instance);

            // Assert
            Assert.AreEqual(typeof(object), service.Contract);
            Assert.AreEqual(typeof(object), service.Type);
            Assert.IsNull(service.Name);
        }


        [TestMethod]
        public void GetFactories()
        {
            // Setup
            var instance = new object();
            var service = new InstanceService(a_contract: typeof(object), a_instance: instance);

            // Execute
            var factories = service.Factories;

            // Assert
            Assert.AreEqual(1, factories.Length);
        }


        [TestMethod]
        public void ResolveService()
        {
            // Setup
            var instance = new object();
            var service = new InstanceService(a_contract: typeof(object), a_instance: instance);

            // Execute
            var result = service.Resolve();

            // Assert
            Assert.AreSame(instance, result);
        }



    }
}
