using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.DTO
{
    public class MatchupDto
    {
        public int MatchupId { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        public class WithMatch : PlayerDto
        {
            public MatchDto Match { get; set; }
        }

        public class WithPlayer : TournamentDto
        {
            public PlayerDto Player { get; set; }
        }

        public class WithAll : TournamentDto
        {
            public PlayerDto Players { get; set; }
            public MatchDto Matches { get; set; }
        }
    }
}