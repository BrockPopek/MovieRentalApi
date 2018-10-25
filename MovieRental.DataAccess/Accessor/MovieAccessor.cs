using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieRental.Contract.Model;
using MovieRental.Contract.DataAccess;
using MovieRental.DataAccess.DbContext;
using MovieRental.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace MovieRental.DataAccess.Accessor
{
    public class MovieAccessor : IMovieAccessor
    {     

        public async Task<IEnumerable<MovieModel>> GetAllAsync()
        {
            using (var context = new ApplicationUserDbContext())
            {
                var movies = await context.Movies
                    .ToListAsync();

                var models = new List<MovieModel>();
                foreach (var movie in movies)
                {
                    models.Add(GetModel(movie));
                }

                return models;
            }
        }

        public async Task<IEnumerable<MovieModel>> GetByIDList(List<int> movieIDs)
        {
            using (var context = new ApplicationUserDbContext())
            {
                var movies = await context.Movies
                    .Where(m => movieIDs.Contains(m.MovieId))
                    .ToListAsync();

                var models = new List<MovieModel>();
                foreach (var movie in movies)
                {
                    models.Add(GetModel(movie));
                }

                return models;
            }
        }

        public async Task<MovieModel> GetByIDAsync(int movieID)
        {
            using (var context = new ApplicationUserDbContext())
            {
                var movie = await context.Movies
                    .FirstOrDefaultAsync(c => c.MovieId == movieID);

                return GetModel(movie);
            }
        }

        public async Task<MovieModel> InsertAsync(MovieModel model, string updateBy)
        {
            using (var context = new ApplicationUserDbContext())
            {
                var movie = new Movie();
                movie.InjectFrom(model);

                movie.MovieId = 0;
                movie.UpdatedDate = DateTime.UtcNow;
                movie.UpdatedBy = updateBy;

                context.Movies.Add(movie);

                await context.SaveChangesAsync();

                model.MovieId = movie.MovieId;

                return model;
            }
        }

        public async Task<MovieModel> UpdateAsync(MovieModel model, string updateBy)
        {
            using (var context = new ApplicationUserDbContext())
            {
                var movie = await context.Movies.FirstOrDefaultAsync(m => m.MovieId == model.MovieId);

                if (movie != null)
                {
                    movie.InjectFrom(model);

                    movie.UpdatedDate = DateTime.UtcNow;
                    movie.UpdatedBy = updateBy;

                    await context.SaveChangesAsync();
                }
                else
                    model = null;  // clear out the model if not found                  

                return model;
            }
        }

        public async Task<bool> DeleteAsync(int movieID)
        {
            var ret = false;
            using (var context = new ApplicationUserDbContext())
            {
                var movie = await context.Movies.FirstOrDefaultAsync(m => m.MovieId == movieID);

                if (movie != null)
                {
                    context.Movies.Remove(movie);

                    await context.SaveChangesAsync();

                    ret = true;
                }
            }
            return ret;
        }

        private MovieModel GetModel(Movie movie)
        {
            MovieModel model = null;
            if (movie != null)
            {
                model = new MovieModel();
                model.InjectFrom(movie);
            }
            return model;
        }

        
    }
}
