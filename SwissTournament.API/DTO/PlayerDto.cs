using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.DTO
{
    public class PlayerDto
    {
        public int PlayerId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public int Standing { get; set; }

        public class WithMatchups<TMatchup> : PlayerDto
        {
            public IEnumerable<TMatchup> Matchups { get; set; }
        }

        // WithTournaments
    }
}