using ask2.Models;

namespace ask2.Repositories
{
    public class LettersQueryResult
    {
        public List<Letter> Letters { get; set; }
        public int Page {  get; set; }
        public int Limit { get; set; }
        public int TotalCount { get; set; }

        public LettersQueryResult(List<Letter> letters, int page, int limit, int totalCount) 
        { 
            Letters = letters;
            Page = page;
            Limit = limit;
            TotalCount = totalCount;   
        }
    }
}
