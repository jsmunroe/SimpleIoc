using System;
using SimpleIoc.Contracts;

namespace SimpleIoc.Factories
{
    public class FuncFactory<TContract> : IServiceFactory
    {
        private readonly Func<TContract> _func;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_func">Function used to create service instance for this contract.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_func"/>" is null.</exception>
        public FuncFactory(Func<TContract> a_func)
        {
            #region Argument Validation

            if (a_func == null)
                throw new ArgumentNullException(nameof(a_func));

            #endregion

            _func = a_func;

            DependencyComplexity = 0;
            Dependencies = new Dependency[] {};
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
        public bool Fulfill(Container a_container)
        {
            return true;
        }

        /// <summary>
        /// Create the service instance.
        /// </summary>
        /// <returns>Created service instance.</returns>
        public object Create()
        {
            return _func();
        }
    }
}