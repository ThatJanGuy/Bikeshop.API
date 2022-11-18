using AutoMapper;
using Bikeshop.API.DbContexts;
using Bikeshop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bikeshop.API.Services
{
    public class BikeshopRepository : IBikeshopRepository
    {
        private readonly BikeshopContext context;
        private readonly IMapper mapper;

        public BikeshopRepository(BikeshopContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Bike>> GetBikesAsync(string? name, string? searchQuery)
        {
            var collection = context.Bikes as IQueryable<Bike>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(b => b.Name == name); 
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(b => b.Name.Contains(searchQuery) ||
                (b.FullDescription != null && b.FullDescription.Contains(searchQuery)));
            }

            var collectionToReturn = await collection.OrderBy(b => b.Name)
                .ToListAsync();

            return collectionToReturn;
        }
    }
}
