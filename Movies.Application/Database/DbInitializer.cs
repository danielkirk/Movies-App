using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Database
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public DbInitializer(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            await connection.ExecuteAsync("""
                CREATE TABLE IF NOT EXISTS movies (
                    id UUID PRIMARY KEY,
                    title TEXT NOT NULL,
                    slug TEXT NOT NULL,
                    yearofrelease INT NOT NULL);
                """);

            await connection.ExecuteAsync("""
                create unique index concurrently if not exists movies_slug_idx 
                on movies 
                using btree(slug);
                """);

        }
    }
}
