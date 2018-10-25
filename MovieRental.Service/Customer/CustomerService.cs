using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Service;
using MovieRental.Contract.Model;
using MovieRental.Contract.DataAccess;

namespace MovieRental.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerAccessor _customerAccessor;
        private readonly IApplicationUserAccessor _applicationUserAccessor;

        public CustomerService(ICustomerAccessor customerAccessor, IApplicationUserAccessor applicationUserAccessor)
        {
            _customerAccessor = customerAccessor;
            _applicationUserAccessor = applicationUserAccessor;
        }

        public async Task<CustomerModel> Create(CustomerModel customer, string updateBy)
        {
            CustomerModel newModel = null;

            // Check for existing customer for user.
            // If there is one, do not create another customer record.
            var existingUser = await _customerAccessor.GetByUserAsync(updateBy);

            if (existingUser == null)
            {
                var appUser = await _applicationUserAccessor.FindByNameAsync(updateBy);

                newModel =  await _customerAccessor.InsertAsync(customer, appUser, updateBy);
            }

            return newModel;
        }

        public async Task<bool> Delete(int customerID)
        {
            return await _customerAccessor.DeleteAsync(customerID);
        }

        public async Task<CustomerModel> GetByID(int customerID)
        {
            return await _customerAccessor.GetByIDAsync(customerID);
        }


        public async Task<CustomerModel> GetByUser(string userName)
        {
            return await _customerAccessor.GetByUserAsync(userName);
        }

        public async Task<IEnumerable<CustomerModel>> GetAll()
        {
            return await _customerAccessor.GetAllAsync();
        }

        public async Task<CustomerModel> Update(CustomerModel customer, string updateBy)
        {
            return await _customerAccessor.UpdateAsync(customer, updateBy);
        }

    }
}
