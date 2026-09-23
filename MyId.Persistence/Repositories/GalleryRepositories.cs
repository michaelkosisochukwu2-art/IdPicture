using Microsoft.EntityFrameworkCore;
using MyId.Application.Interface;
using MyId.Domain.Entities;
using MyId.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyId.Persistence.Repositories
{
    public class GalleryRepositories: IGalleryRepository
    {
        private readonly AppDbContext _context;

        public GalleryRepositories(AppDbContext context)
        {
            _context = context;
        }
       public async Task<IEnumerable<GalleryItem>> GetAllAsync()
        {
            return await _context.GalleryItems
                .OrderByDescending(g=>g.UploadedAt)
                .ToListAsync();

        }

        public async Task AddAsync(GalleryItem item)
        {
            await _context.GalleryItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

    }
}
