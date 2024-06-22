using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using MailKit.Search;
using MimeKit;
using ask2.Models;
using System.Text.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using ask2.Repositories;

namespace ask2.Services
{
    #region [interface]
    public interface ILetterService
    {
        public List<Letter> GetLetters();
    }
    #endregion

    public class LetterService : ILetterService
    {
        #region [fields]
        private readonly ILetterRepository _letterRepository;
        #endregion

        #region [constructor]
        public LetterService(ILetterRepository letterRepository)
        {
            _letterRepository = letterRepository;
        }
        #endregion

        #region [methods]
        public List<Letter> GetLetters()
        {
            return _letterRepository.ReadAllLetters().ToList();
        }
        #endregion
    }
}
