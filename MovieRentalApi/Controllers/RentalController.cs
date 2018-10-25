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
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Rent a movie.
        /// </summary>
        /// <param name="rental">Rental Model</param>
        [HttpPost("[action]")]
        public async Task<ActionResult> RentOut([FromBody] RentalModel rental)
        {
            if (!ModelState.IsValid)
                return BadRequest();
           
            var errorMessage = await _rentalService.RentOut(rental, User.Identity.Name);

            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);
            else
                return Ok();
        }

        /// <summary>
        /// Return movie.
        /// </summary>
        /// <param name="rental">Movie Model</param>
        [HttpPut("[action]")]
        public async Task<ActionResult<ReturnModel>> ReturnMovie([FromBody] RentalModel rental)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var (errorMessage, returnModel) = await _rentalService.ReturnMovie(rental, User.Identity.Name);

            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);
            else
                return Ok(returnModel);
        }

    }
}