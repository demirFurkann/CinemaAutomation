namespace Project.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        ActivationCode = c.Guid(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Role = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AppUserProfiles",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Job = c.String(),
                        Gender = c.String(),
                        Email = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppUsers", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReservationTime = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsApproved = c.Boolean(nullable: false),
                        ConfirmationCode = c.String(),
                        CancellationReason = c.String(),
                        SeansID = c.Int(),
                        AppUserID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppUsers", t => t.AppUserID)
                .ForeignKey("dbo.Seans", t => t.SeansID)
                .Index(t => t.SeansID)
                .Index(t => t.AppUserID);
            
            CreateTable(
                "dbo.ReservationSeats",
                c => new
                    {
                        SeatID = c.Int(nullable: false),
                        ReservationID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeatID, t.ReservationID })
                .ForeignKey("dbo.Reservations", t => t.ReservationID, cascadeDelete: true)
                .ForeignKey("dbo.Seats", t => t.SeatID, cascadeDelete: true)
                .Index(t => t.SeatID)
                .Index(t => t.ReservationID);

            CreateTable(
                "dbo.Seats",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    SeatNo = c.String(),
                    Row = c.String(),
                    Status = c.Int(nullable: false),
                    SaloonID = c.Int(),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedDate = c.DateTime(),
                    DeletedDate = c.DateTime()
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Saloons", t => t.SaloonID)
                .Index(t => t.SaloonID);
            
            CreateTable(
                "dbo.Saloons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SaloonNumber = c.String(),
                        Capacity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Seans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SeansNumber = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        SaloonId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("dbo.Saloons", t => t.SaloonId, cascadeDelete: true)
                .Index(t => t.SaloonId)
                .Index(t => t.FilmId);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MovieName = c.String(),
                        Duration = c.String(),
                        Type = c.String(),
                        Info = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BoxOffices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OfficeNumber = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseDate = c.DateTime(nullable: false),
                        RezervationID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        BoxOfficeID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Seans_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BoxOffices", t => t.BoxOfficeID)
                .ForeignKey("dbo.Seans", t => t.Seans_ID)
                .Index(t => t.BoxOfficeID)
                .Index(t => t.Seans_ID);
            
            CreateTable(
                "dbo.TicketSeats",
                c => new
                    {
                        SeatID = c.Int(nullable: false),
                        TicketID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeatID, t.TicketID })
                .ForeignKey("dbo.Seats", t => t.SeatID, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketID, cascadeDelete: true)
                .Index(t => t.SeatID)
                .Index(t => t.TicketID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketSeats", "TicketID", "dbo.Tickets");
            DropForeignKey("dbo.TicketSeats", "SeatID", "dbo.Seats");
            DropForeignKey("dbo.Seats", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "Seans_ID", "dbo.Seans");
            DropForeignKey("dbo.Tickets", "BoxOfficeID", "dbo.BoxOffices");
            DropForeignKey("dbo.Seats", "SaloonID", "dbo.Saloons");
            DropForeignKey("dbo.Seans", "SaloonId", "dbo.Saloons");
            DropForeignKey("dbo.Reservations", "SeansID", "dbo.Seans");
            DropForeignKey("dbo.Seans", "FilmId", "dbo.Films");
            DropForeignKey("dbo.ReservationSeats", "SeatID", "dbo.Seats");
            DropForeignKey("dbo.ReservationSeats", "ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "AppUserID", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserProfiles", "ID", "dbo.AppUsers");
            DropIndex("dbo.TicketSeats", new[] { "TicketID" });
            DropIndex("dbo.TicketSeats", new[] { "SeatID" });
            DropIndex("dbo.Tickets", new[] { "Seans_ID" });
            DropIndex("dbo.Tickets", new[] { "BoxOfficeID" });
            DropIndex("dbo.Seans", new[] { "FilmId" });
            DropIndex("dbo.Seans", new[] { "SaloonId" });
            DropIndex("dbo.Seats", new[] { "Ticket_ID" });
            DropIndex("dbo.Seats", new[] { "SaloonID" });
            DropIndex("dbo.ReservationSeats", new[] { "ReservationID" });
            DropIndex("dbo.ReservationSeats", new[] { "SeatID" });
            DropIndex("dbo.Reservations", new[] { "AppUserID" });
            DropIndex("dbo.Reservations", new[] { "SeansID" });
            DropIndex("dbo.AppUserProfiles", new[] { "ID" });
            DropTable("dbo.TicketSeats");
            DropTable("dbo.Tickets");
            DropTable("dbo.BoxOffices");
            DropTable("dbo.Films");
            DropTable("dbo.Seans");
            DropTable("dbo.Saloons");
            DropTable("dbo.Seats");
            DropTable("dbo.ReservationSeats");
            DropTable("dbo.Reservations");
            DropTable("dbo.AppUserProfiles");
            DropTable("dbo.AppUsers");
        }
    }
}
