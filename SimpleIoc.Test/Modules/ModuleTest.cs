using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleIoc.Test.Modules
{
    [TestClass]
    public class ModuleTest
    {
        [TestMethod]
        public void ConstructModule()
        {
            // Execute
            new FakeModule();
        }
    }
}
