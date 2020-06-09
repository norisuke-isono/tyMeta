using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entites;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TvProgram> TvPrograms { get; set; }
        public DbSet<Corner> Corners { get; set; }
        public DbSet<Broadcast> Broadcasts { get; set; }
        public DbSet<BaseSchedule> BaseSchedules { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Specification> Specifications { get; set; }
    }
}