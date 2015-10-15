using System;
using SimpleIoc.Contracts;

namespace SimpleIoc.Factories
{
    public class InstanceFactory : IServiceFactory
    {
        private readonly InstanceService _instanceService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_instanceServiceService">Instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_instanceServiceService"/> is null.</exception>
        public InstanceFactory(InstanceService a_instanceServiceService)
        {
            #region Argument Validation

            if (a_instanceServiceService == null)
                throw new ArgumentNullException(nameof(a_instanceServiceService));

            #endregion

            _instanceService = a_instanceServiceService;
            DependencyComplexity = 0;
            Dependencies = new Dependency[0];
            CanCreate = true;
        }

        /// <summary>
        /// Dependency complexity. 
        /// </summary>
        public int DependencyComplexity { get; }

        /// <summary>
        /// Dependencies of this factory.
        /// </summary>
        public Dependency[] Dependencies { get; }

        /// <summary>
        /// Whether this factory can create its service.
        /// </summary>
        public bool CanCreate { get; }

        /// <summary>
        /// Fulfill this factory's dependencies with the given container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container.</param>
        /// <returns>True if the factory's dependencies have been completely fulfilled.</returns>
        public bool Fulfill(IContainer a_container)
        {
            return true;
        }

        /// <summary>
        /// Create the service instance.
        /// </summary>
        /// <returns>Created service instance.</returns>
        public object Create()
        {
            return _instanceService.Resolve();
        }
    }
}