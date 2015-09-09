using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleIoc.Contracts;
using SimpleIoc.Factories;

namespace SimpleIoc
{
    public class InstanceService : IService
    {
        private readonly object _instance;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_contract">Contract type of the contract.</param>
        /// <param name="a_instance">Instance.</param>
        /// <param name="a_name">Name of this service.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_contract"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_instance"/> is null.</exception>
        public InstanceService(Type a_contract, object a_instance, string a_name = null)
        {
            #region Argument Validation

            if (a_contract == null)
                throw new ArgumentNullException(nameof(a_contract));

            if (a_instance == null)
                throw new ArgumentNullException(nameof(a_instance));

            #endregion

            _instance = a_instance;

            Contract = a_contract;
            Name = a_name;
        }

        /// <summary>
        /// Type of this service.
        /// </summary>
        public Type Type => _instance.GetType();

        /// <summary>
        /// Contract type of this service.
        /// </summary>
        public Type Contract { get; }

        /// <summary>
        /// Name of the service.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Factories available to create this service.
        /// </summary>
        public IServiceFactory[] Factories => new IServiceFactory[] {new InstanceFactory(this)};

        /// <summary>
        /// Resolve an instance for this service.
        /// </summary>
        /// <returns>Service instance.</returns>
        public object Resolve()
        {
            return _instance;
        }
    }
}
