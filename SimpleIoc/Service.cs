using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using SimpleIoc.Contracts;
using SimpleIoc.Factories;

namespace SimpleIoc
{
    public class Service : IService
    {
        private readonly Container _container;
        private IServiceFactory[] _factories;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_container">Container that owns this service.</param>
        /// <param name="a_type">Contract of service.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_type"/>" is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_container"/>" is null.</exception>
        public Service(Container a_container, Type a_type)
        {
            #region Argument Validation

            if (a_container == null)
                throw new ArgumentNullException(nameof(a_container));

            if (a_type == null)
                throw new ArgumentNullException(nameof(a_type));

            #endregion

            _container = a_container;
            Type = a_type;
        }

        /// <summary>
        /// Contract of this service.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Factories available to create this service.
        /// </summary>
        public IServiceFactory[] Factories
        {
            get
            {
                if (_factories == null)
                    _factories = DiscoverFactories();

                return _factories;
            }
        }

        /// <summary>
        /// Resolve an instance for this service.
        /// </summary>
        /// <returns>Service instance.</returns>
        public object Resolve()
        {
            foreach (var factory in Factories.Where(i => !i.CanCreate))
                factory.Fulfill(_container);

            var selectedFactory = Factories.FirstOrDefault(i => i.CanCreate);

            // TODO: Cache selected factory. Relinquishing when the parent container is changed.
            // TODO: Enable attribute constructor selection.

            if (selectedFactory == null)
                throw new InvalidOperationException("Service cannot be resolved.");

            return selectedFactory.Create();
        }

        /// <summary>
        /// Discover factories available for this service.
        /// </summary>
        /// <returns>Discovered factories.</returns>
        private IServiceFactory[] DiscoverFactories()
        {
            var constructors = Type.GetConstructors();

            var factories = new List<IServiceFactory>();

            var constructorFactories = constructors.Select(i => new ActivateFactory(this, i));
            factories.AddRange(constructorFactories);

            return factories.OrderByDescending(i => i.DependencyComplexity).ToArray();
        }
    }
}