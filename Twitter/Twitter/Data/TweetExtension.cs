using Twitter.Models;

namespace Twitter.Data

{
    public static class TweetExtension
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<TwitterContext>();  //serviceT
                    // Creates the database if not exists
                    if (context.Database.EnsureCreated())
                    {
                        TweetDBInitializer.InsertData(context); //Cria a base de dados caso não exista
                    }
                    //TweetDBInitializer.InsertData(context); //Cria a base de dados caso não exista
                }
            }
        }
    }
}

