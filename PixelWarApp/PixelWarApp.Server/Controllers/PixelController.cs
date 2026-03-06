using Microsoft.AspNetCore.Mvc;
using PixelWarApp.Server.Entity;
using PixelWarApp.Server.Services;

namespace PixelWarApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PixelController : Controller
    {
        private readonly PixelService _pixelService;

        public PixelController(PixelService pixelService)
        {
            _pixelService = pixelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PixelEntity>>> GetAllPixels()
        {
            var pixels = await _pixelService.GetAllPixelsAsync();
            return Ok(pixels);
        }

        [HttpPost]
        public async Task<IActionResult> AddPixel(PixelEntity pixelEntity)
        {
            var pixel = await _pixelService.AddPixelAsync(pixelEntity);
            return Ok(pixel);
        }
    }
}
