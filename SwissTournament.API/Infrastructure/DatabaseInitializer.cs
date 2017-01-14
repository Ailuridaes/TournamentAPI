using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Infrastructure
{
    public class DatabaseInitializer
    {
        public void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TournamentDataContext, Migrations.Configuration>());
        }
    }
}