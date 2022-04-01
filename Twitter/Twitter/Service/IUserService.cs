using Twitter.Models;

namespace Twitter.Service
{
    public interface IUserService
    {
        public abstract IEnumerable<User> GetAll();

        public abstract User? GetByHandle(string Handle);

        public User? FindByName(string userName);
        public abstract User Create(User newUser);

        public abstract void DeleteByHandle(string Handle);

        public abstract User Update(string Handle, User user);
        public abstract User GetById(int userId);

        public abstract User Get(string Handle, string password);

        public abstract User UpdateImage(int Id, string fileName);

        //public abstract void UpdatePublisher(string isbn, int publisherId);

    }
}
