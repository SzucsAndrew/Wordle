namespace Wordle.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
