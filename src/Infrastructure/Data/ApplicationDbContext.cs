using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TvProgram> TvPrograms { get; set; }
        public DbSet<Corner> Corners { get; set; }
        public DbSet<BaseSchedule> BaseSchedules { get; set; }
    }
}