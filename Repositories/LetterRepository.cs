using ask2.Models;
using Microsoft.EntityFrameworkCore;

namespace ask2.Repositories
{
    #region interface
    interface ILetterRepository
    {
        void AddLetter(Letter letter);
        LettersQueryResult ReadLetters(int page, int count, string searchString);
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

        public LettersQueryResult ReadLetters(int page, int count, string searchString)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                if (searchString != "")
                { 
                    List<Letter> lettersBySearchString = db.Letters.Where(x => x.Sender.Contains(searchString) || x.Headers.Contains(searchString) || x.Text.Contains(searchString)).Skip((page - 1) * count).Take(count).ToList();
                    return new LettersQueryResult(lettersBySearchString, page, count, GetTotalCount(searchString));
                }
                else
                {
                    List<Letter> lettersBySearchString = db.Letters.Skip((page - 1) * count).Take(count).ToList();
                    return new LettersQueryResult(lettersBySearchString, page, count, db.Letters.Count());
                }
            }
        }

        private int GetTotalCount(string searchString)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                return db.Letters.Where(x => x.Sender.Contains(searchString) || x.Headers.Contains(searchString) || x.Text.Contains(searchString)).Count();
            }
        }
        #endregion
    }
}
