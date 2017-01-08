using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.API.Domain
{
    public class Player
    {
        public int PlayerId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public int Standing { get; set; }

        public virtual ICollection<Matchup> Matchups { get; set; }
        public virtual Tournament Tournament { get; set; }

        public Player(string playerName, int tournamentId)
        {
            this.Name = playerName;
            this.TournamentId = tournamentId;
            this.Standing = 1;
        }
    }
}