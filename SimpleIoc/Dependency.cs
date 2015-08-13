using System;

namespace SimpleIoc
{
    public class Dependency
    {
        private Service _service;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_contract">Dependency contract type.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_contract"/>" is null.</exception>
        public Dependency(Type a_contract)
        {
            #region Argument Validation

            if (a_contract == null)
                throw new ArgumentNullException(nameof(a_contract));

            #endregion

            Contract = a_contract;
        }

        /// <summary>
        /// Dependency contract type.
        /// </summary>
        public Type Contract { get; }

        /// <summary>
        /// Whether this dependency has been fulfilled.
        /// </summary>
        public bool IsFulfilled => _service != null;

        /// <summary>
        /// Fulfill this dependency with the given service (<paramref name="a_service"/>).
        /// </summary>
        /// <param name="a_service">Fulfilling service.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_service"/>" is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if "<paramref name="a_service"/>" is not the right type to fulfill this dependency.</exception>
        public void Fulfill(Service a_service)
        {
            #region Argument Validation

            if (a_service == null)
                throw new ArgumentNullException(nameof(a_service));

            #endregion

            if (!Contract.IsAssignableFrom(a_service.Type))
                throw new InvalidOperationException($"Service of type '{a_service.Type?.Name}' does not fulfill depency of type '{Contract?.Name}'.");

            _service = a_service;
        }

        /// <summary>
        /// Fulfill this dependency from the given container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container.</param>
        public bool Fulfill(Container a_container)
        {
            var service = a_container.ResolveService(a_type: Contract);
            if (service == null)
                return false;

            Fulfill(service);
            return true;
        }

        /// <summary>
        /// Create the service instance for this dependency.
        /// </summary>
        /// <returns></returns>
        public object Resolve()
        {
            return _service.Resolve();
        }
    }
}