using System.ComponentModel.DataAnnotations;

namespace Twitter.Models
{
    public class UserViewModel

        // It's the user without the password. It's the one that we are going to use in the autentication. 
    {
        public int Id { get; set; }

        public string Handle { get; set; }
        public string Name { get; set; }

        public string? Email { get; set; }

        public string Avatar { get; set; }
    }
}
