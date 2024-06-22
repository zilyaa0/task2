
namespace ask2.Models
{
    [Serializable]
    public class Letter
    {
        #region [fields]
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Headers { get; set; }
        public string Text { get; set; }
        #endregion

        #region [constructor]
        public Letter(string sender, string headers, string text)
        {
            Sender = sender;
            Headers = headers;
            Text = text;
        }
        #endregion

        #region [methods]
        public override bool Equals(object? obj)
        {
            return obj is Letter letter &&
                   Sender == letter.Sender &&
                   Headers == letter.Headers &&
                   Text == letter.Text;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sender, Headers, Text);
        }
        #endregion
    }
}
