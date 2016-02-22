namespace ChessTD2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Section",
                c => new
                    {
                        SectionID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SectionID);
            
            CreateTable(
                "dbo.SectionPlayer",
                c => new
                    {
                        Section_SectionID = c.Int(nullable: false),
                        Player_PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Section_SectionID, t.Player_PlayerID })
                .ForeignKey("dbo.Section", t => t.Section_SectionID, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.Player_PlayerID, cascadeDelete: true)
                .Index(t => t.Section_SectionID)
                .Index(t => t.Player_PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SectionPlayer", "Player_PlayerID", "dbo.Player");
            DropForeignKey("dbo.SectionPlayer", "Section_SectionID", "dbo.Section");
            DropIndex("dbo.SectionPlayer", new[] { "Player_PlayerID" });
            DropIndex("dbo.SectionPlayer", new[] { "Section_SectionID" });
            DropTable("dbo.SectionPlayer");
            DropTable("dbo.Section");
        }
    }
}
