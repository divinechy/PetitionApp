using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PetitionApi.Models
{
    public partial class PetitionDbContext : DbContext
    {
        public PetitionDbContext()
        {
        }

        public PetitionDbContext(DbContextOptions<PetitionDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Petition> Petitions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserPetition> UserPetitions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(" Server=tcp:127.0.0.1,1433; Database=PetitionDb; MultipleActiveResultSets=true; User=sa; Password=password1@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Petition>(entity =>
            {
                entity.ToTable("Petition");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Details)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__A9D10534BF31E616")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasDefaultValueSql("((2))");
            });

            modelBuilder.Entity<UserPetition>(entity =>
            {
                entity.ToTable("UserPetition");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SignedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SignedEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
