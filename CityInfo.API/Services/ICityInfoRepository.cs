using CityInfo.API.Entities;
namespace CityInfo.API.Services

    // this is the interface which have declaration for all methods which intereacts with the database 

    // Task is used to represent that the method is async in nature 
{
    public interface ICityInfoRepository
    {
        // for getting cities 
        Task<IEnumerable<City>>GetCitiesAsync();

        // for getting cities according to searchquery 
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

        // for getting pointsofinterest with city 
        Task<City?> GetCityAsync(int cityId,bool includePointsOfInterest);

        // for getting only pointsofinterest 
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

        // for getting only one pointofinterest 
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
            int pointOfInterestId);

        // to find out the city exists in db or not 
        Task<bool> CityExistsAsync(int cityId);

        // adding pointofinterst in db 
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

        // deleting pointofinterest form the db 
        void DeletePointOfInterest(PointOfInterest pointOfInterest);

        // saving out the channges after execution 
        Task<bool> SaveChangesAsync();

        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);
    }
}
