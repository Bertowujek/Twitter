using Twitter.Models;

namespace Twitter.Service
{
    public class UserService : IUserService
    {

        private readonly TwitterContext context;

        public UserService (TwitterContext context)
        {
            this.context = context;
        }

        public User Create(User newUser)
        {
            context.Users.Add(newUser);
            context.SaveChanges();
            return newUser;
        }

        public void DeleteByHandle(string Handle)
        {
            var userToDelete = context.Users.Find(Handle);
            if (userToDelete is not null)
            {
                context.Users.Remove(userToDelete);
                context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            var users = context.Users;
            return users;
        }

        public User? GetByHandle(string Handle)
        {
            //var user = context.Users
            //.SingleOrDefault(b => b.Handle == Handle);
            //return user;
            return context.Users.FirstOrDefault(x => x.Handle == Handle);
        }

        //Adicionar atributos do tipo profile picture, bio, localizaçao, website, followers, following
        public User Update(string Handle, User user)
        {
            var userToUpdate = context.Users.Find(Handle);
            if (userToUpdate is null)
            {
                throw new NullReferenceException("User does not exist");
            }
            else
            {
                User us = context.Users.FirstOrDefault(x => x.Handle == Handle);
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Handle = user.Handle;

                context.SaveChanges();
                return userToUpdate;
            }
        }
    }
}
