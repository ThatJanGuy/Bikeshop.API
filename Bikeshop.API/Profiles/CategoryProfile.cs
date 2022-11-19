using AutoMapper;

namespace Bikeshop.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Entities.Category, Models.CategoryDto>();
            CreateMap<Entities.Category, Models.CategoryWithoutBikesDto>();
        }
    }
}
