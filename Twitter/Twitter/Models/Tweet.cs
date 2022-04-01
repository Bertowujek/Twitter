namespace Twitter.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }

        public string Comments { get; set; }

        public virtual User User { get; set; }

    }
}
