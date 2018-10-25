using System.Threading.Tasks;
using MovieRental.Contract.Service;
using MovieRental.Contract.Model;
using MovieRental.Contract.DataAccess;

namespace MovieRental.Service.Movie
{
    public class PaymentService : IPaymentService
    {
        public string ProcessPayment(int customerID, decimal amount)
        {
            // TODO - Implement payment processing here
            return string.Empty;
        }
    }
}
