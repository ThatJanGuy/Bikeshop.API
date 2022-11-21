﻿using AutoMapper;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeDto>>> GetBikes(string? searchQuery, int pageNumber = 1, int pageSize = 25)
        {
            if (pageSize > maxBikesPerPage) pageSize = maxBikesPerPage;

            var (bikeEntities, paginationMetadata) = await bikeshopRepository.GetBikesAsync(searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(mapper.Map<IEnumerable<BikeDto>>(bikeEntities));
        }

        [AllowAnonymous]
        [HttpGet("{bikeId}", Name = "GetBike")]
        public async Task<ActionResult<BikeDto>> GetBike(Guid bikeId)
        {
            var bike = await bikeshopRepository.GetBikeAsync(bikeId);
            if (bike == null) return NotFound();

            return Ok(mapper.Map<BikeDto>(bike));
        }

        [HttpPost]
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

        [HttpPut("{bikeId}")]
        public async Task<ActionResult> UpdateBike(Guid bikeId, BikeForCreationOrFullUpdateDto bike)
        {
            var bikeEntity = await bikeshopRepository.GetBikeAsync(bikeId);
            if (bikeEntity == null) 
                return NotFound();

            mapper.Map(bike, bikeEntity);

            await bikeshopRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{bikeId}")]
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
