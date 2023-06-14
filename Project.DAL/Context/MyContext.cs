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
            modelBuilder.Configurations.Add(new EmployeeMap());
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserProfile> Profile { get; set; }
        public DbSet<BoxOffice> BoxOffices { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmSaloon> FilmSaloons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Saloon> Saloons { get; set; }
        public DbSet<Seans> Seans { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
