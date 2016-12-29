using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public int Round { get; set; }
        public int TotalRounds { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}