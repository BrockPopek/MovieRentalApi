using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MovieRental.Contract.Model;

namespace MovieRental.Contract.DataAccess
{
    public interface ICustomerAccessor
    {
        Task<IEnumerable<CustomerModel>> GetAllAsync();
        Task<CustomerModel> GetByIDAsync(int customerID);
        Task<CustomerModel> GetByUserAsync(string userName);
        Task<CustomerModel> InsertAsync(CustomerModel customerModel, IdentityUser user, string updateBy);
        Task<CustomerModel> UpdateAsync(CustomerModel model, string updateBy);
        Task<bool> DeleteAsync(int customerID);
    }
}
