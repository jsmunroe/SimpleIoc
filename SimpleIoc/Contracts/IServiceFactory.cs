namespace SimpleIoc.Contracts
{
    public interface IServiceFactory
    {
        /// <summary>
        /// Dependency complexity. 
        /// </summary>
        int DependencyComplexity { get; }

        /// <summary>
        /// Dependencies of this factory.
        /// </summary>
        Dependency[] Dependencies { get; }

        /// <summary>
        /// Whether this factory can create its service.
        /// </summary>
        bool CanCreate { get; }

        /// <summary>
        /// Fulfill this factory's dependencies with the given container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container.</param>
        /// <returns>True if the factory's dependencies have been completely fulfilled.</returns>
        bool Fulfill(Container a_container);

        /// <summary>
        /// Create the service instance.
        /// </summary>
        /// <returns>Created service instance.</returns>
        object Create();
    }
}