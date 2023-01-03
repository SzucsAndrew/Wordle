namespace Wordle.Data.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDeactivated { get; set; }
    }
}
