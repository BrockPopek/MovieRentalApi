using MovieRental.Contract.Model;

namespace MovieRental.Contract.Service
{
    public interface IPaymentService
    {
        string ProcessPayment(int customerID, decimal amount);
    }
}
