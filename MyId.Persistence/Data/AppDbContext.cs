using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyId.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyId.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 

        }

        public DbSet<GalleryItem> GalleryItems { get; set; } = null!;
    }
}
