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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get all of the customers in the API.
        /// </summary>
        /// <returns>List of Customers</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetAll()
        {
            return Ok(await _customerService.GetAll());
        }

        /// <summary>
        /// Get the customer record for the given ID.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>Customer record</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<CustomerModel>> GetByID(int customerID)
        {
            var model = await _customerService.GetByID(customerID);

            if (model == null)
                return NotFound();

            return Ok(model);
        }
        
        /// <summary>
        /// Get the customer record for the current User.
        /// </summary>
        /// <returns>Customer Record</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<CustomerModel>> GetCurrentUser()
        {
            var model = await _customerService.GetByUser(User.Identity.Name);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>
        /// Create a new customer for the current logged in user.
        /// </summary>
        /// <param name="customer">Customer Model</param>
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> CreateForCurrentUser([FromBody] CustomerModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
           
            var model = await _customerService.Create(customer, User.Identity.Name);

            // TODO - Need a better way to return error results and process them.
            if (model == null)
                return BadRequest("Customer already exists for this user.");

            return Ok(model);
        }

        /// <summary>
        /// Update a customer.
        /// </summary>
        /// <param name="customer">Customer Model</param>
        [HttpPut]
        public async Task<ActionResult<CustomerModel>> Update([FromBody] CustomerModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var model = await _customerService.Update(customer, User.Identity.Name);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        /// <summary>
        /// Delete the customer.
        /// </summary>
        /// <param name="customerID"></param>
        [HttpDelete("[action]")]
        public async Task<ActionResult> Delete(int customerID)
        {
            var deleted = await _customerService.Delete(customerID);
            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}