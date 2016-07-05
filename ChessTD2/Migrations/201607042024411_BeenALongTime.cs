namespace ChessTD2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BeenALongTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Round",
                c => new
                    {
                        RoundID = c.Int(nullable: false, identity: true),
                        Number = c.Byte(nullable: false),
                        Section_SectionID = c.Int(),
                    })
                .PrimaryKey(t => t.RoundID)
                .ForeignKey("dbo.Section", t => t.Section_SectionID)
                .Index(t => t.Section_SectionID);
            
            CreateTable(
                "dbo.Pairing",
                c => new
                    {
                        PairingID = c.Int(nullable: false, identity: true),
                        Result = c.Int(nullable: false),
                        Black_PlayerID = c.Int(),
                        Round_RoundID = c.Int(),
                        White_PlayerID = c.Int(),
                    })
                .PrimaryKey(t => t.PairingID)
                .ForeignKey("dbo.Player", t => t.Black_PlayerID)
                .ForeignKey("dbo.Round", t => t.Round_RoundID)
                .ForeignKey("dbo.Player", t => t.White_PlayerID)
                .Index(t => t.Black_PlayerID)
                .Index(t => t.Round_RoundID)
                .Index(t => t.White_PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Round", "Section_SectionID", "dbo.Section");
            DropForeignKey("dbo.Pairing", "White_PlayerID", "dbo.Player");
            DropForeignKey("dbo.Pairing", "Round_RoundID", "dbo.Round");
            DropForeignKey("dbo.Pairing", "Black_PlayerID", "dbo.Player");
            DropIndex("dbo.Pairing", new[] { "White_PlayerID" });
            DropIndex("dbo.Pairing", new[] { "Round_RoundID" });
            DropIndex("dbo.Pairing", new[] { "Black_PlayerID" });
            DropIndex("dbo.Round", new[] { "Section_SectionID" });
            DropTable("dbo.Pairing");
            DropTable("dbo.Round");
        }
    }
}
