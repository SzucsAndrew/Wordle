namespace Wordle.Dll.Models
{
    public class MatchModel
    {
        public string HashedWordId { get; set; }
        public string HashedUserId { get; set; }
        public int Attemt { get; set; }
        public bool Won { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
