namespace ChessTD2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relatesTournamentToSections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TournamentPlayer", "Tournament_TournamentID", "dbo.Tournament");
            DropForeignKey("dbo.TournamentPlayer", "Player_PlayerID", "dbo.Player");
            DropIndex("dbo.TournamentPlayer", new[] { "Tournament_TournamentID" });
            DropIndex("dbo.TournamentPlayer", new[] { "Player_PlayerID" });
            AddColumn("dbo.Section", "Tournament_TournamentID", c => c.Int());
            CreateIndex("dbo.Section", "Tournament_TournamentID");
            AddForeignKey("dbo.Section", "Tournament_TournamentID", "dbo.Tournament", "TournamentID");
            DropTable("dbo.TournamentPlayer");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TournamentPlayer",
                c => new
                    {
                        Tournament_TournamentID = c.Int(nullable: false),
                        Player_PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tournament_TournamentID, t.Player_PlayerID });
            
            DropForeignKey("dbo.Section", "Tournament_TournamentID", "dbo.Tournament");
            DropIndex("dbo.Section", new[] { "Tournament_TournamentID" });
            DropColumn("dbo.Section", "Tournament_TournamentID");
            CreateIndex("dbo.TournamentPlayer", "Player_PlayerID");
            CreateIndex("dbo.TournamentPlayer", "Tournament_TournamentID");
            AddForeignKey("dbo.TournamentPlayer", "Player_PlayerID", "dbo.Player", "PlayerID", cascadeDelete: true);
            AddForeignKey("dbo.TournamentPlayer", "Tournament_TournamentID", "dbo.Tournament", "TournamentID", cascadeDelete: true);
        }
    }
}
