using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] MovieModel movieModel)
        {
            int newMovidId = await _movieRepository.UpdateMovieAsync(id, movieModel);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (newMovidId == 0)
            {
                return NotFound();
            }
            
            // If the provided id in the route exists in the database, return the updated record to the client
            movieModel.Id = newMovidId;

            return Ok(movieModel);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateMoviePatch([FromRoute] int id, [FromBody] JsonPatchDocument movieModel)
        {
            int newMovidId = await _movieRepository.UpdateMoviePatchAsync(id, movieModel);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (newMovidId == 0)
            {
                return NotFound();
            }

            // If the provided id in the route exists in the database, return OK to the client
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            int newMovidId = await _movieRepository.DeleteMovieAsync(id);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (newMovidId == 0)
            {
                return NotFound();
            }

            // If the provided id in the route exists in the database, return OK to the client
            return Ok();
        }
    }
}
