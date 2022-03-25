using Twitter.Models;

namespace Twitter.Service
{
    public interface IUserService
    {
        public abstract IEnumerable<User> GetAll();

        public abstract User? GetByHandle(string Handle);

        public abstract User Create(User newUser);

        public abstract void DeleteByHandle(string Handle);

        public abstract User Update(string Handle, User user);

        //public abstract void UpdatePublisher(string isbn, int publisherId);

    }
}
