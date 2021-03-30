using Microsoft.EntityFrameworkCore;

namespace FreePcNameWeb.Models
{
    /// <summary>
    /// Контекст базы данных ОСП.
    /// </summary>
    public partial class OspsContext : DbContext
    {
        public DbSet<Osps> Osps { get; set; }

        public OspsContext(DbContextOptions<OspsContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Osps>(entity =>
            {

                entity.ToTable("pc_locations", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Osp)
                    .HasColumnName("OSP")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
