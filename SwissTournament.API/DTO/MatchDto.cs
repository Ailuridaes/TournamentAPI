using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.DTO
{
    public class MatchDto
    {
        public int MatchId { get; set; }
        public int TournamentId { get; set; }
        public int Round { get; set; }
        public Boolean isCompleted { get; set; }
        
        public class WithMatchups<TMatchup> : MatchDto
        {
            public IEnumerable<TMatchup> Matchups { get; set; }
        }
    }
}