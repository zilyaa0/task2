
namespace ask2.Models
{
    [Serializable]
    public class Letter
    {
        #region properties
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Headers { get; set; }
        public string Text { get; set; }
        public string MessageId { get; set; }
        #endregion

        #region constructor
        public Letter(string sender, string headers, string text, string messageId)
        {
            Sender = sender;
            Headers = headers;
            Text = text;
            MessageId = messageId;
        }
        #endregion
    }
}
