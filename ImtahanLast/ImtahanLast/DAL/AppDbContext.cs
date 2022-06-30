using ImtahanLast.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanLast.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions opt):base(opt)
        {
        }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<LServices> services { get; set; }
    }
}
