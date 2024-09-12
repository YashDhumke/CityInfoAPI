using AutoMapper;
namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile() 
        { 
            // this is the syntax for mapping one object into another object 
            // we are mapping one object into another object with the help of automapper 

            CreateMap<Entities.City, Models.CityWithoutPointsOfIntrestDto>();
            CreateMap<Entities.City, Models.CityDto>();

            //  Entities.City is the source type in the mapping configuration. It represents the entity class typically used for database operations

            // Models.CityWithoutPointsOfIntrestDto is the destination type. It represents a Data Transfer Object (DTO) that will be populated with data from the Entities.City object

            // Models are essential for defining how data is structured and used within different contexts of an application.
        }
    }
}
