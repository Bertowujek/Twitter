using Microsoft.AspNetCore.Mvc;
using Twitter.Models;
using Twitter.Service;

namespace Twitter.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserService? service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }


        [HttpGet]
        public IEnumerable<User> Get()
        {
            return service.GetAll();
        }

        // GET api/<BooksController>/5
        [HttpGet("{Handle}", Name = "GetByHandle")]
        public IActionResult Get(string Handle)
        {
            User? user = service.GetByHandle(Handle);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user != null)
            {
                User newUser = service.Create(user);
                return CreatedAtRoute("GetByHandle", new { Id = newUser.Id }, newUser);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{Handle}")]
        public IActionResult Put(string Handle, [FromBody] User user)
        {
            var userToUpdate = service.GetByHandle(Handle);
            if (user is not null && userToUpdate is not null)
            {
                service.Update(Handle, user);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("{Handle}")]
        public IActionResult Delete(string Handle)
        {
            var book = service.GetByHandle(Handle);

            if (book is not null)
            {
                service.DeleteByHandle(Handle);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }







    }
}
