using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Model;
using MovieRental.Contract.DataAccess;
using MovieRental.DataAccess.DbContext;
using MovieRental.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Omu.ValueInjecter;

namespace MovieRental.DataAccess.Accessor
{
    public class CustomerAccessor : ICustomerAccessor
    {
        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            using (var context = new ApplicationUserDbContext())
            {
                var customers = await context.Customers
                    .Include(c => c.ApplicationUser)
                    .ToListAsync();

                var models = new List<CustomerModel>();
                foreach (var customer in customers)
                {
                    models.Add(GetModel(customer));
                }

                return models;
            }
        }

        public async Task<CustomerModel> GetByIDAsync(int customerID)
        {
            using (var context = new ApplicationUserDbContext())
            {
                var customer = await context.Customers
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefaultAsync(c => c.CustomerId == customerID);

                return GetModel(customer);
            }
        }


        public async Task<CustomerModel> GetByUserAsync(string userName)
        {
            using (var context = new ApplicationUserDbContext())
            {
                var customer = await context.Customers
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefaultAsync(c => c.ApplicationUser.UserName == userName);

                return GetModel(customer);
            }
        }

        public async Task<CustomerModel> InsertAsync(CustomerModel model, IdentityUser user, string updateBy)
        {
            using (var context = new ApplicationUserDbContext())
            {
                var customer = new Entities.Customer();
                customer.InjectFrom(model);

                customer.CustomerId = 0;
                customer.ApplicationUserId = user.Id;
                customer.UpdatedDate = DateTime.UtcNow;
                customer.UpdatedBy = updateBy;

                context.Customers.Add(customer);

                await context.SaveChangesAsync();

                model.CustomerId = customer.CustomerId;

                return model;
            }
        }

        public async Task<CustomerModel> UpdateAsync(CustomerModel model, string updateBy)
        {
            using (var context = new ApplicationUserDbContext())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(m => m.CustomerId == model.CustomerId);

                if (customer != null)
                {
                    customer.InjectFrom(model);

                    customer.UpdatedDate = DateTime.UtcNow;
                    customer.UpdatedBy = updateBy;

                    await context.SaveChangesAsync();
                }
                else
                    model = null;  // clear out the model if not found                  

                return model;
            }
        }

        public async Task<bool> DeleteAsync(int customerID)
        {
            var ret = false;
            using (var context = new ApplicationUserDbContext())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(m => m.CustomerId == customerID);

                if (customer != null)
                {
                    context.Customers.Remove(customer);
                    
                    await context.SaveChangesAsync();

                    ret = true;
                }
            }
            return ret;
        }

        private CustomerModel GetModel(Customer customer)
        {
            CustomerModel model = null;
            if (customer != null)
            {
                model = new CustomerModel();
                model.InjectFrom(customer);
                model.InjectFrom(customer.ApplicationUser);
            }
            return model;
        }

    }
}
