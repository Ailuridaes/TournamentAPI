using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SwissTournament.Data.Infrastructure
{
    public class DatabaseInitializer
    {
        public void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TournamentDataContext, Migrations.Configuration>());
        }
    }
}