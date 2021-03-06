using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MovieStore.API.Data;
using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieStoreContext _context; // Creating an instance of DbContext using dependency injection

        private readonly IMapper _mapper; // Creating an instance of IMapper using dependency injection

        // Constructor to use the dependency injection so to get the instance for DbContext and IMapper
        // Dependency injection allows to get the instance of DbContext and the IMapper in the application because we have registered them in the Startup class
        // All the methods in this repository can now use the instances inside the constructor below
        public MovieRepository(MovieStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Retrieves all the movies from the database and send it to the conntroller class requesting them
        public async Task<List<MovieModel>> GetAllMoviesAsync()
        {
            // Retrieving the list of all movies from the database
            var retrievedRecords = await _context.Movies.ToListAsync();
            // Using auto mapper to convert the list of Movies type to list of MovieModel type
            return _mapper.Map<List<MovieModel>>(retrievedRecords);
        }

        // Retrieves all the movies from the database and send it to the conntroller class requesting them
        public async Task<MovieModel> GetMovieByIdAsync(int movieID)
        {
            // Retrieving the list of all movies from the database
            var retrievedRecord = await _context.Movies.FindAsync(movieID);
            // Using auto mapper to convert the list of Movies type to list of MovieModel type
            return _mapper.Map<MovieModel>(retrievedRecord);
        }

        // Adds a new movie to the database and returns the new movie record id number to the controller
        public async Task<int> AddNewMovieAsync(MovieModel movieModel)
        {
            var movie = new Movies()
            {
                Name = movieModel.Name,
                Rating = movieModel.Rating,
                Genre = movieModel.Genre,
                RentPrice = movieModel.RentPrice
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return movie.Id;
        }

        // Updates a movie record in the database if the id exists. Otherwise, it returns 0
        // Used the try-catch block to notify the requesting client in case a record with provided id doesn't exist in the database
        public async Task<int> UpdateMovieAsync(int id, MovieModel movieModel)
        {
            try
            {
                var movie = new Movies()
                {
                    Id = id,
                    Name = movieModel.Name,
                    Rating = movieModel.Rating,
                    Genre = movieModel.Genre,
                    RentPrice = movieModel.RentPrice
                };

                _context.Movies.Update(movie);
                // Hitting the database only one time for performance
                await _context.SaveChangesAsync();

                return movie.Id;
            }
            catch
            {
                return 0;
            }
            
        }

        // Updates a movie record in the database if the id exists. Otherwise, it returns 0
        // Used the try-catch block to notify the requesting client in case a record with provided id doesn't exist in the database
        public async Task<int> UpdateMoviePatchAsync(int id, JsonPatchDocument movieModel)
        {
            try
            {
                var movieRecord = await _context.Movies.FindAsync(id);

                //if(movieRecord == null)
                //{
                //    throw new Exception();
                //}

                movieModel.ApplyTo(movieRecord);
                await _context.SaveChangesAsync();

                return id;
            }
            catch
            {
                return 0;
            }

        }

        // Deletes a movie record in the database if the id exists. Otherwise, it returns 0
        // Used the try-catch block to notify the requesting client in case a record with provided id doesn't exist in the database
        public async Task<int> DeleteMovieAsync(int id)
        {
            try
            {
                var movieRecord = await _context.Movies.FindAsync(id);
                _context.Movies.Remove(movieRecord);
                await _context.SaveChangesAsync();
                return id;
            }
            catch
            {
                return 0;
            }

        }
    }
}
