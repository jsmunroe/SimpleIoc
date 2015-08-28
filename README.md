# SimpleIOC
This is a simple to use universal inversion of control container. 

##Basic Usage

```C#
    public interface IContract { ... }
    public class MyService{ ... }

    var container = new Container();
    container.Register<IContract, MyService>();
    
    IContract service = container.Resolve<IContract>();

    Assert.IsTrue(service is Service);
```

##Constructor Injection

```C#
    public interface IContract { ... }
    public class MyDependency { ... }
    public class MyService : IContract 
    {
        public MyService(MyDependency dependency) { ... } 
    }

    var container = new Container();
    container.Register<IContract, MyService>();
    container.Register<MyDependency, MyDependency>();

    IContract service = container.Resolve<IContract>();

    Assert.IsTrue(service is Service);
```

##Function-Based Service Registry

```C#
    public interface IContract { ... }
    public class MyService : IContract 
    {
        public MyService(String name, int age) { ... } 
    }
    
    var someService = new MyService("My Service", 3);
    var container = new Container();
    container.Register<IContract>(() => someService);

    IContract service = container.Resolve<IContract>();
    
    Assert.AreSame(service, someService);
```

##Property Injection

```C#
    public interface IContract { ... } 
    public MyDependency { ... }
    public class MyService : IContract
    {
        // Must have a public setter
        [Import]
        public MyDependency Dependency { get; set; }
    }
    
    var container = new Container();
    container.Register<IContract, MyService>();
    container.Register<MyDependency, MyDependency>();
    
    IContract service = container.Resolve<IContract>();    
    
    Assert.IsInstanceOfType((service as MyService).Dependency, typeof(MyDependency));
```
    
##Module Loader

```C#
    public MyModule : IModule 
    {
    	public void Bootstrap(Container a_container)
        {
            // Bootstrap all local services.
        }
    }
    
    var modules = ModuleLoader.Discover();
    
    Assert.IsInstanceOfType(modules.First(), typeof(MyModule));
    
    var container = new Container();
    modules.Bootstrap(container);
```
