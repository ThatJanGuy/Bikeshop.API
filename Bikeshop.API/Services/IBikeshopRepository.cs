using Bikeshop.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bikeshop.API.Services
{
    public interface IBikeshopRepository
    {
        Task<IEnumerable<Bike>> GetBikesAsync(Guid? bikeId, string? searchQuery);
    }
}
