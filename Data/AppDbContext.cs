using Microsoft.EntityFrameworkCore;
using RailwayReservation.Web.Models;

namespace RailwayReservation.Web.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Reservation> Reservations => Set<Reservation>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
