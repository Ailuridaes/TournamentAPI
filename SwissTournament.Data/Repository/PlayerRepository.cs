using SwissTournament.Data.Infrastructure;
using SwissTournament.Core.Domain;
using SwissTournament.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Data.Repository
{
    public class PlayerRepository : Repository<Core.Domain.Player>, IPlayerRepository
    {
        public PlayerRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}