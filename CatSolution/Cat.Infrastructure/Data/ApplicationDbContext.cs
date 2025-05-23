using Cat.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Cat.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext // Изменено наследование
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Core.Entities.Cat> Cats { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Ring> Rings { get; set; }
        public DbSet<RingBreed> RingBreeds { get; set; }
        public DbSet<ExpertRing> ExpertRings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Важно для Identity

            // Конфигурация Cat
            modelBuilder.Entity<Core.Entities.Cat>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.PedigreeNumber).HasMaxLength(50);
                entity.Property(c => c.ParentNames).HasMaxLength(200);
                entity.Property(c => c.Medal).HasMaxLength(20);

                entity.HasOne(c => c.Breed)
                    .WithMany(b => b.Cats)
                    .HasForeignKey(c => c.BreedId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Owner)
                    .WithMany(o => o.Cats)
                    .HasForeignKey(c => c.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Club)
                    .WithMany(c => c.Cats)
                    .HasForeignKey(c => c.ClubId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация Breed
            modelBuilder.Entity<Breed>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(b => b.Name).IsUnique();
            });

            // Конфигурация Club
            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Address).HasMaxLength(200);
            });

            // Конфигурация Expert
            modelBuilder.Entity<Expert>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Club)
                    .WithMany(c => c.Experts)
                    .HasForeignKey(e => e.ClubId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация Owner
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.FullName).IsRequired().HasMaxLength(100);
                entity.Property(o => o.PassportNumber).HasMaxLength(50);
            });

            // Конфигурация Ring
            modelBuilder.Entity<Ring>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Number).IsRequired().HasMaxLength(10);
                entity.HasIndex(r => r.Number).IsUnique();
            });

            // Связующие таблицы
            modelBuilder.Entity<RingBreed>()
                .HasKey(rb => new { rb.RingId, rb.BreedId });

            modelBuilder.Entity<RingBreed>()
                .HasOne(rb => rb.Ring)
                .WithMany(r => r.RingBreeds)
                .HasForeignKey(rb => rb.RingId);

            modelBuilder.Entity<RingBreed>()
                .HasOne(rb => rb.Breed)
                .WithMany(b => b.RingBreeds)
                .HasForeignKey(rb => rb.BreedId);

            modelBuilder.Entity<ExpertRing>()
                .HasKey(er => new { er.ExpertId, er.RingId });

            modelBuilder.Entity<ExpertRing>()
                .HasOne(er => er.Expert)
                .WithMany(e => e.ExpertRings)
                .HasForeignKey(er => er.ExpertId);

            modelBuilder.Entity<ExpertRing>()
                .HasOne(er => er.Ring)
                .WithMany(r => r.ExpertRings)
                .HasForeignKey(er => er.RingId);
        }
    }
}