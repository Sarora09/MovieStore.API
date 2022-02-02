using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Repository
{
    // Interface for MovieRepository
    public interface IMovieRepository
    {
        Task<List<MovieModel>> GetAllMoviesAsync();

        Task<int> AddNewMovieAsync(MovieModel movieModel);

        Task<MovieModel> GetMovieByIdAsync(int movieID);

        Task<int> UpdateMovieAsync(int id, MovieModel movieModel);
    }
}
