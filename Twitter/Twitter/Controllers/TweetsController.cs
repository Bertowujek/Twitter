using Microsoft.AspNetCore.Mvc;
using Twitter.Models;
using Twitter.Service;

namespace Twitter.Controllers
{
    public class TweetsController : ControllerBase
    {
        private readonly ITweetService? serviceT;

        public TweetsController(ITweetService service_)
        {
            this.serviceT = service_;
        }

        [HttpGet]
        public IEnumerable<Tweet> Get()
        {
            return serviceT.GetAll();
        }

        //[HttpGet("{user}", Name = "GetByUser")]
        //public IActionResult Get(User user)
        //{
        //    Tweet? tweet = serviceT.GetByUser(user);
        //    if (tweet == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(tweet);
        //    }
        //}

        [HttpPost]
        public IActionResult Post([FromBody] Tweet tweet)
        {
            if (tweet != null)
            {
                Tweet newTweet = serviceT.Create(tweet);
                return CreatedAtRoute("GetById", new { Id = newTweet.Id }, newTweet);
            }
            else
            {
                return BadRequest();
            }
        }


        // PUT api/<BooksController>/5
        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody] Tweet tweet)
        {
            //var tweetToUpdate = service.GetByUser.OrDefault(tweet.Id);  
            //var tweetToUpdate = service.Update(Id, tweet);
            var tweetToUpdate = serviceT.GetById(Id);
            if (tweet is not null && tweetToUpdate is not null)
            {
                serviceT.Update(Id, tweet);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpPatch("{isbn}/UserId")]
        //public IActionResult Patch(int Id, int UserId)
        //{
        //    var bookToUpdate = service.GetById(Id);
        //    if (bookToUpdate is not null)
        //    {
        //        service.UpdateUser(Id, UserId);
        //        return Ok();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        // DELETE api/<BooksController>/5
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var tweet = serviceT.GetById(Id);

            if (tweet is not null)
            {
                serviceT.DeleteById(Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
