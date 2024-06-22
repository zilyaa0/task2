using ask2.Models;

namespace ask2.Repositories
{
    #region [interface]
    public interface ILetterRepository
    {
        public void AddLetter(Letter letter);
        public List<Letter> ReadAllLetters();
    }
    #endregion

    public class LetterRepository : ILetterRepository
    {
        #region [fields]
        private readonly LettersContext _context;
        #endregion

        #region [constructor]
        public LetterRepository(LettersContext context)
        {
            _context = context;
        }
        #endregion

        #region [methods]
        public void AddLetter(Letter letter)
        {
            _context.Letters.Add(letter);
            _context.SaveChangesAsync();
        }

        public List<Letter> ReadAllLetters()
        {
            return _context.Letters.ToList() ?? new List<Letter>();
        }
        #endregion
    }
}
