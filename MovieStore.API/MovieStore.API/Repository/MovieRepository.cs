using AutoMapper;
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

    }
}
