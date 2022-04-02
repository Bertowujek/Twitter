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

        public abstract void LikeNewLikeByHandle(string Handle); //Create a Like
        public abstract void LikeGetLikingUsersByHandle(string Handle); //View number of Likes
        //public abstract User LikeGetLinkingUsersByHandle(string Handle);
        public abstract void LikeUnlikeByHandle(string Handle); //Remove a previous Like

    }
}
