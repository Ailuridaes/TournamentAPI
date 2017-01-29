using SwissTournament.Data.Infrastructure;
using SwissTournament.Core.Domain;
using SwissTournament.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Data.Repository
{
    public class MatchupRepository : Repository<Core.Domain.Matchup>, IMatchupRepository
    {
        public MatchupRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}