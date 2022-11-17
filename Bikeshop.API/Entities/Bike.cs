using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bikeshop.API.Entities
{
    public class Bike
    {
        public Bike(string name, string shortDescription, string fullDescription, double price, string pictureUrl)
        {
            Name = name;
            ShortDescription = shortDescription;
            FullDescription = fullDescription;
            Price = price;
            PictureUrl = pictureUrl;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(10000)]
        public string FullDescription { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public Guid CategoryId { get; set; }

        public string? Colour { get; set; }
    }
}
