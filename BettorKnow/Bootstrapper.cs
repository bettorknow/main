using System.ServiceModel;
using System.Web.Mvc;
using BettorKnow.Controllers;
using BettorKnow.InterceptionBehaviours;
using BettorKnow.PremierStatisticsService;
using BettorKnow.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Unity.Mvc4;

namespace BettorKnow
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
        var container = new UnityContainer();
        container.AddNewExtension<Interception>();

        


        // register all your components with the container here
        // it is NOT necessary to register your controllers

        // e.g. container.RegisterType<ITestService, TestService>();   
        container.AddNewExtension<Interception>();
        container.RegisterType<IPremierStatistics>(new ContainerControlledLifetimeManager(), new InjectionFactory(c => new ChannelFactory<IPremierStatistics>("BasicHttpBinding_IPremierStatistics").CreateChannel()));
        container.RegisterType<IFixtureService, FixtureService>(
            new Interceptor<InterfaceInterceptor>(),
            new InterceptionBehavior<LogFixtureRequestBehaviour>(),
            new InterceptionBehavior<IgnoreInvalidCertBehaviour>());
        container.RegisterType<IController, FixturesController>("Fixtures");
        RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
    
    }
  }
}