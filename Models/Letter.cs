
namespace ask2.Models
{
    [Serializable]
    public class Letter
    {
        //public long Id { get; set; }    
        public string Sender { get; set; }
        public string Headers { get; set; }
        public string Text { get; set; }
        public Letter(string sender, string headers, string text)
        {
            Sender = sender;
            Headers = headers;
            Text = text;
        }

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
    }
}
