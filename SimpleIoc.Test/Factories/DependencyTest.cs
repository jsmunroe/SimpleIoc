using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Factories;

namespace SimpleIoc.Test.Factories
{
    [TestClass]
    public class DependencyTest
    {
        [TestMethod]
        public void ConstructDepencency()
        {
            // Execute
            var dependency = new Dependency(a_contract: typeof (Dependency1));

            // Assert
            Assert.AreSame(typeof(Dependency1), dependency.Contract);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullDependencyType()
        {
            // Execute
            new Dependency(a_contract: null);
        }

        [TestMethod]
        public void CheckIfFulfilledWhenNot()
        {
            // Setup
            var dependency = new Dependency(typeof (Dependency2));

            // Assert
            Assert.IsFalse(dependency.IsFulfilled);
        }

        [TestMethod]
        public void FulfillWithService()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(Dependency1), typeof(DependencyBase), null);
            var dependency = new Dependency(typeof (Dependency1));

            // Execute
            dependency.Fulfill(a_service: service);

            // Assert
            Assert.IsTrue(dependency.IsFulfilled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FulfillWithServiceOfWrongType()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(Dependency2), typeof(DependencyBase), null);
            var dependency = new Dependency(typeof (Dependency1));

            // Execute
            dependency.Fulfill(a_service: service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FulfillWithNull()
        {
            // Setup
            var dependency = new Dependency(typeof(Dependency1));

            // Execute
            dependency.Fulfill(a_service: null);
        }

        [TestMethod]
        public void FulfillWithSubclassService()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(Dependency2), typeof(DependencyBase), null);
            var dependency = new Dependency(typeof(DependencyBase));

            // Execute
            dependency.Fulfill(a_service: service);

            // Assert
            Assert.IsTrue(dependency.IsFulfilled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FulfillWithSuperclassService()
        {
            // Setup
            var container = new Container();
            var service = new Service(container, typeof(DependencyBase), typeof(DependencyBase), null);
            var dependency = new Dependency(typeof(Dependency2));

            // Execute
            dependency.Fulfill(a_service: service);
        }

        [TestMethod]
        public void FulfillWithContainer()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency2>();
            var dependency = new Dependency(typeof(DependencyBase));

            // Execute
            dependency.Fulfill(a_container: container);

            // Assert
            Assert.IsTrue(dependency.IsFulfilled);
        }

        [TestMethod]
        public void ResolveFulfilledDependency()
        {
            // Setup
            var container = new Container();
            container.Register<DependencyBase, Dependency2>();
            var dependency = new Dependency(typeof(DependencyBase));
            dependency.Fulfill(a_container: container);

            // Execute
            var instance = dependency.Resolve();

            // Assert
            Assert.IsTrue(dependency.IsFulfilled);
        }
    }
}
