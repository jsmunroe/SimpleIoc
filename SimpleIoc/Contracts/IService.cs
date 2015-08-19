using System;

namespace SimpleIoc.Contracts
{
    public interface IService
    {
        /// <summary>
        /// Contract of this service.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Constract type of this service.
        /// </summary>
        Type Contract { get; }

        /// <summary>
        /// Name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Factories available to create this service.
        /// </summary>
        IServiceFactory[] Factories { get; }

        /// <summary>
        /// Resolve an instance for this service.
        /// </summary>
        /// <returns>Service instance.</returns>
        object Resolve();
    }
}