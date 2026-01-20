using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Contracts;

namespace SimpleIoc.Test
{
    [TestClass]
    public class ServiceContractListingTest
    {
        [TestMethod]
        public void ConstructServiceContractListing()
        {
            // Execute
            new ServiceContractListing();
        }

        [TestMethod]
        public void AddService()
        {
            // Setup
            var listing = new ServiceContractListing();
            var service = new TestService {Contract = typeof (ServiceBase), Name = "Service1"};

            // Execute
            listing.Add(service);
        }


        [TestMethod]
        public void AddServiceWithNull()
        {
            // Setup
            var listing = new ServiceContractListing();

            Assert.Throws<ArgumentNullException>(() =>
            {
                // Execute
                listing.Add(a_service: null);
            });
        }


        [TestMethod]
        public void AddServiceWithNullContractType()
        {
            // Setup
            var listing = new ServiceContractListing();
            var service = new TestService { Contract = null, Name = "Service1" };

            Assert.Throws<InvalidOperationException>(() =>
            {
                // Execute
                listing.Add(service);
            });
        }


        [TestMethod]
        public void AddServiceWithNullName()
        {
            // Setup
            var listing = new ServiceContractListing();
            var service = new TestService { Contract = typeof(ServiceBase), Name = null };

            // Execute
            listing.Add(service);
        }


        [TestMethod]
        public void GetServicesWithContractType()
        {
            // Setup
            var listing = new ServiceContractListing();
            var contractType = typeof (ServiceBase);
            var service = new TestService { Contract = contractType, Name = "Service1" };
            listing.Add(service);

            // Execute
            var result = listing.GetServices(contractType);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(service));
        }


        [TestMethod]
        public void GetServicesWithNullContractType()
        {
            // Setup
            var listing = new ServiceContractListing();
            var contractType = typeof(ServiceBase);
            var service = new TestService { Contract = contractType, Name = "Service1" };
            listing.Add(service);

            Assert.Throws<ArgumentNullException>(() =>
            {
                // Execute
                listing.GetServices(a_contract: null);
            });
        }


        [TestMethod]
        public void GetNonExistingServicesWithContractType()
        {
            // Setup
            var listing = new ServiceContractListing();
            var contractType = typeof(ServiceBase);

            // Execute
            var result = listing.GetServices(contractType);

            // Assert
            Assert.IsFalse(result.Any());
        }


        [TestMethod]
        public void GetServiceWithContractTypeAndName()
        {
            // Setup
            var listing = new ServiceContractListing();
            var contractType = typeof(ServiceBase);
            var service = new TestService { Contract = contractType, Name = "Service1" };
            listing.Add(service);

            // Execute
            var result = listing.GetService(contractType, "Service1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(service, result);
        }


        [TestMethod]
        public void GetServiceWithContractTypeAndNullName()
        {
            // Setup
            var listing = new ServiceContractListing();
            var contractType = typeof(ServiceBase);
            var service = new TestService { Contract = contractType, Name = "Service1" };
            listing.Add(service);

            Assert.Throws<ArgumentNullException>(() =>
            {
                // Execute
                var result = listing.GetService(contractType, a_name:null);
            });
        }

        [TestMethod]
        public void CreateChild()
        {
            // Setup
            var listing = new ServiceContractListing();
            var contractType = typeof(ServiceBase);
            var service = new TestService { Contract = contractType, Name = "Service1" };
            listing.Add(service);

            // Execute
            var child = listing.CreateChild();
            var result = child.GetService(contractType, "Service1");

            // Assert
            Assert.IsNotNull(child);
            Assert.IsNotNull(result);
            Assert.AreEqual("Service1", service.Name);
        }

        [TestMethod]
        public void CreateChildAndAddAServiceToTheParent()
        {
            // Setup
            var listing = new ServiceContractListing();

            // Execute
            var child = listing.CreateChild();
            var contractType = typeof(ServiceBase);
            var service = new TestService { Contract = contractType, Name = "Service1" };
            listing.Add(service);

            var result = child.GetService(contractType, "Service1");

            // Assert
            Assert.IsNotNull(child);
            Assert.IsNotNull(result);
            Assert.AreEqual("Service1", service.Name);
        }
    }

    public class TestService : IService
    {
        /// <summary>
        /// Contract of this service.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Constract type of this service.
        /// </summary>
        public Type Contract { get; set; }

        /// <summary>
        /// Name of the service.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Factories available to create this service.
        /// </summary>
        public IServiceFactory[] Factories { get; set; }

        /// <summary>
        /// Resolve an instance for this service.
        /// </summary>
        /// <returns>Service instance.</returns>
        public object Resolve()
        {
            return null;
        }
    }
}
