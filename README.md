# SimpleIOC
This is a simple to use universal inversion of control container. 

##Basic Usage

    public interface IContract { ... }
    public class MyService{ ... }

    var container = new Container();
    container.Register<IContract, MyService>();
    
    IContract service = container.Resolve<IContract>();

    Assert.IsTrue(service is Service);

##Constructor Injection

    public interface IContract { ... }
    public class MyDependency { ... }
    public class MyService : IContract 
    {
        public Service(MyDependency dependency) { ... } 
    }

    var container = new Container();
    container.Register<IContract, MyService>();
    container.Register<MyDependency, MyDependency>();

    IContract service = container.Resolve<IContract>();

    Assert.IsTrue(service is Service);

##Function-Based Service Registry

    public interface IContract { ... }
    public class MyService : IContract 
    {
        public Service(String name, int age) { ... } 
    }
    
    var someService = new MyService("My Service", 3);
    var container = new Container();
    container.Register<IContract>(() => someService);

    IContract service = container.Resolve<IContract>();
    
    Assert.AreSame(service, someService);

##Module Loader

    public MyModule : IModule 
    {
    	public void Bootstrap(Container a_container)
        {
            // Bootstrap all local services.
        }
    }
    
    var modules = ModuleLoader.Discover();
    
    Assert.IsInstanceOfType(modules.First(), typeof(MyModule));
