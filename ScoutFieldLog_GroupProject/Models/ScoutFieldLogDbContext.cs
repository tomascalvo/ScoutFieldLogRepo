using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using ScoutFieldLog_GroupProject.Models;

namespace ScoutFieldLog_GroupProject.Models
{
    public partial class ScoutFieldLogDbContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public ScoutFieldLogDbContext()
        {
        }

        public ScoutFieldLogDbContext(DbContextOptions<ScoutFieldLogDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<ScoutFieldLog_GroupProject.Models.Item> Item { get; set; }

        public DbSet<ScoutFieldLog_GroupProject.Models.LeadSearch> StartupSearch { get; set; }
    }
}
