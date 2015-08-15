# SimpleIOC
This is a simple to use universal inversion of control container. 

##Basic Usage
    var container = new Container();
    container.Register<IContract, Service>();
    
    IContract service = container.Resolve<IContract>();

    Assert.IsTrue(service is Service);

##Constructor Injection

	public interface IContract { ... }
	public class Dependency { ... }
	public class Service : IContract 
	{
		public Service(Dependency dependency) { ... } 
	}

	var container = new Container();
    container.Register<IContract, Service>();
    container.Register<Dependency, Dependency>();

	IContract service = container.Resolve<IContract>();

    Assert.IsTrue(service is Service);
	
