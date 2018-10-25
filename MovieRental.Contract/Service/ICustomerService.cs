using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Model;

namespace MovieRental.Contract.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerModel>> GetAll();
        Task<CustomerModel> GetByID(int customerID);
        Task<CustomerModel> GetByUser(string userName);
        Task<CustomerModel> Create(CustomerModel customer, string updateBy);
        Task<CustomerModel> Update(CustomerModel customer, string updateBy);
        Task<bool> Delete(int customerID);
    }
}
