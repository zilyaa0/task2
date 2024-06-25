using ask2.Models;
using Microsoft.EntityFrameworkCore;

namespace ask2.Repositories
{
    #region interface
    public interface ILetterRepository
    {
        public void AddLetter(Letter letter);
        public List<Letter> ReadAllLetters();
        public List<Letter> ReadLettersByPage(int page, int count);
    }
    #endregion

    class LetterRepository : ILetterRepository
    {
        #region fields
        private readonly IDbContextFactory<LettersContext> _contextFactory;
        #endregion

        #region constructor
        public LetterRepository(IDbContextFactory<LettersContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        #endregion

        #region methods
        public void AddLetter(Letter letter)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                db.Letters.Add(letter);
                db.SaveChanges();
            }
        }

        public List<Letter> ReadAllLetters()
        {
            using (var db = _contextFactory.CreateDbContext())
                return db.Letters.ToList() ?? new List<Letter>();
        }

        public List<Letter> ReadLettersByPage(int page, int count)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                var allLetters = db.Letters.ToList();
                List<Letter> lettersByPage = new List<Letter>();
                for (int i = 0; i < count; i++)
                {
                    lettersByPage.Add(allLetters[(page - 1) * 10 + i]);
                }
                return lettersByPage;
            }
        }
        #endregion
    }
}
