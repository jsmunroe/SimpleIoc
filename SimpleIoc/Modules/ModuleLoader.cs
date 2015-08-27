using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleIoc.Contracts;

namespace SimpleIoc.Modules
{
    public static class ModuleLoader
    {
        /// <summary>
        /// Discover the modules from the current app domain.
        /// </summary>
        public static IEnumerable<IModule> Discover()
        {
            var moduleBase = typeof(IModule);
            var moduleTypes = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                              from type in assembly.GetTypes()
                              where !type.IsAbstract &&
                                    moduleBase.IsAssignableFrom(type)
                              select type;

            foreach (var moduleType in moduleTypes)
            {
                var constructor = moduleType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);

                var module = constructor?.Invoke(null) as IModule;

                if (module != null)
                    yield return module;
            }
        }

        /// <summary>
        /// Discover the modules from the given assembly (<paramref name="a_assembly"/>).
        /// </summary>
        /// <param name="a_assembly"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_assembly"/> is null.</exception>
        public static IEnumerable<IModule> Discover(Assembly a_assembly)
        {
            #region Argument Validation

            if (a_assembly == null)
                throw new ArgumentNullException(nameof(a_assembly));

            #endregion

            var moduleBase = typeof(IModule);
            var moduleTypes = from type in a_assembly.GetTypes()
                              where !type.IsAbstract &&
                                    moduleBase.IsAssignableFrom(type)
                              select type;

            foreach (var moduleType in moduleTypes)
            {
                var constructor = moduleType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);

                var module = constructor?.Invoke(null) as IModule;

                if (module != null)
                    yield return module;
            }
        }

        /// <summary>
        /// Bootstrap each module in "this" sequence (<paramref name="a_this"/>).
        /// </summary>
        /// <param name="a_this">"This" sequence of modules.</param>
        /// <param name="a_container">Container into which to bootstrap.</param>
        public static void Bootstrap(this IEnumerable<IModule> a_this, Container a_container)
        {
            foreach (var module in a_this)
                module.Bootstrap(a_container);
        }

        /// <summary>
        /// Bootstrap all modules in this app domain into "this" container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container into which to bootstrap.</param>
        public static void Bootstrap(this Container a_container)
        {
            Discover().Bootstrap(a_container);
        }
    }
}