using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleIoc.Contracts;

namespace SimpleIoc
{
    public class ServiceContractListing
    {
        private readonly ServiceContractListing _parent = null;

        private readonly Dictionary<string, IService> _servicesByName = new Dictionary<string, IService>();
        private readonly Dictionary<Guid, List<IService>> _servicesByTypeGuid = new Dictionary<Guid, List<IService>>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceContractListing()
        {
            
        }

        /// <summary>
        /// Private Constructor.
        /// </summary>
        /// <param name="a_parent">Parent listing.</param>
        private ServiceContractListing(ServiceContractListing a_parent)
        {
            _parent = a_parent;
        }

        /// <summary>
        /// Add the given service (<paramref name="a_service"/>) to this listing.
        /// </summary>
        /// <param name="a_service">Service to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_service"/> is null.</exception>
        public void Add(IService a_service)
        {
            #region Argument Validation

            if (a_service == null)
                throw new ArgumentNullException(nameof(a_service));

            #endregion

            if (a_service.Contract == null)
                throw new InvalidOperationException("Service does not have a contract type.");

            List<IService> services;

            if (_servicesByTypeGuid.ContainsKey(a_service.Contract.GUID))
                services = _servicesByTypeGuid[a_service.Contract.GUID];
            else
                services = _servicesByTypeGuid[a_service.Contract.GUID] = new List<IService>();

            services.Add(a_service);

            if (a_service.Name != null)
            {
                var name = CreateServiceName(a_service.Contract, a_service.Name);

                if (!_servicesByName.ContainsKey(name))
                    _servicesByName.Add(name, a_service);
            }
        }

        /// <summary>
        /// Get the services for the given contract (<paramref name="a_contract"/>) from this listing.
        /// </summary>
        /// <param name="a_contract">Service contract.</param>
        /// <returns>Sequence of services.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_contract"/> is null.</exception>
        public IEnumerable<IService> GetServices(Type a_contract)
        {
            #region Argument Validation

            if (a_contract == null)
                throw new ArgumentNullException(nameof(a_contract));

            #endregion

            var parentServices = Enumerable.Empty<IService>();
            if (_parent != null)
                parentServices = _parent.GetServices(a_contract);

            if (_servicesByTypeGuid.ContainsKey(a_contract.GUID))
                return parentServices.Concat(_servicesByTypeGuid[a_contract.GUID].AsReadOnly());
                
            return parentServices;
        }


        /// <summary>
        /// Get the services for the given contract (<paramref name="a_contract"/>) from this listing.
        /// </summary>
        /// <param name="a_contract">Service contract.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>Sequence of services.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_contract"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_name"/> is null.</exception>
        public IService GetService(Type a_contract, string a_name)
        {
            #region Argument Validation

            if (a_contract == null)
                throw new ArgumentNullException(nameof(a_contract));

            if (a_name == null)
                throw new ArgumentNullException(nameof(a_name));

            #endregion

            var name = CreateServiceName(a_contract, a_name);

            if (_servicesByName.ContainsKey(name))
                return _servicesByName[name];

            return _parent?.GetService(a_contract, a_name);
        }

        /// <summary>
        /// Create a name for the service with the given contract type (<paramref name="a_contract"/>) and name (<paramref name="a_name"/>).
        /// </summary>
        /// <param name="a_contract">Contract type.</param>
        /// <param name="a_name">Name of the contract.</param>
        /// <returns>Created name.</returns>
        private static string CreateServiceName(Type a_contract, string a_name)
        {
            return a_contract.GUID + "+" + a_name;
        }

        /// <summary>
        /// Create a child of this contract listing.
        /// </summary>
        /// <returns>Created contract listing.</returns>
        public ServiceContractListing CreateChild()
        {
            var child = new ServiceContractListing(this);

            return child;
        }
    }
}
