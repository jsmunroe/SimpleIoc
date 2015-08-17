using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoc.Factories
{
    public class PropertyDependency : Dependency
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_contract">Dependency contract type.</param>
        /// <param name="a_propertyName">Property name.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_contract"/>" is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_propertyName"/>" is null.</exception>
        public PropertyDependency(Type a_contract, String a_propertyName) 
            : base(a_contract)
        {
            #region Argument Validation

            if (a_propertyName == null)
                throw new ArgumentNullException("a_propertyName");

            #endregion

            PropertyName = a_propertyName;
        }

        /// <summary>
        /// Property name.
        /// </summary>
        public string PropertyName { get; }

    }
}
