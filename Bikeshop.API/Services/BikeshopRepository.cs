using Bikeshop.API.DbContexts;
using Bikeshop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bikeshop.API.Services
{
    public class BikeshopRepository : IBikeshopRepository
    {
        private readonly BikeshopContext context;


        public BikeshopRepository(BikeshopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //
        // Regarding Bikes
        //

        public async Task<(IEnumerable<Bike>, PaginationMetadata)> GetBikesAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = context.Bikes as IQueryable<Bike>;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(b => b.Name.Contains(searchQuery) ||
                (b.FullDescription != null && b.FullDescription.Contains(searchQuery)) ||
                (b.ShortDescription != null && b.ShortDescription.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection
                .OrderBy(b => b.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Bike?> GetBikeAsync(Guid bikeId)
        {
            return await context.Bikes.Where(b => b.Id == bikeId).FirstOrDefaultAsync();
        }


        public async Task AddBikeToCategoryAsync(Guid categoryId, Bike bike)
        {
            var categoryToAddTo = await GetCategoryAsync(categoryId);
            if (categoryToAddTo != null)
            {
                categoryToAddTo.Bikes.Add(bike);
            }
        }

        public async Task<bool> BikeExistsAsync(Guid bikeId)
        {
            return await context.Bikes.AnyAsync(b => b.Id == bikeId);
        }


        //
        // Regarding Categories
        //

        public async Task<IEnumerable<Category>?> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(Guid categoryId, bool includeBikes = false)
        {
            if (includeBikes)
            {
                return await context.Categories.Include(c => c.Bikes)
                                               .Where(c => c.Id == categoryId)
                                               .FirstOrDefaultAsync();
            }

            return await context.Categories.Where(c => c.Id == categoryId)
                                           .FirstOrDefaultAsync();
        }

        public async Task<bool> CategoryExistsAsync(Guid categoryId)
        {
            return await context.Categories.AnyAsync(c => c.Id == categoryId);
        }

        //
        // General
        //

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync() >= 0);
        }
    }
}
