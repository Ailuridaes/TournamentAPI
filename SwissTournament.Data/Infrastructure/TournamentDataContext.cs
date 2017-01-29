using SwissTournament.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SwissTournament.Data.Infrastructure
{
    public class TournamentDataContext : DbContext
    {
        public TournamentDataContext() : base("TournamentDatabase")
        {

        }

        public IDbSet<Tournament> Tournaments { get; set; }
        public IDbSet<Player> Players { get; set; }
        public IDbSet<Match> Matches { get; set; }
        public IDbSet<Matchup> Matchups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Players)
                .WithRequired(p => p.Tournament)
                .HasForeignKey(p => p.TournamentId);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Matches)
                .WithRequired(m => m.Tournament)
                .HasForeignKey(m => m.TournamentId);

            modelBuilder.Entity<Match>()
                .HasMany(m => m.Matchups)
                .WithRequired(n => n.Match)
                .HasForeignKey(n => n.MatchId);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Matchups)
                .WithOptional(n => n.Player)
                .HasForeignKey(n => n.PlayerId);
        }


    }
}