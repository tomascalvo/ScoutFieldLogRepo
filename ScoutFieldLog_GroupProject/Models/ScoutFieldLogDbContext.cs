using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class ScoutFieldLogDbContext : DbContext
    {
        public ScoutFieldLogDbContext()
        {
        }

        public ScoutFieldLogDbContext(DbContextOptions<ScoutFieldLogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlignmentScore> AlignmentScore { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<EvalCategory> EvalCategory { get; set; }
        public virtual DbSet<EvalCriterion> EvalCriterion { get; set; }
        public virtual DbSet<Evaluation> Evaluation { get; set; }
        public virtual DbSet<Keywords> Keywords { get; set; }
        public virtual DbSet<PartnerCompany> PartnerCompany { get; set; }
        public virtual DbSet<StartUp> StartUp { get; set; }
        public virtual DbSet<StartUpCompanies> StartUpCompanies { get; set; }
        public virtual DbSet<StartUpCompaniesOld> StartUpCompaniesOld { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlignmentScore>(entity =>
            {
                entity.HasIndex(e => e.EvaluationId);

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p.AlignmentScore)
                    .HasForeignKey(d => d.EvaluationId);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<EvalCategory>(entity =>
            {
                entity.HasIndex(e => e.EvalCategoryName)
                    .HasName("UQ__EvalCate__3B3B27E0996AE86F")
                    .IsUnique();

                entity.Property(e => e.EvalCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EvalCriterion>(entity =>
            {
                entity.HasIndex(e => e.EvalCriterionName)
                    .HasName("UQ__EvalCrit__3A8305A5DF391548")
                    .IsUnique();

                entity.Property(e => e.EvalCriterionName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.EvalCategory)
                    .WithMany(p => p.EvalCriterion)
                    .HasForeignKey(d => d.EvalCategoryId)
                    .HasConstraintName("FK__EvalCrite__EvalC__1BC821DD");
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.HasIndex(e => e.StartUpCompaniesId);

                entity.HasOne(d => d.StartUpCompanies)
                    .WithMany(p => p.Evaluation)
                    .HasForeignKey(d => d.StartUpCompaniesId);
            });

            modelBuilder.Entity<Keywords>(entity =>
            {
                entity.Property(e => e.Word).HasMaxLength(100);
            });

            modelBuilder.Entity<PartnerCompany>(entity =>
            {
                entity.HasIndex(e => e.CompanyName)
                    .HasName("UQ__PartnerC__9BCE05DC2E0E3F33")
                    .IsUnique();

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StartUp>(entity =>
            {
                entity.Property(e => e.Alignments).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.CompanyWebsite).HasMaxLength(100);

                entity.Property(e => e.ContactId).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.DateAdded).HasColumnType("date");

                entity.Property(e => e.DateAssigned).HasMaxLength(100);

                entity.Property(e => e.DateSaved).HasColumnType("date");

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.Landscapes).HasMaxLength(100);

                entity.Property(e => e.Source).HasMaxLength(100);

                entity.Property(e => e.StateProvince)
                    .HasColumnName("State_Province")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.TechnologyAreas).HasMaxLength(100);

                entity.Property(e => e.Themes).HasMaxLength(100);

                entity.Property(e => e.TwoLineSummary).HasMaxLength(100);
            });

            modelBuilder.Entity<StartUpCompanies>(entity =>
            {
                entity.Property(e => e.Alignments).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CompanyContactName).HasMaxLength(100);

                entity.Property(e => e.CompanyContactPhoneNumber).HasMaxLength(100);

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.CompanyWebsite).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.DateAssigned).HasColumnType("date");

                entity.Property(e => e.DateReviewed).HasColumnType("date");

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.Landscapes).HasMaxLength(100);

                entity.Property(e => e.ScoutName).HasMaxLength(100);

                entity.Property(e => e.StateProvince)
                    .HasColumnName("State_Province")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.TechnologyAreas).HasMaxLength(100);

                entity.Property(e => e.Themes).HasMaxLength(100);
            });

            modelBuilder.Entity<StartUpCompaniesOld>(entity =>
            {
                entity.Property(e => e.Alignments).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CompanyContactName).HasMaxLength(100);

                entity.Property(e => e.CompanyContactPhoneNumber).HasMaxLength(100);

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.CompanyWebsite).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.DateAssigned).HasColumnType("date");

                entity.Property(e => e.DateReviewed).HasColumnType("date");

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.Landscapes).HasMaxLength(100);

                entity.Property(e => e.ScoutName).HasMaxLength(100);

                entity.Property(e => e.StateProvince)
                    .HasColumnName("State_Province")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.TechnologyAreas).HasMaxLength(100);

                entity.Property(e => e.Themes).HasMaxLength(100);

                entity.Property(e => e.TwoLineSummary).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
