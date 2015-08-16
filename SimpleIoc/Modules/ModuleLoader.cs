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
            var moduleBase = typeof (IModule);
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
    }
}