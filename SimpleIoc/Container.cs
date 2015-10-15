using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleIoc.Contracts;

namespace SimpleIoc
{
    public class Container
    {
        private readonly ServiceContractListing _services = new ServiceContractListing();

        /// <summary>
        /// Constructor.
        /// </summary>
        public Container()
        {
            
        }

        /// <summary>
        /// Private Constructor.
        /// </summary>
        /// <param name="a_services">Service listing.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_services"/> is null.</exception>
        private Container(ServiceContractListing a_services)
        {
            #region Argument Validation

            if (a_services == null)
                throw new ArgumentNullException(nameof(a_services));

            #endregion

            _services = a_services;
        }

        // TODO: Register with type parameters.

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <param name="a_lifespan">Instance lifespan.</param>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <typeparam name="TService">Type of service.</typeparam>
        public void Register<TContract, TService>(ILifespan a_lifespan = null)
            where TService : TContract
        {
            if (_services.GetServices(typeof(TContract)).Any())
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}'.");

            var newService = new Service(this, typeof(TService), typeof(TContract), a_lifespan);
            _services.Add(newService);
        }

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as itself.
        /// </summary>
        /// <param name="a_lifespan">Instance lifespan.</param>
        /// <typeparam name="TService">Service type.</typeparam>
        public void Register<TService>(ILifespan a_lifespan = null)
        {
            Register<TService, TService>(a_lifespan);
        }

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as the given contract (<typeparamref name="TContract"/>) with the given name (<paramref name="a_name"/>).
        /// </summary>
        /// <param name="a_name">Service name.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <typeparam name="TService">Type of service.</typeparam>
        public void Register<TContract, TService>(string a_name, ILifespan a_lifespan = null)
            where TService : TContract
        {
            if (_services.GetServices(typeof(TContract)).Any(i => i.Name == null))
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}' and without a name.");

            if (_services.GetService(typeof(TContract), a_name) != null)
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}' and with the name '{a_name}'.");

            var newService = new Service(this, typeof(TService), typeof(TContract), a_name, a_lifespan);
            _services.Add(newService);
        }

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as itself  with the given name (<paramref name="a_name"/>).
        /// </summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <param name="a_name">Service name.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        public void Register<TService>(string a_name, ILifespan a_lifespan = null)
        {
            Register<TService, TService>(a_name, a_lifespan);
        }

        /// <summary>
        /// Register the given instance as the given contract (<typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_instance">Instance that resolves the contract.</param>
        public void RegisterInstance<TContract>(TContract a_instance)
        {
            if (_services.GetServices(typeof(TContract)).Any())
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}'.");

            var newService = new InstanceService(typeof(TContract), a_instance);
            _services.Add(newService);
        }

        /// <summary>
        /// Register the given instance as the given contract (<typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_instance">Instance that resolves the contract.</param>
        /// <param name="a_name">Service name.</param>
        public void RegisterInstance<TContract>(TContract a_instance, string a_name)
        {
            if (_services.GetServices(typeof(TContract)).Any(i => i.Name == null))
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}' and without a name.");

            if (_services.GetService(typeof(TContract), a_name) != null)
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}' and with the name '{a_name}'.");

            var newService = new InstanceService(typeof (TContract), a_instance, a_name);
            _services.Add(newService);
        }

        /// <summary>
        /// Register the given service function (<paramref name="a_func"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_func">Service function.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        public void Register<TContract>(Func<TContract> a_func, ILifespan a_lifespan = null)
        {
            if (_services.GetServices(typeof(TContract)).Any())
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}'.");

            var newService = new FuncService<TContract>(a_func);
            _services.Add(newService);
        }

        /// <summary>
        /// Register the given service function (<paramref name="a_func"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_func">Service function.</param>
        /// <param name="a_name">Service name.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        public void Register<TContract>(Func<TContract> a_func, string a_name, ILifespan a_lifespan = null)
        {
            if (_services.GetServices(typeof(TContract)).Any(i => i.Name == null))
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}' and without a name.");

            if (_services.GetService(typeof(TContract), a_name) != null)
                throw new ContainerException($"Service is already registered with contract type '{typeof(TContract).Name}' and with the name '{a_name}'.");

            var newService = new FuncService<TContract>(a_func, a_name);
            _services.Add(newService);
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <returns>Resolved serivce instance.</returns>
        public TContract Resolve<TContract>()
        {
            return (TContract)BuildServiceForContract(typeof(TContract));
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_service">(output) Resolved service instance.</param>
        /// <returns>True iff contract can be resolved.</returns>
        public bool TryResolve<TContract>(out TContract a_service)
        {
            try
            {
                a_service = (TContract)BuildServiceForContract(typeof(TContract));
                return true;
            }
            catch (Exception)
            {
                a_service = default(TContract);
                return false;
            }
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <param name="a_name">Service name.</param>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <returns>Resolved service instance.</returns>
        public TContract Resolve<TContract>(string a_name)
        {
            return (TContract)BuildServiceForContract(typeof(TContract), a_name);
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_service">(output) Resolved service instance.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>True iff contract can be resolved.</returns>
        public bool TryResolve<TContract>(out TContract a_service, string a_name)
        {
            try
            {
                a_service = (TContract)BuildServiceForContract(typeof(TContract), a_name);
                return true;
            }
            catch (Exception)
            {
                a_service = default(TContract);
                return false;
            }
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<paramref name="a_type"/>).
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <returns>Resolved service instance.</returns>
        public object Resolve(Type a_type)
        {
            return BuildServiceForContract(a_type);
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<paramref name="a_type"/>).
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <param name="a_service">(output) Resolved service instance.</param>
        /// <returns>True iff contract can be resolved.</returns>
        public bool TryResolve(Type a_type, out object a_service)
        {
            try
            {
                a_service = BuildServiceForContract(a_type);
                return true;
            }
            catch (Exception)
            {
                a_service = null;
                return false;
            }
        }
        
        /// <summary>
        /// Register the service registered with the given contract type (<paramref name="a_type"/>)
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>True if contract type was resolved.</returns>
        public object Resolve(Type a_type, string a_name)
        {
            return BuildServiceForContract(a_type, a_name);
        }

        /// <summary>
        /// Register the service registered with the given contract type (<paramref name="a_type"/>)
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <param name="a_service">(output) Resolved service instance.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>True iff contract can be resolved.</returns>
        public bool TryResolve(Type a_type, string a_name, out object a_service)
        {
            try
            {
                a_service = BuildServiceForContract(a_type, a_name);
                return true;
            }
            catch (Exception)
            {
                a_service = null;
                return false;
            }
        }
        
        /// <summary>
                /// Resolve all services registerd with the given contract type (<typeparamref name="TContract"/>).
                /// </summary>
                /// <typeparam name="TContract">Contract type.</typeparam>
                /// <returns>All service instances of the contract type.</returns>
        public IEnumerable<TContract> ResolveAll<TContract>()
        {
            return _services.GetServices(typeof(TContract)).Select(BuildService).OfType<TContract>();
        }

        /// <summary>
        /// Resolve all services registerd with the given contract type (<paramref name="a_type"/>).
        /// </summary>
        /// <param name="a_type">Contarct type.</param>
        /// <returns>All service instances of the contract type.</returns>
        public IEnumerable<object> ResolveAll(Type a_type)
        {
            return _services.GetServices(a_type).Select(BuildService);
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<paramref name="a_type"/>) and return
        ///     the service itself.
        /// </summary>
        /// <param name="a_type"></param>
        /// <returns>Resolved service.</returns>
        internal IService ResolveService(Type a_type)
        {
            var existing = _services.GetServices(a_type).FirstOrDefault();
            return existing;
        }

        /// <summary>
        /// Create a child of this container.
        /// </summary>
        /// <returns>Created child container.</returns>
        public Container CreateChild()
        {
            var listing = _services.CreateChild();
            var child = new Container(listing);

            return child;
        }

        /// <summary>
        /// Build the service for the given contract type (<paramref name="a_contract"/>) with the given name (<paramref name="a_name"/>).
        /// </summary>
        /// <param name="a_contract">Contract type.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>Built service instance.</returns>
        private object BuildServiceForContract(Type a_contract, string a_name = null)
        {
            IService service;
            if (a_name == null)
            {
                var services = _services.GetServices(a_contract);
                if (services.Take(2).Count() > 1)
                    throw new ContainerException($"Cannot build single service for contract type '{a_contract.FullName}'. Multiple services are registered with that contract type.");

                service = services.SingleOrDefault();
            }
            else
            {
                service = _services.GetService(a_contract, a_name);
            }

            if (service == null)
                return BuildContract(a_contract);

            return BuildService(service);
        }

        /// <summary>
        /// Attempt to build the given contract type (<paramref name="a_contract"/>) itself. 
        /// </summary>
        /// <param name="a_contract">Contract type.</param>
        /// <returns>Built service instance.</returns>
        private object BuildContract(Type a_contract)
        {
            var service = new Service(this, a_contract, a_contract, null);

            return BuildService(service);
        }

        /// <summary>
        /// Build the given service (<paramref name="a_service"/>).
        /// </summary>
        /// <param name="a_service">Service to build.</param>
        /// <returns>Built service.</returns>
        private static object BuildService(IService a_service)
        {
            try
            {
                return a_service.Resolve();
            }
            catch (InvalidOperationException ex)
            {
                throw new ContainerException($"Could not build the service of type '{a_service.Type.Name}' for contract '{a_service.Contract.Name}'.", ex);
            }
        }
    }
}
