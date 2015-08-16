using System.Linq;
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
    }
}
