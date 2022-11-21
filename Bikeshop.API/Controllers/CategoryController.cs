using AutoMapper;
using Bikeshop.API.Models;
using Bikeshop.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bikeshop.API.Controllers
{
    [Route("api/categories")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBikeshopRepository bikeshopRepository;
        private readonly IMapper mapper;

        public CategoryController(IBikeshopRepository bikeshopRepository, IMapper mapper)
        {
            this.bikeshopRepository = bikeshopRepository ?? throw new ArgumentNullException(nameof(bikeshopRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get the full category list.
        /// </summary>
        /// <remarks>Doesn't require authentication.</remarks>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoryWithoutBikesDto>>> GetCategories()
        {
            var categoriesEntity = await bikeshopRepository.GetCategoriesAsync();

            return Ok(mapper.Map<IEnumerable<CategoryWithoutBikesDto>>(categoriesEntity));
        }

        /// <summary>
        /// Get a specific category by id. Optionally add all bikes of that category.
        /// </summary>
        /// <param name="categoryId">The id of the category to return.</param>
        /// <param name="includeBikes">Whether or not all bikes of the category shall be returned as well.</param>
        /// <remarks>Doesn't require authentication.</remarks>
        /// <response code="200">Returns the requested category. If requested contains all associated bikes.</response>
        /// <response code="404">Is returned if the requested categoryId doesn't exist.</response>
        [AllowAnonymous]
        [HttpGet("{categoryId}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid categoryId, bool includeBikes = false)
        {
            var categoryEntity = await bikeshopRepository.GetCategoryAsync(categoryId, includeBikes);
            if (categoryEntity == null) return NotFound();

            if (includeBikes) return Ok(mapper.Map<CategoryDto>(categoryEntity));

            return Ok(mapper.Map<CategoryWithoutBikesDto>(categoryEntity));
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="category">The new category's information.</param>
        /// <response code="201">Returns the created category.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryWithoutBikesDto>> CreateCategory(CategoryForCreationOrFullUpdateDto category)
        {
            var categoryToCreate = mapper.Map<Entities.Category>(category);

            bikeshopRepository.AddCategory(categoryToCreate);

            await bikeshopRepository.SaveChangesAsync();

            var createdCategoryToReturn = mapper.Map<CategoryWithoutBikesDto>(categoryToCreate);

            return CreatedAtRoute(
                "GetCategory",
                new { categoryId = createdCategoryToReturn.Id },
                createdCategoryToReturn
                );
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <param name="categoryId">The id of the category to be updated.</param>
        /// <param name="category">The new category's information.</param>
        /// <remarks>Only full updates are supported. Trying to partially update will damage your data!</remarks>
        /// <response code="204">Is returned on successful update.</response>
        /// <response code="404">Is returned when the categoryId to update doesn't exist.</response>
        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateCategory(Guid categoryId, CategoryForCreationOrFullUpdateDto category)
        {
            var categoryEntity = await bikeshopRepository.GetCategoryAsync(categoryId, true);
            if (categoryEntity == null) 
                return NotFound();

            mapper.Map(category, categoryEntity);

            await bikeshopRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a category by id.
        /// </summary>
        /// <param name="categoryId">The id of the category to be updated.</param>
        /// <response code="204">Is returned on successful delete.</response>
        /// <response code="404">Is returned when the categoryId to delete doesn't exist.</response>
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            var categoryEntity = await bikeshopRepository.GetCategoryAsync(categoryId, false);
            if (categoryEntity == null)
                return NotFound();

            bikeshopRepository.DeleteCategory(categoryEntity);
            await bikeshopRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
