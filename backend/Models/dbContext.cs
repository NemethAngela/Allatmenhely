using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend.Models
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<Enquery> Enqueries { get; set; } = null!;
        public virtual DbSet<Kind> Kinds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=allatmenhely-allatmenhely.a.aivencloud.com;port=26431;database=defaultdb;user id=avnadmin;password=AVNS_3mZIl5CUrteFcbMPwf_", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(100);

                entity.Property(e => e.PasswordSalt).HasMaxLength(100);
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("Animal");

                entity.HasIndex(e => e.KindId, "KindId");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.IsMale).HasColumnType("bit(1)");

                entity.Property(e => e.IsNeutered)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Photo).HasColumnType("text");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Kind)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.KindId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Animal_ibfk_1");
            });

            modelBuilder.Entity<Enquery>(entity =>
            {
                entity.ToTable("Enquery");

                entity.HasIndex(e => e.AnimalId, "AnimalId");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Enqueries)
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enquery_ibfk_1");
            });

            modelBuilder.Entity<Kind>(entity =>
            {
                entity.ToTable("Kind");

                entity.Property(e => e.Kind1)
                    .HasMaxLength(50)
                    .HasColumnName("Kind");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
