using Microsoft.EntityFrameworkCore;
using PixelWarApp.Server.Entity;

namespace PixelWarApp.Server.Data
{
    public class PixelDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public PixelDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("PixelsDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PixelEntity>(pixelEntity =>
            {
                pixelEntity.HasKey(p => new { p.X, p.Y });

                pixelEntity.Property(p => p.Color)
                    .IsRequired()
                    .HasMaxLength(7);

                pixelEntity.Property(p => p.UpdatedAt)
                    .IsRequired();
            });
        }

        public DbSet<PixelEntity> Pixels
        {
            get; set;
        }
    }
}
