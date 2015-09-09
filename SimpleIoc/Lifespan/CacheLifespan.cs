using System;
using SimpleIoc.Contracts;

namespace SimpleIoc.Lifespan
{
    public class CacheLifespan : ILifespan
    {
        private object _instance = null;
        private DateTime _holdTime = new DateTime();

        /// <summary>
        /// Constructor.
        /// </summary>
        public CacheLifespan()
        {
            Timeout = TimeSpan.FromMinutes(15);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_timeout"></param>
        public CacheLifespan(TimeSpan a_timeout)
        {
            Timeout = a_timeout;
        }

        /// <summary>
        /// Timeout.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Held instance.
        /// </summary>
        public object Instance
        {
            get
            {
                if (HasTimedOut())
                    _instance = null;

                return _instance;
            }
        }

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

            _holdTime = DateTime.Now;
            _instance = a_instance;
        }

        /// <summary>
        /// Refresh this lifespan.
        /// </summary>
        public void Refresh()
        {
            if (HasTimedOut())
                return;

            _holdTime = DateTime.Now;
        }

        /// <summary>
        /// Kill the lifespan.
        /// </summary>
        public void Kill()
        {
            _instance = null;
        }

        /// <summary>
        /// Whether this lifespan has timed out.
        /// </summary>
        /// <returns>True if the lifespan has timed out.</returns>
        private bool HasTimedOut()
        {
            return (Timeout < DateTime.Now - _holdTime);
        } 
    }
}