using Project.ENTITIES.Models;
using Project.MAP.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new BoxOfficeMap());
            modelBuilder.Configurations.Add(new FilmMap());
            modelBuilder.Configurations.Add(new FilmSaloonMap());
            modelBuilder.Configurations.Add(new ReservationMap());
            modelBuilder.Configurations.Add(new SaloonMap());
            modelBuilder.Configurations.Add(new SeansMap());
            modelBuilder.Configurations.Add(new SeatMap());
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new ReservationSeatMap());
            modelBuilder.Configurations.Add(new TicketSeansMap());
            modelBuilder.Configurations.Add(new TicketSeatMap());
        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<AppUserProfile> Profile { get; set; }
        public DbSet<BoxOffice> BoxOffice { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmSaloon> FilmSaloon { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Saloon> Saloon { get; set; }
        public DbSet<Seans> Seans { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<ReservationSeat> ReservationSeat { get; set; }
        public DbSet<TicketSeans> TicketSeans { get; set;}
        public DbSet<TicketSeat> TicketSeat { get; set; }
    }
}
