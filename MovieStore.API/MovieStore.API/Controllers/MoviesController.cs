using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Get all movies
        /// </summary>

        [HttpGet("")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            return Ok(movies);

        }

        /// <summary>
        /// Create a movie
        /// </summary>

        [HttpPost("")]
        public async Task<IActionResult> AddNewMovie([FromBody] MovieModel movieModel)
        {
            int newMovieId = await _movieRepository.AddNewMovieAsync(movieModel);

            movieModel.Id = newMovieId;

            return CreatedAtAction(nameof(GetMovieById), new { id = newMovieId }, movieModel);
        }

        /// <summary>
        /// Get a movie by id
        /// </summary>

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

        /// <summary>
        /// Update a movie by id
        /// </summary>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] MovieModel movieModel)
        {
            int newMovieId = await _movieRepository.UpdateMovieAsync(id, movieModel);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (newMovieId == 0)
            {
                return NotFound();
            }
            
            // If the provided id in the route exists in the database, return the updated record to the client
            movieModel.Id = newMovieId;

            return Ok(movieModel);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateMoviePatch([FromRoute] int id, [FromBody] JsonPatchDocument movieModel)
        {
            int newMovieId = await _movieRepository.UpdateMoviePatchAsync(id, movieModel);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (newMovieId == 0)
            {
                return NotFound();
            }

            // If the provided id in the route exists in the database, return OK to the client
            return Ok();
        }

        /// <summary>
        /// Delete a movie by id
        /// </summary>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            int deletedId = await _movieRepository.DeleteMovieAsync(id);

            // Will work if a record for the provided id in the route doesn't exist in the database
            if (deletedId == 0)
            {
                return NotFound();
            }

            // If the provided id in the route exists in the database, return OK to the client
            return Ok();
        }
    }
}
