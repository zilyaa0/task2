using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ask2.Models
{
    public class LettersContext : DbContext
    {
        #region [methods]
        public LettersContext(DbContextOptions<LettersContext> options) : base(options)
        {

        }
        public DbSet<Letter> Letters { get; set; }
        #endregion
    }
}
