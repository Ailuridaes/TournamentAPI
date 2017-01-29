using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Core.Domain
{
    public class Match
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int Round { get; set; }
        public Boolean IsCompleted { get; set; }

        public virtual ICollection<Matchup> Matchups { get; set; }
        public virtual Tournament Tournament { get; set; }

        public Match(int tournamentId, int round)
        {
            TournamentId = tournamentId;
            Round = round;
        }

        public Match()
        {}
    }
}