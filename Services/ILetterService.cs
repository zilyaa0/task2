using ask2.Models;
using MailKit;

namespace ask2.Services
{
    public interface ILetterService
    {
        public List<Letter> ReadLetter();
        public bool FindLetter(Letter letter);
        public void SaveLetter(Letter letter);
        public List<Letter> AllLetters();
    }
}
