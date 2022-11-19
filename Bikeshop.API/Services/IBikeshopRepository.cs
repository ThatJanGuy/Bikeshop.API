using Bikeshop.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bikeshop.API.Services
{
    public interface IBikeshopRepository
    {
        //
        // Regarding Bikes
        //
        Task<(IEnumerable<Bike>, PaginationMetadata)> GetBikesAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<Bike?> GetBikeAsync(Guid bikeId);
        Task AddBikeToCategoryAsync(Guid categoryId, Bike bike);
        Task<bool> BikeExistsAsync(Guid bikeId);

        //
        // Regarding Categories
        //
        Task<IEnumerable<Category>?> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(Guid categoryId, bool includeBikes);
        Task<bool> CategoryExistsAsync(Guid categoryId);

        //
        // General
        //
        Task<bool> SaveChangesAsync();
    }
}
