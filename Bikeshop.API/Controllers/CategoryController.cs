using AutoMapper;
using Bikeshop.API.Models;
using Bikeshop.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bikeshop.API.Controllers
{
    [Route("api/categories")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryWithoutBikesDto>>> GetCategories()
        {
            var categoriesEntity = await bikeshopRepository.GetCategoriesAsync();

            return Ok(mapper.Map<IEnumerable<CategoryWithoutBikesDto>>(categoriesEntity));
        }

        [HttpGet("{categoryId}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid categoryId, bool includeBikes = false)
        {
            var categoryEntity = await bikeshopRepository.GetCategoryAsync(categoryId, includeBikes);
            if (categoryEntity == null) return NotFound();

            if (includeBikes) return Ok(mapper.Map<CategoryDto>(categoryEntity));

            return Ok(mapper.Map<CategoryWithoutBikesDto>(categoryEntity));
        }

        [HttpPost]
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

        [HttpPut("{categoryId}")]
        public async Task<ActionResult> UpdateCategory(Guid categoryId, CategoryForCreationOrFullUpdateDto category)
        {
            var categoryEntity = await bikeshopRepository.GetCategoryAsync(categoryId, true);
            if (categoryEntity == null) 
                return NotFound();

            mapper.Map(category, categoryEntity);

            await bikeshopRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteBike(Guid categoryId)
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
