using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.API.Models;
using MovieStore.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            return Ok(movies);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);

            if(movie == null)
            {
                return NotFound();
            }

            return Ok(movie);

        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewMovie([FromBody] MovieModel movieModel)
        {
            int newMovidId = await _movieRepository.AddNewMovieAsync(movieModel);

            movieModel.Id = newMovidId;

            return CreatedAtAction(nameof(GetMovieById), new { id = newMovidId }, movieModel);
        }
    }
}
