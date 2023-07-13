using EFCore.BulkExtensions;
using Example.Container.Core.Domain.Entities;
using Example.Container.Core.Domain.Entities.Profile;
using Example.Container.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using OLT.Core;

namespace Example.Container.Infrastructure.Db
{
    public class SqlServerDatabaseContext : OltSqlDbContext<SqlServerDatabaseContext>, IDatabaseContext
    {
        public SqlServerDatabaseContext(DbContextOptions<SqlServerDatabaseContext> options) : base(options)
        {
        }

        public override string DefaultSchema => DbSchemas.Default;
        public override bool DisableCascadeDeleteConvention => false;
        public override OltContextStringTypes DefaultStringType => OltContextStringTypes.NVarchar;
        public override bool ApplyGlobalDeleteFilter => true;

        /// <summary>
        /// Changes the Default Identity for SQL Server to start with these values
        /// </summary>
        /// <remarks>
        /// Package OLT.EF.Core.SqlServer
        /// </remarks>
        protected override int IdentitySeed => 10502;
        protected override int IdentityIncrement => 1;

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