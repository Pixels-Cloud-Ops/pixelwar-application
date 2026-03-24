namespace PixelWarApp.Server.Entity
{
    public class PixelEntity
    {
        public int X { get; set; }

        public int Y { get; set; }

        public string Color { get; set; } = "#ffffff";

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
