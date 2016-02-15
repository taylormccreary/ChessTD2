namespace ChessTD2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mystery : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Player", "TournamentID");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Player", "TournamentID", c => c.Int(nullable: false));
        }
    }
}
