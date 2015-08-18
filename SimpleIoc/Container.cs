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
        private readonly Dictionary<String, IService> _typesByGuid = new Dictionary<String, IService>();

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <typeparam name="TService">Type of service.</typeparam>
        public void Register<TContract, TService>()
            where TService : TContract
        {
            var typeId = CreateTypeId<TContract>();

            _typesByGuid[typeId] = new Service(this, typeof(TService));
        }

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as itself.
        /// </summary>
        /// <typeparam name="TService">Service type.</typeparam>
        public void Register<TService>()
        {
            Register<TService, TService>();
        }

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as the given contract (<typeparamref name="TContract"/>) with the given name (<paramref name="a_name"/>).
        /// </summary>
        /// <param name="a_name">Service name.</param>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <typeparam name="TService">Type of service.</typeparam>
        public void Register<TContract, TService>(String a_name)
            where TService : TContract
        {
            var typeId = CreateTypeId<TContract>(a_name);

            _typesByGuid[typeId] = new Service(this, typeof(TService));
        }

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as itself  with the given name (<paramref name="a_name"/>).
        /// </summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <param name="a_name">Service name.</param>
        public void Register<TService>(String a_name)
        {
            Register<TService, TService>(a_name);
        }

        /// <summary>
        /// Register the given service function (<paramref name="a_func"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_func">Service function.</param>
        public void Register<TContract>(Func<TContract> a_func)
        {
            var typeId = CreateTypeId<TContract>();

            _typesByGuid[typeId] = new FuncService<TContract>(a_func);
        }

        /// <summary>
        /// Register the given service function (<paramref name="a_func"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_func">Service function.</param>
        /// <param name="a_name">Service name.</param>
        public void Register<TContract>(Func<TContract> a_func, String a_name)
        {
            var typeId = CreateTypeId<TContract>();

            _typesByGuid[typeId] = new FuncService<TContract>(a_func);
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>)
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <returns>Resolved serivce instance.</returns>
        public TContract Resolve<TContract>()
        {
            return (TContract)BuildServiceForContract(typeof(TContract));
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>)
        /// </summary>
        /// <param name="a_name">Service name.</param>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <returns>Resolved service instance.</returns>
        public TContract Resolve<TContract>(String a_name)
        {
            return (TContract)BuildServiceForContract(typeof(TContract), a_name);
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<paramref name="a_type"/>)
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <returns>Resolved service instance.</returns>
        public object Resolve(Type a_type)
        {
            return BuildServiceForContract(a_type);
        }

        /// <summary>
        /// Resolve the service registered with the given contract type (<paramref name="a_type"/>) and return
        ///     the service itself.
        /// </summary>
        /// <param name="a_type"></param>
        /// <returns>Resolved service.</returns>
        internal IService ResolveService(Type a_type)
        {
            var typeId = CreateTypeId(a_type);

            if (!_typesByGuid.ContainsKey(typeId))
                return null;

            return _typesByGuid[typeId];
        }

        /// <summary>
        /// Register the service registered with the given contract type (<paramref name="a_type"/>)
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>True if contract type was resolved.</returns>
        public object Resolve(Type a_type, String a_name)
        {
            return BuildServiceForContract(a_type, a_name);
        }

        /// <summary>
        /// Build the service for the given contract type (<paramref name="a_type"/>) with the given name (<paramref name="a_name"/>).
        /// </summary>
        /// <param name="a_type">Contract type.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>Built service.</returns>
        private object BuildServiceForContract(Type a_type, String a_name = null)
        {
            var typeId = CreateTypeId(a_type, a_name);

            // TODO: Enable building of contract itself if possible.

            if (!_typesByGuid.ContainsKey(typeId))
                throw new ContainerException($"Service has not been registered for contract '{a_type.Name}'.");

            var service = _typesByGuid[typeId];
            
            try
            {
                return service.Resolve();
            }
            catch (InvalidOperationException ex)
            {
                throw new ContainerException($"Could not build the service of type '{service.Type.Name}' for contract '{a_type.Name}'.", ex);
            }
        }

        /// <summary>
        /// Create a type identifier for the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_name">Service name.</param>
        /// <returns>Created type identifier.</returns>
        private string CreateTypeId<TContract>(String a_name = null)
        {
            return CreateTypeId(typeof(TContract), a_name);
        }

        /// <summary>
        /// Create a type identifier for the given contract (<paramref name="a_type"/>).
        /// </summary>
        /// <param name="a_type">Contract type.</param>
        /// <returns>Created type identifier.</returns>
        private string CreateTypeId(Type a_type, String a_name = null)
        {
            var typeId = a_type.FullName + a_type.GUID.ToString();

            if (!String.IsNullOrWhiteSpace(a_name))
                typeId += "+" + a_name;

            return typeId;
        }

    }
}
