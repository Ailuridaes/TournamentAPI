using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissTournament.Core.Domain
{
    public class Player
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public int Ranking { get; set; }

        public virtual ICollection<Matchup> Matchups { get; set; }
        public virtual Tournament Tournament { get; set; }

        public Player(string playerName, int tournamentId)
        {
            this.Name = playerName;
            this.TournamentId = tournamentId;
            this.Ranking = 1;
        }

        public Player()
        {}

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

        public double GetMatchWinPercentage(bool withByes = true)
        {
            IEnumerable<Matchup> matchups;
            if (withByes)
            {
                matchups = this.Matchups;
            }
            else
            {
                matchups = Matchups.Where(n => !(n.Match.Matchups.Any(mn => mn.Player.Name == "BYE")));
            }

            var result = (double) (matchups.Count(n => n.DidWin == true)*3 + matchups.Count(n => n.DidTie)) / (double)(matchups.Count() * 3);

            // Lower limit of match-win percentage set to 0.33 by MTG Tournament Rules
            return Math.Max(result, 0.33);
        }

        public double GetGameWinPercentage(bool withByes = true)
        {
            IEnumerable<Matchup> matchups;
            if (withByes)
            {
                matchups = this.Matchups;
            }
            else
            {
                matchups = Matchups.Where(n => !(n.Match.Matchups.Any(mn => mn.Player.Name == "BYE")));
            }

            double result = (double) matchups.Sum(n => n.Wins * 3 + n.Ties) / (double)(matchups.Sum(n => n.Wins + n.Ties + n.Losses) * 3);

            // Lower limit of game-win percentage set to 0.33 by MTG Tournament Rules
            return Math.Max(result, 0.33);
        }


    }
}