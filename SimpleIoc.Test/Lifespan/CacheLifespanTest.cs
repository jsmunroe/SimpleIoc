using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Lifespan;

namespace SimpleIoc.Test.Lifespan
{
    [TestClass]
    public class CacheLifespanTest
    {
        [TestMethod]
        public void ConstructCacheLifespan()
        {
            // Execute
            new CacheLifespan();
        }


        [TestMethod]
        public void ConstructCacheLifespanWithTimespan()
        {
            // Setup
            var timeout = TimeSpan.FromMinutes(1);

            // Execute
            var lifespan = new CacheLifespan(timeout);

            // Assert
            Assert.AreEqual(timeout, lifespan.Timeout);
        }

        [TestMethod]
        public void HoldInstance()
        {
            // Setup
            var lifespan = new CacheLifespan();
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
            var lifespan = new CacheLifespan();

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
            var lifespan = new CacheLifespan();
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
            var lifespan = new CacheLifespan();
            var instance = new object();
            lifespan.Hold(instance);

            // Execute
            lifespan.Kill();

            // Assert
            Assert.IsNull(lifespan.Instance);
        }


        [TestMethod]
        public async Task WaitPastTimeout()
        {
            // Setup
            var lifespan = new CacheLifespan(TimeSpan.FromMilliseconds(500));
            var instance = new object();
            lifespan.Hold(instance);

            // Execute
            await Task.Delay(1000);

            // Assert
            Assert.IsNull(lifespan.Instance);
        }


    }
}
