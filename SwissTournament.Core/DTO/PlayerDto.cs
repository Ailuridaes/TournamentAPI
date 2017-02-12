using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Core.DTO
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public int Ranking { get; set; }

        public class WithMatchups : PlayerDto
        {
            public IEnumerable<MatchupDto> Matchups { get; set; }
        }

        public class WithScores : PlayerDto
        {
            public int MatchWins { get; set; }
            public int MatchLosses { get; set; }
            public int MatchTies { get; set; }
            public int GameWins { get; set; }
            public int GameLosses { get; set; }
            public int GameTies { get; set; }
        }
    }
}