using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Model;

namespace MovieRental.Contract.Service
{
    public interface IRentalService
    {
        Task<string> RentOut(RentalModel model, string updateBy);
        Task<(string errorMessage, ReturnModel returnModel)> ReturnMovie(RentalModel model, string updateBy);
    }
}
