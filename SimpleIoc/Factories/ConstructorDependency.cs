using System;

namespace SimpleIoc.Factories
{
    public class ConstructorDependency : Dependency
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_contract">Dependency contract type.</param>
        /// <param name="a_paramName">Parameter name.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_contract"/>" is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_paramName"/>" is null.</exception>
        public ConstructorDependency(Type a_contract, String a_paramName) 
            : base(a_contract)
        {
            #region Argument Validation

            if (a_paramName == null)
                throw new ArgumentNullException(nameof(a_paramName));

            #endregion

            ParamName = a_paramName;
        }

        /// <summary>
        /// Name of the constructor parameter.
        /// </summary>
        public string ParamName { get; }
    }
}