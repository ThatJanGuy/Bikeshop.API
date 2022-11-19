using AutoMapper;
using Bikeshop.API.Models;
using Bikeshop.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Bikeshop.API.Controllers
{
    [ApiController]
    [Route("api/bikes")]
    public class BikeController : ControllerBase
    {
        private readonly IBikeshopRepository bikeshopRepository;
        private readonly IMapper mapper;
        const int maxBikesPerPage = 25;

        public BikeController(IBikeshopRepository bikeshopRepository, IMapper mapper)
        {
            this.bikeshopRepository = bikeshopRepository ?? throw new ArgumentNullException(nameof(bikeshopRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeDto>>> GetBikes(Guid? bikeId, string? searchQuery, int pageNumber = 1, int pageSize = 25)
        {
            if (pageSize > maxBikesPerPage) pageSize = maxBikesPerPage;

            var (bikeEntities, paginationMetadata) = await bikeshopRepository.GetBikesAsync(bikeId, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(mapper.Map<IEnumerable<BikeDto>>(bikeEntities));
        }
    }
}
