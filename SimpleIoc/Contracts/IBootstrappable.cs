using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoc.Contracts
{
    public interface IBootstrappable
    {
        /// <summary>
        /// Wehther this instance has been bootstrapped.
        /// </summary>
        bool IsBootstrapped { get; }

        /// <summary>
        /// Bootstrap this instance against the given container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container.</param>
        void Bootstrap(Container a_container);
    }
}
