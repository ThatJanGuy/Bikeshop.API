namespace Bikeshop.API.Models
{
    public class BikeDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? FullDescription { get; set; }
        public double? Price { get; set; }
        public string? PictureUrl { get; set; }
        public string? Colour { get; set; }
        public byte[]? Image { get; set; }
    }
}
