using MyId.Application.DTOs;
using MyId.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyId.Application.Interface
{
    public interface IGalleryService
    {
        Task<IEnumerable<GalleryItemDto>> GetAllAsync();
        Task<GalleryItemDto> UploadAsync(CreateGalleryItemDto dto);
    }
}
