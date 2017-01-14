using SimpleInjector;
using SimpleInjector.Packaging;
using SwissTournament.API.Repository;
using System.Linq;

namespace Acme.Api.Injection
{
    public class RepositoryPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<MatchRepository>(Lifestyle.Transient);
            container.Register<MatchupRepository>(Lifestyle.Transient);
            container.Register<PlayerRepository>(Lifestyle.Transient);
            container.Register<TournamentRepository>(Lifestyle.Transient);
        }
    }
}