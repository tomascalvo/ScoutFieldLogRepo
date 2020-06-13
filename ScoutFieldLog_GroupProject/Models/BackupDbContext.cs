//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.Extensions.Configuration;

//namespace ScoutFieldLog_GroupProject.Models
//{
//    public partial class ScoutFieldLogDbContext : DbContext
//    {
//        //This was added
//        public IConfiguration Configuration { get; }
//        public ScoutFieldLogDbContext()
//        {
//        }

//        public ScoutFieldLogDbContext(DbContextOptions<ScoutFieldLogDbContext> options)
//            : base(options)
//        {
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//            //this was added
//                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
