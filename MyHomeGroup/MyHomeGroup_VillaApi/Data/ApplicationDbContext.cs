using Microsoft.EntityFrameworkCore;
using MyHomeGroup_VillaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHomeGroup_VillaApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }

        public DbSet<Amenties> Amenties { get; set; }
    }
}
