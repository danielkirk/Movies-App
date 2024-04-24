using Dapper;
using Movies.Application.Database;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public MovieRepository(IDbConnectionFactory connectionFactory)
        {
            _dbConnectionFactory = connectionFactory;
        }
        public async Task<bool> CreateAsync(Movie movie)
        {
           using var connection = await _dbConnectionFactory.CreateConnectionAsync();
           using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                insert into movies (id, title, slug, yearofrelease)
                values (@Id, @Title, @Slug, @YearOfRelease)
                """, movie, transaction));

            if (result > 0)
            {
                foreach (var genre in movie.Genres)
                {
                await connection.ExecuteAsync(new CommandDefinition("""
                    insert into genres (movieId, name) 
                    values (@MovieId, @Name);
                    """, new { MovieId = movie.Id, Name = genre }));
                }
            }
            transaction.Commit();

            return result > 0;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
           throw new NotImplementedException();
        }

        public Task<bool> ExistsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
