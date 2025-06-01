using Microsoft.EntityFrameworkCore;
using ProductAPI.VSA.Domain;
using ProductAPI.VSA.Services;

namespace ProductAPI.VSA.Data
{
    public class AppDbContext : DbContext
    {
        //public string TenantId {  get; set; }
        //private readonly ITenantService _tenantService;

        public AppDbContext()
        {
            
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            //_tenantService = tenantService;
            //TenantId = _tenantService.GetCurrentTenant()?.TId;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            // Here to make a global filter query in which TenantId will be automatically inserted
            //modelBuilder.Entity<Product>().HasQueryFilter(p=>p.TenantId == TenantId);
            base.OnModelCreating(modelBuilder);
        }


        // To track changes in entity. We supposed to make a tracker for each class (model)
        // implement this interface (IMustHaveTenant).

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    foreach(var entry in ChangeTracker.Entries<IMustHaveTenant>().Where(e=>e.State == EntityState.Added))
        //    {
        //       entry.Entity.TenantId = TenantId;
        //    }
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        

        //To chack provider for each Tenant
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connetionString = _tenantService.GetConnectionString();

        //    if (!string.IsNullOrEmpty(connetionString))
        //    {
        //        var dbProvider = _tenantService.GetDataBaseProvider();
        //        if (dbProvider?.ToLower() == "mssql")
        //        {
        //            optionsBuilder.UseSqlServer(connetionString);
        //        }
        //    }
        //}

        public DbSet<Product> Products { get; set; }
    }
}
