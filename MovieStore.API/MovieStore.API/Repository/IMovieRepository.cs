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
    }
}
