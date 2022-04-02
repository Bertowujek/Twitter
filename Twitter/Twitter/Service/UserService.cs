using Twitter.Models;

namespace Twitter.Service
{
    public class UserService : IUserService
    {

        private readonly TwitterContext context;

        public UserService(TwitterContext context)
        {
            this.context = context;
        }

        // Função que procura o usuário por handle & passworld => Login.
        public User Get(string Handle, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.Handle == Handle && x.Password == password);
            return user;
        }

        public User? FindByName(string userName)
        {
            return context.Users.FirstOrDefault(x => x.Name == userName);
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

        public User GetById(int userId)
        {
            return context.Users.FirstOrDefault(x => x.Id == userId);
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

                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Handle = user.Handle;

                context.SaveChanges();
                return userToUpdate;
            }
        }

        public User UpdateImage(int Id, string fileName)
        {
            var userToUpdate = context.Users.FirstOrDefault(x => x.Id == Id);
            if (userToUpdate is null)
            {
                throw new NullReferenceException("User does not exist");
            }
            else
            {
                userToUpdate.Avatar = fileName;
                context.SaveChanges();
                return userToUpdate;
            }
        }

        //Likes
        public void LikeNewLikeByHandle(string Handle)
        {
            throw new NotImplementedException();
        }

        public void LikeGetLikingUsersByHandle(string Handle)
        {
            throw new NotImplementedException();
        }

        public void LikeUnlikeByHandle(string Handle)
        {
            throw new NotImplementedException();
        }
    }
}
