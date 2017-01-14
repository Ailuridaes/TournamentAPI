using SimpleInjector;
using SimpleInjector.Packaging;
using SwissTournament.API.Infrastructure;

namespace Acme.Api.Injection
{
    public class DataAccessPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<DatabaseInitializer>();
            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<UnitOfWork>();
        }
    }
}