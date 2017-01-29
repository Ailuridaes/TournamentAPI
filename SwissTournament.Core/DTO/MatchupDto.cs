using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Core.DTO
{
    public class MatchupDto
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public Boolean DidWin { get; set; }
        public Boolean DidTie { get; set; }
        public string PlayerName { get; set; }
    }
}