using AutoMapper;
using Bikeshop.API.Models;
using Bikeshop.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Bikeshop.API.Controllers
{
    [ApiController]
    [Authorize]
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
        /// <summary>
        /// Get a list of bikes and pagination metadata.
        /// </summary>
        /// <param name="searchQuery">Searches the "name", "shortDescription", and "FullDescription" fields of all bikes. Only returns hits.</param>
        /// <param name="pageNumber">Returns only entries on this specific page.</param>
        /// <param name="pageSize">Limits the return to this many entries. Max is 25.</param>
        /// <remarks>Also returns an "X-Pagination" header that holds pagination data.</remarks>
        /// <response code="200">Doesnt't require authentication. Returns the requested bikes and the "X-Pagination" header. It is empty when the searchQuery wasn't found.</response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BikeDto>>> GetBikes(string? searchQuery, int pageNumber = 1, int pageSize = 25)
        {
            if (pageSize > maxBikesPerPage) pageSize = maxBikesPerPage;

            var (bikeEntities, paginationMetadata) = await bikeshopRepository.GetBikesAsync(searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(mapper.Map<IEnumerable<BikeDto>>(bikeEntities));
        }

        /// <summary>
        /// Get a bike by id.
        /// </summary>
        /// <remarks>Doesn't require authentication.</remarks>
        /// <param name="bikeId">The id of the bike you want.</param>
        /// <response code="200">Returns the requested bike.</response>
        [AllowAnonymous]
        [HttpGet("{bikeId}", Name = "GetBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BikeDto>> GetBike(Guid bikeId)
        {
            var bike = await bikeshopRepository.GetBikeAsync(bikeId);
            if (bike == null) return NotFound();

            return Ok(mapper.Map<BikeDto>(bike));
        }

        /// <summary>
        /// Create a new bike.
        /// </summary>
        /// <param name="categoryId">The id of the category the bike shall belong to once created.</param>
        /// <param name="bike">The bike's information.</param>
        /// <response code="201">Returns the created bike.</response>
        /// <response code="404">Is returned when the category id doesn't exist.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BikeDto>> CreateBike(Guid categoryId, BikeForCreationOrFullUpdateDto bike)
        {
            if (!await bikeshopRepository.CategoryExistsAsync(categoryId))
                return NotFound();

            var bikeToCreate = mapper.Map<Entities.Bike>(bike);

            await bikeshopRepository.AddBikeToCategoryAsync(categoryId, bikeToCreate);

            await bikeshopRepository.SaveChangesAsync();

            var createdBikeToReturn = mapper.Map<BikeDto>(bikeToCreate);

            return CreatedAtRoute(
                "GetBike",
                new { bikeId = createdBikeToReturn.Id },
                createdBikeToReturn
                );
        }

        /// <summary>
        /// Update an existing bike.
        /// </summary>
        /// <param name="bikeId">The id of the bike to be updated.</param>
        /// <param name="bike">The bike's new information.</param>
        /// <remarks>Only full updates are supported. Trying to partially update will damage your data!</remarks>
        /// <response code="204">Is returned on successful update.</response>
        /// <response code="404">Is returned when the bikeId to update doesn't exist.</response>
        [HttpPut("{bikeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateBike(Guid bikeId, BikeForCreationOrFullUpdateDto bike)
        {
            var bikeEntity = await bikeshopRepository.GetBikeAsync(bikeId);
            if (bikeEntity == null) 
                return NotFound();

            mapper.Map(bike, bikeEntity);

            await bikeshopRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a bike by id.
        /// </summary>
        /// <param name="bikeId">The id of the bike that shall be deleted.</param>
        /// <response code="204">Is returned on successful delete.</response>
        /// <response code="404">Is returned when the bikeId to delete doesn't exist.</response>
        [HttpDelete("{bikeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteBike(Guid bikeId)
        {
            var bikeEntity = await bikeshopRepository.GetBikeAsync(bikeId);
            if (bikeEntity == null)
                return NotFound();

            bikeshopRepository.DeleteBike(bikeEntity);
            await bikeshopRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
