using SwissTournament.Core.Infrastructure;
using System.Data.Entity;

namespace SwissTournament.Data.Infrastructure
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        public void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TournamentDataContext, Migrations.Configuration>());
        }
    }
}