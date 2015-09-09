using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Lifespan;

namespace SimpleIoc.Test.Lifespan
{
    [TestClass]
    public class DefaultLifespanTest
    {
        [TestMethod]
        public void ConstructDefaultLifespan()
        {
            // Execute
            new DefaultLifespan();
        }


        [TestMethod]
        public void HoldInstance()
        {
            // Setup
            var lifespan = new DefaultLifespan();
            var instance = new object();

            // Execute
            lifespan.Hold(instance);

            // Assert
            Assert.IsNull(lifespan.Instance);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HoldInstanceWithNull()
        {
            // Setup
            var lifespan = new DefaultLifespan();

            // Execute
            lifespan.Hold(a_instance: null);
        }

        [TestMethod]
        public void KillInstance()
        {
            // Setup
            var lifespan = new DefaultLifespan();
            var instance = new object();
            lifespan.Hold(instance);

            // Execute
            lifespan.Kill();

            // Assert
            Assert.IsNull(lifespan.Instance);
        }

        [TestMethod]
        public void KillInstanceWithNoInstanceHeld()
        {
            // Setup
            var lifespan = new DefaultLifespan();
            var instance = new object();
            lifespan.Hold(instance);

            // Execute
            lifespan.Kill();

            // Assert
            Assert.IsNull(lifespan.Instance);
        }
    }
}
