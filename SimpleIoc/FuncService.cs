using System;
using SimpleIoc.Contracts;
using SimpleIoc.Factories;

namespace SimpleIoc
{
    public class FuncService<TContract> : IService
    {
        private readonly Func<TContract> _func;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_func">Function used to create an instance of this sevice.</param>
        /// <param name="a_name">Name of this service.</param>
        public FuncService(Func<TContract> a_func, string a_name = null)
        {
            _func = a_func;
            Type = typeof (TContract);
            Contract = typeof(TContract);
            Factories = new IServiceFactory[] {new FuncFactory<TContract>(a_func) };
        }

        /// <summary>
        /// Contract of this service.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Name of this service.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Factories available to create this service.
        /// </summary>
        public IServiceFactory[] Factories { get; }

        /// <summary>
        /// Contract type.
        /// </summary>
        public Type Contract { get; }
        
        /// <summary>
        /// Resolve an instance for this service.
        /// </summary>
        /// <returns>Service instance.</returns>
        public object Resolve()
        {
            return Factories[0].Create();
        }
    }
}