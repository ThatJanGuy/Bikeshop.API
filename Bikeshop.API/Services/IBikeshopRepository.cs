using Bikeshop.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bikeshop.API.Services
{
    public interface IBikeshopRepository
    {
        Task<(IEnumerable<Bike>, PaginationMetadata)> GetBikesAsync(Guid? bikeId, string? searchQuery, int pageNumber, int pageSize);
    }
}
