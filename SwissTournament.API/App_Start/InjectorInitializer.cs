using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using SwissTournament.Core.Infrastructure;

namespace SwissTournament.API.App_Start
{
    public class InjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            // container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.RegisterPackages();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            container.GetInstance<IDatabaseInitializer>().Initialize();
        }
    }
}