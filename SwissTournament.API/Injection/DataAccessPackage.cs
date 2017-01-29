using SimpleInjector;
using SimpleInjector.Packaging;
using SwissTournament.Core.Infrastructure;
using SwissTournament.Data.Infrastructure;

namespace SwissTournament.Api.Injection
{
    public class DataAccessPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IDatabaseInitializer, DatabaseInitializer>();
            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>();
        }
    }
}