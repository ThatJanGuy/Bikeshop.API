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

        public async Task<IEnumerable<Bike>> GetBikesAsync()
        {
            return await context.Bikes
                .OrderBy(b => b.Name)
                .ToListAsync();
        }
    }
}
