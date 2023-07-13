using Example.Container.Core.Domain.Entities.Profile;
using Microsoft.EntityFrameworkCore;
using OLT.Core;

namespace Example.Container.Infrastructure.Abstractions
{
    public interface IDatabaseContext : IOltDbContext
    {
        DbSet<Profile> Profiles { get; set; }
        DbSet<Address> Addresses { get; set; }

        Task<int> SaveChangeAsync();
        Task BulkInsertAsync<T>(List<T> entities) where T : class, IOltEntity;
        Task BulkUpdateAsync<T>(List<T> entities) where T : class, IOltEntity;
    }

}