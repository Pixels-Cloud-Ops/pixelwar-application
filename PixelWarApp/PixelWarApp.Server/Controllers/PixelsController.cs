using Microsoft.AspNetCore.Mvc;
using PixelWarApp.Server.DTO;
using PixelWarApp.Server.Entity;
using PixelWarApp.Server.Services;

namespace PixelWarApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PixelsController : Controller
    {
        private readonly PixelService _pixelService;

        public PixelsController(PixelService pixelService)
        {
            _pixelService = pixelService;
        }

        [HttpGet]
        public async Task<List<PixelDto>> GetAllPixels()
        {
            var pixels = await _pixelService.GetAllPixelsAsync();
            return pixels;
        }

        [HttpPut]
        public async Task<IActionResult> PutPixel(PixelDto pixelDto)
        {
            await _pixelService.PutPixelAsync(pixelDto);
            return Ok();
        }
    }
}
