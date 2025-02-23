﻿using Twitter.Models;

namespace Twitter.Service
{
    public interface ITweetService
    {
        public abstract IEnumerable<Tweet> GetAll();

        public abstract IEnumerable <Tweet> GetByUser(int id);

        public abstract Tweet? GetById(int Id);

        public abstract Tweet Create(Tweet newTweet);

        public abstract void DeleteById(int Id);

        public abstract Tweet Update(int Id, Tweet tweet);

        public abstract Tweet Update(TweetEditModel tweet);

        //public abstract Tweet Like(TweetViewModel model); //??? UserId - O que mudou?
        public abstract void LikeById(int Id);
        public abstract Tweet Comment(TweetViewModel model);


    }
}
