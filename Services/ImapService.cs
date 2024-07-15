using ask2.Models;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using ask2.Repositories;
using MailKit.Search;
using MimeKit;
using System.Collections.Generic;

namespace ask2.Services
{
    #region interface
    interface IImapService
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
                            var uids = inbox.Search(SearchQuery.All);
                            var items = inbox.Fetch(uids, MessageSummaryItems.Envelope | MessageSummaryItems.BodyStructure);
                            for (int i = 0; i < items.Count; i++)
                            {
                                var list = new List<string>();
                                foreach (var att in items[i].Attachments.OfType<BodyPartBasic>())
                                {
                                    list.Add(att.FileName);
                                    var part = (MimePart)client.Inbox.GetBodyPart(items[i].UniqueId, att);
                                    var pathDir = Path.Combine(Environment.CurrentDirectory, "Emails", items[i].UniqueId.ToString());
                                    if (!Directory.Exists(pathDir))
                                        Directory.CreateDirectory(pathDir);
                                    var path = Path.Combine(pathDir, att.FileName);
                                    if (!File.Exists(path))
                                    {
                                        using (var stream = File.Create(path))
                                        {
                                            part.Content.DecodeTo(stream);
                                        }
                                    }
                                }
                                var bodyPart = items[i].TextBody;
                                var body = (TextPart)inbox.GetBodyPart(items[i].UniqueId, bodyPart);
                                AddLetter(new Letter(items[i].Envelope.From.ToString(), items[i].Envelope.Subject, body.Text, items[i].Envelope.MessageId, items[i].UniqueId.ToString()));
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
            if (_letterRepository.FindLetterByMessageId(currentLetter.MessageId))
                _letterRepository.AddLetter(currentLetter);
        }
        #endregion
    }
}
