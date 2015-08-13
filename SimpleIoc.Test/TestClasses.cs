using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoc.Test
{
    public abstract class ServiceBase
    {
        
    }

    public class ServiceWithNoConstructors : ServiceBase
    {

    }

    public class ServiceWithDefaultConstructor : ServiceBase
    {
        public ServiceWithDefaultConstructor()
        {

        }
    }

    public class ServiceWithOneConstructor : ServiceBase
    {
        public ServiceWithOneConstructor(DependencyBase a_dependency1)
        {
            
        }
    }

    public class ServiceWithMultipleConstructors : ServiceBase
    {
        public String Constructor { get; }

        public ServiceWithMultipleConstructors(DependencyBase a_dependency1)
        {
            Constructor = ".ctor(DependencyBase)";
        }

        public ServiceWithMultipleConstructors(DependencyBase a_dependency1, Dependency2 a_dependency2)
        {
            Constructor = ".ctor(DependencyBase, Dependency2)";
        }

        public ServiceWithMultipleConstructors(Dependency2 a_dependency2)
        {
            Constructor = ".ctor(Dependency2)";
        }
    }

    public class Dependency1 : DependencyBase
    {

    }

    public class Dependency2 : DependencyBase
    {

    }

    public abstract class DependencyBase
    {

    }

    public class ComplexService1
    {
        public ComplexService1(ComplexService2 a_service)
        {

        }
    }

    public class ComplexService2
    {
        public ComplexService2(ComplexService3 a_service)
        {

        }
    }

    public class ComplexService3
    {
        public ComplexService3(DependencyBase a_service)
        {

        }
    }
}
