using Microsoft.EntityFrameworkCore;
using Services.Reservations.Core.DataAccess.Entities;

namespace Services.Reservations.Core.DataAccess;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}