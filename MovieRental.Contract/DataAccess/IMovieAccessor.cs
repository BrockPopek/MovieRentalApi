using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Model;

namespace MovieRental.Contract.DataAccess
{
    public interface IMovieAccessor
    {
        Task<IEnumerable<MovieModel>> GetAllAsync();
        Task<IEnumerable<MovieModel>> GetByIDList(List<int> movieIDs);
        Task<MovieModel> GetByIDAsync(int movieID);
        Task<MovieModel> InsertAsync(MovieModel model, string updateBy);
        Task<MovieModel> UpdateAsync(MovieModel model, string updateBy);
        Task<bool> DeleteAsync(int movieID);
    }
}
