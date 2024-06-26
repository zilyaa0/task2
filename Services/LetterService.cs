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
using Microsoft.AspNetCore.Mvc;

namespace ask2.Services
{
    #region interface
    public interface ILetterService
    {
        List<Letter> GetLettersByPage(int page, int count);
    }
    #endregion

     class LetterService : ILetterService
    {
        #region fields
        private readonly ILetterRepository _letterRepository;
        #endregion

        #region constructor
        public LetterService(ILetterRepository letterRepository)
        {
            _letterRepository = letterRepository;
        }
        #endregion

        #region methods
        public List<Letter> GetLettersByPage(int page, int limit)
        {
            return _letterRepository.ReadLettersByPage(page, limit);
        }
        #endregion
    }
}
