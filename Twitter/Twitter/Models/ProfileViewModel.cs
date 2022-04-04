using System.ComponentModel.DataAnnotations;

namespace Twitter.Models
{
    public class ProfileViewModel

    {
        public User Id { get; set; }

        public IEnumerable<Tweet> Tweets { get; set; }

        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }

        public string Comments { get; set; }

        public string Avatar { get; set; }// = Path.GetFileName("/images/user.png");
    }
}
