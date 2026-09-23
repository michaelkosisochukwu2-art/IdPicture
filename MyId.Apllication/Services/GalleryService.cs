using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyId.Application.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyId.Application.DTOs;
using MyId.Domain.Entities;
using System.Threading.Tasks;

namespace MyId.Application.Services
{
    public class GalleryService: IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IWebHostEnvironment _env;
        public GalleryService(IGalleryRepository galleryRepository, IWebHostEnvironment env)
        {
            _galleryRepository = galleryRepository;
            _env = env;
        }
        public async Task<IEnumerable<GalleryItemDto>> GetAllAsync()
        {
            var items = await _galleryRepository.GetAllAsync();
            return items.Select(g => new GalleryItemDto
            {
                Id = g.Id,
                Title = g.Title,
                Description = g.Description,
                ImageUrl = g.ImageUrl,
                UploadedAt = g.UploadedAt
            });
        }

        public async Task<GalleryItemDto> UploadAsync(CreateGalleryItemDto dto)
        {
            if (dto.ImageFile == null || dto.ImageFile.Length == 0)
                throw new ArgumentException("Valid image file is required.");

            string uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(dto.ImageFile.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ImageFile.CopyToAsync(stream);
            }

            var item = new GalleryItem
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = $"/uploads/{uniqueFileName}",
                UploadedAt = DateTime.UtcNow
            };

            await _galleryRepository.AddAsync(item);

            return new GalleryItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                UploadedAt = item.UploadedAt
            };
        }
    }
}