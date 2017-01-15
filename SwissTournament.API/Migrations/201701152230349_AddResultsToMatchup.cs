namespace SwissTournament.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResultsToMatchup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matchups", "DidWin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Matchups", "DidTie", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matchups", "DidTie");
            DropColumn("dbo.Matchups", "DidWin");
        }
    }
}
