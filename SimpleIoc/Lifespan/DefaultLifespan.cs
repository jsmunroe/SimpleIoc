using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleIoc.Contracts;

namespace SimpleIoc.Lifespan
{
    public class DefaultLifespan : ILifespan
    {
        /// <summary>
        /// Held instance.
        /// </summary>
        public object Instance { get; } = null;

        /// <summary>
        /// Hold the given instance (<paramref name="a_instance"/>) until kill is called.
        /// </summary>
        /// <param name="a_instance">Instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_instance"/> is null.</exception>
        public void Hold(object a_instance)
        {
            #region Argument Validation

            if (a_instance == null)
                throw new ArgumentNullException(nameof(a_instance));

            #endregion

            // Not used here.
        }

        /// <summary>
        /// Refresh this lifespan.
        /// </summary>
        public void Refresh()
        {
            // Not used here.
        }

        /// <summary>
        /// Kill the lifespan.
        /// </summary>
        public void Kill()
        {
            // Not used here.
        }
    }
}
