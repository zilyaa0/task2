using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using ask2.Repositories;
using System.Data;
using MailKit.Search;
using System.Net.Mail;
using MimeKit;
using Org.BouncyCastle.Asn1.X509;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting.Server;
using System.Xml;
using System.IO;

namespace ask2.Services
{
    #region Interface
    public interface IFileService
    {
        List<FileName> GetAllFiles(string uniqueId);
        MemoryStream LoadFile(string uniqueId, string fileName);
    }
    #endregion
    class FileService : IFileService
    {
        #region fields
        private readonly IConfiguration _configuration;
        #endregion

        #region constructor
        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public List<FileName> GetAllFiles(string uniqueId)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Emails", uniqueId);
            List<string> filesFromLetter = (from a in Directory.GetFiles(path) select Path.GetFileName(a)).ToList();
            var files = new List<FileName>();
            foreach (var f in filesFromLetter)
                files.Add(new FileName() { Name = f });
            return files;
        }

        public MemoryStream LoadFile(string uniqueId, string fileName) 
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Emails", uniqueId, fileName);
            byte[] fileData = File.ReadAllBytes(path);
            MemoryStream stream = new MemoryStream();
            stream.Write(fileData, 0, fileData.Length);
            stream.Position = 0;
            return stream;
        }
        #endregion
    }
}