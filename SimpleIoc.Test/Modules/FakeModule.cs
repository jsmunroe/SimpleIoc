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
        /// Wehther this instance has been bootstrapped.
        /// </summary>
        public bool IsBootstrapped
        {
            get { return false; }
        }

        /// <summary>
        /// Bootstrap this instance against the given container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container.</param>
        public void Bootstrap(IContainer a_container)
        {
            BootstrapCalled = true;
        }
    }
}
