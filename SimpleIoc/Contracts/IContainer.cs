using System;
using System.Collections.Generic;

namespace SimpleIoc.Contracts
{
    public interface IContainer
    {
        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <param name="a_lifespan">Instance lifespan.</param>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <typeparam name="TService">Type of service.</typeparam>
        void Register<TContract, TService>(ILifespan a_lifespan = null)
            where TService : TContract;

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as itself.
        /// </summary>
        /// <param name="a_lifespan">Instance lifespan.</param>
        /// <typeparam name="TService">Service type.</typeparam>
        void Register<TService>(ILifespan a_lifespan = null);

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as the given contract (<typeparamref name="TContract"/>) with the given name (<paramref name="a_name"/>).
        /// </summary>
        /// <param name="a_name">Service name.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <typeparam name="TService">Type of service.</typeparam>
        void Register<TContract, TService>(string a_name, ILifespan a_lifespan = null)
            where TService : TContract;

        /// <summary>
        /// Register the given service (<typeparamref name="TService"/>) as itself  with the given name (<paramref name="a_name"/>).
        /// </summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <param name="a_name">Service name.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        void Register<TService>(string a_name, ILifespan a_lifespan = null);

        /// <summary>
        /// Register the given instance as the given contract (<typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_instance">Instance that resolves the contract.</param>
        void RegisterInstance<TContract>(TContract a_instance);

        /// <summary>
        /// Register the given instance as the given contract (<typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_instance">Instance that resolves the contract.</param>
        /// <param name="a_name">Service name.</param>
        void RegisterInstance<TContract>(TContract a_instance, string a_name);

        /// <summary>
        /// Register the given service function (<paramref name="a_func"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_func">Service function.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        void Register<TContract>(Func<TContract> a_func, ILifespan a_lifespan = null);

        /// <summary>
        /// Register the given service function (<paramref name="a_func"/>) as the given contract (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_func">Service function.</param>
        /// <param name="a_name">Service name.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        void Register<TContract>(Func<TContract> a_func, string a_name, ILifespan a_lifespan = null);

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <returns>Resolved serivce instance.</returns>
        TContract Resolve<TContract>();

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_service">(output) Resolved service instance.</param>
        /// <returns>True iff contract can be resolved.</returns>
        bool TryResolve<TContract>(out TContract a_service);

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <param name="a_name">Service name.</param>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <returns>Resolved service instance.</returns>
        TContract Resolve<TContract>(string a_name);

        /// <summary>
        /// Resolve the service registered with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Type of contract.</typeparam>
        /// <param name="a_service">(output) Resolved service instance.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>True iff contract can be resolved.</returns>
        bool TryResolve<TContract>(out TContract a_service, string a_name);

        /// <summary>
        /// Resolve the service registered with the given contract type (<paramref name="a_type"/>).
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <returns>Resolved service instance.</returns>
        object Resolve(Type a_type);

        /// <summary>
        /// Resolve the service registered with the given contract type (<paramref name="a_type"/>).
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <param name="a_service">(output) Resolved service instance.</param>
        /// <returns>True iff contract can be resolved.</returns>
        bool TryResolve(Type a_type, out object a_service);

        /// <summary>
        /// Register the service registered with the given contract type (<paramref name="a_type"/>)
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>True if contract type was resolved.</returns>
        object Resolve(Type a_type, string a_name);

        /// <summary>
        /// Register the service registered with the given contract type (<paramref name="a_type"/>)
        /// </summary>
        /// <param name="a_type">Type of contract.</param>
        /// <param name="a_service">(output) Resolved service instance.</param>
        /// <param name="a_name">Service name.</param>
        /// <returns>True iff contract can be resolved.</returns>
        bool TryResolve(Type a_type, string a_name, out object a_service);

        /// <summary>
        /// Resolve all services registerd with the given contract type (<typeparamref name="TContract"/>).
        /// </summary>
        /// <typeparam name="TContract">Contract type.</typeparam>
        /// <returns>All service instances of the contract type.</returns>
        IEnumerable<TContract> ResolveAll<TContract>();

        /// <summary>
        /// Resolve all services registerd with the given contract type (<paramref name="a_type"/>).
        /// </summary>
        /// <param name="a_type">Contarct type.</param>
        /// <returns>All service instances of the contract type.</returns>
        IEnumerable<object> ResolveAll(Type a_type);

        object Build(Type a_type);

        TService Build<TService>();


        /// <summary>
        /// Create a child of this container.
        /// </summary>
        /// <returns>Created child container.</returns>
        IContainer CreateChild();

        /// <summary>
        /// Resolve the service registered with the given contract type (<paramref name="a_type"/>) and return
        ///     the service itself.
        /// </summary>
        /// <param name="a_type"></param>
        /// <returns>Resolved service.</returns>
        IService ResolveService(Type a_type);
    }
}