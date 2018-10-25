using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MovieRental.Contract.Model;
using MovieRental.Contract.Service;


namespace MovieRentalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get all of the movies in the API.
        /// </summary>
        /// <returns>List of Movies</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetAll()
        {
            return Ok(await _movieService.GetAll());
        }

        /// <summary>
        /// Get the movie record for the given ID.
        /// </summary>
        /// <param name="movieID"></param>
        /// <returns>Movie record</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<MovieModel>> GetByID(int movieID)
        {
            var model = await _movieService.GetByID(movieID);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>
        /// Create a new movie.
        /// </summary>
        /// <param name="movie">Movie Model</param>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<MovieModel>> Create([FromBody] MovieModel movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();
           
            var model = await _movieService.Create(movie, User.Identity.Name);
            
            return Ok(model);
        }

        /// <summary>
        /// Update a movie.
        /// </summary>
        /// <param name="movie">Movie Model</param>
        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<MovieModel>> Update([FromBody] MovieModel movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var model = await _movieService.Update(movie, User.Identity.Name);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>
        /// Delete the customer.
        /// </summary>
        /// <param name="movieID"></param>
        [HttpDelete("[action]")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Delete(int movieID)
        {
            var deleted = await _movieService.Delete(movieID);
            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}