namespace Project.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yaso5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUser",
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
                "dbo.AppUserProfile",
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
                .ForeignKey("dbo.AppUser", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReservationTime = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsApproved = c.Boolean(nullable: false),
                        ConfirmationCode = c.String(),
                        CancellationReason = c.String(),
                        SeansID = c.Int(),
                        SeatID = c.Int(),
                        AppUserID = c.Int(),
                        EmployeeID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppUser", t => t.AppUserID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .ForeignKey("dbo.Seans", t => t.SeansID)
                .Index(t => t.SeansID)
                .Index(t => t.AppUserID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserRole = c.Int(nullable: false),
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
                        SessionNumber = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        FilmSaloonID = c.Int(),
                        TicketID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        FilmSaloon_SaloonID = c.Int(),
                        FilmSaloon_FilmID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FilmSaloon", t => new { t.FilmSaloon_SaloonID, t.FilmSaloon_FilmID })
                .ForeignKey("dbo.Ticket", t => t.TicketID)
                .Index(t => t.TicketID)
                .Index(t => new { t.FilmSaloon_SaloonID, t.FilmSaloon_FilmID });
            
            CreateTable(
                "dbo.FilmSaloon",
                c => new
                    {
                        SaloonID = c.Int(nullable: false),
                        FilmID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SaloonID, t.FilmID })
                .ForeignKey("dbo.Film", t => t.FilmID, cascadeDelete: true)
                .ForeignKey("dbo.Saloon", t => t.SaloonID, cascadeDelete: true)
                .Index(t => t.SaloonID)
                .Index(t => t.FilmID);
            
            CreateTable(
                "dbo.Film",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MovieName = c.String(),
                        Duration = c.String(),
                        Type = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Saloon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SaloonNumber = c.String(),
                        Capaciyt = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        BoxOfficeID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BoxOffice", t => t.BoxOfficeID)
                .Index(t => t.BoxOfficeID);
            
            CreateTable(
                "dbo.BoxOffice",
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
                "dbo.Seat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SeatNumber = c.String(),
                        Status = c.Int(nullable: false),
                        ReservationID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Reservation", t => t.ReservationID)
                .Index(t => t.ReservationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seat", "ReservationID", "dbo.Reservation");
            DropForeignKey("dbo.Seans", "TicketID", "dbo.Ticket");
            DropForeignKey("dbo.Ticket", "BoxOfficeID", "dbo.BoxOffice");
            DropForeignKey("dbo.Reservation", "SeansID", "dbo.Seans");
            DropForeignKey("dbo.Seans", new[] { "FilmSaloon_SaloonID", "FilmSaloon_FilmID" }, "dbo.FilmSaloon");
            DropForeignKey("dbo.FilmSaloon", "SaloonID", "dbo.Saloon");
            DropForeignKey("dbo.FilmSaloon", "FilmID", "dbo.Films");
            DropForeignKey("dbo.Reservation", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Reservation", "AppUserID", "dbo.AppUser");
            DropForeignKey("dbo.AppUserProfile", "ID", "dbo.AppUser");
            DropIndex("dbo.Seat", new[] { "ReservationID" });
            DropIndex("dbo.Ticket", new[] { "BoxOfficeID" });
            DropIndex("dbo.FilmSaloon", new[] { "FilmID" });
            DropIndex("dbo.FilmSaloon", new[] { "SaloonID" });
            DropIndex("dbo.Seans", new[] { "FilmSaloon_SaloonID", "FilmSaloon_FilmID" });
            DropIndex("dbo.Seans", new[] { "TicketID" });
            DropIndex("dbo.Reservation", new[] { "EmployeeID" });
            DropIndex("dbo.Reservation", new[] { "AppUserID" });
            DropIndex("dbo.Reservation", new[] { "SeansID" });
            DropIndex("dbo.AppUserProfile", new[] { "ID" });
            DropTable("dbo.Seat");
            DropTable("dbo.BoxOffice");
            DropTable("dbo.Ticket");
            DropTable("dbo.Saloon");
            DropTable("dbo.Films");
            DropTable("dbo.FilmSaloon");
            DropTable("dbo.Seans");
            DropTable("dbo.Employee");
            DropTable("dbo.Reservation");
            DropTable("dbo.AppUserProfile");
            DropTable("dbo.AppUser");
        }
    }
}
