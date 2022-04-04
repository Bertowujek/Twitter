using Microsoft.EntityFrameworkCore;
using Twitter.Models;

namespace Twitter.Service
{
    public class TweetService : ITweetService
    {

        private readonly TwitterContext context;

        public TweetService(TwitterContext context)
        {
            this.context = context;
        }

        public Tweet Create(Tweet newTweet)
        {
            User us = context.Users.Find(newTweet.User.Id);

            if (us is null)
            {
                throw new NullReferenceException("User does not exist");
            }
            else
            {
                newTweet.User = us;
                context.Tweets.Add(newTweet);
                context.SaveChanges();
                return newTweet;
            }
        }

        public void DeleteById(int Id)
        {
            var tweetToDelete = context.Tweets.Find(Id);
            if (tweetToDelete is not null)
            {
                context.Tweets.Remove(tweetToDelete);
                context.SaveChanges();
            }
        }

        public IEnumerable<Tweet> GetAll()
        {
            var tweets = context.Tweets.Include(p => p.User);
            // var tweets = context.Tweets;
            return tweets;
        }

        public IEnumerable<Tweet> GetByUser(int id)
        {
            //var tweet = context.Tweets.Include(t => t.User).All(b => b.User == user);
            var tweets = context.Tweets.Include(t => t.User).Where(b => b.User.Id == id);
            // var tweet = context.Tweets.SingleOrDefault(b => b.User);
            return tweets;
        }

        //public Tweet? GetByUser(User user)
        //{
        //    var tweet = context.Tweets.Include(t => t.User).SingleOrDefault(b => b.User == user);
        //    // var tweet = context.Tweets.SingleOrDefault(b => b.User);
        //    return tweet;
        //}
        //FOi criado este getbyid por causa do put do TweetsController
        public Tweet? GetById(int Id)
        {
            var tweet = context.Tweets
            .Include(b => b.User)
            .SingleOrDefault(b => b.Id == Id);
            return tweet;
        }

        public Tweet Update(int Id, Tweet tweet)
        {
            //tweet.User.Id !== jwt.id
            //return error
            var tweetToUpdate = context.Tweets.Find(Id);
            if (tweetToUpdate is null)
            {
                throw new NullReferenceException("Tweet does not exist");
            }
            else
            {
                User us = context.Users.Find(tweet.User.Id);
                tweetToUpdate.Date = tweet.Date;
                tweetToUpdate.Text = tweet.Text;
                //tweetToUpdate.User = user;
                tweetToUpdate.Likes = tweet.Likes;
                tweetToUpdate.Comments = tweet.Comments;

                context.SaveChanges();
                return tweetToUpdate;
            }
        }

        public void LikeById(int Id)
        {
            var tweetLike = context.Tweets.Find(Id);
            while (tweetLike is not null && tweetLike.Likes >= 0) //while porque se o tweet for apagado, deixa de ser possível dar um like

            {
                context.Tweets.Add(tweetLike);
                tweetLike.Likes++;
                context.SaveChanges();
            }
        }

        public Tweet Comment(TweetViewModel model)
        {
            //tweet.User.Id !== jwt.id
            //return error
            var tweetToComment = context.Tweets.Find(model.Id);
            if (tweetToComment is null)
            {
                throw new NullReferenceException("Tweet does not exist");
            }
            else
            {
                tweetToComment.Comments = model.Comment;

                context.SaveChanges();
                return tweetToComment;
            }
        }

        public Tweet Update(TweetEditModel tweet)
        {
            var tweetToUpdate = context.Tweets.Find(tweet.Id);
            if (tweetToUpdate is null)
            {
                throw new NullReferenceException("Tweet does not exist");
            }
            else
            {
                tweetToUpdate.Text = tweet.Text;

                context.SaveChanges();
                return tweetToUpdate;
            }
        }

        public void UpdateUser(int Id, int userId)
        {
            var tweetToUpdate = context.Tweets.Find(Id);
            var userToUpdate = context.Users.Find(userId);

            if (tweetToUpdate is null || userToUpdate is null)
            {
                throw new NullReferenceException("Tweet or user does not exist");
            }

            tweetToUpdate.User = userToUpdate;

            context.SaveChanges();
        }
    }
}

//public Tweet Like(int Id)
//{
//    //tweet.User.Id !== jwt.id
//    //tweetToUpdate = context.Tweets.Include(t => t.User.Id).Where(t => t.User.Id == model.UserId);

//    var tweetLike = context.Tweets.Find(Id);
//    //tweetToUpdate = context.Tweets.Include(tweet.).SingleOrDefault(b => b.User == UserId);

//    while (tweetLike is not null && tweetLike.Likes >= 0 //while porque se o tweet for apagado, deixa de ser possível dar um like

//    {
//        context.Tweets.Add(tweetLike);
//        tweetLike.Likes++;
//        context.SaveChanges();
//    }
//    return tweetLike;
//}