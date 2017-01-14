using SwissTournament.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Repository
{
    public class TournamentRepository : Repository<Domain.Tournament>
    {
        public TournamentRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}