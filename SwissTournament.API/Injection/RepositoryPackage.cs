using SimpleInjector;
using SimpleInjector.Packaging;
using SwissTournament.Core.Repository;
using SwissTournament.Data.Repository;

namespace SwissTournament.Api.Injection
{
    public class RepositoryPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IMatchRepository, MatchRepository>(Lifestyle.Transient);
            container.Register<IMatchupRepository, MatchupRepository>(Lifestyle.Transient);
            container.Register<IPlayerRepository, PlayerRepository>(Lifestyle.Transient);
            container.Register<ITournamentRepository, TournamentRepository>(Lifestyle.Transient);
        }
    }
}