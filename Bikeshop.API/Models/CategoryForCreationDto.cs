using System.ComponentModel.DataAnnotations;

namespace Bikeshop.API.Models
{
    public class CategoryForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
