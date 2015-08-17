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
        /// <param name="a_func"></param>
        public FuncService(Func<TContract> a_func)
        {
            _func = a_func;
            Type = typeof (TContract);
            Factories = new IServiceFactory[] {new FuncFactory<TContract>(a_func) };
        }

        /// <summary>
        /// Contract of this service.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Factories available to create this service.
        /// </summary>
        public IServiceFactory[] Factories { get; }

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