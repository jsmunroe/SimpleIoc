using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Modules;

namespace SimpleIoc.Test.Modules
{
    [TestClass]
    public class ModuleLoaderTest
    {
        [TestMethod]
        public void LoadModules()
        {
            var modules = ModuleLoader.Discover();

            Assert.AreEqual(1, modules.Count());
            Assert.IsInstanceOfType(modules.First(), typeof(FakeModule));
        }


        [TestMethod]
        public void LoadModulesFromExecutingAssembly()
        {
            var modules = ModuleLoader.Discover(Assembly.GetExecutingAssembly());

            Assert.AreEqual(1, modules.Count());
            Assert.IsInstanceOfType(modules.First(), typeof(FakeModule));
        }

        [TestMethod]
        public void LoadModulesWithNullAssembly()
        {
            var modules = ModuleLoader.Discover(a_assembly: null);
        }
    }
}
