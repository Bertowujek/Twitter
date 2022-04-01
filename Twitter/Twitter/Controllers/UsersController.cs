using Microsoft.AspNetCore.Mvc;
using Twitter.Models;
using Twitter.Service;

namespace Twitter.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserService? serviceU;

        public UsersController(IUserService service_)
        {
            this.serviceU = service_;
        }


        [HttpGet]
        public IEnumerable<User> Get()
        {
            return serviceU.GetAll();
        }

        // GET api/<BooksController>/5
        [HttpGet("{Handle}", Name = "GetByHandle")]
        public IActionResult Get(string Handle)
        {
            User? user = serviceU.GetByHandle(Handle);
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
                User newUser = serviceU.Create(user);
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
            var userToUpdate = serviceU.GetByHandle(Handle);
            if (user is not null && userToUpdate is not null)
            {
                serviceU.Update(Handle, user);
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
            var user = serviceU.GetByHandle(Handle);

            if (user is not null)
            {
                serviceU.DeleteByHandle(Handle);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }







    }
}
