using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Tweet
    {
        [Column("id")]
        public int Id { get; set; }

        [NotMapped]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(140)]
        public string Text { get; set; }

        [MinLength(0)]
        public int Likes { get; set; }

        public string Comments { get; set; }

        public virtual User User { get; set; }

    }
}
