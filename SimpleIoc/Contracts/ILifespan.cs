namespace SimpleIoc.Contracts
{
    public interface ILifespan
    {
        /// <summary>
        /// Held instance.
        /// </summary>
        object Instance { get; }

        /// <summary>
        /// Hold the given instance (<paramref name="a_instance"/>) until kill is called.
        /// </summary>
        /// <param name="a_instance">Instance.</param>
        void Hold(object a_instance);

        /// <summary>
        /// Refresh this lifespan.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Kill the lifespan.
        /// </summary>
        void Kill();
    }
}