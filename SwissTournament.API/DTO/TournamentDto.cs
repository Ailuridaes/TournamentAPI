using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.DTO
{
    public class TournamentDto
    {
        public int TournamentId { get; set; }
        public int Round { get; set; }
        public int TotalRounds { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public class WithAll<TPlayer> : TournamentDto
        {
            public IEnumerable<TPlayer> Players { get; set; }
            public IEnumerable<MatchDto> Matches { get; set; }
        }
    }
}