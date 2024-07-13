using Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistance;

public class FeaturesDbContext : DbContext
{
    public DbSet<Client> Clients { get; init; }

    public FeaturesDbContext(DbContextOptions<FeaturesDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FeaturesDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
