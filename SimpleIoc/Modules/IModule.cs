namespace SimpleIoc.Modules
{
    public interface IModule
    {
        /// <summary>
        /// Bootstrap this module.
        /// </summary>
        /// <param name="a_container">Container into which to </param>
        void Bootstrap(Container a_container);
    }
}