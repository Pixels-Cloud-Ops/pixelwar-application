using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixelWarApp.Server.Data;
using PixelWarApp.Server.DTO;
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

        public async Task<List<PixelDto>> GetAllPixelsAsync()
        {
            List<PixelEntity> pixelsEntity = await _pixelDbContext.Pixels.ToListAsync();

            List<PixelDto> result = pixelsEntity
                .Select(p => new PixelDto
                {
                    X = p.X,
                    Y = p.Y,
                    Color = p.Color
                })
                .ToList();
            return result;
        }

        public async Task<PixelEntity> AddPixelAsync(PixelDto pixelDto)
        {
            var newPixel = new PixelEntity { Color = pixelDto.Color , X = pixelDto.X , Y = pixelDto.Y , UpdatedAt = DateTime.UtcNow};
            _pixelDbContext.Pixels.Add(newPixel);       
            await _pixelDbContext.SaveChangesAsync();
            return newPixel;
        }

        public async Task PutPixelAsync(PixelDto pixelDto)
        {
            var pixel = await _pixelDbContext.Pixels.FindAsync(pixelDto.X, pixelDto.Y);

            if (pixel != null)
            {
                pixel.Color = pixelDto.Color;
                pixel.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                var newPixel = new PixelEntity
                {
                    X = pixelDto.X,
                    Y = pixelDto.Y,
                    Color = pixelDto.Color,
                    UpdatedAt = DateTime.UtcNow
                };

                await _pixelDbContext.Pixels.AddAsync(newPixel);
            }

            await _pixelDbContext.SaveChangesAsync();
        }

    }
}

