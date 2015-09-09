using System;
using SimpleIoc.Contracts;
using SimpleIoc.Factories;
using SimpleIoc.Lifespan;

namespace SimpleIoc
{
    public class FuncService<TContract> : IService
    {
        private readonly Func<TContract> _func;
        private readonly ILifespan _lifespan = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_func">Function used to create an instance of this sevice.</param>
        /// <param name="a_name">Name of this service.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        public FuncService(Func<TContract> a_func, string a_name, ILifespan a_lifespan = null)
        {
            _func = a_func;
            Type = typeof (TContract);
            Name = a_name;
            _lifespan = a_lifespan ?? new DefaultLifespan();
            Contract = typeof(TContract);
            Factories = new IServiceFactory[] {new FuncFactory<TContract>(a_func) };
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_func">Function used to create an instance of this sevice.</param>
        /// <param name="a_lifespan">Instance lifespan.</param>
        public FuncService(Func<TContract> a_func, ILifespan a_lifespan = null)
        {
            _func = a_func;
            Type = typeof (TContract);
            Name = null;
            _lifespan = a_lifespan ?? new DefaultLifespan();
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
            var instance = _lifespan.Instance;
            if (instance != null)
            {
                _lifespan.Refresh();
                return instance;
            }

            instance = Factories[0].Create();

            _lifespan.Hold(instance);

            return instance;
        }
    }
}