using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bikeshop.API.Entities
{
    public class Bike
    {
        public Bike(string name, Guid categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? ShortDescription { get; set; }

        [MaxLength(10000)]
        public string? FullDescription { get; set; }

        public double? Price { get; set; }

        [MaxLength(2048)]
        public string? PictureUrl { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string? Colour { get; set; }
    }
}
