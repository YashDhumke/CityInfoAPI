//using CityInfo.API.Data;
using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    // detail execution of methods defined in the interface 
    // this methods interacts with the database to do the execution 
    public class CityInfoRepository : ICityInfoRepository
    {
        
        private readonly CityInfoContext _context;


        // injected the dependancy context for use 
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        // detailed method for getting cities form the db 
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }



        // getting cities according to query 
        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber , int pageSize)
        {

            var collection = _context.Cities as IQueryable<City>;

            if (!string.IsNullOrEmpty(name)) 
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery) || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);

        }



        // detailed method for getting only city
        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }



        // method execution for checking city exists in db or not 
        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c=> c.Id ==cityId);
        }



        // method for getting pointsofinterest for the city 
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId
            )
        {
            return await _context.PointOfInterests
                          .Where(p => p.CityId == cityId).ToListAsync();

        }



        // method for getting only one pointofinterest 
        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
            int pointOfInterestId)
        {
            return await _context.PointOfInterests
             .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
             .FirstOrDefaultAsync();
        }

        

        // adding pointofinterest in the db 
        public async Task AddPointOfInterestForCityAsync(int cityId, 
            PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }



        // deleting a pointofinterest from the db 
        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterests.Remove(pointOfInterest);
        }



        // saving out the changes in the database
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }
    }
}