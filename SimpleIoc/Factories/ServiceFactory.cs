using System;
using System.Linq;
using System.Reflection;
using SimpleIoc.Contracts;

namespace SimpleIoc.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly Service _service;
        private readonly ConstructorInfo _constructor;

        private Dependency[] _dependencies; 

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_service">Service that defines the instance created by this factory.</param>
        /// <param name="a_constructor">Constructor used to create the instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_service"/>" is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_constructor"/>" is null.</exception>
        public ServiceFactory(Service a_service, ConstructorInfo a_constructor)
        {
            #region Argument Validation

            if (a_service == null)
                throw new ArgumentNullException(nameof(a_service));

            if (a_constructor == null)
                throw new ArgumentNullException(nameof(a_constructor));

            #endregion

            _service = a_service;
            _constructor = a_constructor;

            DependencyComplexity = _constructor.GetParameters().Length;
        }

        /// <summary>
        /// Dependency complexity. 
        /// </summary>
        public int DependencyComplexity { get; set; }

        /// <summary>
        /// Dependencies of this factory.
        /// </summary>
        public Dependency[] Dependencies
        {
            get
            {
                if (_dependencies == null)
                    _dependencies = DiscoverDependencies();

                return _dependencies;
            }
        }

        /// <summary>
        /// Whether this factory can create its service.
        /// </summary>
        public bool CanCreate { get { return Dependencies.All(i => i.IsFulfilled); } }

        /// <summary>
        /// Discover the dependencies for this factory.
        /// </summary>
        /// <returns>Discovered dependencies.</returns>
        private Dependency[] DiscoverDependencies()
        {
            return _constructor.GetParameters().Select(i => new Dependency(i.ParameterType)).ToArray();
        }

        /// <summary>
        /// Fulfill this factory's dependencies with the given container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container.</param>
        /// <returns>True if the factory's dependencies have been completely fulfilled.</returns>
        public bool Fulfill(Container a_container)
        {
            var fulfilled = true;

            foreach (var dependency in Dependencies)
            {
                if (!dependency.Fulfill(a_container))
                    fulfilled = false;
            }

            return fulfilled;
        }

        /// <summary>
        /// Create the service instance.
        /// </summary>
        /// <returns>Created service instance.</returns>
        public object Create()
        {
            if (!CanCreate)
                throw new InvalidOperationException("Cannot create this factory because not all of the dependencies have been fulfilled.");

            var parameters = Dependencies.Select(i => i.Resolve()).ToArray();

            // TODO: Discover attributed public property dependencies.

            return _constructor.Invoke(parameters);
        }

    }
}