using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyId.Application.DTOs;
using MyId.Application.Interface;

namespace IdPicture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        /// <summary>
        /// Viewers endpoint to retrieve all gallery/portfolio items
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _galleryService.GetAllAsync();
            return Ok(items);
        }

        /// <summary>
        /// Upload endpoint for adding new images to the gallery
        /// </summary>
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] CreateGalleryItemDto dto)
        {
            var result = await _galleryService.UploadAsync(dto);
            return Ok(result);
        }
    }
}
