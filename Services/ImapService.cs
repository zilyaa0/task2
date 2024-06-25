using ask2.Models;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using ask2.Repositories;

namespace ask2.Services
{
    #region interface
    public interface IImapService
    {
        void Start();
    }
    #endregion

     class ImapService : IImapService
    {
        #region fields
        private readonly IConfiguration _configuration;
        private readonly ILetterRepository _letterRepository;
        #endregion

        #region constructor
        public ImapService(IConfiguration configuration, ILetterRepository letterRepository)
        {
            _configuration = configuration;
            _letterRepository = letterRepository;
        }
        #endregion

        #region methods
        public void Start()
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        using (var client = new ImapClient())
                        {
                            client.Connect("imap.mail.ru", 993, SecureSocketOptions.SslOnConnect);

                            client.Authenticate(_configuration["Email"], _configuration["Password"]);

                            var inbox = client.Inbox;
                            inbox.Open(FolderAccess.ReadOnly);
                            for (int i = 0; i < inbox.Count; i++)
                            {
                                var message = inbox.GetMessage(i);
                                AddLetter(new Letter(message.From.ToString(), message.Subject, message.TextBody));
                            }
                            client.Disconnect(true);
                        }
                    }
                    catch
                    {
                        return;
                    }
                    Thread.Sleep(1000 * 60);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
        private void AddLetter(Letter currentLetter)
        {
            List<Letter> letters = _letterRepository.ReadAllLetters();
            foreach (Letter letter in letters)
            {
                if (letter.GetHashCode() == currentLetter.GetHashCode())
                {
                    return;
                }
            }
            _letterRepository.AddLetter(currentLetter);
        }
        #endregion
    }
}
