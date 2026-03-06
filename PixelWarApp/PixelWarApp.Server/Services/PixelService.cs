using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixelWarApp.Server.Data;
using PixelWarApp.Server.Entity;

namespace PixelWarApp.Server.Services
{
    public class PixelService
    {
        private readonly PixelDbContext _pixelDbContext;

        public PixelService(PixelDbContext pixelDbContext)
        {
            _pixelDbContext = pixelDbContext;
        }

        public async Task<ActionResult<IEnumerable<PixelEntity>>> GetAllPixelsAsync()
        {
            return await _pixelDbContext.Pixels.ToListAsync();
        }

        public async Task<PixelEntity> AddPixelAsync(PixelEntity pixelEntity)
        {
            var newPixel = new PixelEntity { Color = pixelEntity.Color , X = pixelEntity.X , Y = pixelEntity.Y , UpdatedAt = DateTime.UtcNow};
            _pixelDbContext.Pixels.Add(newPixel);       
            await _pixelDbContext.SaveChangesAsync();
            return newPixel;
        }

    }
}

