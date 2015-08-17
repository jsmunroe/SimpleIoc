using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleIoc.Contracts;
using SimpleIoc.Modules;

namespace SimpleIoc.Test.Modules
{
    public class FakeModule : IModule
    {
        public bool BootstrapCalled = false;

        /// <summary>
        /// Bootstrap this module.
        /// </summary>
        /// <param name="a_container">Container into which to </param>
        public void Bootstrap(Container a_container)
        {
            BootstrapCalled = true;
        }
    }
}
