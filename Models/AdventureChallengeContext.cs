using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdventureChallenge.Models
{
    public partial class AdventureChallengeContext : DbContext
    {
        public AdventureChallengeContext()
        {
        }

        public AdventureChallengeContext(DbContextOptions<AdventureChallengeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Challenge> Challenges { get; set; } = null!;
        public virtual DbSet<ChallengeHint> ChallengeHints { get; set; } = null!;
        public virtual DbSet<Foto> Fotos { get; set; } = null!;
        public virtual DbSet<Hint> Hints { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserChallenge> UserChallenges { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Challenge>(entity =>
            {
                entity.ToTable("Challenge");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Personen)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("personen");

                entity.Property(e => e.Prijs)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("prijs");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Tijdstip)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tijdstip");
            });

            modelBuilder.Entity<ChallengeHint>(entity =>
            {
                entity.ToTable("ChallengeHint");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChallengeId).HasColumnName("challenge_id");

                entity.Property(e => e.HintId).HasColumnName("hint_id");

                entity.HasOne(d => d.Challenge)
                    .WithMany(p => p.ChallengeHints)
                    .HasForeignKey(d => d.ChallengeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChallengeHint_Challenge");

                entity.HasOne(d => d.Hint)
                    .WithMany(p => p.ChallengeHints)
                    .HasForeignKey(d => d.HintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChallengeHint_Hint");
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.ToTable("Foto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Foto1)
                    .HasColumnType("image")
                    .HasColumnName("foto");
            });

            modelBuilder.Entity<Hint>(entity =>
            {
                entity.ToTable("Hint");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Beschrijving)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("beschrijving");

                entity.Property(e => e.FontIcoon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("font_icoon");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Beheer).HasColumnName("beheer");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Naam)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("naam");

                entity.Property(e => e.Wachtwoord)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("wachtwoord");
            });

            modelBuilder.Entity<UserChallenge>(entity =>
            {
                entity.ToTable("UserChallenge");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Afgerond).HasColumnName("afgerond");

                entity.Property(e => e.Beschrijving)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("beschrijving");

                entity.Property(e => e.ChallengeId).HasColumnName("challenge_id");

                entity.Property(e => e.FotoId).HasColumnName("foto_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Challenge)
                    .WithMany(p => p.UserChallenges)
                    .HasForeignKey(d => d.ChallengeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChallenge_Challenge");

                entity.HasOne(d => d.Foto)
                    .WithMany(p => p.UserChallenges)
                    .HasForeignKey(d => d.FotoId)
                    .HasConstraintName("FK_UserChallenge_Foto");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserChallenges)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChallenge_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
