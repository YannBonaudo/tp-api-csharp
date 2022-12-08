using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.dbContext.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateUser([FromBody] User user)
        {
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();
            var users = await this.dbContext.Users.ToListAsync();

            return Ok(users);
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User request)
        {
            User user = this.dbContext.Users.Find((User user)=> user.Id == request.Id);
            if (user == null)
            {
                return BadRequest(request);
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Place = request.Place;

            return user;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = this.dbContext.Users.Find((User x) => x.Id == id);
            if (user == null)
            {
                return NotFound("User not Found");
            }
            return Ok(user);
        }
    }
}
