using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LoginApi.Models
{
    public partial class LoginDBContext : DbContext
    {
        public LoginDBContext()
        {
        }

        public LoginDBContext(DbContextOptions<LoginDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=LoginDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserLogin");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .HasMaxLength(70)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
