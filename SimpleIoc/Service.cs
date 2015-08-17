using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using SimpleIoc.Contracts;
using SimpleIoc.Factories;

namespace SimpleIoc
{
    public class Service : IService
    {
        private readonly Container _container;

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

            Factories = DiscoverFactories();
        }

        /// <summary>
        /// Contract of this service.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Factories available to create this service.
        /// </summary>
        public IServiceFactory[] Factories { get; }

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
        /// Get property dependencies.
        /// </summary>
        /// <returns>Property dependencies.</returns>
        internal IEnumerable<PropertyDependency> GetPropertyDependencies()
        {
            var items = from property in Type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        let attr = property.GetCustomAttribute<ImportAttribute>()
                        where property.CanWrite &&
                              attr != null
                        select new
                        {
                            Property = property,
                            Attr = attr,
                        };

            foreach (var item in items)
            {
                var type = item.Property.PropertyType;

                if (type.IsAssignableFrom(item.Attr.ContractType))
                    type = item.Attr.ContractType;

                yield return new PropertyDependency(type, item.Property);
            }
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

    [AttributeUsage(AttributeTargets.Property, AllowMultiple= false, Inherited = false)]
    public class ImportAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Attribute"/> class.
        /// </summary>
        public ImportAttribute(Type a_contractType = null)
        {
            ContractType = a_contractType;
        }

        public Type ContractType { get; }
    }
}