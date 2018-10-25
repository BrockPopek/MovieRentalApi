using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Model;

namespace MovieRental.Contract.DataAccess
{
    public interface IRentalAccessor
    {
        Task<string> InsertAsync(RentalModel rentalModel, CustomerModel customerModel, string updateBy);
        Task<(string errorMessage, decimal amountOwed)> UpdateAsync(RentalModel rentalModel, CustomerModel customerModel, string updateBy);
    }
}
