namespace Twitter.Models
{
    //Adicionar atributos do tipo profile picture, bio, localizaçao, website, followers, following
    public class User
    {
        //internal User user;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Handle { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }// = Path.GetFileName("/images/user.png");

    }
}
