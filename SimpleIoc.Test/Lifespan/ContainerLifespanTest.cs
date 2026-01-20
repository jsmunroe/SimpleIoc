using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Lifespan;

namespace SimpleIoc.Test.Lifespan
{
    [TestClass]
    public class ContainerLifespanTest
    {

        [TestMethod]
        public void ConstructContainerLifespan()
        {
            // Execute
            new ContainerLifespan();
        }


        [TestMethod]
        public void HoldInstance()
        {
            // Setup
            var lifespan = new ContainerLifespan();
            var instance = new object();

            // Execute
            lifespan.Hold(instance);

            // Assert
            Assert.AreSame(instance, lifespan.Instance);
        }


        [TestMethod]
        public void HoldInstanceWithNull()
        {
            // Setup
            var lifespan = new ContainerLifespan();

            Assert.Throws<ArgumentNullException>(() =>
            {
                // Execute
                lifespan.Hold(a_instance: null);
            });
        }

        [TestMethod]
        public void KillInstance()
        {
            // Setup
            var lifespan = new ContainerLifespan();
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
            var lifespan = new ContainerLifespan();
            var instance = new object();
            lifespan.Hold(instance);

            // Execute
            lifespan.Kill();

            // Assert
            Assert.IsNull(lifespan.Instance);
        }
    }
}
