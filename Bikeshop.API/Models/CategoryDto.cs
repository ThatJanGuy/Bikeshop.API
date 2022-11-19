using Bikeshop.API.Entities;

namespace Bikeshop.API.Models
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<BikeDto> Bikes { get; set; }
            = new List<BikeDto>();
    }
}
