﻿namespace Bikeshop.API.Models
{
    public class CategoryWithoutBikesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
