using System.Threading.Tasks;
using MovieRental.Contract.Service;
using MovieRental.Contract.Model;
using MovieRental.Contract.DataAccess;
using System.Collections.Generic;

namespace MovieRental.Service.Movie
{
    public class RentalService : IRentalService
    {
        private readonly IRentalAccessor _rentalAccessor;
        private readonly IPaymentService _paymentService;
        private readonly ICustomerService _customerService;

        private const string CustomerNotFoundError = "Customer record does not exist for current user.";

        public RentalService(IRentalAccessor rentalAccessor, IPaymentService paymentService, ICustomerService customerService)
        {
            _rentalAccessor = rentalAccessor;
            _paymentService = paymentService;
            _customerService = customerService;
        }
        
        public async Task<string> RentOut(RentalModel model, string updateBy)
        {
            // Check for existing customer for user.
            // If there is not one, then give an error.
            var customer = await _customerService.GetByUser(updateBy);

            if (customer == null)
                return CustomerNotFoundError;

            return await _rentalAccessor.InsertAsync(model, customer, updateBy);
        }

        public async Task<(string errorMessage, ReturnModel returnModel)> ReturnMovie(RentalModel model, string updateBy)
        {
            var returnModel = new ReturnModel();

            // Check for existing customer for user.
            // If there is not one, then give an error.
            var customer = await _customerService.GetByUser(updateBy);

            if (customer == null)
                return (CustomerNotFoundError, returnModel);

            var (errorMessage, amountOwed) = await _rentalAccessor.UpdateAsync(model, customer, updateBy);

            if (string.IsNullOrEmpty(errorMessage) && amountOwed > 0)
            {
                returnModel.TotalAmount = amountOwed;
                errorMessage = _paymentService.ProcessPayment(customer.CustomerId, amountOwed);

                returnModel.PaidInFull = string.IsNullOrEmpty(errorMessage);
            }

            return (errorMessage, returnModel);
        }

        public async Task<IEnumerable<RentalHistoryModel>> GetAllForUser(string updateBy)
        {
            // Check for existing customer for user.
            // If there is not one, then give an error.
            var customer = await _customerService.GetByUser(updateBy);

            return await _rentalAccessor.GetAllForCustomerAsync(customer.CustomerId);
        }
    }
}
