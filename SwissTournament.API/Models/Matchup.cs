using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Models
{
    public class Matchup
    {
        public int MatchupId { get; set; }
        public int MatchId { get; set; }
        public int? PlayerId { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        public virtual Match Match { get; set; }
        public virtual Player Player { get; set; }

        public Matchup(int matchId, int playerId)
        {
            this.MatchId = matchId;
            this.PlayerId = playerId;
        }

        public Matchup(Match match, Player player)
        {
            this.MatchId = match.MatchId;
            this.PlayerId = player.PlayerId;
        }
    }
}