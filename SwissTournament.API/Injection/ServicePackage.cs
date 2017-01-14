using SimpleInjector;
using SimpleInjector.Packaging;
using SwissTournament.API.Service;
using System.Linq;

namespace Acme.Api.Injection
{
    public class ServicePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<TournamentService>(Lifestyle.Transient);
        }
    }
}