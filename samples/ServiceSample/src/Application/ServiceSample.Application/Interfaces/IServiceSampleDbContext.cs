using ApplicationFramework.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceSample.Domain.Entities;

namespace ServiceSample.Application.Interfaces;

public interface IServiceSampleDbContext : IDbContext
{
    DbSet<User> Users { get; set; }
}
