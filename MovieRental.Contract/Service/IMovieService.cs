using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Model;

namespace MovieRental.Contract.Service
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieModel>> GetAll();
        Task<MovieModel> GetByID(int movieID);
        Task<MovieModel> Create(MovieModel movie, string updateBy);
        Task<MovieModel> Update(MovieModel movie, string updateBy);
        Task<bool> Delete(int movieID);
    }
}
