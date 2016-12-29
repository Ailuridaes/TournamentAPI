namespace SwissTournament.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        Round = c.Int(nullable: false),
                        isCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId);
            
            CreateTable(
                "dbo.Matchups",
                c => new
                    {
                        MatchupId = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        PlayerId = c.Int(),
                        Wins = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        Ties = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchupId)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .Index(t => t.MatchId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        Name = c.String(),
                        Standing = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        TournamentId = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        TotalRounds = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.TournamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matchups", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Matches", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Matchups", "PlayerId", "dbo.Players");
            DropIndex("dbo.Players", new[] { "TournamentId" });
            DropIndex("dbo.Matchups", new[] { "PlayerId" });
            DropIndex("dbo.Matchups", new[] { "MatchId" });
            DropIndex("dbo.Matches", new[] { "TournamentId" });
            DropTable("dbo.Tournaments");
            DropTable("dbo.Players");
            DropTable("dbo.Matchups");
            DropTable("dbo.Matches");
        }
    }
}
