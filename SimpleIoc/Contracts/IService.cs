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