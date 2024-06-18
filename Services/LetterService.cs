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

namespace ask2.Services
{
    public class LetterService : ILetterService
    {
        private readonly IConfiguration Configuration;
        private List<Letter> letterList = new List<Letter>();

        public LetterService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool FindLetter(Letter currentLetter)
        {
            if (!File.Exists(Configuration["FileName"]))
            {
                var f = File.Create(Configuration["FileName"]);
                f.Close();
                return true;
            }
            else
            {
                List<Letter> letters = AllLetters();
                foreach (Letter letter in letters)
                {
                    if (letter.GetHashCode() == currentLetter.GetHashCode())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<Letter> ReadLetter()
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.mail.ru", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate(Configuration["Email"], Configuration["Password"]);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                var query = SearchQuery.SentSince(DateTime.Now.AddMonths(-2));
                var uids = inbox.Search(query);
  
                for (int i = 0; i < uids.Count; i++)
                {
                    var item = inbox.GetMessage(uids[i]);
                    SaveLetter(new Letter(item.From.ToString(), item.Subject, item.TextBody));
                }
                client.Disconnect(true);
                return letterList;
            } 
        }

        public void SaveLetter(Letter currentLetter)
        {
            if (FindLetter(currentLetter))
            {
                List<Letter> allLetters = AllLetters();
                allLetters.Add(currentLetter);
                string serialize = JsonConvert.SerializeObject(allLetters);
                File.WriteAllText(Configuration["FileName"], serialize);
                letterList.Add(currentLetter);
            }
        }

        public List<Letter> AllLetters()
        {
            string json = File.ReadAllText(Configuration["FileName"]);
            List<Letter> allLetters = JsonConvert.DeserializeObject<List<Letter>>(json);
            return allLetters ?? new List<Letter>();
        }
    }
}
