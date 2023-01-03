namespace Wordle.Data.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Won { get; set; }
        public int Attempt { get; set; }

        public int WordId { get; set; }
        public Word Word { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}