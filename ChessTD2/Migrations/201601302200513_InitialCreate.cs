namespace ChessTD2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerID);
            
            CreateTable(
                "dbo.Tournament",
                c => new
                    {
                        TournamentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TournamentID);
            
            CreateTable(
                "dbo.TournamentPlayer",
                c => new
                    {
                        Tournament_TournamentID = c.Int(nullable: false),
                        Player_PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tournament_TournamentID, t.Player_PlayerID })
                .ForeignKey("dbo.Tournament", t => t.Tournament_TournamentID, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.Player_PlayerID, cascadeDelete: true)
                .Index(t => t.Tournament_TournamentID)
                .Index(t => t.Player_PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentPlayer", "Player_PlayerID", "dbo.Player");
            DropForeignKey("dbo.TournamentPlayer", "Tournament_TournamentID", "dbo.Tournament");
            DropIndex("dbo.TournamentPlayer", new[] { "Player_PlayerID" });
            DropIndex("dbo.TournamentPlayer", new[] { "Tournament_TournamentID" });
            DropTable("dbo.TournamentPlayer");
            DropTable("dbo.Tournament");
            DropTable("dbo.Player");
        }
    }
}
