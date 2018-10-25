using System;
using System.Linq;
using System.Threading.Tasks;
using MovieRental.Contract.Model;
using MovieRental.Contract.DataAccess;
using MovieRental.DataAccess.DbContext;
using MovieRental.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace MovieRental.DataAccess.Accessor
{
    public class RentalAccessor : IRentalAccessor
    {     
        public async Task<string> InsertAsync(RentalModel rentalModel, CustomerModel customerModel, string updateBy)
        {
            var errorMessage = string.Empty;

            using (var context = new ApplicationUserDbContext())
            {
                var customer = new Customer();
                customer.InjectFrom(customerModel);

                if (customer == null)
                    return "Customer not found.";

                var movies = await context.Movies
                    .Where(m => rentalModel.MovieIds.Contains(m.MovieId))
                    .ToListAsync();

                if (movies.Count == 0)
                    return "No movies were found.";
                
                foreach (var movie in movies)
                {
                    // If a movie is not available, then stop the rental
                    if (movie.NumberAvailable == 0)
                    {
                        errorMessage = $"Movie - {movie.Name} - is not available.";
                        break;
                    }

                    movie.NumberAvailable--;

                    var rental = new Rental
                    {
                        CustomerId = customer.CustomerId,
                        Movie = movie,
                        RentedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        UpdatedBy = updateBy
                    };

                    context.Rentals.Add(rental);
                }

                // No errors, then save.
                if (string.IsNullOrEmpty(errorMessage))
                    await context.SaveChangesAsync();
            }

            return errorMessage;
        }

        public async Task<(string errorMessage, decimal amountOwed)> UpdateAsync(RentalModel rentalModel, CustomerModel customerModel, string updateBy)
        {
            var errorMessage = string.Empty;
            var amountOwed = 0.00m;

            using (var context = new ApplicationUserDbContext())
            {
                foreach (var movieID in rentalModel.MovieIds)
                {
                    // Find if the customer has the movie rented out now.
                    var rental = await context.Rentals
                        .Include(c => c.Movie)
                        .FirstOrDefaultAsync(m => m.Customer.CustomerId == customerModel.CustomerId && 
                            m.Movie.MovieId == movieID && 
                            !m.ReturnedDate.HasValue);

                    if (rental == null)
                    {
                        errorMessage = "Movie is not currently rented out for customer.";
                        break;
                    }

                    rental.Movie.NumberAvailable++;

                    // If number available is greater than what is in stock, then bump up 
                    if (rental.Movie.NumberAvailable > rental.Movie.NumberInStock)
                    {
                        errorMessage = $"Movie - {rental.Movie.Name} - number available is greater than what is in stock.";
                        break;
                    }

                    rental.ReturnedDate = DateTime.UtcNow;

                    // Now figure cost
                    amountOwed += rental.Movie.AmountPerDay * (decimal)Math.Ceiling((rental.ReturnedDate.Value - rental.RentedDate).TotalDays);

                    rental.UpdatedDate = DateTime.UtcNow;
                    rental.UpdatedBy = updateBy;
                }

                // No errors, then save.
                if (string.IsNullOrEmpty(errorMessage))
                    await context.SaveChangesAsync();
            }

            return (errorMessage, amountOwed);
        }

    }
}
