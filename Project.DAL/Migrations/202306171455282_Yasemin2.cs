namespace Project.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Yasemin2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", "Ticket_ID", "dbo.Tickets");
            DropIndex("dbo.Seats", new[] { "Ticket_ID" });
            DropColumn("dbo.Seats", "Ticket_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seats", "Ticket_ID", c => c.Int());
            CreateIndex("dbo.Seats", "Ticket_ID");
            AddForeignKey("dbo.Seats", "Ticket_ID", "dbo.Tickets", "ID");
        }
    }
}
