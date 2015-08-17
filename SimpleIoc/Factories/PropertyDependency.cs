using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// <param name="a_property">Property name.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_contract"/>" is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_property"/>" is null.</exception>
        public PropertyDependency(Type a_contract, PropertyInfo a_property) 
            : base(a_contract)
        {
            #region Argument Validation

            if (a_property == null)
                throw new ArgumentNullException("a_property");

            #endregion

            Property = a_property;
        }

        /// <summary>
        /// Property name.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Apply the value of the service herein to the given instance (<paramref name="a_instance"/>).
        /// </summary>
        /// <param name="a_instance">Instance to which to apply value.</param>
        public void Apply(object a_instance)
        {
            var value = Resolve();

            Property.SetValue(a_instance, value);
        }
    }
}
