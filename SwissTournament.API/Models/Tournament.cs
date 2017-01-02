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
        public const int DEFAULT_ROUNDS = 3;

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Match> Matches { get; set; }

        // TODO: Move this logic to Repository pattern
        public Tournament(int totalRounds)
        {
            this.Round = 1;
            this.TotalRounds = totalRounds;
            this.StartTime = DateTime.Now;
        }

        public Tournament() : this(DEFAULT_ROUNDS)
        {
        }
    }
}