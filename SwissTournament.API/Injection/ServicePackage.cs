using SimpleInjector;
using SimpleInjector.Packaging;
using SwissTournament.Core.Service;

namespace SwissTournament.Api.Injection
{
    public class ServicePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ITournamentService, TournamentService>(Lifestyle.Transient);
            container.Register<IMatchService, MatchService>(Lifestyle.Transient);
        }
    }
}