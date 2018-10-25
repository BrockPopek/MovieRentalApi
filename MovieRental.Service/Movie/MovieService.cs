using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Service;
using MovieRental.Contract.Model;
using MovieRental.Contract.DataAccess;

namespace MovieRental.Service.Movie
{
    public class MovieService : IMovieService
    {
        private readonly IMovieAccessor _movieAccessor;

        public MovieService(IMovieAccessor movieAccessor)
        {
            _movieAccessor = movieAccessor;
        }

        public async Task<MovieModel> Create(MovieModel movie, string updateBy)
        {
            return await _movieAccessor.InsertAsync(movie, updateBy);
        }

        public async Task<bool> Delete(int movieID)
        {
            return await _movieAccessor.DeleteAsync(movieID);
        }

        public async Task<IEnumerable<MovieModel>> GetAll()
        {
            return await _movieAccessor.GetAllAsync();
        }

        public async Task<MovieModel> GetByID(int movieID)
        {
            return await _movieAccessor.GetByIDAsync(movieID);
        }

        public async Task<MovieModel> Update(MovieModel movie, string updateBy)
        {
            return await _movieAccessor.UpdateAsync(movie, updateBy);
        }
    }
}
