using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace SimpleIoc.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterAssembly(this Container container, Assembly assembly)
        {
            var attributeType = typeof(ServiceAttribute);

            foreach (var type in assembly.GetTypes())
            {
                var serviceAttribute = type.GetCustomAttribute<ServiceAttribute>();
                if (serviceAttribute == null)
                    continue;

                var contractType = serviceAttribute.ContractType;
                container.Register(contractType, type);
            }
        }

        public static void RegisterAppDomain(this Container container)
        {
            var attributeType = typeof(ServiceAttribute);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                RegisterAssembly(container, assembly);
            }

        }
    }
}
