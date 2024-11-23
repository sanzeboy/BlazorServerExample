using Microsoft.EntityFrameworkCore;
using PopulationPortal.Domain.Entities.Addresses;
using PopulationPortal.Domain.Entities.AppUsers;
using PopulationPortal.Domain.Entities.Populations;

namespace PopulationPortal.Application.Infrastructures
{
    public interface IApplicationDbContext : IDisposable
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Population> Populations { get; set; }
        Task<int> SaveChangesAsync();
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry Entry(object entity);

    }
}
