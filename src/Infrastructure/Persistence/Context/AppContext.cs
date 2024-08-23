using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions options) : base(options)
    { }

  
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Define the one-to-one relationship between Organization and Detail

        // modelBuilder.Entity<Product>()
        //     .HasOne(s => s.Detail)
        //     .WithOne(s => s.Product)
        //     .HasForeignKey<ProductDetail>(s => s.Id);
        
        modelBuilder.ApplyDbSeedData();
        
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //ChangeTracker 

        // var datas = ChangeTracker
        //     .Entries<BaseEntity<int>>();
        //
        // foreach (var data in datas)
        // {
        //     _ = data.State switch
        //     {
        //         EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
        //         EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
        //         _ => DateTime.UtcNow
        //     };
        // }

        return await base.SaveChangesAsync(cancellationToken);
    }
    
}