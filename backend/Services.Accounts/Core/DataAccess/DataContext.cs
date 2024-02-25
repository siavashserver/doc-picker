using Microsoft.EntityFrameworkCore;
using Services.Accounts.Core.DataAccess.Entities;

namespace Services.Accounts.Core.DataAccess;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}