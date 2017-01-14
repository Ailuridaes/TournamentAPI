using SwissTournament.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Repository
{
    public class MatchRepository : Repository<Domain.Match>
    {
        public MatchRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}