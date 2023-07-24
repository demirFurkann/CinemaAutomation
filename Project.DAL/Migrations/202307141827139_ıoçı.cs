namespace Project.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ıoçı : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Seats", "SeatStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seats", "SeatStatus", c => c.Int(nullable: false));
        }
    }
}
