using System.ComponentModel.DataAnnotations;

namespace Bikeshop.API.Models
{
    public class BikeForCreationOrFullUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }

        [MaxLength(200)]
        public string? ShortDescription { get; set; }

        [MaxLength(10000)]
        public string? FullDescription { get; set; }

        public double? Price { get; set; }

        [MaxLength(2048)]
        public string? PictureUrl { get; set; }

        public string? Colour { get; set; }
    }
}
