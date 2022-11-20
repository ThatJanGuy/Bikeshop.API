using AutoMapper;

namespace Bikeshop.API.Profiles
{
    public class BikeProfile : Profile
    {
        public BikeProfile()
        {
            CreateMap<Entities.Bike, Models.BikeDto>();
            CreateMap<Models.BikeForCreationOrFullUpdateDto, Entities.Bike>();
        }
    }
}
