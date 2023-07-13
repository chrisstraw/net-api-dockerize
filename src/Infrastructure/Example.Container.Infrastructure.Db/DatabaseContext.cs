using EFCore.BulkExtensions;
using Example.Container.Core.Domain.Entities;
using Example.Container.Core.Domain.Entities.Profile;
using Example.Container.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using OLT.Core;

namespace Example.Container.Infrastructure.Db
{

    public class DatabaseContext : OltDbContext<DatabaseContext>, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public override string DefaultSchema => DbSchemas.Default;
        public override bool DisableCascadeDeleteConvention => false;
        public override OltContextStringTypes DefaultStringType => OltContextStringTypes.NVarchar;
        public override bool ApplyGlobalDeleteFilter => true;


        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }



        public Task<int> SaveChangeAsync()
        {
            return base.SaveChangesAsync();
        }

        public Task BulkInsertAsync<T>(List<T> entities) where T : class, IOltEntity
        {
            return DbContextBulkExtensions.BulkInsertAsync(this, entities);
        }

        public Task BulkUpdateAsync<T>(List<T> entities) where T : class, IOltEntity
        {
            return DbContextBulkExtensions.BulkUpdateAsync(this, entities);
        }

    }
}