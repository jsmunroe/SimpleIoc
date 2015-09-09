using System;
using SimpleIoc.Contracts;

namespace SimpleIoc.Lifespan
{
    public class ContainerLifespan : ILifespan
    {
        /// <summary>
        /// Held instance.
        /// </summary>
        public object Instance { get; private set; }

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

            Instance = a_instance;
        }

        /// <summary>
        /// Notify that the lifespan should still be holding on to its instance.
        /// </summary>
        public void Refresh()
        {
            // Not used.
        }

        /// <summary>
        /// Kill the lifespan.
        /// </summary>
        public void Kill()
        {
            Instance = null;
        }
    }
}