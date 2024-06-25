using ask2.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ask2.Repositories
{
    public class LettersContextFactory : IDbContextFactory<LettersContext>
    {
        private readonly string _connectionString;
        public LettersContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public LettersContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LettersContext>();
            optionsBuilder.UseSqlite(_connectionString);

            return new LettersContext(optionsBuilder.Options);
        }
    }
}
