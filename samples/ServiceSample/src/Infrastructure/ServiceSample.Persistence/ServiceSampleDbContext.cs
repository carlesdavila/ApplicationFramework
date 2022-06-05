using Microsoft.EntityFrameworkCore;
using ServiceSample.Domain.Entities;
using ServiceSample.Application.Interfaces;

namespace ServiceSample.Persistence;

public class ServiceSampleDbContext : DbContext, IServiceSampleDbContext
{
    public ServiceSampleDbContext(DbContextOptions<ServiceSampleDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityColumns();
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
