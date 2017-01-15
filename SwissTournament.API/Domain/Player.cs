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

        public int GetMatchWins()
        {
            return Matchups.Where(n => n.DidWin == true).Count();
        }

        public int GetMatchLosses()
        {
            return Matchups.Where(n => n.DidWin == false && n.DidTie == false).Count();
        }
        public int GetMatchTies()
        {
            return Matchups.Where(n => n.DidTie == true).Count();
        }

        public int GetGameWins()
        {
            return Matchups.Sum(n => n.Wins);
        }

        public int GetGameLosses()
        {
            return Matchups.Sum(n => n.Losses);
        }
        
        public int GetGameTies()
        {
            return Matchups.Sum(n => n.Ties);
        }



    }
}