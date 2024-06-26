using ask2.Models;
using Microsoft.EntityFrameworkCore;

namespace ask2.Repositories
{
    #region interface
    interface ILetterRepository
    {
        void AddLetter(Letter letter);
        List<Letter> ReadLettersByPage(int page, int count);
        bool FindLetterByMessageId(string messageId);

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

        public bool FindLetterByMessageId(string messageId)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                if (db.Letters.FirstOrDefault(x => x.MessageId == messageId) == null)
                    return true;
                else
                    return false;
            }
        }

        public List<Letter> ReadLettersByPage(int page, int count)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                List<Letter> lettersByPage = db.Letters.Skip((page - 1) * count).Take(count).ToList();
                return lettersByPage;
            }
        }
        #endregion
    }
}
