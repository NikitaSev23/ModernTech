using Microsoft.EntityFrameworkCore;

namespace ModernTech.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public virtual DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.FullName).HasColumnName("fullname");
                entity.Property(e => e.NumberOfRecordBook).HasColumnName("numRecBook");
                entity.Property(e => e.BirthDate).HasColumnName("birthdate");
                entity.Property(e => e.EnrollementDate).HasColumnName("enrolldate");
            });
        }
    }
}