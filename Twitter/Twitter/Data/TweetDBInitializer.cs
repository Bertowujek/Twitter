using Twitter.Models;

namespace Twitter.Data
{
    public static class TweetDBInitializer
    {
        public static void InsertData(TwitterContext context)
        {
            // Adds a publisher
            var user = new User
            {
                Name = "Joao",
                Email = "test@gmail",
                Password = "1234",
                Handle = "Handle",
                Avatar = ""
            };
            context.Users.Add(user);

            var user1 = new User
            {
                Name = "Ariana",
                Email = "ariana@hotmail.com",
                Password = "abcd",
                Handle = "Ari",
                Avatar = ""
            };
            context.Users.Add(user1);

            // Adds some tweets
            context.Tweets.Add(new Tweet
            {
                User = user,
                Date = DateTime.Now,
                Text = "Olá Mundo",
                Likes = 1,
                Comments = "Funciona",

            });
            context.Tweets.Add(new Tweet
            {
                User = user,
                Date = DateTime.Now,
                Text = "Olá Terra",
                Likes = 1,
                Comments = "Funciona",

            });
            context.Tweets.Add(new Tweet
            {
                User = user,
                Date = DateTime.Now,
                Text = "Estou a perceber disto",
                Likes = 1,
                Comments = "sFunciona",

            });
            context.Tweets.Add(new Tweet
            {
                User = user1,
                Date = DateTime.Now,
                Text = "Nova tentativa",
                Likes = 1,
                Comments = "a ver se dá",

            });


            // Saves changes
            context.SaveChanges();
        }
    }
}

