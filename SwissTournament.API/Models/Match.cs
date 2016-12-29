using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int TournamentId { get; set; }
        public int Round { get; set; }
        public Boolean isCompleted { get; set; }

        public virtual ICollection<Matchup> Matchups { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}