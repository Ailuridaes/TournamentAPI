namespace SwissTournament.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRankingName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Ranking", c => c.Int(nullable: false));
            DropColumn("dbo.Players", "Standing");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "Standing", c => c.Int(nullable: false));
            DropColumn("dbo.Players", "Ranking");
        }
    }
}
