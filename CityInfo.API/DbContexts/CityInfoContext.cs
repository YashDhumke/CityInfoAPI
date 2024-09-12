using CityInfo.API.Entities;using Microsoft.EntityFrameworkCore;namespace CityInfo.API.DbContexts{    public class CityInfoContext : DbContext    {

        public DbSet<City> Cities { get; set; } //Dbset is context created for city entity
        public DbSet<PointOfInterest> PointOfInterests { get; set; }

        //this method tells that Dbcontext is being used to connect to sqlite database

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //        optionsBuilder.UseSqlite("YourConnectionStringHere");
        //    base.OnConfiguring(optionsBuilder);    
        //}

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)        {        }        protected override void OnModelCreating(ModelBuilder modelBuilder)        {            modelBuilder.Entity<City>()                .HasData(                new City("new York city")                {                    Id = 1,                    Description = "the one with big park"                },                new City("Antwerp")                {                    Id = 2,                    Description = "the one with cathedral"                },                new City("paris")                {                    Id = 3,                    Description = "the one wit eiffle tower"

                });            modelBuilder.Entity<PointOfInterest>()                .HasData(                new PointOfInterest("central park")                {                    Id = 1,                    CityId = 1,                    Description = "the most visited urban park"                },                new PointOfInterest("Empire State Building")                {                    Id = 2,                    CityId = 1,                    Description = "A 102-story of skyscrapper"                },                new PointOfInterest("cathedral")                {                    Id = 3,                    CityId = 2,                    Description = "A gothic style cathedral"                },                 new PointOfInterest("Grote Markt")                 {                     Id = 4,                     CityId = 2,                     Description = "The central square of Antwerp"                 },            new PointOfInterest("Louvre Museum")            {                Id = 5,                CityId = 3,                Description = "The world's largest art museum"            },            new PointOfInterest("Notre-Dame Cathedral")            {                Id = 6,                CityId = 3,                Description = "Famous Gothic cathedral in Paris"            }                );            base.OnModelCreating(modelBuilder);        }    }}