using Cat.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cat.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Core.Entities.Cat> Cats { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Ring> Rings { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<RingBreed> RingBreeds { get; set; }
        public DbSet<ExpertRing> ExpertRings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Core.Entities.Cat>(entity =>
            {
                entity.ToTable("cats");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.PedigreeNumber)
                    .HasMaxLength(20);

                entity.Property(c => c.ParentNames)
                    .HasMaxLength(100);

                entity.Property(c => c.Medal)
                    .HasMaxLength(20);

                entity.Property(c => c.LastVaccination)
                    .IsRequired();

                entity.HasOne(c => c.Breed)
                    .WithMany(b => b.Cats)
                    .HasForeignKey(c => c.BreedId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Club)
                    .WithMany(cl => cl.Cats)
                    .HasForeignKey(cl => cl.ClubId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Ring)
                    .WithMany(r => r.Cats)
                    .HasForeignKey(c => c.RingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Owner)
                    .WithMany(o => o.Cats)
                    .HasForeignKey(c => c.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(c => c.PedigreeNumber)
                    .IsUnique()
                    .HasFilter("\"PedigreeNumber\" IS NOT NULL");
            });

            builder.Entity<Breed>(entity =>
            {
                entity.ToTable("breeds");

                entity.HasKey(b => b.Id);

                entity.Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(b => b.Name)
                    .IsUnique();
            });

            builder.Entity<Club>(entity =>
            {
                entity.ToTable("clubs");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Address)
                    .HasMaxLength(200);

                entity.Property(c => c.GoldMedals)
                    .HasDefaultValue(0);

                entity.Property(c => c.SilverMedals)
                    .HasDefaultValue(0);

                entity.Property(c => c.BronzeMedals)
                    .HasDefaultValue(0);
            });

            builder.Entity<Ring>(entity =>
            {
                entity.ToTable("rings");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.Number)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(r => r.Location)
                    .HasMaxLength(100);

                entity.Property(r => r.Timetable)
                    .HasColumnType("jsonb");

                entity.HasIndex(r => r.Number)
                    .IsUnique();
            });

            builder.Entity<Expert>(entity =>
            {
                entity.ToTable("experts");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Specializations)
                    .HasColumnType("jsonb");

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.HasOne(e => e.Club)
                    .WithMany(c => c.Experts)
                    .HasForeignKey(e => e.ClubId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Owner>(entity =>
            {
                entity.ToTable("owners");

                entity.HasKey(o => o.Id);

                entity.Property(o => o.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(o => o.PassportNumber)
                    .HasMaxLength(50);

                entity.Property(o => o.PassportIssuedBy)
                    .HasMaxLength(100);

                entity.HasIndex(o => o.PassportNumber)
                    .IsUnique();
            });

            builder.Entity<RingBreed>(entity =>
            {
                entity.ToTable("ring_breeds");

                entity.HasKey(rb => new { rb.RingId, rb.BreedId });

                entity.HasOne(rb => rb.Ring)
                    .WithMany(r => r.RingBreeds)
                    .HasForeignKey(rb => rb.RingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rb => rb.Breed)
                    .WithMany(b => b.RingBreeds)
                    .HasForeignKey(rb => rb.BreedId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ExpertRing>(entity =>
            {
                entity.ToTable("expert_rings");

                entity.HasKey(er => new { er.ExpertId, er.RingId });

                entity.HasOne(er => er.Expert)
                    .WithMany(e => e.ExpertRings)
                    .HasForeignKey(er => er.ExpertId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(er => er.Ring)
                    .WithMany(r => r.ExpertRings)
                    .HasForeignKey(er => er.RingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}