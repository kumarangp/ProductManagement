using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Entities;

namespace ProductManagement.Infrastructure.Data {
    public class ApplicationDbContext :DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id = 1,
                    Name = "Product 1",
                    Category = "Category 1",
                    IsAvailable = true,
                    Description = "Sample description",
                    Price = 100.50M,
                    CreatedAt = new DateTime(2025, 1, 27, 0, 0, 0)
                }
            );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            foreach (var entry in ChangeTracker.Entries<Product>()) {
                if (entry.State == EntityState.Added) {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
